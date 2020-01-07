using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    public class AttackInfo : MonoBehaviour
    {
        public CharacterControl attacker = null;
        public Attack attackAbility; // statedata
        public List<string> colliderNames = new List<string>();
        //public bool launchIntoAir;
        public DEATH_TYPE deathType;
        public bool mustCollide;
        public bool mustFaceAttacker;
        public float lethalRange;
        public int maxHits;
        public int currentHits;
        public bool isRegisterd;
        public bool isFinished;

        public void ResetInfo(Attack attack, CharacterControl _attacker)
        {
            isRegisterd = false;
            isFinished = false;
            attackAbility = attack;
            attacker = _attacker;
        }

        public void Register(Attack attack)
        {
            isRegisterd = true;

            attackAbility = attack;
            colliderNames = attack.colliderNames;
            deathType = attack.deathType;
            //launchIntoAir = attack.lanchIntoAir;
            mustCollide = attack.mustCollide;
            mustFaceAttacker = attack.mustFaceAttacker;
            lethalRange = attack.lethalRange;
            maxHits = attack.maxHits;
            currentHits = 0;
        }

        private void OnDisable()
        {
            isFinished = true;
        }
    }
}


