using System;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;


namespace Unity.FPS.UI
{
    public class LoadoutMenuButton : MonoBehaviour
    {
        [Tooltip("The Loadout state that is loaded when the user presses this button")]
        public string buttonState;

        public Unity.FPS.Game.LoadoutManager loadoutManager;

        [Tooltip("Coming Soon Items")]
        public bool comingSoon;
        public GameObject ComingSoonModal;

        public void SetLoadoutState()
        {
            if (comingSoon)
            {
                ComingSoonModal.SetActive(true);
            }
            else
            {

                loadoutManager.ChangeLoadoutState(buttonState);
            }
        }
    }
}