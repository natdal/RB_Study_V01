using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    public class PlayGame : MonoBehaviour
    {
        public CharacterSelect characterSelect;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (characterSelect.selectedCharacterType != PLAYERBLE_CHARACTER_TYPE.NONE)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(GAME_SCENES.GameScene.ToString());
                }
                else
                {
                    Debug.Log("Must select character first");
                }
            }  
        }
    }
}

