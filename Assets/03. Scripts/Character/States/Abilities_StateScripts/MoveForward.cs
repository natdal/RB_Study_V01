using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    // 이 StateScripts에서 애니메이션도 끄고 킨다.
    [CreateAssetMenu(fileName = "New State", menuName = "ver_01/AbilityData/MoveForward")]
    public class MoveForward : StateData // 아래 정의된 행동거지를 CharacterStateBase에서 UpdateAbility를 호출해가지고 Animator를 갱신시킨다.
    {
        public bool allowEarlyTurn;
        public bool lockDirection;
        public bool constant;
        public AnimationCurve speedGraph;
        public float speed;
        public float blockDistance;
        
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (allowEarlyTurn && !control.animationProgress.disallowEarlyTurn)
            {
                if (control.moveLeft)
                {
                    control.FaceForward(false);
                }
                if (control.moveRight)
                {
                    control.FaceForward(true);
                }
            }

            control.animationProgress.disallowEarlyTurn = false;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator); // 캐릭터컨트롤러에 지금 에니메이터를 받아와.

            if (control.jump)
            {
                animator.SetBool(TRANSITION_PARAMETER.Jump.ToString(), true);
            }

            if (constant) // 횡이동이면 여기로. constant는 일정하다는 뜻
            {
                ConstantMove(control, animator, stateInfo); // 계속 이동해
            }
            else
            {
                ControlledMove(control, animator, stateInfo); // 이동해
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }

        private void ConstantMove(CharacterControl control, Animator animator, AnimatorStateInfo stateInfo) // 횡이동
        {
            if (!CheckFront(control))
            {
                control.MoveForward(speed, speedGraph.Evaluate(stateInfo.normalizedTime)); // 그래프를 일자로 만들면 가속도 없이 깔끔하겠네.
            }
        }

        private void ControlledMove(CharacterControl control, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (control.moveRight && control.moveLeft) // 둘 다 True면 걍 가만히 있어. ad 같이 누를때여.
            {
                animator.SetBool(TRANSITION_PARAMETER.Move.ToString(), false);
                return;
            }

            if (!control.moveRight && !control.moveLeft) // 둘 다 false면 걍 가만히 있어. 아무것도 안할떄여.
            {
                animator.SetBool(TRANSITION_PARAMETER.Move.ToString(), false);
                return;
            }

            if (control.moveRight)
            {
                if (!CheckFront(control)) // 앞에 뭐가 있으면 밑에꺼 해라. 이걸 안해주면 벽으로 계속 가려고 해서 덜덜 떨린다.
                {
                    //내 animtor를 넣겠다. GetCharacterControl
                    //CharacterStateBase에서 CharacterControl을 생성해줬으니까 거기에 있는 speed같은 것에 접근 할 수 있다.
                    control.MoveForward(speed, speedGraph.Evaluate(stateInfo.normalizedTime));

                }
            }

            if (control.moveLeft)
            {
                if (!CheckFront(control)) //아하... 한쪽 벽에 붙는 순간 CheckFront에 걸려버리니까... 다시 안먹는구나... 이거 버그픽스 하것지뭐
                {
                    control.MoveForward(speed, speedGraph.Evaluate(stateInfo.normalizedTime));

                }
            }

            CheckTurn(control);
        }

        private void CheckTurn(CharacterControl control)
        {
            if (!lockDirection)
            {
                if (control.moveRight)
                {
                    control.transform.rotation = Quaternion.Euler(0f, 0f, 0f); // 앞모습
                }

                if (control.moveLeft)
                {
                    control.transform.rotation = Quaternion.Euler(0f, 180f, 0f); // 뒷모습
                }
            }
        }

        bool CheckFront(CharacterControl control) // 이걸 판단하면 좋은 이유가 바닥을 만나면 Landing모션을 취할 수 있다.
        {
            
            foreach (GameObject o in control.frontSpheres)
            {

                Debug.DrawRay(o.transform.position, control.transform.forward * 0.3f, Color.yellow);
                RaycastHit hit;
                if (Physics.Raycast(o.transform.position, control.transform.forward, out hit, blockDistance)) // 앞으로 쏴. 근데 뭐가 맞았네?
                {
                    if (!control.ragdollParts.Contains(hit.collider)) // ray가 레그돌에 안맞았냐?
                    {
                        if (!IsBodyPart(hit.collider) && !Ledge.IsLedge(hit.collider.gameObject) && !Ledge.IsLedgeChecker(hit.collider.gameObject))
                        {
                            return true;
                        }
                    }
                }
            }

            return false; // 앞에 없네!
        }

        bool IsBodyPart(Collider col)
        {
            CharacterControl control = col.transform.root.GetComponent<CharacterControl>();

            if (control == null)
            {
                return false;
            }

            if (control.gameObject == col.gameObject)
            {
                return false;
            }

            if (control.ragdollParts.Contains(col))
            {
                return true;
            }

            return false;
        }
    }
}

