using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public Room[] rooms;
    public Room[] altRooms;
    public int currentRoom = 0;
    public static MainManager Instance;
    public KeyCode[] keys = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.Space, KeyCode.LeftShift };
    public KeyCode[] tempKeys = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.Space, KeyCode.LeftShift };
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void TempKey(KeyCode newKey, int pos)
    {
        MainManager.Instance.tempKeys[pos] = newKey;
    }
    public void SetKeys()
    {
        MainManager.Instance.keys = MainManager.Instance.tempKeys;
    }
    public void SetDefault()
    {
        KeyCode[] temp = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.Space, KeyCode.LeftShift };
        MainManager.Instance.keys = temp;
    }
    public KeyCode[] GetKeys()
    {
        return MainManager.Instance.keys;
    }
    public Room getRoom()
    {
        if (currentRoom < 100)
        {
            return rooms[currentRoom];
        }
        else
        {
            return altRooms[currentRoom - 100];
        }
    }
    public void setRoom(int num)
    {
        currentRoom = num;
    }
}
