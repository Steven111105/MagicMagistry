using System.Collections;
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
    // Start is called before the first frame update
    void Start()
    {
        waveNumber = 0;
        Invoke(nameof(StartWave),3f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartWave(){
        waveNumber++;
        int enemyCount = Mathf.RoundToInt(waveNumber * 1.5f);
        for (int i = 0; i < enemyCount; i++)
        {
            // Vector2 spawnPos = (Vector2)player.position + Random.insideUnitCircle.normalized * spawnRange;
            Vector2 spawnPos = new Vector2(Random.Range(-18.5f, 18.5f), Random.Range(-19.5f, 19.5f));
            while (Vector2.Distance(spawnPos, player.position) < 7f)
            {
                spawnPos = new Vector2(Random.Range(-18.5f, 18.5f), Random.Range(-19.5f, 19.5f));
            }
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            int enemyType = Random.Range(0, 2);
            // int enemyType = 1;
            enemy.GetComponent<EnemySetup>().spawner = this;
            enemy.GetComponent<EnemySetup>().SetupEnemy(enemyType);
        }
        int slimeCount = waveNumber / 5;
        // slimeCount = 1;
        for (int i = 0; i < slimeCount; i++)
        {
            // Vector2 spawnPos = (Vector2)player.position + Random.insideUnitCircle.normalized * spawnRange;
            Vector2 spawnPos = new Vector2(Random.Range(-18.5f, 18.5f), Random.Range(-19.5f, 19.5f));
            while (Vector2.Distance(spawnPos, player.position) < 7f)
            {
                spawnPos = new Vector2(Random.Range(-18.5f, 18.5f), Random.Range(-19.5f, 19.5f));
            }
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            enemy.GetComponent<EnemySetup>().spawner = this;
            enemy.GetComponent<EnemySetup>().SetupEnemy(2); // Assuming 2 is the type for SlimeEnemy
        }
        StartCoroutine(WaveTimer());
    }

    IEnumerator WaveTimer(){
        roundTimer = 0;
        while(roundTimer < 7*waveNumber + 15f){
            roundTimer += Time.deltaTime;
            yield return null;
        }
        StartWave();
    }
}