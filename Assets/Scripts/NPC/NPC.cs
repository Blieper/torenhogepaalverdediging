using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPC : MonoBehaviour {

    NavMeshAgent Agent;
    public GameObject Target;

    //bool IsMoving = true;

    public virtual void Start () {
        Agent = GetComponent<NavMeshAgent>();
    }
	
	public virtual void Update () {

	}

    public virtual void MoveToTarget ()
    {
        Agent.SetDestination(Target.transform.position);
        //IsMoving = true;
    }
}
