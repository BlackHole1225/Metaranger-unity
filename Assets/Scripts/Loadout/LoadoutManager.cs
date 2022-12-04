using System;
using System.Collections.Generic;
using Unity.FPS.Game;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

namespace Unity.FPS.Game
{

    public class LoadoutManager : MonoBehaviour
    {
        public string loadoutState = "None";
        public string gameItemViewing = "None";
        public string gameItemUpgradeTree = "None";

        [Header("Text")]
        [Tooltip("The main large title text to the right")]
        public GameObject MainTitleTextObject;
        public TMPro.TextMeshProUGUI MainTitleText;
        [Tooltip("The title text that appears over the menu buttons")]
        public GameObject SideTitleTextObject;
        public TMPro.TextMeshProUGUI SideTitleText;
        [Tooltip("The description text that appears underneath the SideTitle Text")]
        public GameObject DescriptionTextObject;
        public TMPro.TextMeshProUGUI DescriptionText;

        [Header("Power Up Game Items")]
        [Tooltip("The Game Items displayed when 'PowerUps' is selected")]
        public GameObject[] PowerUpItems;

        [Header("Weapon Game Items")]
        [Tooltip("The Game Items displayed when 'Weapons' is selected")]
        public GameObject[] Weapons;

        [Header("Upgrade Trees")]
        [Tooltip("All the Upgrade Item Trees")]
        public GameObject[] UpgradeTrees;
        [Tooltip("The background image for the Upgrade Trees")]
        public GameObject UpgradeTreeBackground;

        [Header("Other")]
        public GameObject LoadoutMenuButtons;
        public Web3Manager Web3Manager;


        void Start()
        {
            // Resets the state once the player reaches the Loadout menu
            loadoutState = "None";
        }

        void SetItemsActive(GameObject[] gameObjects, bool active)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.SetActive(active);
            }
        }

        async void DisplayGameItems(string state)
        {
            MainTitleTextObject.SetActive(false);
            SideTitleTextObject.SetActive(true);

            if (state == "Weapons")
            {
                SetItemsActive(PowerUpItems, false);
                SetItemsActive(Weapons, true);
                await Web3Manager.ownsGameItems("BlasterContract");
                await Web3Manager.ownsGameItems("DiscLauncherContract");
                await Web3Manager.ownsGameItems("ShotgunContract");
                await Web3Manager.ownsGameItems("SniperContract");
            }

            if (state == "PowerUps")
            {
                SetItemsActive(Weapons, false);
                SetItemsActive(PowerUpItems, true);
                await Web3Manager.ownsGameItems("JetpackContract");
            }
        }

        void DisplayUpgradeTree(string upgradeTreeName)
        {
            if (upgradeTreeName != "NoUpgradeTree")
            {
                UpgradeTreeBackground.SetActive(true);
                // Hide all the trees initially
                SetItemsActive(UpgradeTrees, false);
                foreach (GameObject upgradeTree in UpgradeTrees)
                {
                    if (upgradeTree.name == upgradeTreeName)
                    {
                        upgradeTree.SetActive(true);
                        break;
                    }
                }
            }
        }

        public void ChangeLoadoutState(string newState)
        {
            gameItemViewing = "None";
            gameItemUpgradeTree = "None";
            SideTitleText.text = "LOADOUT";
            UpgradeTreeBackground.SetActive(false);
            DescriptionTextObject.SetActive(false);
            LoadoutMenuButtons.SetActive(true);
            loadoutState = newState;
            DisplayGameItems(newState);
        }

        public void ChangeGameItemViewing(string GITitle, string GIUpgradeTree, string GIDescription)
        {
            SetItemsActive(Weapons, false);
            SetItemsActive(PowerUpItems, false);
            gameItemViewing = GITitle;
            DisplayUpgradeTree(GIUpgradeTree);
            SideTitleText.text = GITitle;
            DescriptionText.text = GIDescription;
            DescriptionTextObject.SetActive(true);
            LoadoutMenuButtons.SetActive(false);
        }

        public async Task<string> GetVitalityItemValue(string itemName)
        {
            return await Web3Manager.getBalance(itemName);
        }

        public async Task<string> GetOwnsGameItem(string contractName, string itemName)
        {
            return await Web3Manager.ownsGameItem(contractName, itemName);
        }
    }


}