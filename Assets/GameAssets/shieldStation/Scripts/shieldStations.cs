using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldStations : MonoBehaviour //Auf den Player ziehen, wird zum Speichern des Arrays benötigt
{
    public GameObject[] stations;
    void Start()
    {
        
        for (int i = 0; i < stations.Length; i++)
        {
            stations[i].SetActive(false);
        }

        int random = Random.Range(0, (stations.Length - 1));
        stations[random].SetActive(true);
    }


}
