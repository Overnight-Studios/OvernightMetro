using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int toRoom;
    public bool dir;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            if (other.gameObject.GetComponent<PlayerMove>().enabled == true)
            {
                other.gameObject.GetComponent<PlayerDoor>().Door(toRoom, transform.position.x, dir);
            }
        }
    }
}
