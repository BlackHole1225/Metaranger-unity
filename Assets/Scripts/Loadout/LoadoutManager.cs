using System;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;


namespace Unity.FPS.Game
{


    public class LoadoutManager : MonoBehaviour
    {
        public static string loadoutState = "None";
        public static string gameItemViewing = "None";

        static void Start()
        {
            // Resets the state once the player reaches the Loadout menu
            loadoutState = "None";
        }

        public static void ChangeLoadoutState(string newState)
        {
            Debug.Log("New State " + newState);
            loadoutState = newState;
        }

        public static void ChangeGameItemViewing(string currentGameItemViewed)
        {
            Debug.Log("Current Game Item Viewed " + currentGameItemViewed);
            gameItemViewing = currentGameItemViewed;
        }

    }


}