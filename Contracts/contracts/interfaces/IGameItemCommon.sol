// SPDX-License-Identifier: MIT
pragma solidity 0.8.17;

interface IGameItemCommon {
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
}