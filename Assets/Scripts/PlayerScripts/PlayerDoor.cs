using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoor : MonoBehaviour
{
    private GameObject manager;
    public LayerMask playerLayerMask;
    public Rigidbody rb;
    private bool moving = false;
    //Goal is how far on the x axis player should move before they can control themselves again
    private float goal = 0;
    //Direction is either -1 or 1
    private int direction = 0;
    private float timer = 0;
    private int room = 0;

    void Start()
    {
        //Allows PlayerDoor to set currentRoom from MainManager
        manager = GameObject.Find("MainManager");
        this.gameObject.transform.GetChild(1).GetComponent<Animator>().enabled = false;
    }

    void Update()
    {
        //Sends a ray beneath player and sets y pos if it hits floor, maintains this height even if PlayerMove is disabled
        Ray myRay = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(myRay, out hit, GetComponent<CapsuleCollider>().height / 2 + .1f, playerLayerMask))
        {
            if (this.gameObject.GetComponent<PlayerMove>().grounded)
            transform.position = hit.point + new Vector3(0, GetComponent<CapsuleCollider>().height / 2 + .1f, 0);
        }

        //Happens when player is moving through door
        if (moving)
        {
            timer += Time.deltaTime;
            if (timer > 1f)
            {
                manager.GetComponent<MainManager>().setRoom(room);
            }

            //Moves through door with velocity of 5
            float temp = 3 * direction;
            rb.velocity = new Vector3(temp, -5f, 0);

            //If player is past goal x, disables moving
            if (direction == 1) { if (transform.position.x >= goal) { moving = false; } }
            else { if (transform.position.x <= goal) { moving = false; } }
        }
        //Player can move whenever not moving through door
        else 
        {
            this.gameObject.transform.GetChild(1).GetComponent<Animator>().enabled = false;
            timer = 0;
            GetComponent<PlayerMove>().enabled = true;
        }
    }

    //Upon entering a Door hitbox, sent roomNumber door goes to, Door position, and if door is going left or right
    public void Door(int num, float pos, bool dir)
    {
        //Disables player controls during door "cutscene"
        GetComponent<PlayerMove>().enabled = false;
        room = num;
        if (dir) { direction = 1; }
        else { direction = -1; }

        //Establishes goal pos as three in front of player
        goal = pos + (5 * direction);
        moving = true;
        this.gameObject.transform.GetChild(1).GetComponent<Animator>().enabled = true;
        this.gameObject.transform.GetChild(1).GetComponent<Animator>().Play("Fade");
    }
}
