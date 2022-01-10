using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameStart : MonoBehaviour
{
    // Spielstart bei Griff zur Waffe
    public bool gameStarted = false;
    private bool gameOver;
    public CVirtPlayerController cVirtPlayerController;
    private GameObject restartText;
    public int restartCounter = 0;
    
    void Start()
    {
        restartText = GameObject.Find("restartText"); 
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted && SteamVR_Actions.default_shoot.GetLastStateDown(SteamVR_Input_Sources.Any) && !gameOver)
        {
            startGame();
        }

        if (gameStarted && !gameOver)
        {
            if(GetComponent<SC_DamageReceiver>().playerHP <= 0)
            {
                endGame();
                restartText.GetComponent<Text>().enabled = true;
            }
        }
        if(gameOver)
        {
            if (SteamVR_Actions.default_shoot.GetLastStateDown(SteamVR_Input_Sources.Any))
            {
                restartCounter++;
            }
            
                
                if (restartCounter >= 5)
                {
                    gameOver = false;
                    gameStarted = true;
                    restartCounter = 0;
                    //SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single); Funktioniert nicht da der PlayerController den Tag DontDestroyonLoad hat
                    cVirtPlayerController.GetComponent<SC_DamageReceiver>().playerHP = 100;
                    cVirtPlayerController.transform.position = new Vector3(-15, 0, 19);
                    FindObjectOfType<killcounter>().setKills(0);
                    restartText.GetComponent<Text>().enabled = false;
                FindObjectOfType<Canvas_Menu>().DeactivateCanvas();
            }
            
        }
    }
    public void startGame()
    {
        gameStarted = true;
    }
    public void endGame()
    {
        gameStarted = false;
        foreach(Enemy_AI enemy in FindObjectsOfType<Enemy_AI>())
        {
            Destroy(enemy.gameObject);
        }
        gameOver = true;
        restartCounter = 0;
        //TODO: Kill all Enemys, deactivate Spawner, Neustart(Loadscene)
    }
}
