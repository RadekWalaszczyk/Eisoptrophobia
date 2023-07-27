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
    [SerializeField] float SanityDistance;
    [SerializeField] AudioSource AgroSound;
    [SerializeField] Animator Anim;

    public bool dead = false;

    private void Start()
    {
        player = PlayerController.inst;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public bool CheckIfPlayerIsNear()
    {
        Debug.DrawLine(transform.position + Vector3.up * 2f, player.transform.position + Vector3.up);
        if (Physics.Linecast(transform.position + Vector3.up, player.transform.position + Vector3.up, out RaycastHit hitInfo, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore))
            return false;
        else
        {
            var distance = Vector3.Distance(transform.position, player.transform.position);
            return distance < SeeDistance;
        }
    }

    public void MoveToPlayer()
    {
        navMeshAgent.SetDestination(player.transform.position);
    }

    public bool CanDicreaseSanity(bool withAttack = false)
    {
        var distance = Vector3.Distance(transform.position, player.transform.position);
        var finalDistance = distance < SanityDistance;

        if (finalDistance && withAttack)
            PlayerController.inst.Health.GetDamage(1);

        return finalDistance;
    }

    public void Dead()
    {
        dead = true;
        navMeshAgent.isStopped = false;
        navMeshAgent.enabled = false;
        Anim.SetBool("Dead", true);
        StartCoroutine(DeadDelay());
        GetComponent<Collider>().enabled = false;
    }

    public void PlayAgroSound()
    {
        AgroSound.Play();
    }

    IEnumerator DeadDelay()
    {
        Anim.SetBool("Chase", false);
        Anim.enabled = false;
        yield return new WaitForSeconds(3.5f);
        Destroy(gameObject);
    }
}
