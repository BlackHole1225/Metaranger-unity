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
        await CheckVitalityItems();
        await CheckBlasterItems();
        await CheckJetpackItems();
        await CheckShotgunItems();
        await CheckDiscLauncherItems();
        await CheckSniperItems();
        SceneManager.LoadScene(PlayerPrefs.GetString("ArenaSelected"));
    }

    void Update()
    {
        ProgressBar.fillAmount = itemsCompleted / totalItems;
        ProgressText.text = Math.Round(itemsCompleted / totalItems * 100, 2).ToString() + "%";
    }

    async Task CheckVitalityItems()
    {
        Debug.Log("Started checking the VitalityItems");

        // Health
        Debug.Log("Checking health");
        string onChainHealth = await Web3Manager.getBalance("Health");
        Single.TryParse(onChainHealth, out float healthIncrement);
        PlayerPrefs.SetFloat("HealthIncrement", healthIncrement);
        itemsCompleted++;
        Debug.Log("itemsCompleted / totalItems " + (itemsCompleted / totalItems).ToString());


        // Shields 
        Debug.Log("Checking shields");
        string onChainShields = await Web3Manager.getBalance("Shields");
        Single.TryParse(onChainShields, out float shieldsIncrement);
        PlayerPrefs.SetFloat("ShieldsIncrement", shieldsIncrement);
        itemsCompleted++;
        Debug.Log("itemsCompleted / totalItems " + (itemsCompleted / totalItems).ToString());


        // Armour
        Debug.Log("Checking armour");
        string onChainArmour = await Web3Manager.getBalance("Armour");
        Single.TryParse(onChainArmour, out float armourIncrement);
        PlayerPrefs.SetFloat("ArmourIncrement", armourIncrement);
        itemsCompleted++;
        Debug.Log("itemsCompleted / totalItems " + (itemsCompleted / totalItems).ToString());

    }

    async Task CheckBlasterItems()
    {
        Debug.Log("Started checking the BlasterItems");

        string[] blasterTokens = { "BlasterAccuracy", "BlasterStoppingPower", "BlasterRapidFire", "BlasterAimSpeed", "BlasterCooldown" };

        await Web3Manager.ownsGameItems("BlasterContract");
        itemsCompleted += 5;

        // foreach (string token in blasterTokens)
        // {
        //     Debug.Log("Checking " + token);
        //     string onChainValue = await Web3Manager.ownsGameItem("BlasterContract", token);
        //     Debug.Log("onChainValue for " + token + ": " + onChainValue);
        //     if (Boolean.TryParse(onChainValue, out bool result))
        //     {
        //         PlayerPrefs.SetString(token, onChainValue);
        //     }
        //     else
        //     {
        //         continue;
        //     }
        //     itemsCompleted++;
        //     Debug.Log("itemsCompleted / totalItems " + (itemsCompleted / totalItems).ToString());

        // }
    }

    async Task CheckJetpackItems()
    {
        Debug.Log("Started checking the JetpackItems");

        string[] jetpackTokens = { "JetpackBase", "JetpackFlightSpeed", "JetpackDuration", "JetpackCooldown" };

        await Web3Manager.ownsGameItems("JetpackContract");
        itemsCompleted += 4;

        // foreach (string token in jetpackTokens)
        // {
        //     Debug.Log("Checking " + token);
        //     string onChainValue = await Web3Manager.ownsGameItem("JetpackContract", token);
        //     Debug.Log("onChainValue for " + token + ": " + onChainValue);
        //     if (Boolean.TryParse(onChainValue, out bool result))
        //     {
        //         PlayerPrefs.SetString(token, onChainValue);
        //     }
        //     else
        //     {
        //         continue;
        //     }
        //     // If there is no base token, they won't have any of the others
        //     if (token == "JetpackBase" && onChainValue == "false")
        //     {
        //         itemsCompleted += 4;
        //         Debug.Log("itemsCompleted / totalItems " + (itemsCompleted / totalItems).ToString());

        //         break;
        //     };

        //     itemsCompleted++;
        //     Debug.Log("itemsCompleted / totalItems " + (itemsCompleted / totalItems).ToString());

        // }
    }

    async Task CheckShotgunItems()
    {
        Debug.Log("Started checking the ShotgunItems");

        string[] shotgunTokens = { "ShotgunBase", "ShotgunSpreadshot", "ShotgunCooldown", "ShotgunStoppingPower", "ShotgunExtraBarrel", "ShotgunAimSpeed" };

        await Web3Manager.ownsGameItems("ShotgunContract");
        itemsCompleted += 6;

        // foreach (string token in shotgunTokens)
        // {
        //     Debug.Log("Checking " + token);
        //     string onChainValue = await Web3Manager.ownsGameItem("ShotgunContract", token);
        //     Debug.Log("onChainValue for " + token + ": " + onChainValue);
        //     if (Boolean.TryParse(onChainValue, out bool result))
        //     {
        //         PlayerPrefs.SetString(token, onChainValue);
        //     }
        //     else
        //     {
        //         continue;
        //     }
        //     // If there is no base token, they won't have any of the others
        //     if (token == "ShotgunBase" && onChainValue == "false")
        //     {
        //         itemsCompleted += 6;
        //         Debug.Log("itemsCompleted / totalItems " + (itemsCompleted / totalItems).ToString());

        //         break;
        //     }

        //     itemsCompleted++;
        //     Debug.Log("itemsCompleted / totalItems " + (itemsCompleted / totalItems).ToString());

        // }
    }

    async Task CheckDiscLauncherItems()
    {
        Debug.Log("Started checking the DiscLauncherItems");

        string[] discLauncherTokens = { "DiscLauncherBase", "DiscLauncherAimSpeed", "DiscLauncherChargeSpeed", "DiscLauncherStoppingPower", "DiscLauncherCooldown" };

        await Web3Manager.ownsGameItems("DiscLauncherContract");
        itemsCompleted += 5;

        // foreach (string token in discLauncherTokens)
        // {
        //     Debug.Log("Checking " + token);
        //     string onChainValue = await Web3Manager.ownsGameItem("DiscLauncherContract", token);
        //     Debug.Log("onChainValue for " + token + ": " + onChainValue);
        //     if (Boolean.TryParse(onChainValue, out bool result))
        //     {
        //         PlayerPrefs.SetString(token, onChainValue);
        //     }
        //     else
        //     {
        //         continue;
        //     }
        //     // If there is no base token, they won't have any of the others
        //     if (token == "DiscLauncherBase" && onChainValue == "false")
        //     {
        //         itemsCompleted += 5;
        //         Debug.Log("itemsCompleted / totalItems " + (itemsCompleted / totalItems).ToString());

        //         break;
        //     }

        //     itemsCompleted++;
        //     Debug.Log("itemsCompleted / totalItems " + (itemsCompleted / totalItems).ToString());

        // }
    }

    async Task CheckSniperItems()
    {
        Debug.Log("Started checking the Sniper Items");

        string[] sniperTokens = { "SniperBase", "SniperAimSpeed", "SniperCooldown", "SniperStoppingPower", "SniperZoom" };

        await Web3Manager.ownsGameItems("SniperContract");
        itemsCompleted += 5;

        // foreach (string token in sniperTokens)
        // {
        //     Debug.Log("Checking " + token);
        //     string onChainValue = await Web3Manager.ownsGameItem("SniperContract", token);
        //     Debug.Log("onChainValue for " + token + ": " + onChainValue);
        //     if (Boolean.TryParse(onChainValue, out bool result))
        //     {
        //         PlayerPrefs.SetString(token, onChainValue);
        //     }
        //     else
        //     {
        //         continue;
        //     }
        //     // If there is no base token, they won't have any of the others
        //     if (token == "SniperBase" && onChainValue == "false")
        //     {
        //         itemsCompleted += 5;
        //         Debug.Log("itemsCompleted / totalItems " + (itemsCompleted / totalItems).ToString());

        //         break;
        //     }

        //     itemsCompleted++;
        //     Debug.Log("itemsCompleted / totalItems " + (itemsCompleted / totalItems).ToString());

        // }
    }
}