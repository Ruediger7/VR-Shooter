using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldStations : MonoBehaviour //Auf den Player ziehen, wird zum Speichern des Arrays benötigt
{
    public GameObject[] stations;
    // Start is called before the first frame update
    void Start()
    {
        stations = GameObject.FindGameObjectsWithTag("shieldRegenStation");
        for (int i = 0; i < stations.Length; i++)
        {
            stations[i].SetActive(false);
        }
    }
}
