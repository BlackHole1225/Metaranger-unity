using System;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WalletConnection : MonoBehaviour
{
    [Header("Connect Button")]
    [Tooltip("The Connect Button GameObject")]
    public GameObject connectButton;

    [Header("METR Balance Items")]
    [Tooltip("The METR Balance GameObject")]
    public GameObject metrBalanceObject;

    [Tooltip("The METR Balance text")]
    public TMPro.TextMeshProUGUI metrBalanceText;

    [Header("Web3")]
    [Tooltip("Web3 Manager instance")]
    public Web3Manager Web3Manager;

    private bool balanceAttained = false;

    void Start()
    {
        connectButton.SetActive(true);
        metrBalanceObject.SetActive(false);
    }

    async void Update()
    {
        if (PlayerPrefs.GetString("Account") != "" && !balanceAttained)
        {
            await Web3Manager.getMETRBalance();
            connectButton.SetActive(false);
            metrBalanceObject.SetActive(true);
            metrBalanceText.text = Web3Manager.METRBalance.ToString();
            balanceAttained = true;

        }
    }
}

