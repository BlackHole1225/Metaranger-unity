using System;
using System.Collections.Generic;
using Unity.FPS.Game;
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

        void DisplayGameItems(string state)
        {
            MainTitleTextObject.SetActive(false);
            SideTitleTextObject.SetActive(true);

            if (state == "Weapons")
            {
                SetItemsActive(PowerUpItems, false);
                SetItemsActive(Weapons, true);
            }

            if (state == "PowerUps")
            {
                SetItemsActive(Weapons, false);
                SetItemsActive(PowerUpItems, true);
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
            else
            {
                // This is where you would show the purchase modal for the non-upgradable items
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

        public string GetVitalityItemValue(string itemName)
        {
            Web3Manager.getBalance(itemName);
            return "1000Test";
        }

        // public bool CheckIfUnlocked(string contractName, string itemName, bool vitalityItem)
        // {
        //     if(vitalityItem)
        //     {
        //         return Web3Manager.
        //     }
        //     return Web3Manager.Chec
        // }
    }


}