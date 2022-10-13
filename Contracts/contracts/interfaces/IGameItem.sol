// SPDX-License-Identifier: MIT
pragma solidity 0.8.17;

interface IGameItem is IGameItemCommon{

    ////////////
    // STRUCTS
    ////////////

    /// @notice Restricts the values that an in-game item can have
    /// @dev Sets the values that can be set and accessed for an in-game item
    struct ItemValues {
        uint256 index;
        string[] prereqs;
        uint256 price;
        bool exists;
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

    /// @notice Returns the price of a specific game item
    /// @dev Returns the price property of a game item via its plaintext name
    /// @param itemName The plaintext name of the game item
    function getPrice(string memory itemName) external returns (uint256 price);

    /// @notice Returns the prerequisite game items for a specific game item
    /// @dev Returns the prereqs property of a game item via its plaintext name
    /// @param itemName The plaintext name of the game item
    function getPrereqs(string memory itemName) external returns (string[] memory prereqs);

    /// @notice Creates an in-game item for the user
    /// @dev Mints the token to an address that represents an in-game item
    /// @param account The address that is receiving the item
    /// @param itemName The plaintext name of the token requested to be minted
    function mintGameItem(address account, string memory itemName) external;

    /// @notice Identifies if a user owns a specific game token or not
    /// @dev Returns whether or not an address owns the inputted game token
    /// @param account The address that is checking if they own the item
    /// @param itemName The plaintext name of the token checked
    function ownsGameItem(address account, string memory itemName) external returns(bool owns);

    ////////////
    // ERRORS
    ////////////

    /// @notice User doesn't own enough METR to mint requested in-game token
    error InadequateMETR();

    /// @notice User doesn't own prerequisite tokens to mint requested in-game token 
    error InadequatePreReqs();

    /// @notice Provided token name doesn't exist on token contract
    error ItemDoesntExist();

    /// @notice Required initial values are missing
    // ! Possibly don't need this
    error InvalidInitialiserFormat();

    /// @notice Contract has been initialised without any game items to instantiate
    error NoItemsToInitialise();

    /// @notice Buyer already owns this token
    error AlreadyOwned();

    /// @notice Can't set a price for an item below 1
    error InadequatePrice();
}