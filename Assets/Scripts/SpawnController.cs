using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public static SpawnController instance;

    // public PaperWeight paperWeight;

    public Transform[] spawnPoints;
    public GameObject paperWeightPrefab;

    public float minSpawnDelay = 0.5f;
    public float maxSpawnDelay = 3f;
    public float timeElapsed;
    public float spawnDelay;

    private bool gamePaused;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gamePaused = false;
        StartCoroutine("WeightSpawnTimer");
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (spawnDelay >= 0.5f && gamePaused == false)
        {
            float decreaseDelayOverTime = maxSpawnDelay - ((maxSpawnDelay - minSpawnDelay) / 30f * timeElapsed);
            spawnDelay = Mathf.Clamp(decreaseDelayOverTime, minSpawnDelay, maxSpawnDelay);
        }
    }

    void SpawnWeight()
    {
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(paperWeightPrefab, randomSpawnPoint.position, Quaternion.identity);
    }

    IEnumerator WeightSpawnTimer()
    {
        yield return new WaitForSeconds(spawnDelay);

        SpawnWeight();
        
        StartCoroutine("WeightSpawnTimer");
    }

    public void pauseGame()
    {
        gamePaused = true;
        StopCoroutine("WeightSpawnTimer");
        Time.timeScale = 0;
    }

    public void resumeGame()
    {
        gamePaused = false;
        StartCoroutine("WeightSpawnTimer");
        Time.timeScale = 1;
    }
}
