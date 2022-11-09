using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web3Manager : MonoBehaviour
{
    // All contracts will share these aspects
    string chain = "polygon";
    string network = "mumbai";

    // The different GameItem Contracts
    public static GameItemContract BlasterContract = new GameItemContract("BlasterContractAddress", "BlasterContainerABI");
    public static GameItemContract DiscLauncherContract = new GameItemContract("DiscLauncherContractAddress", "DiscLauncherContainerABI");
    public static GameItemContract ShotgunContract = new GameItemContract("ShotgunContractAddress", "ShotgunContainerABI");
    public static GameItemContract SniperContract = new GameItemContract("SniperContractAddress", "SniperContainerABI");
    public static GameItemContract JetpackContract = new GameItemContract("JetpackContractAddress", "JetpackContainerABI");
}