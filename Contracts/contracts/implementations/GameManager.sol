// SPDX-License-Identifier: MIT
pragma solidity 0.8.17;

import "../interfaces/IGameManager.sol";

contract GameManager is IGameManager {

    string private _digitalKey; // The key that allows Game Manager functions

    constructor(ItemInitialiser[][] memory gameItemTokens, string digitalKey){
        // 1. Assign digital key
        _digitalKey = digitalKey;
        
        // 2. Deploy METR contract
        // 3. Deploy Game Item contracts, using METR contract address
    }

    function createMETR() private; // This may not have to be an explicit function

    // This also may not have to be an explicit function
    function createGameItem(ItemInitialiser[] memory itemsToInitialise, address metrContractAddress) private;

    function mintMETR(address account, uint256 amount, string memory digitalKey) external;

    function purchaseGameItem(address account, string memory itemName, string memory digitalKey) external;


}