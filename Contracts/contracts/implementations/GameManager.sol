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

    constructor(string memory digitalKey, address metrContractAddress, address vitalityContractAddress){
        // 1. Assign digital key
        _digitalKey = digitalKey;

        // 2. Instantiate METRToken contract
        gameToken = METRToken(metrContractAddress);

        // 3. Instantiate VitalityItemContract
        vitalityItemContract = VitalityItem(vitalityContractAddress);
    }

    function createGameItem(GameItemDetails memory newGameItem, string memory digitalKey) external {
        if(keccak256(bytes(_digitalKey)) != keccak256(bytes(digitalKey))) revert InvalidDigitalKey();
        if(gameItemContracts[newGameItem.contractName].exists) revert GameItemAlreadyExists();
        GameItem gameItem = new GameItem(newGameItem.gameItems, address(gameToken));
        gameItemContracts[newGameItem.contractName] = GameContractDetails(address(gameItem), true);
        gameToken.grantRolesToGameItem(address(gameItem), digitalKey);
        emit ContractDeployed(address(gameItem), address(gameItem), newGameItem.contractName, newGameItem.contractName);
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