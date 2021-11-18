using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaffezuHuefte : MonoBehaviour
{
    Vector3 startpos;
    Quaternion startrot;
    //public GameObject Forward;
    //public GameObject Drehachse;
    public GameObject player;
    public GameObject holster;
    private void Start()
    {
        startpos = this.transform.localPosition;
        startrot = this.transform.localRotation;
    }

    public void teleportzuHuefte()
    {
        //Drehachse.transform.rotation = Forward.transform.position;
        
        this.transform.SetParent(holster.transform, false);
        //Drehachse.transform.SetParent(player.transform);
        transform.localPosition = startpos;
        transform.localRotation = startrot;
    }

    private void Update()
    {
        //Drehachse.transform.rotation = Forward.transform.rotation;
        //Drehachse.transform.position = player.transform.position;
    }
}
