using System;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.UI
{
    public class ToggleLoadoutUI : MonoBehaviour
    {
        [Tooltip("Objects that are turned off when the action is performed")]
        public GameObject[] objectsToTurnOff;

        [Tooltip("Objects that are turned on when the action is performed")]
        public GameObject[] objectsToTurnOn;

        public void ToggleGameObjects()
        {

            foreach (GameObject gameObject in objectsToTurnOff)
            {
                gameObject.SetActive(false);
            }

            foreach (GameObject gameObject in objectsToTurnOn)
            {
                gameObject.SetActive(true);
            }

        }


    }



}