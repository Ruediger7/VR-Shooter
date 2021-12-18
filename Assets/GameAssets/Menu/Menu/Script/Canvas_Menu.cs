using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Menu : MonoBehaviour
{
    public Text text;
    public Image img;
    public Button btn;
    public GameObject canvas;

    private Color original;
    public static int kills;

    private Camera camera;

    private void Start()
    {
        camera = GetComponent<Camera>();
        kills = 0;

        btn.onClick.AddListener(ShowHighscore);

        Debug.Log("Start");

        btn.gameObject.SetActive(false);

        StartCoroutine(DelayedText());
    }

    // Update is called once per frame
    private void Update()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.collider != null)
            {
                if(hit.distance <= 3.0f)
                {
                    Vector3 oldposition = camera.transform.position;
                    oldposition.z += 3.0f;
                    Vector3 newposition;
                    newposition.x = oldposition.x;
                    newposition.y = oldposition.y;
                    newposition.z = oldposition.z - (3.0f - hit.distance);

                    this.gameObject.transform.position = newposition;
                }
                else
                {
                    Vector3 position = camera.transform.position;
                    position.z += 3.0f;
                    this.gameObject.transform.position = position;
                }
            }
        }
    }

    IEnumerator DelayedText()
    {
        Debug.Log("Wait");
        yield return new WaitForSeconds(4);

        Debug.Log("Over");

        ChangeText("Ziehe deine Waffe wenn du bereit bist los zu starten.");

        yield return new WaitForSeconds(5);

        // Hier fehlt ein Event bei dem der Spieler seine Waffe zieht
        DeactivateCanvas();
    }

    public void DeactivateCanvas()
    {
        Debug.Log("Deactivate");
        canvas.SetActive(false);
    }

    public void ActivateCanvas()
    {
        Debug.Log("Activate");
        canvas.SetActive(true);
    }

    public void ChangeText(string t)
    {
        Debug.Log("Change Text");
        text.text = t;
    }

    public void GameOver(int score)
    {
        kills = score;
        ActivateCanvas();
        Debug.Log("Game Over");
        original = text.color;
        text.color = Color.red;
        ChangeText("Game Over \n Score: " + kills);
        ActivateHighscoreButton();

    }

    public void ActivateHighscoreButton()
    {
        btn.gameObject.SetActive(true);
    }

    public void ShowHighscore()
    {
        text.color = original;

        ChangeText("Highscore \n " + kills);

        // Mit einem Speicher von vorangegangenen Aktionen kann man hier noch viel machen.
    }

}
