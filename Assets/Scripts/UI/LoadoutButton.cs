using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity.FPS.UI
{
    public class LoadoutButton : MonoBehaviour
    {
        [Tooltip("The warning modal that is displayed")]
        public GameObject WarningModal;

        public void HandlePress()
        {
            if (PlayerPrefs.GetString("Account") == "")
            {
                WarningModal.SetActive(true);
            }
            else
            {
                SceneManager.LoadScene("Loadout");
            }
        }
    }
}