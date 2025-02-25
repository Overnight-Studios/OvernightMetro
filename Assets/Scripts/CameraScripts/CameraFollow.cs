using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Smoothly follows behind the player without directly sticking to their position
    //Gets sent the room information and adjusts to it, not moving beyond walls and zooming in for smaller spaces 

    private GameObject manager;
    //Base z distance from player
    private Vector3 offset = new Vector3(0, 0, -10);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private Transform target;

    void Start()
    {
        //Allows CameraFollow to get data from MainManager such as current room
        manager = GameObject.Find("MainManager");
    }

    // Update is called once per frame
    void Update()
    {
        //Room variable stores Top, Bottom, Right, and Left camera bounds respectively Room bounds are Ceiling y - 5.5, Floor y + 5.5, RightWall x - 9.5, LeftWall x + 9.5  (the .5 is for half the block thickness of 1)
        float[] bounds = manager.GetComponent<MainManager>().getRoom().bounds;

        Vector3 targetposition = target.position + offset;

        //If top is less than bottom, or right is less that left, the room is smaller than 10 high or 18 wide
        //Finds whichever axis is smaller relative to its 10 or 18 and zooms based on the smaller of the two
        if (bounds[0] < bounds[1] || bounds[2] < bounds[3])
        {
            float param1 = bounds[1] - bounds[0] - 10;
            float param2 = ((bounds[3] - bounds[2]) * (10f / 18f)) - 10;
            if (param1 < param2) { offset = new Vector3(0, 0, param1); }
            else { offset = new Vector3(0, 0, param2); }
        }
        else { offset = new Vector3(0, 0, -10); }
        
        //Moves camera position to target position based on bounds and offset
        //Avoids Ceiling and Floor bounds
        if (targetposition.y >= bounds[0]) { targetposition = new Vector3(targetposition.x, bounds[0], targetposition.z); }
        if (targetposition.y <= bounds[1]) { targetposition = new Vector3(targetposition.x, bounds[1], targetposition.z); }
        //Average position between bound 0 and 1 if bounds overlap and camera zooms
        if (bounds[0] < bounds[1]) { targetposition = new Vector3(targetposition.x, ((bounds[0] + bounds[1]) /2 ), targetposition.z); }

        //Avoids Right and Left wall bounds
        if (targetposition.x >= bounds[2]) { targetposition = new Vector3(bounds[2], targetposition.y, targetposition.z); }
        if (targetposition.x <= bounds[3]) { targetposition = new Vector3(bounds[3], targetposition.y, targetposition.z); }
        //Average position between bound 2 and 3 if bounds overlap and camera zooms
        if (bounds[2] < bounds[3]) { targetposition = new Vector3(((bounds[2] + bounds[3]) / 2), targetposition.y, targetposition.z); }

        //Moves smoothly toward target position
        transform.position = Vector3.SmoothDamp(transform.position, targetposition, ref velocity, smoothTime);
    }
}
