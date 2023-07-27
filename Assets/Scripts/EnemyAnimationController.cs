using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;
    [SerializeField] bool isVisible;
    PlayerController player;

    private void Start()
    {
        player = enemyController.player;
    }

    private void OnBecameInvisible()
    {
        isVisible = false;
    }

    private void OnBecameVisible()
    {
        isVisible = true;
    }

    float dmgDelay = 0.5f;
    private void Update()
    {
        if (isVisible && !enemyController.dead)
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
