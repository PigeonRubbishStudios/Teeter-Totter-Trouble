using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public string sceneToLoad;

    public GameObject[] gameObjects;

    private PlayerController playerController;
    private SpawnController spawnController;
    private Rigidbody2D platform;
    private GameObject pauseButton;
    private GameObject pauseOverlay;

    public Text scoreText;
    public int score;

    private void Start()
    {
        playerController = gameObjects[0].GetComponent<PlayerController>();
        platform = gameObjects[1].GetComponent<Rigidbody2D>();
        pauseButton = gameObjects[2];
        pauseOverlay = gameObjects[3];
        spawnController = gameObjects[4].GetComponent<SpawnController>();
        pauseOverlay.SetActive(false);
        StartCoroutine("scoreUpdate");
    }

    private void Update()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void loadSelectedScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void pauseScene()
    {
        playerController.pauseGame();
        spawnController.pauseGame();
        platform.constraints = RigidbodyConstraints2D.FreezeAll;
        pauseButton.SetActive(false);
        pauseOverlay.SetActive(true);
        PaperWeight.instance.pauseGame();
        Time.timeScale = 0;
        StopCoroutine("scoreUpdate");
    }

    public void resumeScene()
    {
        spawnController.resumeGame();
        playerController.resumeGame();
        platform.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezePosition;
        pauseButton.SetActive(true);
        pauseOverlay.SetActive(false);
        PaperWeight.instance.resumeGame();
        Time.timeScale = 1;
        StartCoroutine("scoreUpdate");
    }

    IEnumerator scoreUpdate()
    {
        yield return new WaitForSeconds(1.5f);

        score = score + 5;

        StartCoroutine("scoreUpdate");
    }
}
