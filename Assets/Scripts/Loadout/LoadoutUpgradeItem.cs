using System;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.UI
{
    // Enum to represnt the state of the button

    public enum State
    {
        Unavailable,
        Available,
        Owned,
    }

    public class LoadoutUpgradeItem : MonoBehaviour
    {
        [Header("Upgrade Name")]
        public string UpgradeName;

        [Header("Button States")]
        [Tooltip("The Unavailable State")]
        public GameObject UnavailableButton;
        [Tooltip("The Available State")]
        public GameObject AvailableButton;
        [Tooltip("The Owned State")]
        public GameObject OwnedButton;

        [Header("Prerequisite Game Items")]
        public LoadoutUpgradeItem[] prerequisites;

        public State upgradeState = State.Unavailable;

        bool CheckOwned()
        {
            // TODO
            // This is where you would check if this Game Item is owned
            return false;
        }

        // This function is called by other Game Items, to see if it is owned already
        // so you don't have to recursively make calls to the blockchain for the token
        public State ItemStatus()
        {
            return upgradeState;
        }

        public void CheckStatus()
        {
            // If the item has no prerequisites, it is Owned
            // If the user has the game item, then it is Owned
            bool result = CheckOwned();
            if (result || prerequisites.Length == 0)
            {
                upgradeState = State.Owned;
                UnavailableButton.SetActive(false);
                AvailableButton.SetActive(false);
                OwnedButton.SetActive(true);
                return;
            }

            // At this point, the user doesn't own the item
            OwnedButton.SetActive(false);

            // If the user doesn't have all the prerequisities, it is Unavailable
            foreach (LoadoutUpgradeItem prereq in prerequisites)
            {
                State prereqStatus = prereq.ItemStatus();
                if (prereqStatus == State.Unavailable || prereqStatus == State.Available)
                {
                    upgradeState = State.Unavailable;
                    AvailableButton.SetActive(false);
                    UnavailableButton.SetActive(true);
                    break;
                }

                // If the user has all the prerequisities, it is Available
                upgradeState = State.Available;
                UnavailableButton.SetActive(false);
                AvailableButton.SetActive(true);
            }
        }

        void Start()
        {
            CheckStatus();
        }

        void Update()
        {
            CheckStatus();
        }
    }


}