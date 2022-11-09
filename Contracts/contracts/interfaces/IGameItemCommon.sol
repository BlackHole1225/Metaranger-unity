// SPDX-License-Identifier: MIT
pragma solidity 0.8.17;

interface IGameItemCommon {

    ////////////
    // EVENTS
    ////////////

    /// @notice Emits event that an in-game item has been minted
    /// @dev Emits event that an in-game item has been minted
    /// @param account The address that received the in-game item
    /// @param item The name of the item that was minted
    event ItemMinted(address indexed account, string indexed item);

    ////////////
    // STRUCTS
    ////////////

    /// @notice Restricts how a new game item token can be created
    /// @dev Sets the values that the constructor can receive and interpret to make a new game token
    struct ItemInitialiser {
        string itemName;
        string[] prereqs;
        uint256 price;
        bool exists;
    }

    ////////////
    // ERRORS
    ////////////

    /// @notice Caller does not have correct role to perform this action
    error InvalidRole();

    /// @notice Digital Key provided is incorrect
    error InvalidDigitalKey();

    /// @notice User doesn't own enough METR to mint requested in-game token
    error InadequateMETR();

    /// @notice Provided token name doesn't exist on token contract
    error ItemDoesntExist();

    /// @notice Contract has been initialised without any game items to instantiate
    error NoItemsToInitialise();

    /// @notice Can't set a price for an item below 1
    error InadequatePrice();
}