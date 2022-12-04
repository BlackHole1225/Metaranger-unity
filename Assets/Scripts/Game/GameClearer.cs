using System;
using UnityEngine;

public class GameClearer : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (!PlayerPrefs.HasKey("AlreadyCleared"))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetString("AlreadyCleared", "true");
        }

    }
}