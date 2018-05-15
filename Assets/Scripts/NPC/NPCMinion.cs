using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMinion : NPC {

    bool ReachedTarget = false;

	public override void Start () {
        base.Start();

        Target = GameObject.FindGameObjectWithTag("NPC End Point");

        MoveToTarget();
	}

    public override void Update () {
        base.Update();

        // Check if we've reached the destination
        if (!Agent.pathPending) {
            if (Agent.remainingDistance <= Agent.stoppingDistance) {
                if (!Agent.hasPath || Agent.velocity.sqrMagnitude == 0f && !ReachedTarget) {
                    ReachedTarget = true;

                    GameObject GameplayObject = GameObject.FindGameObjectWithTag("Gameplay Object");
                    Gameplay.Statistics.TakeDamage(10);

                    Health healthStat = GetComponent<Health>();

                    if (healthStat != null)
                        healthStat.Value = 0;
                }
            }
        }
    }
}
