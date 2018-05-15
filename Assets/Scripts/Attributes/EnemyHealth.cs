using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health {
    public GameplayNPCSpawner SpawnObject;

    public override void Start () {
        base.Start();

        SpawnObject = GameObject.FindGameObjectWithTag("Gameplay Object").GetComponent<GameplayNPCSpawner>();
    }

    public override void Eliminate() {
        SpawnObject.EliminateEnemy(gameObject);

        //base.Eliminate();
    }
}
