using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InputScript : MonoBehaviour
{
    //Code is on all buttons in the keybind/input menu, when button is pressed, detects next input and sets that Key to that input. 


    private static readonly KeyCode[] keyCodes = Enum.GetValues(typeof(KeyCode))
                                                 .Cast<KeyCode>()
                                                 .Where(k => ((int)k < (int)KeyCode.Mouse0))
                                                 .ToArray();
    private GameObject manager;
    public int pos;
    private KeyCode key;
    private bool awaiting = false;
    // Start is called before the first frame update
    void OnEnable()
    {
        this.GetComponent<Button>().onClick.AddListener(Pressed);
        manager = GameObject.Find("MainManager");
        key = manager.GetComponent<MainManager>().GetKeys()[pos];
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = key.ToString();
        if (Input.anyKeyDown && awaiting)
        {
            GetCurrentKeyDown();
            manager.GetComponent<MainManager>().TempKey(key, pos);
            awaiting = false;
        }
    }
    void Pressed()
    {
        awaiting = true;
    }
    private void GetCurrentKeyDown()
    {
        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKey(keyCodes[i]))
            {
                key = keyCodes[i];
            }
        }
    }
}
