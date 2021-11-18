using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Gun : MonoBehaviour
{
    //public float bulletSpeed = 40;
    public float weaponDmg = 15;
    public float fireRate = 0.1f;
    float nextFireTime = 0;
    public GameObject bulletPrefab;
    public Transform FirePoint;
    //public AudioSource audioSource;
    //public AudioClip audioClip;

    private void Update()
    {
        if (SteamVR_Actions.default_shoot.GetLastStateDown(SteamVR_Input_Sources.Any))
        {
            Fire();
        }
    }

    public void Fire()
    {
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            RaycastHit hit;
            if (Physics.Raycast(FirePoint.transform.position, FirePoint.transform.forward, out hit, 100))
            {

                Debug.Log("Treffer");
            }
            GameObject spawnedBullet = Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
            SC_Bullet bullet = spawnedBullet.GetComponent<SC_Bullet>();
            bullet.SetDamage(weaponDmg);
            //spawnedBullet.GetComponent<Rigidbody>().velocity = bulletSpeed * FirePoint.forward;
            //audioSource.PlayOneShot(audioClip);
            //Destroy(spawnedBullet, 2);
        }
    }
}