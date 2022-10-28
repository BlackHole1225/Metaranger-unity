// SPDX-License-Identifier: MIT
pragma solidity 0.8.17;

import "../interfaces/IGameManager.sol";
import "./METRToken.sol";
import "./GameItem.sol";

contract GameManager is IGameManager {

    string private _digitalKey; // The key that allows Game Manager functions
    METRToken public gameToken; // In-contract reference to the METRToken

    mapping(string => GameContractDetails) public gameItemContracts; 

    constructor(GameItemDetails[] memory gameItemTokens, string memory digitalKey){
        // 1. Assign digital key
        _digitalKey = digitalKey;

        // 2. Deploy METR contract
        gameToken = new METRToken(digitalKey);

        // 3. Deploy Game Item contracts, using METR contract address
        for(uint i; i < gameItemTokens.length){
            GameItem gameItem = new GameItem(gameItemTokens[i].gameItems, address(gameToken));
            gameItemContracts[gameItemTokens[i].contractName] = GameContractDetails(address(gameItem), true);
            gameToken.grantRolesToGameItem(address(gameItem), digitalKey);
            unchecked{
                ++i;
            }
        }
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
        emit GameItemPurchased(account, itemName);
    }
}