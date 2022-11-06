using System;
using System.Collections.Generic;
using Unity.FPS.Game;
using Unity.FPS.UI;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Unity.FPS.UI
{
    public class LoadoutPurchaseModal : MonoBehaviour
    {
        [Tooltip("The Title Label, which should hold the name of the Game Item being purchased")]
        public TMPro.TextMeshProUGUI TitleLabel;

        [Tooltip("The Modal Image, which should hold an image of the Game Item being purchased")]
        public Image GameItemImage;

        [Tooltip("The price of the Game item being purchased")]
        public TMPro.TextMeshProUGUI PriceLabel;

        [Tooltip("Refernce to itself")]
        public GameObject thisModal;

        void CompletePurchase()
        {
            // This is where you would make the blockchain transaction to purchase the item
            Debug.Log("Let's purchase the Game Item");
            thisModal.SetActive(false);
        }

        // This function will populate the modal and show it
        public void PurchaseGameItem(string GIName, int GIPrice, Sprite GIImage, float imgWidth, float imgHeight)
        {
            TitleLabel.text = GIName;
            GameItemImage.sprite = GIImage;
            GameItemImage.rectTransform.sizeDelta = new Vector2(imgWidth, imgHeight);
            PriceLabel.text = GIPrice.ToString();
            thisModal.SetActive(true);
        }
    }
}