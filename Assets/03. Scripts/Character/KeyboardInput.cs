using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    public class KeyboardInput : MonoBehaviour // 키보드 입력 받는 클래스
    {
        void Update()
        {

            if (Input.GetKey(KeyCode.LeftShift))
            {
                VirtualInputManager.Instance.turbo = true;
            }
            else
            {
                VirtualInputManager.Instance.turbo = false;
            }

            if (Input.GetKey(KeyCode.W))
            {
                VirtualInputManager.Instance.moveUp = true;
            }
            else
            {
                VirtualInputManager.Instance.moveUp = false;
            }

            if (Input.GetKey(KeyCode.S))
            {
                VirtualInputManager.Instance.moveDown = true;
            }
            else
            {
                VirtualInputManager.Instance.moveDown = false;
            }

            if (Input.GetKey(KeyCode.D))
            {
                VirtualInputManager.Instance.moveRight = true; // 매니저에 있는 값을 true로 바꿈. 싱글턴이라 항상 존재함.
            }
            else
            {
                VirtualInputManager.Instance.moveRight = false;
            }

            if (Input.GetKey(KeyCode.A))
            {
                VirtualInputManager.Instance.moveLeft = true;
            }
            else
            {
                VirtualInputManager.Instance.moveLeft = false;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                VirtualInputManager.Instance.jump = true;
            }
            else
            {
                VirtualInputManager.Instance.jump = false;
            }

            if (Input.GetKey(KeyCode.Return))
            {
                VirtualInputManager.Instance.attack = true;
            }
            else
            {
                VirtualInputManager.Instance.attack = false;
            }
        }
    }
}

