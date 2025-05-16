using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    float roundTimer;
    public int waveNumber;
    [SerializeField] GameObject enemyPrefab;
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        waveNumber = 0;
        StartWave();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartWave(){
        waveNumber++;
        int enemyCount = waveNumber * 2;
        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 spawnPos =  (Vector2)player.position + Random.insideUnitCircle.normalized*10f;
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            // int enemyType = Random.Range(0, 2);
            int enemyType = 1;
            enemy.GetComponent<EnemySetup>().spawner = this;
            enemy.GetComponent<EnemySetup>().SetupEnemy(enemyType);
        }
        StartCoroutine(WaveTimer());
    }

    IEnumerator WaveTimer(){
        roundTimer = 0;
        while(roundTimer < 7*waveNumber + 20f){
            roundTimer += Time.deltaTime;
            yield return null;
        }
        StartWave();
    }
}