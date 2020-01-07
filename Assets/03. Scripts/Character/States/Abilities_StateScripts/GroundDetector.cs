using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    [CreateAssetMenu(fileName = "New State", menuName = "ver_01/AbilityData/GroundDetector")]
    public class GroundDetector : StateData
    {
        [Range(0.01f,1f)]
        public float checkTime;
        public float distance;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (stateInfo.normalizedTime >= checkTime)
            {
                if (IsGrounded(control))
                {
                    animator.SetBool(TRANSITION_PARAMETER.Grounded.ToString(), true);

                }
                else
                {
                    animator.SetBool(TRANSITION_PARAMETER.Grounded.ToString(), false);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        bool IsGrounded(CharacterControl control) // 이걸 판단하면 좋은 이유가 바닥을 만나면 Landing모션을 취할 수 있다.
        {
            if ((control.RIGID_BODY.velocity.y > -0.001f) && (control.RIGID_BODY.velocity.y <= 0f)) // 그럼 땅이지. 근데 정점에 있을때는 y가 0이지 않나? 이거 보정해야되겠는데
            {
                return true; // 땅이여!
            }

            if (control.RIGID_BODY.velocity.y < 0f) // 떨어지고 있는 중이네!
            {
                foreach (GameObject o in control.bottomSpheres)
                {
                    Debug.DrawRay(o.transform.position, -Vector3.up * 0.7f, Color.yellow);
                    RaycastHit hit;
                    if (Physics.Raycast(o.transform.position, -Vector3.up, out hit, distance)) // 아래로 쏴. 근데 뭐가 맞았네?
                    {
                        if (!control.ragdollParts.Contains(hit.collider) && !Ledge.IsLedge(hit.collider.gameObject) && !Ledge.IsLedgeChecker(hit.collider.gameObject)) // 플레이어의 레그돌파츠에 ray가 안부딪히면 아직 살아있다는 이야기다.
                        {
                            return true; // 땅에 도착했네!
                        }
                    }
                }
            }

            return false; // 공중이다!
        }

    }
}
