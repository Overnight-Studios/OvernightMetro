using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //Door is used for hard transitions between rooms

    public int toRoom;
    public bool dir;

    void OnTriggerEnter(Collider other)
    {
        //When player enters door sends custom information to PlayerDoor
        if (other.gameObject.tag == "Player") 
        {
            if (other.gameObject.GetComponent<PlayerMove>().enabled == true)
            {
                other.gameObject.GetComponent<PlayerDoor>().Door(toRoom, transform.position.x, dir);
            }
        }
    }
}
