using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expand : MonoBehaviour
{
    //Expand is used for expanding a room into a sub-room seamlessly, can be used for smooth chase scenes and secret rooms

    public bool dir;
    //true = Up/Down, false = Left/Right

    public int[] rooms;
    //Above room and Below room
    //Or Right room and Left room

    private GameObject manager;

    void Start()
    {
        //Allows Expand to get and set currentRoom in MainManager
        manager = GameObject.Find("MainManager");
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (dir)
            {
                //if Player moved upwards through this
                if (other.transform.position.y > transform.position.y)
                {
                    manager.GetComponent<MainManager>().setRoom(rooms[0]);
                }
                //if Player moved downwards through this
                else
                {
                    manager.GetComponent<MainManager>().setRoom(rooms[1]);
                }
            }
            else
            {
                //if Player moved from left to right through this
                if (other.transform.position.x > transform.position.x)
                {
                    manager.GetComponent<MainManager>().setRoom(rooms[0]);
                }
                //if Player moved right to left through this
                else
                {
                    manager.GetComponent<MainManager>().setRoom(rooms[1]);
                }
            }

        }
    }
}
