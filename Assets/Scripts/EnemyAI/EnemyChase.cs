using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : StateMachineBehaviour
{
    EnemyController enemyController;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyController = animator.GetComponentInParent<EnemyController>();
    }

    float damageDelay = 1;
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemyController.navMeshAgent.isActiveAndEnabled == true)
            enemyController.MoveToPlayer();

        if (enemyController.CanAttack())
        {
            if (enemyController.navMeshAgent.isActiveAndEnabled == true)
                enemyController.navMeshAgent.isStopped = true;

            damageDelay -= Time.deltaTime;
            if (damageDelay <= 0)
            {
                PlayerController.inst.Health.GetDamage(1);
                damageDelay = 1;
            }
        }
        else
        {
            if (enemyController.navMeshAgent.isActiveAndEnabled == true)
                enemyController.navMeshAgent.isStopped = false;
        }
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}
}
