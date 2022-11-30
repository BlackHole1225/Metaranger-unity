using System;
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
    string rpc = "https://rpc-mumbai.matic.today";
    string value = "0";
    string gasLimit = "";
    string gasPrice = "";
    private string digitalKey = "2cd347f69a4cbb6545677cf5b3f50019370cdb858579315d08f15b23e89f4b15e4773d3eda46f393c98e57ef179babcd096c415301955bf043faa30c058807cbe233290dc1f69d9e77e5b5e222a27ca681b50d548b639875ae74844ee338cc567d0ce4b2e9f79fc19656fc601d23ff0180a0dda2d8a961bb9b378fa36b49e4d10fde93c8927a2a94be7ef4d41cff87878d8a104ade3d38a9c82e66148214568f27f4e995907407e10b271409cba8daf1f5be1c93929f38d3a8da3df97eab90d909482986edb05eec";
    string playerAddress;
    BigInteger gweiConverter = 1000000000000000000;

    // Game Manger Contract
    string gameManagerAddress = "0xd45dEF180645269b81eFEaFE8D4D83f368B5adc6";
    string gameManagerABI = "[ { \"inputs\": [ { \"internalType\": \"string\", \"name\": \"digitalKey\", \"type\": \"string\" }, { \"internalType\": \"address\", \"name\": \"metrContractAddress\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"vitalityContractAddress\", \"type\": \"address\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"constructor\" }, { \"inputs\": [], \"name\": \"ContractDoesntExist\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"GameItemAlreadyExists\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"InadequateMETR\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"InadequatePrice\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"InvalidDigitalKey\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"InvalidRole\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"ItemDoesntExist\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"NoItemsToInitialise\", \"type\": \"error\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"indexedContractAddress\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"address\", \"name\": \"contractAddress\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"string\", \"name\": \"indexedContractType\", \"type\": \"string\" }, { \"indexed\": false, \"internalType\": \"string\", \"name\": \"contractType\", \"type\": \"string\" } ], \"name\": \"ContractDeployed\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"string\", \"name\": \"indexedItemName\", \"type\": \"string\" }, { \"indexed\": false, \"internalType\": \"string\", \"name\": \"itemName\", \"type\": \"string\" } ], \"name\": \"GameItemPurchased\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"string\", \"name\": \"indexedItem\", \"type\": \"string\" }, { \"indexed\": false, \"internalType\": \"string\", \"name\": \"item\", \"type\": \"string\" } ], \"name\": \"ItemMinted\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"uint256\", \"name\": \"amount\", \"type\": \"uint256\" } ], \"name\": \"METREarned\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"string\", \"name\": \"indexedItemName\", \"type\": \"string\" }, { \"indexed\": false, \"internalType\": \"string\", \"name\": \"itemName\", \"type\": \"string\" } ], \"name\": \"VitalityItemPurchased\", \"type\": \"event\" }, { \"inputs\": [ { \"components\": [ { \"internalType\": \"string\", \"name\": \"contractName\", \"type\": \"string\" }, { \"components\": [ { \"internalType\": \"string\", \"name\": \"itemName\", \"type\": \"string\" }, { \"internalType\": \"string[]\", \"name\": \"prereqs\", \"type\": \"string[]\" }, { \"internalType\": \"uint256\", \"name\": \"price\", \"type\": \"uint256\" }, { \"internalType\": \"bool\", \"name\": \"exists\", \"type\": \"bool\" } ], \"internalType\": \"struct IGameItemCommon.ItemInitialiser[]\", \"name\": \"gameItems\", \"type\": \"tuple[]\" } ], \"internalType\": \"struct IGameManager.GameItemDetails\", \"name\": \"newGameItem\", \"type\": \"tuple\" }, { \"internalType\": \"string\", \"name\": \"digitalKey\", \"type\": \"string\" } ], \"name\": \"createGameItem\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"string\", \"name\": \"\", \"type\": \"string\" } ], \"name\": \"gameItemContracts\", \"outputs\": [ { \"internalType\": \"address\", \"name\": \"contractAddress\", \"type\": \"address\" }, { \"internalType\": \"bool\", \"name\": \"exists\", \"type\": \"bool\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"gameToken\", \"outputs\": [ { \"internalType\": \"contract METRToken\", \"name\": \"\", \"type\": \"address\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"amount\", \"type\": \"uint256\" }, { \"internalType\": \"string\", \"name\": \"digitalKey\", \"type\": \"string\" } ], \"name\": \"mintMETR\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"internalType\": \"string\", \"name\": \"contractName\", \"type\": \"string\" }, { \"internalType\": \"string\", \"name\": \"itemName\", \"type\": \"string\" }, { \"internalType\": \"string\", \"name\": \"digitalKey\", \"type\": \"string\" } ], \"name\": \"purchaseGameItem\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"internalType\": \"string\", \"name\": \"itemName\", \"type\": \"string\" }, { \"internalType\": \"string\", \"name\": \"digitalKey\", \"type\": \"string\" } ], \"name\": \"purchaseVitalityItem\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" } ]";

    // METR Contract
    string METRAddress = "0x22Ac36f2932c73559df2b288A375e12c8fa9B7dB";

    // METR Balance
    public BigInteger METRBalance;

    // ABIs
    private static string gameItemABI = "[ { \"inputs\": [ { \"components\": [ { \"internalType\": \"string\", \"name\": \"itemName\", \"type\": \"string\" }, { \"internalType\": \"string[]\", \"name\": \"prereqs\", \"type\": \"string[]\" }, { \"internalType\": \"uint256\", \"name\": \"price\", \"type\": \"uint256\" }, { \"internalType\": \"bool\", \"name\": \"exists\", \"type\": \"bool\" } ], \"internalType\": \"struct IGameItemCommon.ItemInitialiser[]\", \"name\": \"itemsToInitialise\", \"type\": \"tuple[]\" }, { \"internalType\": \"address\", \"name\": \"metrContractAddress\", \"type\": \"address\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"constructor\" }, { \"inputs\": [], \"name\": \"AlreadyOwned\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"InadequateMETR\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"InadequatePreReqs\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"InadequatePrice\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"InvalidDigitalKey\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"InvalidRole\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"ItemDoesntExist\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"NoItemsToInitialise\", \"type\": \"error\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"bool\", \"name\": \"approved\", \"type\": \"bool\" } ], \"name\": \"ApprovalForAll\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"string\", \"name\": \"indexedItem\", \"type\": \"string\" }, { \"indexed\": false, \"internalType\": \"string\", \"name\": \"item\", \"type\": \"string\" } ], \"name\": \"ItemMinted\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"indexed\": true, \"internalType\": \"bytes32\", \"name\": \"previousAdminRole\", \"type\": \"bytes32\" }, { \"indexed\": true, \"internalType\": \"bytes32\", \"name\": \"newAdminRole\", \"type\": \"bytes32\" } ], \"name\": \"RoleAdminChanged\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"sender\", \"type\": \"address\" } ], \"name\": \"RoleGranted\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"sender\", \"type\": \"address\" } ], \"name\": \"RoleRevoked\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"uint256[]\", \"name\": \"ids\", \"type\": \"uint256[]\" }, { \"indexed\": false, \"internalType\": \"uint256[]\", \"name\": \"values\", \"type\": \"uint256[]\" } ], \"name\": \"TransferBatch\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"id\", \"type\": \"uint256\" }, { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"value\", \"type\": \"uint256\" } ], \"name\": \"TransferSingle\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": false, \"internalType\": \"string\", \"name\": \"value\", \"type\": \"string\" }, { \"indexed\": true, \"internalType\": \"uint256\", \"name\": \"id\", \"type\": \"uint256\" } ], \"name\": \"URI\", \"type\": \"event\" }, { \"inputs\": [], \"name\": \"DEFAULT_ADMIN_ROLE\", \"outputs\": [ { \"internalType\": \"bytes32\", \"name\": \"\", \"type\": \"bytes32\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"MINTER_ROLE\", \"outputs\": [ { \"internalType\": \"bytes32\", \"name\": \"\", \"type\": \"bytes32\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"id\", \"type\": \"uint256\" } ], \"name\": \"balanceOf\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address[]\", \"name\": \"accounts\", \"type\": \"address[]\" }, { \"internalType\": \"uint256[]\", \"name\": \"ids\", \"type\": \"uint256[]\" } ], \"name\": \"balanceOfBatch\", \"outputs\": [ { \"internalType\": \"uint256[]\", \"name\": \"\", \"type\": \"uint256[]\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"string\", \"name\": \"itemName\", \"type\": \"string\" } ], \"name\": \"getPrereqs\", \"outputs\": [ { \"internalType\": \"string[]\", \"name\": \"prereqs\", \"type\": \"string[]\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"string\", \"name\": \"itemName\", \"type\": \"string\" } ], \"name\": \"getPrice\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"price\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" } ], \"name\": \"getRoleAdmin\", \"outputs\": [ { \"internalType\": \"bytes32\", \"name\": \"\", \"type\": \"bytes32\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"grantRole\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"hasRole\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" } ], \"name\": \"isApprovedForAll\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"internalType\": \"string\", \"name\": \"itemName\", \"type\": \"string\" } ], \"name\": \"mintGameItem\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"internalType\": \"string\", \"name\": \"itemName\", \"type\": \"string\" } ], \"name\": \"ownsGameItem\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"owns\", \"type\": \"bool\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"renounceRole\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"revokeRole\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"internalType\": \"uint256[]\", \"name\": \"ids\", \"type\": \"uint256[]\" }, { \"internalType\": \"uint256[]\", \"name\": \"amounts\", \"type\": \"uint256[]\" }, { \"internalType\": \"bytes\", \"name\": \"data\", \"type\": \"bytes\" } ], \"name\": \"safeBatchTransferFrom\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"id\", \"type\": \"uint256\" }, { \"internalType\": \"uint256\", \"name\": \"amount\", \"type\": \"uint256\" }, { \"internalType\": \"bytes\", \"name\": \"data\", \"type\": \"bytes\" } ], \"name\": \"safeTransferFrom\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" }, { \"internalType\": \"bool\", \"name\": \"approved\", \"type\": \"bool\" } ], \"name\": \"setApprovalForAll\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes4\", \"name\": \"interfaceId\", \"type\": \"bytes4\" } ], \"name\": \"supportsInterface\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"name\": \"uri\", \"outputs\": [ { \"internalType\": \"string\", \"name\": \"\", \"type\": \"string\" } ], \"stateMutability\": \"view\", \"type\": \"function\" } ]";
    private static string vitalityItemABI = "[ { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"metrContractAddress\", \"type\": \"address\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"constructor\" }, { \"inputs\": [], \"name\": \"InadequateMETR\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"InadequatePrice\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"InvalidDigitalKey\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"InvalidRole\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"ItemDoesntExist\", \"type\": \"error\" }, { \"inputs\": [], \"name\": \"NoItemsToInitialise\", \"type\": \"error\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"bool\", \"name\": \"approved\", \"type\": \"bool\" } ], \"name\": \"ApprovalForAll\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"string\", \"name\": \"indexedItem\", \"type\": \"string\" }, { \"indexed\": false, \"internalType\": \"string\", \"name\": \"item\", \"type\": \"string\" } ], \"name\": \"ItemMinted\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"indexed\": true, \"internalType\": \"bytes32\", \"name\": \"previousAdminRole\", \"type\": \"bytes32\" }, { \"indexed\": true, \"internalType\": \"bytes32\", \"name\": \"newAdminRole\", \"type\": \"bytes32\" } ], \"name\": \"RoleAdminChanged\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"sender\", \"type\": \"address\" } ], \"name\": \"RoleGranted\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"sender\", \"type\": \"address\" } ], \"name\": \"RoleRevoked\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"uint256[]\", \"name\": \"ids\", \"type\": \"uint256[]\" }, { \"indexed\": false, \"internalType\": \"uint256[]\", \"name\": \"values\", \"type\": \"uint256[]\" } ], \"name\": \"TransferBatch\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"id\", \"type\": \"uint256\" }, { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"value\", \"type\": \"uint256\" } ], \"name\": \"TransferSingle\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": false, \"internalType\": \"string\", \"name\": \"value\", \"type\": \"string\" }, { \"indexed\": true, \"internalType\": \"uint256\", \"name\": \"id\", \"type\": \"uint256\" } ], \"name\": \"URI\", \"type\": \"event\" }, { \"inputs\": [], \"name\": \"DEFAULT_ADMIN_ROLE\", \"outputs\": [ { \"internalType\": \"bytes32\", \"name\": \"\", \"type\": \"bytes32\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"MINTER_ROLE\", \"outputs\": [ { \"internalType\": \"bytes32\", \"name\": \"\", \"type\": \"bytes32\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"id\", \"type\": \"uint256\" } ], \"name\": \"balanceOf\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address[]\", \"name\": \"accounts\", \"type\": \"address[]\" }, { \"internalType\": \"uint256[]\", \"name\": \"ids\", \"type\": \"uint256[]\" } ], \"name\": \"balanceOfBatch\", \"outputs\": [ { \"internalType\": \"uint256[]\", \"name\": \"\", \"type\": \"uint256[]\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"internalType\": \"string\", \"name\": \"itemName\", \"type\": \"string\" } ], \"name\": \"getBalance\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"balance\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"string\", \"name\": \"itemName\", \"type\": \"string\" } ], \"name\": \"getPrice\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"price\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" } ], \"name\": \"getRoleAdmin\", \"outputs\": [ { \"internalType\": \"bytes32\", \"name\": \"\", \"type\": \"bytes32\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"grantRole\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"hasRole\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" } ], \"name\": \"isApprovedForAll\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" }, { \"internalType\": \"string\", \"name\": \"itemName\", \"type\": \"string\" } ], \"name\": \"mintVitalityItem\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"renounceRole\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes32\", \"name\": \"role\", \"type\": \"bytes32\" }, { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"revokeRole\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"internalType\": \"uint256[]\", \"name\": \"ids\", \"type\": \"uint256[]\" }, { \"internalType\": \"uint256[]\", \"name\": \"amounts\", \"type\": \"uint256[]\" }, { \"internalType\": \"bytes\", \"name\": \"data\", \"type\": \"bytes\" } ], \"name\": \"safeBatchTransferFrom\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"id\", \"type\": \"uint256\" }, { \"internalType\": \"uint256\", \"name\": \"amount\", \"type\": \"uint256\" }, { \"internalType\": \"bytes\", \"name\": \"data\", \"type\": \"bytes\" } ], \"name\": \"safeTransferFrom\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"operator\", \"type\": \"address\" }, { \"internalType\": \"bool\", \"name\": \"approved\", \"type\": \"bool\" } ], \"name\": \"setApprovalForAll\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"bytes4\", \"name\": \"interfaceId\", \"type\": \"bytes4\" } ], \"name\": \"supportsInterface\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"name\": \"uri\", \"outputs\": [ { \"internalType\": \"string\", \"name\": \"\", \"type\": \"string\" } ], \"stateMutability\": \"view\", \"type\": \"function\" } ]";

    // The different contracts in the game
    Dictionary<string, GameContract> gameContracts = new Dictionary<string, GameContract>(){
       {"BlasterContract",new GameContract("0xcd8a7e2ec16fb3ff6fad142dc30cdb8c2237348b", gameItemABI) },
       {"DiscLauncherContract",new GameContract("0x7d41e26d950908ab5d44611171cc7a2acc0a7aba", gameItemABI) },
       {"ShotgunContract",new GameContract("0xbe8af99a91b85ab400ab7f0901556a9c28d08ad2", gameItemABI) },
       {"JetpackContract",new GameContract("0xdf289994a3809592392cd3e4129d10fada0be03e", gameItemABI) },
       {"SniperContract",new GameContract("0x71d3529831ea10706f617654188e6aabc9b102b2", gameItemABI) },
       {"VitalityItemsContract",new GameContract("0x85B3C588912Cbd2F415bFBC4A34f069554385663", vitalityItemABI) },
    };

    // The indices that represent the game items on each contract
    Dictionary<string, string[]> gameItemIndices = new Dictionary<string, string[]>(){
        {"BlasterContract", new string[]{"0","1","2","3","4"}},
        {"JetpackContract", new string[]{"0","1","2","3"}},
        {"ShotgunContract", new string[]{"0","1","2","3","4","5"}},
        {"DiscLauncherContract", new string[]{"0","1","2","3","4"}},
        {"SniperContract", new string[]{"0","1","2","3","4"}},
    };

    Dictionary<string, List<string>> gameItemNames = new Dictionary<string, List<string>>(){
        {"BlasterContract", new List<string>{"BlasterAccuracy", "BlasterStoppingPower", "BlasterRapidFire", "BlasterAimSpeed", "BlasterCooldown"}},
        {"JetpackContract", new List<string>{"JetpackBase", "JetpackFlightSpeed", "JetpackDuration", "JetpackCooldown"}},
        {"ShotgunContract", new List<string>{"ShotgunBase", "ShotgunSpreadshot", "ShotgunCooldown", "ShotgunStoppingPower", "ShotgunExtraBarrel", "ShotgunAimSpeed"}},
        {"DiscLauncherContract", new List<string>{"DiscLauncherBase", "DiscLauncherAimSpeed", "DiscLauncherChargeSpeed", "DiscLauncherStoppingPower", "DiscLauncherCooldown"}},
        {"SniperContract", new List<string>{"SniperBase", "SniperAimSpeed", "SniperCooldown", "SniperStoppingPower", "SniperZoom"}},
    };

    void Start()
    {
        playerAddress = PlayerPrefs.GetString("Account");
    }

    void Update()
    {
        playerAddress = PlayerPrefs.GetString("Account");
    }

    // HELPER FUNCTIONS
    string DetermineGameItemContract(string itemName)
    {
        if (itemName.Contains("Blaster")) return "BlasterContract";
        if (itemName.Contains("DiscLauncher")) return "DiscLauncherContract";
        if (itemName.Contains("Shotgun")) return "ShotgunContract";
        if (itemName.Contains("Jetpack")) return "JetpackContract";
        if (itemName.Contains("Sniper")) return "SniperContract";
        return null;
    }

    string DetermineERC1155Index(string itemName)
    {
        int index = -1;

        if (itemName.Contains("Blaster"))
        {
            if (itemName.Contains("Accuracy")) index = 0;
            if (itemName.Contains("AimSpeed")) index = 1;
            if (itemName.Contains("Cooldown")) index = 2;
            if (itemName.Contains("RapidFire")) index = 3;
            if (itemName.Contains("StoppingPower")) index = 4;
        }

        if (itemName.Contains("DiscLauncher"))
        {
            if (itemName.Contains("AimSpeed")) index = 0;
            if (itemName.Contains("Base")) index = 1;
            if (itemName.Contains("ChargeSpeed")) index = 2;
            if (itemName.Contains("Cooldown")) index = 3;
            if (itemName.Contains("StoppingPower")) index = 4;

        };

        if (itemName.Contains("Shotgun"))
        {
            if (itemName.Contains("AimSpeed")) index = 0;
            if (itemName.Contains("Base")) index = 1;
            if (itemName.Contains("Cooldown")) index = 2;
            if (itemName.Contains("ExtraBarrel")) index = 3;
            if (itemName.Contains("Spreadshot")) index = 4;
            if (itemName.Contains("StoppingPower")) index = 5;
        };

        if (itemName.Contains("Jetpack"))
        {
            if (itemName.Contains("Base")) index = 0;
            if (itemName.Contains("Cooldown")) index = 1;
            if (itemName.Contains("Duration")) index = 2;
            if (itemName.Contains("FlightSpeed")) index = 3;

        };
        if (itemName.Contains("Sniper"))
        {
            if (itemName.Contains("AimSpeed")) index = 0;
            if (itemName.Contains("Base")) index = 1;
            if (itemName.Contains("Cooldown")) index = 2;
            if (itemName.Contains("StoppingPower")) index = 3;
            if (itemName.Contains("Zoom")) index = 4;
        };

        Debug.Log("Index for " + itemName + ": " + index);

        if (index == -1)
        {
            Debug.Log("No index found for " + itemName);
        };

        return index.ToString();
    }

    // GAME MANAGER FUNCTIONS
    public async Task mintMETR(int amount)
    {
        string[] obj = { playerAddress, amount.ToString() + "000000000000000000", digitalKey }; // Convert amount to wei
        string args = JsonConvert.SerializeObject(obj);
        string response = await Web3GL.SendContract("mintMETR", gameManagerABI, gameManagerAddress, args, value, gasLimit, gasPrice);
        Debug.Log("Response from mintMETR in Web3Manager " + response);
    }

    public async Task purchaseGameItem(string itemName)
    {
        string contractName = DetermineGameItemContract(itemName);
        string[] obj = { playerAddress, contractName, itemName, digitalKey };
        string args = JsonConvert.SerializeObject(obj);
        string response = await Web3GL.SendContract("purchaseGameItem", gameManagerABI, gameManagerAddress, args, value, gasLimit, gasPrice);
        Debug.Log("Response from purchaseGameItem in Web3Manager " + response);
    }

    public async Task purchaseVitalityItem(string itemName)
    {
        string[] obj = { playerAddress, itemName, digitalKey };
        string args = JsonConvert.SerializeObject(obj);
        string response = await Web3GL.SendContract("purchaseVitalityItem", gameManagerABI, gameManagerAddress, args, value, gasLimit, gasPrice);
        Debug.Log("Response from purchaseVitalityItem in Web3Manager " + response);
    }

    // GAME ITEM FUNCTIONS
    public async void getPrice(string contractName, string itemName)
    {
        GameContract gameContract = gameContracts[contractName];
        string[] obj = { itemName };
        string args = JsonConvert.SerializeObject(obj);
        string response = await EVM.Call(chain, network, gameContract.Address, gameContract.ABI, "getPrice", args, rpc);
        Debug.Log("Response from getPrice in Web3Manager " + response);
    }

    public async Task<string> ownsGameItem(string contractName, string itemName)
    {
        GameContract gameContract = gameContracts[contractName];
        try
        {
            BigInteger erc1155Test = await ERC1155.BalanceOf(chain, network, gameContract.Address, playerAddress, DetermineERC1155Index(itemName), rpc);
            bool owns = erc1155Test > 0;
            return owns.ToString();
        }
        catch
        {
            Debug.Log("Error getting balance for " + itemName);
            return false.ToString();
        }
    }

    public async Task ownsGameItems(string contractName)
    {
        GameContract gameContract = gameContracts[contractName];
        string[] itemIndices = gameItemIndices[contractName];
        string[] playerAddressArray = new string[itemIndices.Length];

        for (int i = 0; i < playerAddressArray.Length; i++)
        {
            playerAddressArray[i] = playerAddress;
        }

        List<BigInteger> balances = await ERC1155.BalanceOfBatch(chain, network, gameContract.Address, playerAddressArray, itemIndices, rpc);
        for (int i = 0; i < balances.Count; i++)
        {
            if (balances[i] > 0)
            {
                PlayerPrefs.SetString(gameItemNames[contractName][i] + "Owned", "true");
            }
            else
            {
                PlayerPrefs.SetString(gameItemNames[contractName][i] + "Owned", "false");
            }

            Debug.Log(gameItemNames[contractName][i] + "Owned: " + PlayerPrefs.GetString(gameItemNames[contractName][i] + "Owned"));
        }
    }

    // VITALITY ITEM FUNCTIONS
    public async Task<string> getBalance(string itemName)
    {
        string[] obj = { playerAddress, itemName };
        string args = JsonConvert.SerializeObject(obj);
        string response = await EVM.Call(chain, network, gameContracts["VitalityItemsContract"].Address, gameContracts["VitalityItemsContract"].ABI, "getBalance", args, rpc);
        PlayerPrefs.SetString(itemName, response);
        return response;
    }

    // METR FUNCTIONS
    public async Task getMETRBalance()
    {
        // Waits until we actually have a value for the playerAddress
        while (playerAddress == "" || playerAddress == null)
        {
            await new WaitForSeconds(1f);
        };

        BigInteger balance = await ERC20.BalanceOf(chain, network, METRAddress, playerAddress, rpc);
        METRBalance = balance / gweiConverter;

        PlayerPrefs.SetInt("METRBalance", (int)METRBalance);
    }
}

