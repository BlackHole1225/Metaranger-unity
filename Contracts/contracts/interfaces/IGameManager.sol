// SPDX-License-Identifier: MIT
pragma solidity 0.8.17;

import "./IGameItemCommon.sol";

interface IGameManager is IGameItemCommon {

    ////////////
    // STRUCTS
    ////////////

    /// @dev Represents the data structure for the Manager's interal reference to the GameItem contracts
    struct GameContractDetails {
        address contractAddress;
        bool exists;
    }

    /// @notice Details about a specific game item contract
    /// @dev Sets out the interface for a GameItemContract as interpreted by the GameManager
    struct GameItemDetails {
        string contractName;
        ItemInitialiser[] gameItems;
    }

    ////////////
    // EVENTS
    ////////////

    /// @notice Alerts user that a METR order has been successfully made
    /// @param account The address that received the METR
    /// @param amount The amount of METR that was minted
    event METREarned(address indexed account, uint256 indexed amount);

    /// @notice Alerts user that they have successfully received the game item
    /// @param account The address that received the game item
    /// @param itemName The name of the item that was purchased
    event GameItemPurchased(address indexed account, string indexed indexedItemName, string itemName);

    /// @notice Alerts user that they have successfully received the vitality item
    /// @param account The address that received the vitality item
    /// @param itemName The name of the item that was purchased
    event VitalityItemPurchased(address indexed account, string indexed indexedItemName, string itemName);

    /// @notice Event emitted when the GameManager deploys a contract
    /// @param contractAddress The address that the contract was deployed to
    /// @param contractType The type of contract that was deployed
    event ContractDeployed(address indexed contractAddress, string indexed indexedContractType, string contractType);

    ////////////
    // FUNCTIONS
    ////////////

    /// @notice Creates METR token for the user
    /// @dev Mints an amount of METR token to a specified address
    /// @param account The address that is receiving the METR
    /// @param amount The amount of METR that they are receiving
    /// @param digitalKey String hash that only comes from the game
    function mintMETR(address account, uint256 amount, string memory digitalKey) external;

    /// @notice Purchases the game item requested
    /// @dev Mints the Game Item token specified
    /// @param account The address that is receiving the Game Item
    /// @param contractName The name of the Game Item
    /// @param itemName The name of the Game Item attempting to be purchased
    /// @param digitalKey String hash that only comes from the game
    function purchaseGameItem(address account, string memory contractName, string memory itemName, string memory digitalKey) external;

    /// @notice Purchases the vitality item requested
    /// @dev Mints the Vitality Item specified
    /// @param account The address that is receiving the Vitality Item
    /// @param itemName The name of the Vitality Item
    /// @param digitalKey String hash that verifies the game is making the contract call
    function purchaseVitalityItem(address account, string memory itemName, string memory digitalKey) external;

    ////////////
    // ERRORS
    ////////////

    /// @notice Contract name doesn't exist
    error ContractDoesntExist();

}