using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Smoothly follows behind the player without directly sticking to their position
    //Gets sent the room information and adjusts to it, not moving beyond walls and zooming for smaller spaces 
    private GameObject manager;
    private Vector3 offset = new Vector3(0, 0, -10);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("MainManager");
    }

    // Update is called once per frame
    void Update()
    {
        float[] bounds = manager.GetComponent<MainManager>().getRoom().bounds;
        Vector3 targetposition = target.position + offset;

        
        if (bounds[0] < bounds[1] || bounds[2] < bounds[3])
        {
            float param1 = bounds[1] - bounds[0] - 10;
            float param2 = ((bounds[3] - bounds[2]) * (10f / 18f)) - 10;
            if (param1 < param2) { offset = new Vector3(0, 0, param1); }
            else { offset = new Vector3(0, 0, param2); }
        }
        else { offset = new Vector3(0, 0, -10); }
        

        if (targetposition.y >= bounds[0]) { targetposition = new Vector3(targetposition.x, bounds[0], targetposition.z); }
        if (targetposition.y <= bounds[1]) { targetposition = new Vector3(targetposition.x, bounds[1], targetposition.z); }
        if (bounds[0] < bounds[1]) { targetposition = new Vector3(targetposition.x, ((bounds[0] + bounds[1]) /2 ), targetposition.z); }

        if (targetposition.x >= bounds[2]) { targetposition = new Vector3(bounds[2], targetposition.y, targetposition.z); }
        if (targetposition.x <= bounds[3]) { targetposition = new Vector3(bounds[3], targetposition.y, targetposition.z); }
        if (bounds[2] < bounds[3]) { targetposition = new Vector3(((bounds[2] + bounds[3]) / 2), targetposition.y, targetposition.z); }

        transform.position = Vector3.SmoothDamp(transform.position, targetposition, ref velocity, smoothTime);
    }
}
