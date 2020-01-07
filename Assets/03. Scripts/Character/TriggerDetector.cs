using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    public enum GENERAL_BODY_PART
    {
        Upper,
        Lower,
        Arm,
        Leg,
    }

    public class TriggerDetector : MonoBehaviour
    {

        public GENERAL_BODY_PART generalBodyPart;

        public List<Collider> collidingParts = new List<Collider>();
        private CharacterControl owner;

        private void Awake()
        {
            owner = this.GetComponentInParent<CharacterControl>();
        }

        private void OnTriggerEnter(Collider col)
        {
            if (owner.ragdollParts.Contains(col))
            {
                return;
            }

            CharacterControl attacker = col.transform.root.GetComponent<CharacterControl>();

            if (attacker == null) // 플레이어가 아니란거지
            {
                return;
            }

            if (col.gameObject == attacker.gameObject) // 같은놈을 쳤단거지
            {
                return;
            }

            if (!collidingParts.Contains(col)) // 파츠 부분 충돌 구현!!!!!!!!!!
            {
                collidingParts.Add(col); // 트리거에 들어오면 충돌에 넣어!
            }
        }

        private void OnTriggerExit(Collider attacker) // 나가면!
        {
            if (collidingParts.Contains(attacker))
            {
                collidingParts.Remove(attacker); // 충돌에서 빼!!
            }
        }
    }
}


