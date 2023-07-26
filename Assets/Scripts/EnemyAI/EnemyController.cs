using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    PlayerController player;
    public NavMeshAgent navMeshAgent;

    [SerializeField] float SeeDistance;
    [SerializeField] float AttackDistance;

    private void Start()
    {
        player = PlayerController.inst;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public bool CheckIfPlayerIsNear()
    {
        var distance = Vector3.Distance(transform.position, player.transform.position);
        return distance < SeeDistance;
    }

    public void MoveToPlayer()
    {
        navMeshAgent.SetDestination(player.transform.position);
    }

    public bool CanAttack()
    {
        var distance = Vector3.Distance(transform.position, player.transform.position);
        return distance < AttackDistance;
    }
}
