using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    public class VirtualInputManager : Singleton<VirtualInputManager> // 입력매니저
    {
        public bool turbo;
        public bool moveUp;
        public bool moveDown;
        public bool moveRight;
        public bool moveLeft;
        public bool jump;
        public bool attack;
    }

}
