using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEnemyDamageValues : StateMachineBehaviour
{
    [SerializeField] private bool isPunch;
    [SerializeField] private DamagePayload.Severity severity;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<EnemyController>().SetUpDamage(isPunch, severity);
    }
}
