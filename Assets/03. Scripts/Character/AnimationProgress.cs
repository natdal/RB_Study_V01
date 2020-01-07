using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    public class AnimationProgress : MonoBehaviour
    {
        
        //public Dictionary<StateData, int> currentRunningAbilities = new Dictionary<StateData, int>();

        public bool cameraShaken;
        public List<POOL_OBJECT_TYPE> spawnedObjList = new List<POOL_OBJECT_TYPE>();
        //public bool ragdollTriggered;
        //public MoveForward latestMoveForward;

        //[Header("Attack Button")]
        public bool attackTriggered;
        //public bool attackButtonIsReset;
        public float maxPressTime; // 47강 임시 변수, 나중에 삭제

        //[Header("GroundMovement")]
        public bool disallowEarlyTurn;
        //public bool lockDirectionNextState;

        //[Header("Colliding Objects")]
        //public GameObject ground;
        //public GameObject blockingObj;
        
        //[Header("AirControl")]
        public bool jumped;
        //public float airMomentum;
        //public bool cancelPull;
        //public Vector3 maxFallVelocity;
        //public bool canWallJump;
        //public bool checkWallBlock;
        
        //[Header("UpdateBoxCollider")]
        //public bool updatingBoxCollider;
        //public bool updatingSpheres;
        //public Vector3 targetSize;
        //public float sizeSpeed;
        //public Vector3 targetCenter;
        //public float centerSpeed;

        //[Header("Damage Info")]
        //public Attack attack;
        //public CharacterControl attacker;
        //public TriggerDetector damagedTrigger;
        //public GameObject attackingPart;

        private CharacterControl control;
        private float pressTime;

        private void Awake()
        {
            control = GetComponentInParent<CharacterControl>();
            pressTime = 0f;
        }

        private void Update()
        {
            if (control.attack)
            {
                pressTime += Time.deltaTime;
            }
            else
            {
                pressTime = 0f;
            }

            if (pressTime == 0f)
            {
                attackTriggered = false;
            }else if (pressTime > maxPressTime)
            {
                attackTriggered = false;
            }
            else
            {
                attackTriggered = true;
            }
        }

        /* 임시 주석, 이게 전체 코드
        private void Awake()
        {
            control = GetComponent<CharacterControl>();
        }

        private void Update()
        {
            if (control.attack)
            {
                if (attackButtonIsReset)
                {
                    attackTriggered = true;
                    attackButtonIsReset = false;
                }
            }
            else
            {
                attackButtonIsReset = true;
                attackTriggered = false;
            }
        }

        public bool IsRunning(System.Type type)
        {
            foreach (KeyValuePair<StateData, int> data in currentRunningAbilities)
            {
                if (data.Key.GetType() == type)
                {
                    return true;
                }
            }

            return false;
        }

        public bool RightSideIsBlocked()
        {
            if (blockingObj == null)
            {
                return false;
            }

            if ((blockingObj.transform.position -
                control.transform.position).z > 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool LeftSideIsBlocked()
        {
            if (blockingObj == null)
            {
                return false;
            }

            if ((blockingObj.transform.position -
                control.transform.position).z < 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        */

    }
}