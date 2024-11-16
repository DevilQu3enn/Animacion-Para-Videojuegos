using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerDamageValues : StateMachineBehaviour
{
    [SerializeField] private bool isPunch;
    [SerializeField] private DamagePayload.Severity severity;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerControllerFinal p = animator.gameObject.GetComponent<PlayerControllerFinal>();
        p.SetUpDamage(isPunch, severity);
        p.UnlockAttackInput();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        float progress = animatorStateInfo.normalizedTime;
        if(progress >= 0.5f){
            PlayerControllerFinal p = animator.gameObject.GetComponent<PlayerControllerFinal>();
            p.UnlockAttackInput();
        }
    }
}
