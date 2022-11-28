using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        [Tooltip("Reference to itself")]
        public GameObject thisModal;

        [Tooltip("Reference to the Web3Manager")]
        public Web3Manager Web3Manager;

        [Tooltip("Reference to the LoadoutManager")]
        public LoadoutManager LoadoutManager;

        private string GameItemName;

        public async void CompletePurchase()
        {

            Debug.Log("GameItemName " + GameItemName);

            if (GameItemName.Contains("Health") || GameItemName.Contains("Shields") || GameItemName.Contains("Armour"))
            {
                Debug.Log("This is a vitality item");
                await Web3Manager.purchaseVitalityItem(GameItemName);
                await LoadoutManager.GetVitalityItemValue(GameItemName);
            }
            else
            {
                Debug.Log("This is a normal game item");
                await Web3Manager.purchaseGameItem(GameItemName);
            }

            thisModal.SetActive(false);
        }

        // This function will populate the modal and show it
        public void PurchaseGameItem(string GIName, int GIPrice, Sprite GIImage, float imgWidth, float imgHeight)
        {
            TitleLabel.text = GIName;
            GameItemName = GIName;
            GameItemImage.sprite = GIImage;
            GameItemImage.rectTransform.sizeDelta = new Vector2(imgWidth, imgHeight);
            PriceLabel.text = GIPrice.ToString();
            thisModal.SetActive(true);
            Debug.Log("GameItemName " + GIName);
        }
    }
}