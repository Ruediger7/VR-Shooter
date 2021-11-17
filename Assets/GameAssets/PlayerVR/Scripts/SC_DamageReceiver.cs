using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_DamageReceiver : MonoBehaviour, IEntity
{
    //This script will keep track of player HP
    public float playerHP = 100;
    public float timer = 0.0f;
    public SC_DamageReceiver player;

    private void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else if(playerHP<100)
        {
            playerHP += 10;
            timer=0.5f;
        }
    }
    
    public void ApplyDamage(float points)
    {
        playerHP -= points;
        timer = 3;
        if (playerHP <= 0)
        {
            //Player is dead
            //playerController.canMove = false;
            playerHP = 0;
        }
    }

    
}