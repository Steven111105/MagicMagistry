using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    float roundTimer;
    public int waveNumber;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] public Sprite chaseEnemySprite;
    [SerializeField] public Sprite shootingEnemySprite;
    [SerializeField] public Sprite slimeEnemySprite;
    public GameObject bulletPrefab;
    public GameObject damageTextPrefab;
    public float spawnRange = 15f;
    public List<GameObject> enemies = new List<GameObject>();
    public TMP_Text waveText;
    public bool isSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        waveNumber = 0;
        Invoke(nameof(StartWave), 3f);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
                i--;
            }
        }

        if (enemies.Count == 0 && !IsInvoking(nameof(StartWave)))
        {
            StopCoroutine(WaveTimer());
            Debug.Log("All enemies defeated, starting next wave in 2 seconds...");
            Invoke(nameof(StartWave), 2f);
            isSpawning = true;
        }
    }

    void StartWave()
    {
        waveNumber++;
        waveText.gameObject.SetActive(true);
        StartCoroutine(HideWaveText());
        waveText.text = "Wave " + waveNumber;
        Debug.Log("Starting wave " + waveNumber);
        int enemyCount = Mathf.RoundToInt(waveNumber * 1.5f);
        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 spawnPos = new Vector2(Random.Range(-18.5f, 18.5f), Random.Range(-19.5f, 19.5f));
            while (Vector2.Distance(spawnPos, player.position) < 10f)
            {
                spawnPos = new Vector2(Random.Range(-18.5f, 18.5f), Random.Range(-19.5f, 19.5f));
            }
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            enemies.Add(enemy);
            int enemyType = Random.Range(0, 2);
            // int enemyType = 1;
            enemy.GetComponent<EnemySetup>().spawner = this;
            enemy.GetComponent<EnemySetup>().SetupEnemy(enemyType);
        }

        int slimeCount = waveNumber / 5;
        for (int i = 0; i < slimeCount; i++)
        {
            Vector2 spawnPos = new Vector2(Random.Range(-18.5f, 18.5f), Random.Range(-19.5f, 19.5f));
            while (Vector2.Distance(spawnPos, player.position) < 10f)
            {
                spawnPos = new Vector2(Random.Range(-18.5f, 18.5f), Random.Range(-19.5f, 19.5f));
            }
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            enemies.Add(enemy);
            enemy.GetComponent<EnemySetup>().spawner = this;
            enemy.GetComponent<EnemySetup>().SetupEnemy(2); // Assuming 2 is the type for SlimeEnemy
        }
        isSpawning = false;
        StartCoroutine(WaveTimer());
    }

    IEnumerator WaveTimer()
    {
        roundTimer = 0;
        while (roundTimer < 7 * waveNumber + 15f)
        {
            roundTimer += Time.deltaTime;
            yield return null;
        }
        Debug.Log("Wave " + waveNumber + " ended");
        StartWave();
    }
    
    IEnumerator HideWaveText()
    {
        yield return new WaitForSeconds(2f);
        waveText.gameObject.SetActive(false);
    }
}