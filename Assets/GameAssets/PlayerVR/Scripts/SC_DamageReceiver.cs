using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_DamageReceiver : MonoBehaviour, IEntity
{
    //This script will keep track of player HP
    public float playerHP = 100;
    public float timer = 0.0f;
    public SC_DamageReceiver player;
    private Canvas_Menu menu;

    private void Update()
    {
        //automatic HP regeneration
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else if(playerHP < 100)
        {
            playerHP += 10;
            timer = 0.5f;
        }

        if(playerHP <= 0)
        {
            Dead();
        }
    }
    
    public void ApplyDamage(float points)
    {
        playerHP -= points;
        Debug.Log(playerHP);
        timer = 3;

        //Player is dead
        if (playerHP <= 0)
        {
            //playerController.canMove = false;
            playerHP = 0;
        }
    }

    public void Dead()
    {
        menu.GameOver(Canvas_Menu.kills);
    }     
}
