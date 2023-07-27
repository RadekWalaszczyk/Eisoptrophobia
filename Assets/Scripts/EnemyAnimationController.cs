using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;
    PlayerController player;

    private void Start()
    {
        player = PlayerController.inst;
    }

    float dmgDelay = 0.5f;
    private void Update()
    {
        if (!Physics.Linecast(transform.position + Vector3.up, player.transform.position + Vector3.up, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore) && !enemyController.dead)
        {
            dmgDelay -= Time.deltaTime;
            if (dmgDelay <= 0)
            {
                Attack();
                dmgDelay = 0.5f;
            }
        }
    }

    void Attack()
    {
        enemyController.CanDicreaseSanity(true);
    }
}
