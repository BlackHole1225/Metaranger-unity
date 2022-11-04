using System;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;
using Unity.FPS.UI;

namespace Unity.FPS.UI
{
    public class LoadoutUpgradeBranch : MonoBehaviour
    {

        [Tooltip("The Upgrade Item that this branch points to")]
        public LoadoutUpgradeItem destinationUpgrade;

        [Header("Branch State Images")]
        public GameObject AvailableBranch;
        public GameObject UnavailableBranch;
        public GameObject OwnedBranch;

        public void CheckStatus()
        {
            State destinationUpgradeState = destinationUpgrade.ItemStatus();

            if (destinationUpgradeState == State.Unavailable)
            {
                UnavailableBranch.SetActive(true);
            }
            else if (destinationUpgradeState == State.Available)
            {
                AvailableBranch.SetActive(true);
            }
            else
            {
                OwnedBranch.SetActive(true);
            }
        }

        void Start()
        {
            // Reset branches whenever this runs
            AvailableBranch.SetActive(false);
            UnavailableBranch.SetActive(false);
            OwnedBranch.SetActive(false);
            CheckStatus();
        }

        void Update()
        {
            CheckStatus();
        }
    }
}