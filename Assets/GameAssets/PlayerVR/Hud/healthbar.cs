using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
<<<<<<< HEAD
    public GameObject Player;

=======
>>>>>>> 598115dc1dad497cb6e2c65fa65ce935975c3c90
    // Changes the healthbar according to amount
    public void updateHealth(float amount)
    {
        hpBar.fillAmount += amount / 100f;
    }

    private void Update()
    {
<<<<<<< HEAD
        this.updateHealth(Player.GetComponent<SC_DamageReceiver>().playerHP);
=======
        this.updateHealth(-10*Time.deltaTime);
>>>>>>> 598115dc1dad497cb6e2c65fa65ce935975c3c90
    }

    // Select Healthbar Image
    public Image hpBar;
}
