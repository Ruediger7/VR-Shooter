using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float radius = 3f;
    public float waveTime = 60f;
    public int anzEnemys = 5;
    private float timer = 0f;
    public GameStart gameStart; 
    void Start()
    {
        gameStart = FindObjectOfType<GameStart>();
    }

    
    void Update()
    {
        if (!gameStart.gameStarted) return;

        timer += Time.deltaTime;
        if(timer >= waveTime)
        {
            timer = 0;
            for(int i = 0; i < anzEnemys; i++)//Generate Enemys
            {
                float randomX = Random.RandomRange(0, radius);
                float randomZ = Random.RandomRange(0, radius);
                int negative = Random.Range(-1, 1) == 0 ? 1 : -1; // to transform also negative position
                int negate = Random.Range(-1, 1) == 0 ? 1 : -1;

                GameObject gameObject = new GameObject();
                Transform transformEnemy = gameObject.transform; //Copy by Reference deswegen neues Transform
                transformEnemy.localRotation = 
                    new Quaternion(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);
   
                    transformEnemy.position +=  (transform.position + new Vector3(randomX*negative*negate, 0, randomZ*negative+negate));

                generateEnemy(transformEnemy);
            }
        }
    }
    public void generateEnemy(Transform positon)
    {
        Instantiate(enemyPrefab, positon);
    }
}
