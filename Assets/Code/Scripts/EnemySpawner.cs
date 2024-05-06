using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour {

    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private float enemiesPerSecond = 1f;
    [SerializeField] private float timeBetweenWaves = 5f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private Dictionary<int, int> enemiesLeftToSpawn = new Dictionary<int, int>();
    private float eps; // enemies Per second
    private bool isSpawning = false;

    private void Awake() {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start() {
        StartCoroutine(StartWave());
    }

    private void Update() {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / eps)) {
            SpawnEnemy();
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && AllEnemiesSpawned()) {
            EndWave();
        }
    }

    private void EnemyDestroyed() {
        enemiesAlive--;
    }

    private IEnumerator StartWave() {
        yield return new WaitForSeconds(timeBetweenWaves);

        isSpawning = true;
        eps = enemiesPerSecond;
        SetupEnemiesLeftToSpawn();
    }

    private void EndWave() {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        LevelManager.main.IncreaseRound();
        StartCoroutine(StartWave());
    }

    private void SetupEnemiesLeftToSpawn() {
        enemiesLeftToSpawn.Clear();

        switch (currentWave) {
            case 1:
                enemiesLeftToSpawn.Add(0, 20);
                break;
            case 2:
                enemiesLeftToSpawn.Add(0, 35);
                break;
            case 3:
                enemiesLeftToSpawn.Add(0, 25);
                enemiesLeftToSpawn.Add(1, 5);
                break;
            case 4:
                enemiesLeftToSpawn.Add(0, 35);
                enemiesLeftToSpawn.Add(1, 18);
                break;
            case 5:
                enemiesLeftToSpawn.Add(0, 5);
                enemiesLeftToSpawn.Add(1, 27);
                break;
            case 6:
                enemiesLeftToSpawn.Add(0, 15);
                enemiesLeftToSpawn.Add(1, 15);
                enemiesLeftToSpawn.Add(2, 4);
                break;
            case 7:
                enemiesLeftToSpawn.Add(0, 20);
                enemiesLeftToSpawn.Add(1, 20);
                enemiesLeftToSpawn.Add(2, 5);
                break;
            case 8:
                enemiesLeftToSpawn.Add(0, 10);
                enemiesLeftToSpawn.Add(1, 20);
                enemiesLeftToSpawn.Add(2, 14);
                break;
            case 9:
                enemiesLeftToSpawn.Add(2, 30);
                break;
            case 10:
                enemiesLeftToSpawn.Add(1, 102);
                break;
            case 11:
                enemiesLeftToSpawn.Add(0, 10);
                enemiesLeftToSpawn.Add(1, 10);
                enemiesLeftToSpawn.Add(2, 12);
                enemiesLeftToSpawn.Add(3, 3);
                break;
            case 12:
                enemiesLeftToSpawn.Add(1, 15);
                enemiesLeftToSpawn.Add(2, 10);
                enemiesLeftToSpawn.Add(3, 5);
                break;
            case 13:
                enemiesLeftToSpawn.Add(1, 50);
                enemiesLeftToSpawn.Add(2, 23);
                break;
            case 14:
                enemiesLeftToSpawn.Add(0, 49);
                enemiesLeftToSpawn.Add(1, 15);
                enemiesLeftToSpawn.Add(2, 10);
                enemiesLeftToSpawn.Add(3, 9);
                break;
            case 15:
                enemiesLeftToSpawn.Add(0, 20);
                enemiesLeftToSpawn.Add(1, 15);
                enemiesLeftToSpawn.Add(2, 12);
                enemiesLeftToSpawn.Add(3, 10);
                enemiesLeftToSpawn.Add(4, 5);
                break;
            case 16:
                enemiesLeftToSpawn.Add(2, 40);
                enemiesLeftToSpawn.Add(3, 8);
                break;
            case 17:
                enemiesLeftToSpawn.Add(5, 1);
                break;
            default:
                Debug.Log("Wave " + currentWave + " not implemented yet!");
                break;
        }
    }

    private void SpawnEnemy() {
        foreach (var kvp in enemiesLeftToSpawn) {
            int enemyTypeIndex = kvp.Key;
            int numberOfEnemies = kvp.Value;
            GameObject prefabToSpawn = enemyPrefabs[enemyTypeIndex];
            if (numberOfEnemies > 0) {
                Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
                enemiesAlive++;
                enemiesLeftToSpawn[enemyTypeIndex]--;
                return; // Spawn only one enemy per frame
            }
        }
    }

    private bool AllEnemiesSpawned() {
        foreach (var count in enemiesLeftToSpawn.Values) {
            if (count > 0) {
                return false;
            }
        }
        return true;
    }
}