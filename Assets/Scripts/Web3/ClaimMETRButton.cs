using System;
using System.Collections.Generic;
using Unity.FPS.Game;
using Unity.FPS.UI;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Unity.FPS.Game
{
    public class ClaimMETRButton : MonoBehaviour
    {
        [Tooltip("Warning Modal GameObject")]
        public GameObject WarningModal;

        [Tooltip("Web3 Manager instance")]
        public Web3Manager Web3Manager;

        [Tooltip("Reference to the button itself")]
        public GameObject ClaimButton;

        // Used to prevent double dipping
        private bool hasClaimed = false;

        public async void ClaimMETR()
        {
            if (!hasClaimed)
            {
                if (PlayerPrefs.GetString("Account") == "")
                {
                    WarningModal.SetActive(true);
                }
                else
                {
                    await Web3Manager.mintMETR(GameScore.playerScore);
                    ClaimButton.SetActive(false);
                    hasClaimed = true;
                }
            }
        }

    }
}
