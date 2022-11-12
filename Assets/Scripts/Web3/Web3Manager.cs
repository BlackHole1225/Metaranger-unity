using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Numerics;
using UnityEngine;
using Newtonsoft.Json;

public class Web3Manager : MonoBehaviour
{
    // All contracts will share these aspects
    string chain = "polygon";
    string network = "mumbai";

    // TODO Find out what this is, and possibly store it as an env variable
    private string digitalKey = "digitalKey";
    string playerAddress = PlayerPrefs.GetString("Account");

    // Game Manger Contract
    string gameManagerAddress = "GameManagerAddress";
    string gameManagerABI = "GameManagerABI";

    // METR Contract
    string METRAddress = "METRContractAddress";

    // The different contracts in the game
    // TODO Populate these with the actual values
    Dictionary<string, GameContract> gameContracts = new Dictionary<string, GameContract>(){
       {"BlasterContract",new GameContract("BlasterContractAddress", "BlasterContractABI") },
       {"DiscLauncherContract",new GameContract("DiscLauncherContractAddress", "DiscLauncherContractABI") },
       {"ShotgunContract",new GameContract("ShotgunContractAddress", "ShotgunContractABI") },
       {"JetpackContract",new GameContract("JetpackContractAddress", "JetpackContractABI") },
       {"VitalityItemsContract",new GameContract("VitalityItemsContractAddress", "VitalityItemsContractABI") },
    };

    // GAME MANAGER FUNCTIONS
    public async void mintMETR(int amount)
    {
        string[] obj = { playerAddress, amount.ToString(), digitalKey };
        string args = JsonConvert.SerializeObject(obj);
        string response = await EVM.Call(chain, network, gameManagerAddress, gameManagerABI, "mintMETR", args);
        Debug.Log("Response from mintMETR in Web3Manager " + response);
    }

    public async void purchaseGameItem(string contractName, string itemName)
    {
        string[] obj = { playerAddress, contractName, itemName, digitalKey };
        string args = JsonConvert.SerializeObject(obj);
        string response = await EVM.Call(chain, network, gameManagerAddress, gameManagerABI, "purchaseGameItem", args);
        Debug.Log("Response from purchaseGameItem in Web3Manager " + response);
    }

    public async void purchaseVitalityItem(string itemName)
    {
        string[] obj = { playerAddress, itemName, digitalKey };
        string args = JsonConvert.SerializeObject(obj);
        string response = await EVM.Call(chain, network, gameManagerAddress, gameManagerABI, "purchaseVitalityItem", args);
        Debug.Log("Response from purchaseVitalityItem in Web3Manager " + response);
    }

    // GAME ITEM FUNCTIONS

    // TODO Find out how to get the price out of the string response
    public async void getPrice(string contractName, string itemName)
    {
        GameContract gameContract = gameContracts[contractName];
        string[] obj = { itemName };
        string args = JsonConvert.SerializeObject(obj);
        string response = await EVM.Call(chain, network, gameContract.Address, gameContract.ABI, "getPrice", args);
        Debug.Log("Response from getPrice in Web3Manager " + response);
    }

    // TODO Find out how to get the result out of the string response
    public async void ownsGameItem(string contractName, string itemName)
    {
        GameContract gameContract = gameContracts[contractName];
        string[] obj = { playerAddress, itemName };
        string args = JsonConvert.SerializeObject(obj);
        string response = await EVM.Call(chain, network, gameContract.Address, gameContract.ABI, "ownsGameItem", args);
        Debug.Log("Response from ownsGameItem in Web3Manager " + response);
    }

    // VITALITY ITEM FUNCTIONS
    // TODO Find out how to get the price out of the string response
    public async void getBalance(string itemName)
    {
        string[] obj = { playerAddress, itemName };
        string args = JsonConvert.SerializeObject(obj);
        string response = await EVM.Call(chain, network, gameContracts["VitalityItemContract"].Address, gameContracts["VitalityItemContract"].ABI, "getBalance", args);
        Debug.Log("Response from getBalance in Web3Manager " + response);
    }

    // METR FUNCTIONS
    public async Task<BigInteger> getMETRBalance()
    {
        BigInteger balance = await ERC20.BalanceOf(chain, network, METRAddress, playerAddress);
        return balance;
    }



}