using System.Collections;
using UnityEngine;

public class SC_Bullet : MonoBehaviour
{
    public float bulletSpeed = 345;
    public float hitForce = 50f;
    public float destroyAfter = 3.5f;

    private LineRenderer bulletTrail;
    float currentTime = 0;
    Vector3 newPos;
    Vector3 oldPos;
    bool hasHit = false;

    float dmgPoints;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        newPos = transform.position;
        oldPos = newPos;
        //bulletTrail = GetComponent<LineRenderer>();
	//bulletTrail.SetPosition(0,newPos);

        while(currentTime<destroyAfter && !hasHit)
        {
            Vector3 velocity = transform.forward * bulletSpeed;
            newPos += velocity * Time.deltaTime;
            Vector3 direction = newPos - oldPos;
            float distance = direction.magnitude;
      
            RaycastHit hit;
            if(Physics.Raycast(oldPos,direction,out hit, distance))
            {
                //noch testen ob if notwendig ist oder auch durch Tag ersetzen
                if (hit.rigidbody != null)
                {
                    IEntity npc = hit.transform.GetComponent<IEntity>();
                    if (npc != null)
                    {
                        npc.ApplyDamage(dmgPoints);
                    }
                }
                newPos = hit.point;
                StartCoroutine(DestroyBullet());
            }
            currentTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();
            transform.position = newPos;
	    //bulletTrail.SetPosition(1,newPos);
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
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    public void SetDamage(float weaponDmg)
    {
        dmgPoints = weaponDmg;
    }
}
