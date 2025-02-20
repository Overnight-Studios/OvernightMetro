using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoor : MonoBehaviour
{
    private GameObject manager;
    public LayerMask playerLayerMask;
    public Rigidbody rb;
    private bool moving = false;
    private float goal = 0;
    private int direction = 0;
    //private GameObject cameraBlack;
    //private GameObject mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("MainManager");
        //cameraBlack = GameObject.Find("CameraBlack");
        //mainCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        Ray myRay = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(myRay, out hit, GetComponent<CapsuleCollider>().height / 2 + .1f, playerLayerMask))
        {
            if (this.gameObject.GetComponent<PlayerMove>().grounded)
            transform.position = hit.point + new Vector3(0, GetComponent<CapsuleCollider>().height / 2 + .1f, 0);
        }
        if (moving)
        {
            //mainCamera.GetComponent<Camera>().enabled = false;
            //cameraBlack.GetComponent<Camera>().enabled = true;
            float temp = 5 * direction;
            rb.velocity = new Vector3(temp, -5f, 0);
            if (direction == 1) { if (transform.position.x >= goal) { moving = false; } }
            else { if (transform.position.x <= goal) { moving = false; } }
        }
        else 
        { 
            GetComponent<PlayerMove>().enabled = true;
            //mainCamera.GetComponent<Camera>().enabled = true;
            //cameraBlack.GetComponent<Camera>().enabled = false;
        }
    }

    public void Door(int num, float pos, bool dir)
    {
        GetComponent<PlayerMove>().enabled = false;
        manager.GetComponent<MainManager>().setRoom(num);
        if (dir) { direction = 1; }
        else { direction = -1; }
        goal = pos + (3 * direction);
        moving = true;
    }
}
