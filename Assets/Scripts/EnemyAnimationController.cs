using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;
    [SerializeField] bool ignoreAttacks;

    public void Attack()
    {
        if (!ignoreAttacks)
            enemyController.CanDicreaseSanity(true);
    }
}
