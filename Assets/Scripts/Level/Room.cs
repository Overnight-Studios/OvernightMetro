using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Room : ScriptableObject
{
    public int roomNum;
    //Room Number identifier
    public GameObject[] enemies;
    //List of enemies to be spawned
    public Vector3[] pos;
    //Spawn positions
    public bool[] defeat;
    //Should or shouldn't be spawned
    public float[] bounds;
    //Up, Down, Right, Left camera bounds
    //Bounds are Ceiling y - 5.5, Floor y + 5.5, RightWall x - 9.5, LeftWall x + 9.5
}
