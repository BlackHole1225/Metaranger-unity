using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.UI;


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

        [Tooltip("Reference to the LoadoutManager")]
        public Unity.FPS.Game.LoadoutManager LOM;

        [Header("Button States")]
        [Tooltip("The Unavailable State")]
        public GameObject UnavailableButton;
        [Tooltip("The Available State")]
        public GameObject AvailableButton;
        [Tooltip("The Owned State")]
        public GameObject OwnedButton;

        [Header("Prerequisite Game Items")]
        public LoadoutUpgradeItem[] prerequisites;

        [Tooltip("The price of this upgrade")]
        public int price;

        [Tooltip("Reference to the LoadoutPurchaseModal")]
        public LoadoutPurchaseModal LPM;

        [Tooltip("Reference to this Game Item's image")]
        public Sprite UpgradeItemImage;

        [Tooltip("Images's width when displayed on the Modal")]
        public float imgWidth;
        [Tooltip("Image's height when displayed on the Modal")]
        public float imgHeight;

        public State upgradeState = State.Unavailable;

        string DetermineGameItemContract()
        {
            if (UpgradeName.Contains("Blaster")) return "BlasterContract";
            if (UpgradeName.Contains("DiscLauncher")) return "DiscLauncherContract";
            if (UpgradeName.Contains("Shotgun")) return "ShotgunContract";
            if (UpgradeName.Contains("Jetpack")) return "JetpackContract";
            if (UpgradeName.Contains("Sniper")) return "SniperContract";
            return null;
        }

        async Task<bool> CheckOwned()
        {
            if (UpgradeName == "BlasterBase") return true;
            string contractName = DetermineGameItemContract();
            string result = await LOM.GetOwnsGameItem(contractName, UpgradeName);
            if (Boolean.TryParse(result, out bool ownsItem))
            {
                return bool.Parse(result);
            }
            else
            {
                return false;
            }
        }

        // This function is called by other Game Items, to see if it is owned already
        // so you don't have to recursively make calls to the blockchain for the token
        public State ItemStatus()
        {
            return upgradeState;
        }

        public async void CheckStatus()
        {
            // If the item has no prerequisites, it is Owned
            // If the user has the game item, then it is Owned
            bool result = await CheckOwned();


            if (prerequisites.Length == 0 || (prerequisites.Length != 0 && result))
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

        public void PurchaseGameItem()
        {
            if (upgradeState == State.Available)
            {
                LPM.PurchaseGameItem(UpgradeName, price, UpgradeItemImage, imgWidth, imgHeight);
            }
        }



        void Start()
        {
            CheckStatus();
        }
    }


}