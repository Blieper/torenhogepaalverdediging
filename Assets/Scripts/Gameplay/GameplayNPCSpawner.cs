using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayNPCSpawner : MonoBehaviour {

    public float SpawnRate = 1f;
    public int WavesUntilEnemies = 3;
    public int WavesUntilBosses = 20;

    public List<GameObject> Minions;
    public List<GameObject> Fighters;
    public List<GameObject> Bosses;

    public List<GameObject> Enemies;
    public int EnemyCount;
    public int EnemiesToSpawn;
    public GameObject SpawnObject;

    void Start () {
        Enemies = new List<GameObject>(); 
    }

    public void OnWaveStart (int Wave) {
        Enemies = new List<GameObject>();
        EnemyCount = 0;

        if (Wave >= WavesUntilBosses) {
            return;
        }

        EnemiesToSpawn = Wave * 2 + 5;

        TimedSpawn();
    }

    public void SpawnEnemy () {
        Enemies.Add(Instantiate(
                        Minions[0],
                        SpawnObject.transform.position,
                        SpawnObject.transform.rotation));

        EnemyCount++;
    }

    public void TimedSpawn () {
        SpawnEnemy();

        if (EnemyCount < EnemiesToSpawn) {
            Invoke("TimedSpawn", SpawnRate);
        }
    }

    public void EliminateEnemy (GameObject Enemy) { 
        Enemies.Remove(Enemy);

        if (EnemyCount == EnemiesToSpawn && Enemies.Count == 0) {
            GetComponent<GameplayWaves>().EndWave();
        }

        GameObject.Destroy(Enemy);
    }
}
