using System;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;
namespace Unity.FPS.Game
{


    public class LoadoutManager : MonoBehaviour
    {

        private string loadoutState = "None";
        private string gameItemViewing = "None";

        [Header("Title Text GameObjects")]
        [Tooltip("The main large title text to the right")]
        public GameObject MainTitleText;
        [Tooltip("The title text that appears over the menu buttons")]
        public GameObject MenuTitleText;

        [Header("Power Up Game Items")]
        [Tooltip("The Game Items displayed when 'PowerUps' is selected")]
        public GameObject[] PowerUpItems;

        [Header("Weapon Game Items")]
        [Tooltip("The Game Items displayed when 'Weapons' is selected")]
        public GameObject[] Weapons;


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
            MainTitleText.SetActive(false);
            MenuTitleText.SetActive(true);

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

        public void ChangeLoadoutState(string newState)
        {
            Debug.Log("New State " + newState);
            loadoutState = newState;
            DisplayGameItems(newState);
        }

        public void ChangeGameItemViewing(string currentGameItemViewed)
        {
            Debug.Log("Current Game Item Viewed " + currentGameItemViewed);
            gameItemViewing = currentGameItemViewed;
        }



    }


}