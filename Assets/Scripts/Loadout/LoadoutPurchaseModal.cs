using System;
using System.Collections.Generic;
using Unity.FPS.Game;
using Unity.FPS.UI;
using UnityEngine;
using TMPro;

namespace Unity.FPS.UI
{
    public class LoadoutPurchaseModal : MonoBehaviour
    {
        [Tooltip("The Title Label, which should hold the name of the Game Item being purchased")]
        public TMPro.TextMeshProUGUI TitleLabel;

        [Tooltip("The Modal Image, which should hold an image of the Game Item being purchased")]
        public Sprite GameItemImage;

        [Tooltip("The price of the Game item being purchased")]
        public TMPro.TextMeshProUGUI PriceLabel;

        [Tooltip("Refernce to itself")]
        public GameObject thisModal;

        void CompletePurchase()
        {
            // This is where you would make the blockchain transaction to purchase the item
        }

        // This function will populate the modal and show it
        void PurchaseGameItem(string GIName, Sprite GIImage, int GIPrice)
        {
            TitleLabel.text = GIName;
            GameItemImage = GIImage;
            PriceLabel.text = GIPrice.ToString();
            thisModal.SetActive(true);
        }
    }
}