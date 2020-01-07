using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    public class PlayerSpawn : MonoBehaviour
    {
        public CharacterSelect characterSelect;

        private string objName;

        private void Start()
        {
            switch (characterSelect.selectedCharacterType)
            {
                case PLAYERBLE_CHARACTER_TYPE.YELLOW :
                    {
                        objName = "yBot_Yellow";
                    }
                    break;
                case PLAYERBLE_CHARACTER_TYPE.RED:
                    {
                        objName = "yBot_Red";
                    }
                    break;
                case PLAYERBLE_CHARACTER_TYPE.GREEN:
                    {
                        objName = "yBot_Green";
                    }
                    break;
            }

            GameObject obj = Instantiate(Resources.Load(objName, typeof(GameObject))) as GameObject;
            obj.transform.position = this.transform.position;
            GetComponent<MeshRenderer>().enabled = false;

            //카메라 연결
            Cinemachine.CinemachineVirtualCamera[] arr = GameObject.FindObjectsOfType<Cinemachine.CinemachineVirtualCamera>();
            foreach (Cinemachine.CinemachineVirtualCamera v in arr)
            {
                CharacterControl control = CharacterManager.Instance.GetCharacter(characterSelect.selectedCharacterType);
                Collider target = control.GetBodyPart("Spine");
                
                v.LookAt = target.transform;
                v.Follow = target.transform;

            }



        }
    }
}