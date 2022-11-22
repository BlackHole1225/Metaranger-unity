using System;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;
using TMPro;


namespace Unity.FPS.UI
{
    public class LoadoutGameItem : MonoBehaviour
    {
        [Header("Basic Game Item Variables")]
        [Tooltip("The title of the Game Item")]
        public string GameItemTitle;

        [Tooltip("The name of the Game Item, to be interpreted by the smart contracts")]
        public string GameItemName;

        [Tooltip("The upgrade tree associated with this Game Item")]
        public string UpgradeTreeName;

        [Tooltip("The description for this Game Item")]
        public string GameItemDescription;

        [Tooltip("The text component that visually displays the name of the Game Item")]
        public TMPro.TextMeshProUGUI GameItemText;

        [Tooltip("Reference to the LoadoutManager")]
        public Unity.FPS.Game.LoadoutManager loadoutManager;

        [Tooltip("Reference to the Game Item Purchase Modal")]
        public LoadoutPurchaseModal gameItemPurchaseModal;

        [Tooltip("Determines if the Game Item is unlocked by default or not")]
        public bool UnlockedByDefault;

        [Tooltip("'Upgrade' text displayed when the item is purchased, but not any upgrades")]
        public GameObject UpgradeTextObject;

        [Header("Current Value Variables")]
        [Tooltip("The current value of the Game Item, if this item has a value")]
        public string CurrentValue;

        [Tooltip("The text component associated with the Current Value of this Game Item")]
        public TMPro.TextMeshProUGUI CurrentValueText;

        [Tooltip("The gameObject that represents the Current Value of this Game Item")]
        public GameObject CurrentValueObject;

        [Header("Price Variables")]
        [Tooltip("The price of the Game Item in METR")]
        public int Price;

        [Tooltip("The text component associated with the Price of this Game Item")]
        public TMPro.TextMeshProUGUI PriceText;

        [Tooltip("The gameObject that represents the text price and the METR Icon")]
        public GameObject PriceObject;

        [Header("Game Item Images and Indicators")]
        [Tooltip("The Image that is displayed if the Game Item has been purchased")]
        public GameObject PurchasedImage;
        [Tooltip("The Image that is displayed if the Game Item has not been purchased")]
        public GameObject UnavailableImage;
        [Tooltip("The list of UpgradeIndicators, if the Game Item has them")]
        public GameObject[] UpgradeIndicators;
        [Tooltip("Reference to this Game Item's image")]
        public Sprite GameItemImage;
        [Tooltip("Images's width when displayed on the Modal")]
        public float imgWidth;
        [Tooltip("Image's height when displayed on the Modal")]
        public float imgHeight;

        // Internal reference as to whether this Game Item has been unlocked or not
        bool Unlocked = false;

        void Start()
        {
            GameItemText.text = GameItemTitle;
            if (UnlockedByDefault) Unlocked = true;
            CheckItemStatuses();

            if (UnlockedByDefault && GameItemTitle != "BLASTER")
            {
                GetVitalityItemValue();
            }
        }

        // TODO Determine if this is needed
        // If this runs every frame, it might consume computation and make the game slow
        void Update()
        {
            // CheckItemStatuses();
        }

        public void SelectGameItem()
        {


            if (!Unlocked || UpgradeTreeName == "NoUpgradeTree")
            {
                gameItemPurchaseModal.PurchaseGameItem(GameItemName, Price, GameItemImage, imgWidth, imgHeight);
            }
            else
            {
                loadoutManager.ChangeGameItemViewing(GameItemTitle, UpgradeTreeName, GameItemDescription);
            }
        }

        // TODO Implement this when the smart contracts have been deployed
        bool CheckIfUnlocked(string gameItem)
        {
            /*
            * This will read the player's balance of the specific Game Token
            * and set Unlocked to either true or false based on this
            */

            return true;
        }

        public async void GetVitalityItemValue()
        {
            string onChainValue = await loadoutManager.GetVitalityItemValue(GameItemName);
            Debug.Log(GameItemName + " onChainValue in LoadoutGameItem " + onChainValue);
            if (onChainValue != "0")
            {
                Debug.Log("New Current Value for " + GameItemName + " " + (Int32.Parse(CurrentValue) + Int32.Parse(onChainValue)).ToString());
                CurrentValue = (Int32.Parse(CurrentValue) + Int32.Parse(onChainValue)).ToString();
                CurrentValueText.text = CurrentValue.ToString();

            }
        }

        void ActivateIndicators(bool whichOne)
        {
            // foreach (GameObject indicator in UpgradeIndicators)
            // {
            //     if (whichOne) // True means we are showing the indicators
            //     {
            //         bool result = CheckIfUnlocked(indicator.name);
            //         if (result)
            //         {
            //             UpgradeTextObject.SetActive(false);
            //             indicator.SetActive(whichOne);
            //         }
            //     }
            //     else
            //     {
            //         indicator.SetActive(whichOne);
            //     }
            // }
        }

        // TODO Decide if this should be called whenever a purchased has been made or something?
        public void CheckItemStatuses()
        {
            // Check if this item has been unlocked
            // TODO Implement this when smart contracts deployed
            // CheckIfUnlocked();

            if (Unlocked == false)
            {
                ActivateIndicators(false);
                PurchasedImage.SetActive(false);
                UnavailableImage.SetActive(true);
                PriceText.text = Price.ToString();
                PriceObject.SetActive(true);
            }
            else
            {
                PurchasedImage.SetActive(true);
                UnavailableImage.SetActive(false);
                PriceObject.SetActive(false);
                UpgradeTextObject.SetActive(true);

                // If this is a Vitality Item
                if (UnlockedByDefault && GameItemTitle != "BLASTER")
                {
                    UpgradeTextObject.SetActive(false);
                    CurrentValueText.text = CurrentValue.ToString();
                    Debug.Log("\n\tCurrentValue " + CurrentValue.ToString());
                    CurrentValueObject.SetActive(true);
                }
                else
                {
                    ActivateIndicators(true);
                }
            }
        }



    }

}