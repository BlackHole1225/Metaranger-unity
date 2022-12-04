using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.FPS.UI;
using TMPro;


public class CheckBlockchainBeforeGame : MonoBehaviour
{
    [Tooltip("Reference to the Web3Manager")]
    public Web3Manager Web3Manager;

    [Tooltip("Visual Progress Bar")]
    public Image ProgressBar;

    [Tooltip("Text displaying the progress as a percentage")]
    public TMPro.TextMeshProUGUI ProgressText;

    private float totalItems = 28f;
    private float itemsCompleted = 0f;

    async void Start()
    {
        await CheckVitalityItem("Health");
        await CheckVitalityItem("Shields");
        await CheckVitalityItem("Armour");
        await CheckGameItems("BlasterContract", 5);
        await CheckGameItems("JetpackContract", 4);
        await CheckGameItems("ShotgunContract", 6);
        await CheckGameItems("DiscLauncherContract", 5);
        await CheckGameItems("SniperContract", 5);
        SceneManager.LoadScene(PlayerPrefs.GetString("ArenaSelected"));
    }

    void Update()
    {
        ProgressBar.fillAmount = itemsCompleted / totalItems;
        ProgressText.text = Math.Round(itemsCompleted / totalItems * 100, 2).ToString() + "%";
    }

    async Task CheckVitalityItem(string itemName)
    {
        float incValue = 0f;

        try
        {
            string onChainValue = await Web3Manager.getBalance(itemName);

            if (Single.TryParse(onChainValue, out float increment))
            {
                incValue = increment;
            }
            else
            {
                incValue = 0f;
            }
        }
        catch
        {
            Debug.Log("Error checking and setting onChainValue for " + itemName);
        }

        PlayerPrefs.SetFloat(itemName + "Increment", incValue);
        itemsCompleted++;
    }

    async Task CheckGameItems(string contractName, int numOfItems)
    {
        try
        {
            await Web3Manager.ownsGameItems(contractName);
        }
        catch
        {
            Debug.LogError("Error checking items on " + contractName);
        }

        itemsCompleted += numOfItems;
    }
}