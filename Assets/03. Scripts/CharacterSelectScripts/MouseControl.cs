using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    public class MouseControl : MonoBehaviour
    {

        Ray ray;
        RaycastHit hit;
        public PLAYERBLE_CHARACTER_TYPE selectedCharacterType;
        public CharacterSelect characterSelect;
        CharacterSelectLight characterSelectLight;
        CharacterHoverLight characterHoverLight;
        GameObject whiteSelection;
        Animator characterSelectCamAnimator;

        private void Awake()
        {
            characterSelect.selectedCharacterType = PLAYERBLE_CHARACTER_TYPE.NONE;
            characterSelectLight = GameObject.FindObjectOfType<CharacterSelectLight>();
            characterHoverLight = GameObject.FindObjectOfType<CharacterHoverLight>();

            whiteSelection = GameObject.Find("Test_WhiteSelect");
            whiteSelection.SetActive(false);

            characterSelectCamAnimator = GameObject.Find("CharacterSelectCameraController").GetComponent<Animator>();
        }

        private void Update()
        {
            ray = CameraManager.Instance.mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                CharacterControl control = hit.collider.gameObject.GetComponent<CharacterControl>();
                if (control != null)
                {
                    selectedCharacterType = control.playerbleCharacterType;
                }
                else
                {
                    selectedCharacterType = PLAYERBLE_CHARACTER_TYPE.NONE;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (selectedCharacterType != PLAYERBLE_CHARACTER_TYPE.NONE)
                {
                    characterSelect.selectedCharacterType = selectedCharacterType;
                    characterSelectLight.transform.position = characterHoverLight.transform.position;
                    CharacterControl control = CharacterManager.Instance.GetCharacter(selectedCharacterType);
                    characterSelectLight.transform.parent = control.skinnedMeshAnimator.transform;
                    characterSelectLight.light.enabled = true;

                    whiteSelection.SetActive(true);
                    whiteSelection.transform.parent = control.skinnedMeshAnimator.transform;
                    whiteSelection.transform.localPosition = new Vector3(0f, -0.05f, 0f);
                }
                else
                {
                    characterSelect.selectedCharacterType = PLAYERBLE_CHARACTER_TYPE.NONE;
                    characterSelectLight.light.enabled = false;
                    whiteSelection.SetActive(false);
                }

                foreach (CharacterControl c in CharacterManager.Instance.characters)
                {
                    if(c.playerbleCharacterType == selectedCharacterType)
                    {
                        c.skinnedMeshAnimator.SetBool(TRANSITION_PARAMETER.ClickAnimation.ToString(), true);
                    }
                    else
                    {
                        c.skinnedMeshAnimator.SetBool(TRANSITION_PARAMETER.ClickAnimation.ToString(), false);
                    }
                }

                characterSelectCamAnimator.SetBool(selectedCharacterType.ToString(), true);
            }
        }


    }
}


