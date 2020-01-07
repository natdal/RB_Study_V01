using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    [CreateAssetMenu(fileName = "New State", menuName = "ver_01/AbilityData/ShakeCamera")]
    public class ShakeCamera : StateData
    {

        [Range(0f, 0.99f)]
        public float shakeTiming;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (shakeTiming == 0f) // 첫 흔들림
            {
                CharacterControl control = characterState.GetCharacterControl(animator);
                CameraManager.Instance.ShakeCamera(0.2f);
                control.animationProgress.cameraShaken = true;
            }

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (!control.animationProgress.cameraShaken)
            {
                if (stateInfo.normalizedTime >= shakeTiming) // 딜레이 만큼 프레임마다 반복 흔들림
                {
                    control.animationProgress.cameraShaken = true;
                    CameraManager.Instance.ShakeCamera(0.2f);
                }
            }

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            control.animationProgress.cameraShaken = false;
        }
    }
}

