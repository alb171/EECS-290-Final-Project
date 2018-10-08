using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowCharacter : MonoBehaviour {

    [SerializeField] Transform target;
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        Debug.Log("agent isOnNavMesh = " + agent.isOnNavMesh);
        agent.SetDestination(target.position);
    }
}
