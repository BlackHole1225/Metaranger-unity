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

        public void SetLoadoutState()
        {
            LoadoutManager.ChangeLoadoutState(buttonState);
        }
    }
}