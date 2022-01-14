using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldInduction : MonoBehaviour
{
    private GameObject[] stations;
    public GameObject playerController;

    public AudioClip hochfahren;
    public AudioClip runterfahren;
    //public AudioClip voll;
    public AudioClip laden;


    private AudioSource audioSource;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        stations = FindObjectOfType<shieldStations>().stations;
    }

    void OnTriggerEnter(Collider other) {
        if (other.bounds.Contains(playerController.transform.position))
        {
            audioSource.PlayOneShot(laden);
            playerController.GetComponent<SC_DamageReceiver>().playerHP = 100;

            // Wo wird diese Stadtion disabled damit eine neue sinn macht?

            audioSource.PlayOneShot(runterfahren);
            enableRandomStation();
        }
    }

    // Update is called once per frame
    private void Update()
    {
       
    }


    public void enableRandomStation()
    {
        // nachdem eine Station benutzt wurde wird zufällig eine neue aktiviert
        int random = Random.Range(0, (stations.Length - 1));
       ;
        if (stations[random].active) //dieses GameObjekt
        {
            int newNumber = ((stations.Length - 1) - random);
            stations[newNumber].SetActive(true);
            AudioSource.PlayClipAtPoint(hochfahren, stations[newNumber].transform.position);
           
        }
        else
        {
            stations[random].SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
