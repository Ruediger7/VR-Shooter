using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwapper : MonoBehaviour
{
    public Material TV1;
    public Material TV2;
    public Material TV3;
    public Material TV4;
    public Material TV5;

    public Material[] MaterialArray = new Material[5];
    

    public GameObject TVScreen;
    void Start()
    {

        TVScreen.GetComponent<MeshRenderer>().material = TV2;
    }

    
    void Update()
    {
        TVScreen.GetComponent<MeshRenderer>().material = TV1;
        System.Threading.Thread.Sleep(1000);
        TVScreen.GetComponent<MeshRenderer>().material = TV2;
        System.Threading.Thread.Sleep(500);
        TVScreen.GetComponent<MeshRenderer>().material = TV3;
        System.Threading.Thread.Sleep(50);
        TVScreen.GetComponent<MeshRenderer>().material = TV4;
        System.Threading.Thread.Sleep(2000);
        TVScreen.GetComponent<MeshRenderer>().material = TV5;
        System.Threading.Thread.Sleep(1000);


    }
}
