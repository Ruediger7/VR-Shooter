using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldInduction : MonoBehaviour
{
    private GameObject[] stations;
    public float healRadius = 2;
    public GameObject playerController;

    // Start is called before the first frame update
    private void Start()
    {
        stations = FindObjectOfType<shieldStations>().stations;
    }


    // Update is called once per frame
    private void Update()
    {
        if(Vector3.Distance(playerController.transform.position, transform.position) <= healRadius)
        {
            playerController.GetComponent<SC_DamageReceiver>().playerHP = 100;
            enableRandomStation();
        }
    }


    public void enableRandomStation()
    {
        // nachdem eine Station benutzt wurde wird zufällig eine neue aktiviert
        int random = Random.Range(0, (stations.Length - 1));
       ;
        if (stations[random].active) //dieses GameObjekt
        {
            int newNumber = ((stations.Length - 1) - random);
            Debug.Log(random + " : " + (newNumber));
            stations[newNumber].SetActive(true);
           
        }
        else
        {
            stations[random].SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
