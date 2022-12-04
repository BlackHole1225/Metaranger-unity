using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Unity.FPS.Game;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class WalletConnection : MonoBehaviour
{
    [Header("Connection Buttons")]
    [Tooltip("The Connect Button GameObject")]
    public GameObject connectButton;

    [Tooltip("The Disconnect Button GameObject")]
    public GameObject disconnectButton;

    [Header("METR Balance Items")]
    [Tooltip("The METR Balance GameObject")]
    public GameObject metrBalanceObject;

    [Tooltip("The METR Balance text")]
    public TMPro.TextMeshProUGUI metrBalanceText;

    [Header("Web3")]
    [Tooltip("Web3 Manager instance")]
    public Web3Manager Web3Manager;

    private bool balanceAttained = false;

    async void Start()
    {
        connectButton.SetActive(true);
        metrBalanceObject.SetActive(false);
        disconnectButton.SetActive(false);
        if (!balanceAttained)
        {
            await CheckBalance();
        }
    }

    public async Task CheckBalance()
    {

        while (PlayerPrefs.GetString("Account") == "" || PlayerPrefs.GetString("Account") == null)
        {
            await new WaitForSeconds(1f);
        }

        connectButton.SetActive(false);
        metrBalanceObject.SetActive(true);
        disconnectButton.SetActive(true);

        metrBalanceText.text = "Retrieving Balance";
        await Web3Manager.getMETRBalance();
        metrBalanceText.text = PlayerPrefs.GetInt("METRBalance").ToString();
        balanceAttained = true;
    }

    public void Logout()
    {
        PlayerPrefs.SetString("Account", "");
        connectButton.SetActive(true);
        metrBalanceObject.SetActive(false);
        disconnectButton.SetActive(false);
    }

}

