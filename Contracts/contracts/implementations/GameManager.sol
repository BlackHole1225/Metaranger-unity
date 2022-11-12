// SPDX-License-Identifier: MIT
pragma solidity 0.8.17;

import "../interfaces/IGameManager.sol";
import "./METRToken.sol";
import "./GameItem.sol";
import "./VitalityItem.sol";

contract GameManager is IGameManager {

    string private _digitalKey; // The key that allows Game Manager functions
    METRToken public gameToken; // In-contract reference to the METRToken
    VitalityItem private vitalityItemContract; // In-contract reference to the VitalityItem contract
    mapping(string => GameContractDetails) public gameItemContracts; // References to the Game Item contracts


    constructor(GameItemDetails[] memory gameItemTokens, string memory digitalKey){
        // 1. Assign digital key
        _digitalKey = digitalKey;

        // 2. Deploy METR contract
        gameToken = new METRToken(digitalKey);
        emit ContractDeployed(address(gameToken), "METRToken", "METRToken");

        // 3. Deploy Game Item contracts, using METR contract address
        for(uint i; i < gameItemTokens.length;){
            GameItem gameItem = new GameItem(gameItemTokens[i].gameItems, address(gameToken));
            emit ContractDeployed(address(gameItem), gameItemTokens[i].contractName, gameItemTokens[i].contractName);
            gameItemContracts[gameItemTokens[i].contractName] = GameContractDetails(address(gameItem), true);
            gameToken.grantRolesToGameItem(address(gameItem), digitalKey);
            unchecked{
                ++i;
            }
        }

        // 4. Deploy Vitality Item contract
        vitalityItemContract = new VitalityItem(address(gameToken));
        emit ContractDeployed(address(vitalityItemContract), "VitalityItem", "VitalityItem");
        gameToken.grantRolesToGameItem(address(vitalityItemContract), digitalKey);
    }

    function mintMETR(address account, uint256 amount, string memory digitalKey) external {
        if(keccak256(bytes(_digitalKey)) != keccak256(bytes(digitalKey))) revert InvalidDigitalKey();
        gameToken.mintToken(account, amount);
        emit METREarned(account, amount);
    }

    function purchaseGameItem(address account, string memory contractName, string memory itemName, string memory digitalKey) external {
        if(keccak256(bytes(_digitalKey)) != keccak256(bytes(digitalKey))) revert InvalidDigitalKey();
        if(!gameItemContracts[contractName].exists) revert ContractDoesntExist();
        GameItem gameItemContract = GameItem(gameItemContracts[contractName].contractAddress);
        gameItemContract.mintGameItem(account, itemName);
        emit GameItemPurchased(account, itemName, itemName);
    }

    function purchaseVitalityItem(address account, string memory itemName, string memory digitalKey) external {
        if(keccak256(bytes(_digitalKey)) != keccak256(bytes(digitalKey))) revert InvalidDigitalKey();
        vitalityItemContract.mintVitalityItem(account, itemName);
        emit VitalityItemPurchased(account, itemName, itemName);
    }
}