using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPC : MonoBehaviour {

    public NavMeshAgent Agent;
    GameplayNPCSpawner Spawner;
    public GameObject Target;

    public virtual void Start () {
        Agent = GetComponent<NavMeshAgent>();
    }
	
	public virtual void Update () {

	}

    public virtual void MoveToTarget ()
    {
        Agent.SetDestination(Target.transform.position);
    }
}
