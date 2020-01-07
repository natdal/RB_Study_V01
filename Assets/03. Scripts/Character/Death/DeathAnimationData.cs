using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{

    public enum DEATH_TYPE
    {
        NONE,
        LAUNCH_INTO_AIR,
        GROUND_SHOCK,
    }

    [CreateAssetMenu(fileName = "New ScriptableObject", menuName = "ver_01/Death/DeathAnimationData")]
    public class DeathAnimationData : ScriptableObject
    {
        public List<GENERAL_BODY_PART> generalBodyParts = new List<GENERAL_BODY_PART>();
        public RuntimeAnimatorController animator;
        public DEATH_TYPE deathType;
        public bool isFacingAttacker;
    }

}
