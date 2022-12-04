using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        [Tooltip("The contract associated with this GameItem")]
        public string ContractName;

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

        [Tooltip("The UpgradeIndicator GameObject itself")]
        public GameObject UpgradeIndicatorsObject;

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

        async Task Start()
        {
            GameItemText.text = GameItemTitle;
            if (UnlockedByDefault) Unlocked = true;
            CheckItemStatuses();

            if (UnlockedByDefault && GameItemTitle != "BLASTER")
            {
                await GetVitalityItemValue();
            }
        }

        // TODO Determine if this is needed
        // If this runs every frame, it might consume computation and make the game slow
        void Update()
        {
            CheckItemStatuses();
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

        bool CheckIfUnlocked(string gameItem)
        {

            string owns = PlayerPrefs.GetString(gameItem + "Owned");

            if (Boolean.TryParse(owns, out bool ownsItem))
            {
                return ownsItem;
            }
            else
            {
                return false;
            }
        }

        public async Task GetVitalityItemValue()
        {
            string onChainValue;

            if (PlayerPrefs.GetString(GameItemName) == "")
            {
                onChainValue = await loadoutManager.GetVitalityItemValue(GameItemName);
            }
            else
            {
                onChainValue = PlayerPrefs.GetString(GameItemName);
            }

            if (onChainValue != "0")
            {
                int onChainIncrement = Int32.Parse(onChainValue) * 10;
                CurrentValue = (Int32.Parse(CurrentValue) + onChainIncrement).ToString();
                CurrentValueText.text = CurrentValue.ToString();
            }
        }

        void ActivateIndicators(bool whichOne)
        {
            foreach (GameObject indicator in UpgradeIndicators)
            {
                if (whichOne) // True means we are showing the indicators
                {

                    bool result = CheckIfUnlocked(indicator.name);


                    if (result)
                    {
                        UpgradeTextObject.SetActive(false);
                        UpgradeIndicatorsObject.SetActive(true);
                        indicator.SetActive(true);
                    }
                }
                else
                {
                    indicator.SetActive(whichOne);
                }
            }
        }

        // TODO Decide if this should be called whenever a purchased has been made or something?
        public void CheckItemStatuses()
        {
            // Check if this item has been unlocked
            if (!UnlockedByDefault)
            {
                Unlocked = CheckIfUnlocked(GameItemName);
            }


            if (!Unlocked)
            {
                ActivateIndicators(false);
                ShowPurchased(false);
                PriceText.text = Price.ToString();
                PriceObject.SetActive(true);
            }
            else
            {
                ShowPurchased(true);
                PriceObject.SetActive(false);
                UpgradeTextObject.SetActive(true);

                // If this is a Vitality Item
                if (UnlockedByDefault && GameItemTitle != "BLASTER")
                {
                    UpgradeTextObject.SetActive(false);
                    CurrentValueText.text = CurrentValue.ToString();
                    CurrentValueObject.SetActive(true);
                }
                else
                {
                    ActivateIndicators(true);
                }
            }
        }

        void ShowPurchased(bool which)
        {
            PurchasedImage.SetActive(which);
            UnavailableImage.SetActive(!which);
        }



    }

}