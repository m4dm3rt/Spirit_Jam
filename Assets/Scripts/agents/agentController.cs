using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class agentController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private GameObject Player;

    [HideInInspector]public Vector3 targetPos;
    [HideInInspector]public bool hasPoint = false;
    [HideInInspector]public int pointIndex;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.Warp(this.transform.position);

        Player = FindObjectOfType<playerController>().gameObject;
    }

    private void FixedUpdate() {
        if(hasPoint){
            navMeshAgent.SetDestination(targetPos);
        }
    }
}
