using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float speed = 10f;
    private bool sneak = false;
    private float jumpStr = 10;
    public int horz = 0;
    public Rigidbody rb;
    public LayerMask playerLayerMask;
    private GameObject manager;
    public bool grounded = false;

    //Set Keys
    public KeyCode[] keys = {KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.Space, KeyCode.LeftShift};
    //WASD, Jump, Sneak
    //0123, 4, 5
    void Start()
    {
        manager = GameObject.Find("MainManager");
        keys = manager.GetComponent<MainManager>().GetKeys();
    }

    // Update is called once per frame
    void Update()
    {
        //seperated Sneak and changing speed in case some scenes force the player to sneak
        if (Input.GetKeyDown(keys[5])) { sneak = !sneak; }
        if (sneak) { 
            speed = 2.5f; 
            transform.localScale = new Vector3(1, .5f, 1);
            GetComponent<CapsuleCollider>().height = 1; 
        } 
        else 
        { 
            speed = 10f;
            transform.localScale = new Vector3(1, 1, 1);
            GetComponent<CapsuleCollider>().height = 2;
        }

        //Horizontal using whatever two keybinds are for left and right
        if (Input.GetKey(keys[1]) && Input.GetKey(keys[3])) { horz = 0; }
        else if (Input.GetKey(keys[1])) { horz = -1; }
        else if (Input.GetKey(keys[3])) { horz = 1; }
        else { horz = 0; }

        //rotate and move for left/right
        if (horz != 0) { transform.rotation = Quaternion.Euler(transform.rotation.x, 90 * horz / Mathf.Abs(horz), transform.rotation.z); }
        if (transform.rotation.eulerAngles.y == 90 || transform.rotation.eulerAngles.y == 270)
        {
            rb.velocity = new Vector3(horz * speed, rb.velocity.y, rb.velocity.z);
            Debug.Log(horz);
        }

        //Grounding
        Ray myRay = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(myRay, out hit, GetComponent<CapsuleCollider>().height/2 + .1f, playerLayerMask))
        {
            if (grounded) { rb.velocity = new Vector3(rb.velocity.x, -5, rb.velocity.z); }
            //-5 is an arbitrary number that keeps them grounded while moving down slopes without risking clipping
            if (rb.velocity.y <= 0) { grounded = true; }

            //Pitfall
            if (hit.transform.CompareTag("Pitfall")) { Destroy(hit.transform.gameObject); grounded = false; }
        }

        //If player can jump determined by if player is grounded and can be held for lengthened jumps
        if (Input.GetKeyDown(keys[4]) && grounded) { rb.velocity = new Vector3(rb.velocity.x, jumpStr, rb.velocity.z); grounded = false; }
        if (Input.GetKeyUp(keys[4]) && rb.velocity.y > 0f ) { rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * .5f , rb.velocity.z); }
    }
}
