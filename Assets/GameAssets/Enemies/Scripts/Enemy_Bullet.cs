using System.Collections;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    public float bulletSpeed = 345;
    public float hitForce = 50f;
    public float destroyAfter = 3.5f;

    private float currentTime = 0;
    private bool hasHit = false;
    private float dmgPoints;

    Vector3 newPos;
    Vector3 oldPos;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        newPos = transform.position;
        oldPos = newPos;

        while (currentTime < destroyAfter && !hasHit)
        {
            Vector3 velocity = transform.forward * bulletSpeed;
            newPos += velocity * Time.deltaTime;
            Vector3 direction = newPos - oldPos;
            float distance = direction.magnitude;
            
            RaycastHit hit;
            if (Physics.Raycast(oldPos, direction, out hit, distance))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    IEntity Player = hit.transform.GetComponent<IEntity>();
                    if (Player != null)
                    {
                        Player.ApplyDamage(dmgPoints);
                    }
                }
                newPos = hit.point;
                StartCoroutine(DestroyBullet());
            }
            currentTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();

            transform.position = newPos;
            oldPos = newPos;
        }
        if (!hasHit)
        {
            StartCoroutine(DestroyBullet());
        }
    }

    IEnumerator DestroyBullet()
    {
        hasHit = true;
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    public void SetDamage(float weaponDmg)
    {
        dmgPoints = weaponDmg;
    }
}
