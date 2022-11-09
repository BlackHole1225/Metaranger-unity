// SPDX-License-Identifier: MIT
pragma solidity 0.8.17;

import "./IGameItemCommon.sol";

interface IVitalityItem is IGameItemCommon{

    ////////////
    // STRUCTS
    ////////////

    /// @notice Restricts the values that an in-game item can have
    /// @dev Sets the values that can be set and accessed for an in-game item
    struct ItemValues{
        uint256 index;
        uint256 price;
        bool exists;
    }

    ////////////
    // FUNCTIONS
    ////////////

    /// @notice Returns the price of a specific game item
    /// @dev Returns the price property of a game item via its plaintext name
    /// @param itemName The plaintext name of the game item
    function getPrice(string memory itemName) external returns (uint256 price);

    /// @notice Returns the balance of a specific game item
    /// @param account The account who's balance this function is checking
    /// @param itemName The plaintext name of the game item
    function getBalance(address account, string memory itemName) external returns (uint256 balance);

    /// @notice Creates an in-game item for the user
    /// @dev Mints the token to an address that represents an in-game item
    /// @param account The address that is receiving the item
    /// @param itemName The plaintext name of the token requested to be minted
    function mintVitalityItem(address account, string memory itemName) external;

    

}