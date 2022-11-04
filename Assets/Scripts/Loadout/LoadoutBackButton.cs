using System;
using System.Collections.Generic;
using Unity.FPS.Game;
using Unity.FPS.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity.FPS.UI
{
    public class LoadoutBackButton : MonoBehaviour
    {
        [Tooltip("Reference to the LoadoutManager")]
        public Unity.FPS.Game.LoadoutManager LM;

        public void ChangeUI()
        {
            if (LM.gameItemViewing != "None")
            {
                // The ChangeLoadoutState function hides the Upgrade Tree when it is called
                // so just call it again to revert to the original Loadout menu state
                LM.ChangeLoadoutState(LM.loadoutState);
            }
            else
            {
                SceneManager.LoadScene("IntroMenu");
            }
        }


    }
}