using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    [CreateAssetMenu(fileName = "New State", menuName = "ver_01/AbilityData/TeleportOnLedge")]
    public class TeleportOnLedge : StateData
    {

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = CharacterManager.Instance.GetCharacter(animator);

            //CharacterControl control = animator.gameObject.GetComponentInChildren<TriggerDetector>().owner;

            Vector3 endPosition = control.ledgeChecker.grabbedLedge.transform.position + control.ledgeChecker.grabbedLedge.endPosition;

            control.transform.position = endPosition;
            control.skinnedMeshAnimator.transform.position = endPosition;
            control.skinnedMeshAnimator.transform.parent = control.transform;
        }
    }
}
