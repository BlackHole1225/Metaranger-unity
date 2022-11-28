using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Unity.FPS.UI
{
    public class SelectArenaButton : MonoBehaviour
    {
        public string SceneName = "";

        void Update()
        {
            if (EventSystem.current.currentSelectedGameObject == gameObject
              && Input.GetButtonDown(GameConstants.k_ButtonNameSubmit))
            {
                LoadTargetScene();
            }
        }

        public void LoadTargetScene()
        {
            if (PlayerPrefs.GetString("Account") == "")
            {
                SceneManager.LoadScene(SceneName);

            }
            else
            {
                PlayerPrefs.SetString("ArenaSelected", SceneName);
                SceneManager.LoadScene("CheckBlockchain");
            }
        }
    }
}