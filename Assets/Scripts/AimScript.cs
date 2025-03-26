using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AimScript : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask playerLayerMask;
    public float tick = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.Find("AimPos");

        this.transform.position = player.transform.position;

        Ray myRay = new Ray(transform.position, -(GameObject.Find("Main Camera").GetComponent<Camera>().WorldToScreenPoint(transform.position) - Input.mousePosition));
        RaycastHit hit;
        if (Physics.Raycast(myRay, out hit, 50, playerLayerMask))
        {
            transform.GetChild(0).transform.localScale = new Vector3(.05f, .025f, hit.distance);
            transform.GetChild(0).transform.localPosition = new Vector3(0, 0, (hit.distance/-2f));
        }

        if (Input.GetMouseButton(1)) {
            tick += Time.deltaTime;
            if (tick > 2) { tick = 1; } 
            if (tick > .6f) { transform.GetChild(0).gameObject.SetActive(true); } 
        }
        if (Input.GetMouseButtonUp(1))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            tick = 0;
        }
    }
}
