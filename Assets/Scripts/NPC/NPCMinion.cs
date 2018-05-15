using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMinion : NPC {

	public override void Start () {
        base.Start();

        Target = GameObject.FindGameObjectWithTag("NPC End Point");

        MoveToTarget();
	}
}
