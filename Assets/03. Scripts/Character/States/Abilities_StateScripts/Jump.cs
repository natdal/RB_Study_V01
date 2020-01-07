using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    [CreateAssetMenu(fileName = "New State", menuName = "ver_01/AbilityData/Jump")]
    public class Jump : StateData
    {
        [Range(0f,1f)]
        public float jumpTiming;
        public float jumpForce;
        public AnimationCurve pull; // 꾹 누를수록 높이 점프
        //private bool isJumped; // AniamationProgress로 옮김

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (jumpTiming == 0f)
            {
                CharacterControl control = characterState.GetCharacterControl(animator);

                characterState.GetCharacterControl(animator).RIGID_BODY.AddForce(Vector3.up * jumpForce);
                animator.SetBool(TRANSITION_PARAMETER.Grounded.ToString(), false); // 임시 추가
                control.animationProgress.jumped = true;
            }

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            control.pullMultiplier = pull.Evaluate(stateInfo.normalizedTime);

            if (!control.animationProgress.jumped && stateInfo.normalizedTime >= jumpTiming)
            {
                characterState.GetCharacterControl(animator).RIGID_BODY.AddForce(Vector3.up * jumpForce);
                control.animationProgress.jumped = true;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            control.pullMultiplier = 0f; // 꾹 누를수록 더 높이. 어퍼컷같은거엔 필요없응꼐 0 

            control.animationProgress.jumped = false;
        }
    }
}

