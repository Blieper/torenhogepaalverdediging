using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayWaves : MonoBehaviour {

    public int Wave = 0;
    public float TimeBetweenWaves = 5f;
    GameplayNPCSpawner Spawner;

	void Start () {
        Spawner = GetComponent<GameplayNPCSpawner>();

        StartWave();
    }
	
    void Update() {
        //if (Spawner.Enemies.Count == 0) {
        //    print("Wave ended!");

        //    EndWave();
        //}
    }

    public void StartWave () {
        Wave++;

        Spawner.OnWaveStart(Wave);
    }

    public void EndWave () {
        Invoke("StartWave", TimeBetweenWaves);
    }
}
