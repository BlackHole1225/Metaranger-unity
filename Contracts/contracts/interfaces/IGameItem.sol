// SPDX-License-Identifer: MIT
pragma solidity 0.8.17;

interface IGameItem {

    ////////////
    // STRUCTS
    ////////////

    /// @notice Restricts the values that an in-game item can have
    /// @dev Sets the values that can be set and accessed for an in-game item
    struct ItemValues {
        uint256 index;
        string[] prereqs;
        uint256 price;
    }

    ////////////
    // EVENTS
    ////////////

    /// @notice Emits event that an in-game item has been minted
    /// @dev Emits event that an in-game item has been minted
    /// @param account The address that received the in-game item
    /// @param item The name of the item that was minted
    event ItemMinted(address indexed account, string indexed item);

    ////////////
    // FUNCTIONS
    ////////////

    /// @notice Checks if a user owns enough METR to purchase requested token
    /// @dev Checks if user's address has enough METR token to purchase the requested token
    /// @param itemName The plaintext name of the token requested to be minted
    function checkPrice(string itemName);

    /// @notice Checks if a user has owns the tokens required to receive the requested token
    /// @dev Checks if the user owns the prerequisite tokens for the requested token
    /// @param itemName The plaintext name of the token requested to be minted
    function checkPreReqs(string itemName);

    /// @notice Creates an in-game item for the user
    /// @dev Mints the token to an address that represents an in-game item
    /// @param account The address that is receiving the item
    /// @param id The id of the in-game item to be minted
    function mintGameItem(address account, uint256 id);

    ////////////
    // ERRORS
    ////////////

    /// @notice User doesn't own enough METR to mint requested in-game token
    error InadequateMETR();

    /// @notice User doesn't own prerequisite tokens to mint requested in-game token 
    error InadequatePreReqs();

}