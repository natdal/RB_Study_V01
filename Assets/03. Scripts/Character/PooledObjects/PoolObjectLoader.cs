using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{

    public enum POOL_OBJECT_TYPE
    {
        ATTACKINFO,
        HAMMER_OBJ,
        HAMMER_VFX,
    }

    public class PoolObjectLoader : MonoBehaviour
    {
        public static PoolObject InstantiatePrefab(POOL_OBJECT_TYPE objType)
        {
            GameObject obj = null;

            switch (objType)
            {
                case POOL_OBJECT_TYPE.ATTACKINFO:
                    {
                        obj = Instantiate(Resources.Load("AttackInfo", typeof(GameObject)) as GameObject);
                        break;
                    }
                case POOL_OBJECT_TYPE.HAMMER_OBJ:
                    {
                        obj = Instantiate(Resources.Load("ThorHammer", typeof(GameObject)) as GameObject);
                        break;
                    }
                case POOL_OBJECT_TYPE.HAMMER_VFX:
                    {
                        obj = Instantiate(Resources.Load("GrenadeIceExp", typeof(GameObject)) as GameObject);
                        break;
                    }
            }

            return obj.GetComponent<PoolObject>();

        }
    }
}
