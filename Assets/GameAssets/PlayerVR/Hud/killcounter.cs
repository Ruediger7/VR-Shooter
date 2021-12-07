using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class killcounter : MonoBehaviour
{
    int killamount = 0;
    private GameObject text;

    private void Start()
    {
        text = GameObject.Find("Text");
    }
    public void updateKill()
    {
        killamount++;
        
        text.GetComponent<Text>().text = "Kills: " +  killamount;
    }

}
