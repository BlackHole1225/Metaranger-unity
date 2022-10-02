// SPDX-License-Identifier: MIT
pragma solidity 0.8.17;

interface IMETRToken {

    ////////////
    // STRUCTS
    ////////////

    /// @notice Restricts what can be used to interact with the token
    /// @dev Restricts the parameters to a specific format
    struct TokenArgs {
        address account;
        uint256 amount;
    }

    ////////////
    // EVENTS
    ////////////

    /// @notice Emits event that an amount of METR token has been minted
    /// @dev Emits event that an amount of METR token has been minted
    /// @param account The address that received the METR
    /// @param amount The amount of METR that was minted
    event METRMinted(address indexed account, uint256 indexed amount);

    /// @notice Emits event that an amount of METR token has been burned
    /// @dev Emits event that an amount of METR token has been burned
    /// @param account The address that was deprived the METR
    /// @param amount The amount of METR that was burned
    event METRBurned(address indexed account, uint256 indexed amount);

    ////////////
    // FUNCTIONS
    ////////////

    /// @notice Creates METR Tokens
    /// @dev Mints METR tokens for a specific address
    /// @param tokenArgs The relevant arguments in form of a TokenArgs
    function mintToken(TokenArgs calldata tokenArgs) external;

    /// @notice Destroys METR Tokens
    /// @dev Burns METR tokens for a specific address
    /// @param tokenArgs The relevant arguments in form of a TokenArgs
    function burnToken(TokenArgs calldata tokenArgs) external;

    ////////////
    // ERRORS
    ////////////

    /// @notice Caller does not have correct role to perform this action
    error InvalidRole();

}