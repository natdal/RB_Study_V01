using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    [CreateAssetMenu(fileName = "New State", menuName = "ver_01/AbilityData/SpawnObject")]
    public class SpawnObject : StateData
    {
        public POOL_OBJECT_TYPE objectType;
        [Range(0f,1f)]
        public float spawnTiming;
        public string parentObjName = string.Empty;
        public bool stickToParent;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (spawnTiming == 0f)
            {
                CharacterControl control = characterState.GetCharacterControl(animator);
                SpawnObj(control);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (!control.animationProgress.spawnedObjList.Contains(objectType))
            {
                if (stateInfo.normalizedTime >= spawnTiming)
                {
                    SpawnObj(control);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            if (control.animationProgress.spawnedObjList.Contains(objectType))
            {
                control.animationProgress.spawnedObjList.Remove(objectType);
            }
        }

        private void SpawnObj(CharacterControl control)
        {
            if (control.animationProgress.spawnedObjList.Contains(objectType))
            {
                return;
            }

            GameObject obj = PoolManager.Instance.GetObject(objectType);

            Debug.Log("spawning " + objectType.ToString() + " | looking for: " + parentObjName);
            if (!string.IsNullOrEmpty(parentObjName))
            {
                GameObject parent = control.GetChildObj(parentObjName);
                obj.transform.parent = parent.transform;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;
            }

            if (!stickToParent)
            {
                obj.transform.parent = null;
            }

            obj.SetActive(true);

            control.animationProgress.spawnedObjList.Add(objectType);

        }
    }
}

