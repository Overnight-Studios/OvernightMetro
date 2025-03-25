using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    //MainMananager stores room positions and keybinds
    public static MainManager Instance;

    //The list of rooms and subrooms are set in the public array in Unity
    public Room[] rooms;
    public Room[] altRooms;
    public int currentRoom = 0;

    //Establish default keybinds
    public KeyCode[] keys = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.Space, KeyCode.LeftShift};
    public KeyCode[] tempKeys = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.Space, KeyCode.LeftShift,  };

    //Does not destroy between scenes but will destroy itself if there's another copy of itself in the space
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

    //Keybinding
    //Sets the Key at pos in tempKeys
    public void TempKey(KeyCode newKey, int pos)
    {
        MainManager.Instance.tempKeys[pos] = newKey;
    }
    //Sets tempKeys keybinding to the current keybinding
    public void SetKeys()
    {
        MainManager.Instance.keys = MainManager.Instance.tempKeys;
    }
    //Sets default keybinding
    public void SetDefault()
    {
        KeyCode[] temp = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.Space, KeyCode.LeftShift };
        MainManager.Instance.keys = temp;
    }
    //Gets current keybinding, used by PlayerMove on start
    public KeyCode[] GetKeys()
    {
        return MainManager.Instance.keys;
    }

    //Rooms
    //Gets current room, used by CameraFollow to know room bounds
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
    //Changes currentRoom to num
    public void setRoom(int num)
    {
        currentRoom = num;
    }
}
