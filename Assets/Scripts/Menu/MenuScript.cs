using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    //Main menuing buttons 

    public GameObject[] objs;

    //Establishes buttons
    void Start()
    {
        objs[0].GetComponent<Button>().onClick.AddListener(Begin);
        objs[1].GetComponent<Button>().onClick.AddListener(Settings);
        objs[3].GetComponent<Button>().onClick.AddListener(Sound);
        objs[4].GetComponent<Button>().onClick.AddListener(Inputs);
        objs[6].GetComponent<Button>().onClick.AddListener(Return1);
        objs[8].GetComponent<Button>().onClick.AddListener(Return2);
        objs[9].GetComponent<Button>().onClick.AddListener(SetInputs);
        objs[10].GetComponent<Button>().onClick.AddListener(SetDef);
    }

    void Begin()
    {
        SceneManager.LoadScene(1);
    }
    void Settings()
    {
        objs[7].SetActive(false);
        objs[2].SetActive(true);
    }
    void Return1()
    {
        objs[7].SetActive(true);
        objs[2].SetActive(false);
    }
    void Sound()
    {

    }
    void Inputs()
    {
        objs[2].SetActive(false);
        objs[5].SetActive(true);
    }
    void Return2()
    {
        objs[2].SetActive(true);
        objs[5].SetActive(false);
    }
    void SetInputs()
    {
        GameObject manager = GameObject.Find("MainManager");
        manager.GetComponent<MainManager>().SetKeys();
    }
    void SetDef()
    {
        GameObject manager = GameObject.Find("MainManager");
        manager.GetComponent<MainManager>().SetDefault();
        objs[2].SetActive(true);
        objs[5].SetActive(false);
    }
}
