﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    [CreateAssetMenu(fileName = "New State", menuName = "ver_01/AbilityData/ResetLocalPosition")]
    public class ResetLocalPosition : StateData
    {
        public bool onStart;
        public bool onEnd;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (onStart)
            {
                CharacterControl control = characterState.GetCharacterControl(animator);
                control.skinnedMeshAnimator.transform.localPosition = Vector3.zero;
                control.skinnedMeshAnimator.transform.localRotation = Quaternion.identity;
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (onEnd)
            {
                CharacterControl control = characterState.GetCharacterControl(animator);
                control.skinnedMeshAnimator.transform.localPosition = Vector3.zero;
                control.skinnedMeshAnimator.transform.localRotation = Quaternion.identity;
            }
        }
    }
}

