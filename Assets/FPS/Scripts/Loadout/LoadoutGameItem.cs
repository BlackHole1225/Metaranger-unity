using System;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;
using TMPro;


namespace Unity.FPS.UI
{
    public class LoadoutGameItem : MonoBehaviour
    {
        [Tooltip("The name of the Game Item")]
        public string GameItemTitle;

        [Tooltip("The text component that visually displays the name of the Game Item")]
        public TMPro.TextMeshProUGUI GameItemText;

        [Tooltip("Determines if the Game Item is unlocked by default or not")]
        public bool UnlockedByDefault;

        [Tooltip("The current value of the Game Item, if this item has a value")]
        public int CurrentValue;

        [Tooltip("The text component associated with the Current Value of this Game Item")]
        public TMPro.TextMeshProUGUI CurrentValueText;

        [Tooltip("The gameObject that represents the Current Value of this Game Item")]
        public GameObject CurrentValueObject;

        [Tooltip("The price of the Game Item in METR")]
        public int Price;

        [Tooltip("The text component associated with the Price of this Game Item")]
        public TMPro.TextMeshProUGUI PriceText;

        [Tooltip("The gameObject that represents the text price and the METR Icon")]
        public GameObject PriceObject;

        [Tooltip("The list of UpgradeIndicators, if the Game Item has them")]
        public GameObject[] UpgradeIndicators;

        [Tooltip("The Image that is displayed if the Game Item has been purchased")]
        public GameObject PurchasedImage;

        [Tooltip("The Image that is displayed if the Game Item has not been purchased")]
        public GameObject UnavailableImage;

        // Internal reference as to whether this Game Item has been unlocked or not
        bool Unlocked;

        void Start()
        {
            CheckItemStatuses();
        }

        // TODO Determine if this is needed
        // If this runs every frame, it might consume computation and make the game slow
        static void Update()
        {
        }

        public void SetGameItemViewing()
        {
            LoadoutManager.ChangeGameItemViewing(GameItemTitle);
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

        void ActivateIndicators(bool whichOne)
        {
            foreach (GameObject indicator in UpgradeIndicators)
            {
                if (whichOne) // True means we are showing the indicators
                {
                    bool result = CheckIfUnlocked(indicator.name);
                    if (result)
                    {
                        indicator.SetActive(whichOne);
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
            // TODO Implement this when smart contracts uploaded
            // CheckIfUnlocked();

            if (!Unlocked)
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

                if (UnlockedByDefault)
                {
                    CurrentValueText.text = CurrentValue.ToString();
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