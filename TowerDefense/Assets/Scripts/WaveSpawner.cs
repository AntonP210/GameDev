using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform spawnPoint;


    public float timeBetweenWaves = 10f; //test value
    private float countDown = 15f;
    public TextMeshProUGUI waveCountDownText;

    private int waveIndex = 1;
    public int maxWaveIndex;
    public GameManager gameManager;

    private void Update()
    {
        if (waveIndex < maxWaveIndex)
        {
            if (countDown <= 0f)
            {
                StartCoroutine(SpawnWave());
                countDown = timeBetweenWaves;
            }

            countDown -= Time.deltaTime;
            waveCountDownText.text = Mathf.Round(countDown).ToString();
        }
        else
        {
            EndLevel();
        }
    }
    private void Start()
    {
        waveIndex = 0;
        maxWaveIndex = PlayerStats.totalWaves = waves.Length;
    }

    IEnumerator SpawnWave()
    {
        Wave wave = waves[waveIndex];

        PlayerStats.Wave++;
        waveIndex++;
        Debug.Log("wave started: " + waveIndex);

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        


    }
    void EndLevel()
    {
        if (waveIndex == maxWaveIndex)
        {
            if (PlayerStats.Health > 0)
            {
                if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    Time.timeScale = 0f;
                    gameManager.WinLevel();

                }
            }
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }

    public void StartWave()//button of start wave
    {

        if (countDown > 5f && (waveIndex < maxWaveIndex))
        {
            PlayerStats.Money += 5;
        }
        countDown = 0;

    }
}
