using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    public class AttackManager : Singleton<AttackManager>
    {
        public List<AttackInfo> currentAttacks = new List<AttackInfo>();
    }
}


