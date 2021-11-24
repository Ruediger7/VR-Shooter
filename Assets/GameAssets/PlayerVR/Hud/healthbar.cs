using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    // Changes the healthbar according to amount
    public void updateHealth(float amount)
    {
        hpBar.fillAmount += amount / 100f;
    }

    private void Update()
    {
        this.updateHealth(-10*Time.deltaTime);
    }

    // Select Healthbar Image
    public Image hpBar;
}
