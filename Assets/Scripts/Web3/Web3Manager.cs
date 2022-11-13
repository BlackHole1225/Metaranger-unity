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

    private string digitalKey = "2cd347f69a4cbb6545677cf5b3f50019370cdb858579315d08f15b23e89f4b15e4773d3eda46f393c98e57ef179babcd096c415301955bf043faa30c058807cbe233290dc1f69d9e77e5b5e222a27ca681b50d548b639875ae74844ee338cc567d0ce4b2e9f79fc19656fc601d23ff0180a0dda2d8a961bb9b378fa36b49e4d10fde93c8927a2a94be7ef4d41cff87878d8a104ade3d38a9c82e66148214568f27f4e995907407e10b271409cba8daf1f5be1c93929f38d3a8da3df97eab90d909482986edb05eec";
    // string playerAddress = PlayerPrefs.GetString("Account");
    string playerAddress;

    // Game Manger Contract
    string gameManagerAddress = "0xe9BE279252f3cb9FE70A8B146955F04Ad2957f90";
    string gameManagerABI = "[ { \"inputs\": [ { \"internalType\": \"string\", \"name\": \"digitalKey\", \"type\": \"string\" }, { \"internalType\": \"address\", \"name\": \"metrContractAddress\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"vitalityContractAddress\", \"type\": \"address\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"constructor\" }, { \"inputs\": [], \"name\": \"ContractDoesntExist\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"GameItemAlreadyExists\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"InadequateMETR\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"InadequatePrice\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"InvalidDigitalKey\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"InvalidRole\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"ItemDoesntExist\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"NoItemsToInitialise\", \"type\": \"error\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"contractAddress\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"string\", \"name\": \"indexedContractType\", \"type\": \"string\" }, { \"indexed\": false, \"internalType\": \"string\", \"name\": \"contractType\", \"type\": \"string\" } ], \"name\": \"ContractDeployed\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"string\", \"name\": \"indexedItemName\", \"type\": \"string\" }, { \"indexed\": false, \"internalType\": \"string\", \"name\": \"itemName\", \"type\": \"string\" } ], \"name\": \"GameItemPurchased\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"string\", \"name\": \"indexedItem\", \"type\": \"string\" }, { \"indexed\": false, \"internalType\": \"string\", \"name\": \"item\", \"type\": \"string\" } ], \"name\": \"ItemMinted\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"uint256\", \"name\": \"amount\", \"type\": \"uint256\" } ], \"name\": \"METREarned\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"string\", \"name\": \"indexedItemName\", \"type\": \"string\" }, { \"indexed\": false, \"internalType\": \"string\", \"name\": \"itemName\", \"type\": \"string\" } ], \"name\": \"VitalityItemPurchased\", \"type\": \"event\" }, { \"inputs\": [ { \"components\": [ { \"internalType\": \"string\", \"name\": \"contractName\", \"type\": \"string\" }, { \"components\": [ { \"internalType\": \"string\", \"name\": \"itemName\", \"type\": \"string\" }, { \"internalType\": \"string[]\", \"name\": \"prereqs\", \"type\": \"string[]\" }, { \"internalType\": \"uint256\", \"name\": \"price\", \"type\": \"uint256\" }, { \"internalType\": \"bool\", \"name\": \"exists\", \"type\": \"bool\" } ], \"internalType\": \"struct IGameItemCommon.ItemInitialiser[]\", \"name\": \"gameItems\", \"type\": \"tuple[]\" } ], \"internalType\": \"struct IGameManager.GameItemDetails\", \"name\": \"newGameItem\", \"type\": \"tuple\" }, { \"internalType\": \"string\", \"name\": \"digitalKey\", \"type\": \"string\" } ], \"name\": \"createGameItem\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"string\", \"name\": \"\", \"type\": \"string\" } ], \"name\": \"gameItemContracts\", \"outputs\": [ { \"internalType\": \"address\", \"name\": \"contractAddress\", \"type\": \"address\" }, { \"internalType\": \"bool\", \"name\": \"exists\", \"type\": \"bool\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"gameToken\", \"outputs\": [ { \"internalType\": \"contract METRToken\", \"name\": \"\", \"type\": \"address\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"amount\", \"type\": \"uint256\" }, { \"internalType\": \"string\", \"name\": \"digitalKey\", \"type\": \"string\" } ], \"name\": \"mintMETR\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"internalType\": \"string\", \"name\": \"contractName\", \"type\": \"string\" }, { \"internalType\": \"string\", \"name\": \"itemName\", \"type\": \"string\" }, { \"internalType\": \"string\", \"name\": \"digitalKey\", \"type\": \"string\" } ], \"name\": \"purchaseGameItem\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"internalType\": \"string\", \"name\": \"itemName\", \"type\": \"string\" }, { \"internalType\": \"string\", \"name\": \"digitalKey\", \"type\": \"string\" } ], \"name\": \"purchaseVitalityItem\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" } ] ";

    // METR Contract
    string METRAddress = "0x6af3D87F0c37d73142765314f3773eC692996638";

    // METR Balance
    public BigInteger METRBalance;

    // ABIs
    private static string gameItemABI = "[ { \"inputs\": [ { \"components\": [ { \"internalType\": \"string\", \"name\": \"itemName\", \"type\": \"string\" }, { \"internalType\": \"string[]\", \"name\": \"prereqs\", \"type\": \"string[]\" }, { \"internalType\": \"uint256\", \"name\": \"price\", \"type\": \"uint256\" }, { \"internalType\": \"bool\", \"name\": \"exists\", \"type\": \"bool\" } ], \"internalType\": \"struct IGameItemCommon.ItemInitialiser[]\", \"name\": \"itemsToInitialise\", \"type\": \"tuple[]\" }, { \"internalType\": \"address\", \"name\": \"metrContractAddress\", \"type\": \"address\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"constructor\" }, { \"inputs\": [], \"name\": \"AlreadyOwned\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"InadequateMETR\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"InadequatePreReqs\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"InadequatePrice\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"InvalidDigitalKey\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"InvalidRole\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"ItemDoesntExist\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"NoItemsToInitialise\", \"type\": \"error\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"bool\", \"name\": \"approved\", \"type\": \"bool\" } ], \"name\": \"ApprovalForAll\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"string\", \"name\": \"indexedItem\", \"type\": \"string\" }, { \"indexed\": false, \"internalType\": \"string\", \"name\": \"item\", \"type\": \"string\" } ], \"name\": \"ItemMinted\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"indexed\": true, \"internalType\": \"bytes32\", \"name\": \"previousAdminRole\", \"type\": \"bytes32\" }, { \"indexed\": true, \"internalType\": \"bytes32\", \"name\": \"newAdminRole\", \"type\": \"bytes32\" } ], \"name\": \"RoleAdminChanged\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"sender\", \"type\": \"address\" } ], \"name\": \"RoleGranted\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"sender\", \"type\": \"address\" } ], \"name\": \"RoleRevoked\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"uint256[]\", \"name\": \"ids\", \"type\": \"uint256[]\" }, { \"indexed\": false, \"internalType\": \"uint256[]\", \"name\": \"values\", \"type\": \"uint256[]\" } ], \"name\": \"TransferBatch\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"id\", \"type\": \"uint256\" }, { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"value\", \"type\": \"uint256\" } ], \"name\": \"TransferSingle\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": false, \"internalType\": \"string\", \"name\": \"value\", \"type\": \"string\" }, { \"indexed\": true, \"internalType\": \"uint256\", \"name\": \"id\", \"type\": \"uint256\" } ], \"name\": \"URI\", \"type\": \"event\" }, { \"inputs\": [], \"name\": \"DEFAULT_ADMIN_ROLE\", \"outputs\": [ { \"internalType\": \"bytes32\", \"name\": \"\", \"type\": \"bytes32\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"MINTER_ROLE\", \"outputs\": [ { \"internalType\": \"bytes32\", \"name\": \"\", \"type\": \"bytes32\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"id\", \"type\": \"uint256\" } ], \"name\": \"balanceOf\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address[]\", \"name\": \"accounts\", \"type\": \"address[]\" }, { \"internalType\": \"uint256[]\", \"name\": \"ids\", \"type\": \"uint256[]\" } ], \"name\": \"balanceOfBatch\", \"outputs\": [ { \"internalType\": \"uint256[]\", \"name\": \"\", \"type\": \"uint256[]\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"string\", \"name\": \"itemName\", \"type\": \"string\" } ], \"name\": \"getPrereqs\", \"outputs\": [ { \"internalType\": \"string[]\", \"name\": \"prereqs\", \"type\": \"string[]\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"string\", \"name\": \"itemName\", \"type\": \"string\" } ], \"name\": \"getPrice\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"price\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" } ], \"name\": \"getRoleAdmin\", \"outputs\": [ { \"internalType\": \"bytes32\", \"name\": \"\", \"type\": \"bytes32\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"grantRole\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"hasRole\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" } ], \"name\": \"isApprovedForAll\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"internalType\": \"string\", \"name\": \"itemName\", \"type\": \"string\" } ], \"name\": \"mintGameItem\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"internalType\": \"string\", \"name\": \"itemName\", \"type\": \"string\" } ], \"name\": \"ownsGameItem\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"owns\", \"type\": \"bool\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"renounceRole\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"revokeRole\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"internalType\": \"uint256[]\", \"name\": \"ids\", \"type\": \"uint256[]\" }, { \"internalType\": \"uint256[]\", \"name\": \"amounts\", \"type\": \"uint256[]\" }, { \"internalType\": \"bytes\", \"name\": \"data\", \"type\": \"bytes\" } ], \"name\": \"safeBatchTransferFrom\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"id\", \"type\": \"uint256\" }, { \"internalType\": \"uint256\", \"name\": \"amount\", \"type\": \"uint256\" }, { \"internalType\": \"bytes\", \"name\": \"data\", \"type\": \"bytes\" } ], \"name\": \"safeTransferFrom\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" }, { \"internalType\": \"bool\", \"name\": \"approved\", \"type\": \"bool\" } ], \"name\": \"setApprovalForAll\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes4\", \"name\": \"interfaceId\", \"type\": \"bytes4\" } ], \"name\": \"supportsInterface\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"name\": \"uri\", \"outputs\": [ { \"internalType\": \"string\", \"name\": \"\", \"type\": \"string\" } ], \"stateMutability\": \"view\", \"type\": \"function\" } ]";
    private static string vitalityItemABI = "[ { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"metrContractAddress\", \"type\": \"address\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"constructor\" }, { \"inputs\": [], \"name\": \"InadequateMETR\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"InadequatePrice\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"InvalidDigitalKey\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"InvalidRole\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"ItemDoesntExist\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"NoItemsToInitialise\", \"type\": \"error\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"bool\", \"name\": \"approved\", \"type\": \"bool\" } ], \"name\": \"ApprovalForAll\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"string\", \"name\": \"indexedItem\", \"type\": \"string\" }, { \"indexed\": false, \"internalType\": \"string\", \"name\": \"item\", \"type\": \"string\" } ], \"name\": \"ItemMinted\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"indexed\": true, \"internalType\": \"bytes32\", \"name\": \"previousAdminRole\", \"type\": \"bytes32\" }, { \"indexed\": true, \"internalType\": \"bytes32\", \"name\": \"newAdminRole\", \"type\": \"bytes32\" } ], \"name\": \"RoleAdminChanged\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"sender\", \"type\": \"address\" } ], \"name\": \"RoleGranted\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"sender\", \"type\": \"address\" } ], \"name\": \"RoleRevoked\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"uint256[]\", \"name\": \"ids\", \"type\": \"uint256[]\" }, { \"indexed\": false, \"internalType\": \"uint256[]\", \"name\": \"values\", \"type\": \"uint256[]\" } ], \"name\": \"TransferBatch\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"id\", \"type\": \"uint256\" }, { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"value\", \"type\": \"uint256\" } ], \"name\": \"TransferSingle\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": false, \"internalType\": \"string\", \"name\": \"value\", \"type\": \"string\" }, { \"indexed\": true, \"internalType\": \"uint256\", \"name\": \"id\", \"type\": \"uint256\" } ], \"name\": \"URI\", \"type\": \"event\" }, { \"inputs\": [], \"name\": \"DEFAULT_ADMIN_ROLE\", \"outputs\": [ { \"internalType\": \"bytes32\", \"name\": \"\", \"type\": \"bytes32\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"MINTER_ROLE\", \"outputs\": [ { \"internalType\": \"bytes32\", \"name\": \"\", \"type\": \"bytes32\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"id\", \"type\": \"uint256\" } ], \"name\": \"balanceOf\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address[]\", \"name\": \"accounts\", \"type\": \"address[]\" }, { \"internalType\": \"uint256[]\", \"name\": \"ids\", \"type\": \"uint256[]\" } ], \"name\": \"balanceOfBatch\", \"outputs\": [ { \"internalType\": \"uint256[]\", \"name\": \"\", \"type\": \"uint256[]\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"internalType\": \"string\", \"name\": \"itemName\", \"type\": \"string\" } ], \"name\": \"getBalance\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"balance\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"string\", \"name\": \"itemName\", \"type\": \"string\" } ], \"name\": \"getPrice\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"price\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" } ], \"name\": \"getRoleAdmin\", \"outputs\": [ { \"internalType\": \"bytes32\", \"name\": \"\", \"type\": \"bytes32\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"grantRole\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"hasRole\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" } ], \"name\": \"isApprovedForAll\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"internalType\": \"string\", \"name\": \"itemName\", \"type\": \"string\" } ], \"name\": \"mintVitalityItem\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"renounceRole\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"revokeRole\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"internalType\": \"uint256[]\", \"name\": \"ids\", \"type\": \"uint256[]\" }, { \"internalType\": \"uint256[]\", \"name\": \"amounts\", \"type\": \"uint256[]\" }, { \"internalType\": \"bytes\", \"name\": \"data\", \"type\": \"bytes\" } ], \"name\": \"safeBatchTransferFrom\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"id\", \"type\": \"uint256\" }, { \"internalType\": \"uint256\", \"name\": \"amount\", \"type\": \"uint256\" }, { \"internalType\": \"bytes\", \"name\": \"data\", \"type\": \"bytes\" } ], \"name\": \"safeTransferFrom\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" }, { \"internalType\": \"bool\", \"name\": \"approved\", \"type\": \"bool\" } ], \"name\": \"setApprovalForAll\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes4\", \"name\": \"interfaceId\", \"type\": \"bytes4\" } ], \"name\": \"supportsInterface\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"name\": \"uri\", \"outputs\": [ { \"internalType\": \"string\", \"name\": \"\", \"type\": \"string\" } ], \"stateMutability\": \"view\", \"type\": \"function\" } ]";

    // The different contracts in the game
    Dictionary<string, GameContract> gameContracts = new Dictionary<string, GameContract>(){
       {"BlasterContract",new GameContract("0xFA75088e4ccC1653628968D485ae3bc937c38B26", gameItemABI) },
       {"DiscLauncherContract",new GameContract("DiscLauncherContractAddress", gameItemABI) },
       {"ShotgunContract",new GameContract("ShotgunContractAddress", gameItemABI) },
       {"JetpackContract",new GameContract("JetpackContractAddress", gameItemABI) },
       {"VitalityItemsContract",new GameContract("VitalityItemsContractAddress", vitalityItemABI) },
    };

    void Start()
    {
        playerAddress = PlayerPrefs.GetString("Account");
    }

    void Update()
    {
        playerAddress = PlayerPrefs.GetString("Account");

    }

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
    public async void getMETRBalance()
    {
        BigInteger balance = await ERC20.BalanceOf(chain, network, METRAddress, playerAddress);
        METRBalance = balance;
    }
}