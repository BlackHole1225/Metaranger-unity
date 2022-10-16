// SPDX-License-Identifier: MIT
pragma solidity 0.8.17;

import "../interfaces/IMETRToken.sol";
import "@openzeppelin/contracts/token/ERC20/ERC20.sol";
import "@openzeppelin/contracts/token/ERC20/extensions/ERC20Burnable.sol";
import "@openzeppelin/contracts/access/AccessControl.sol";

contract METRToken is ERC20, ERC20Burnable, AccessControl, IMETRToken {
    bytes32 public constant MINTER_ROLE = keccak256("MINTER_ROLE");
    bytes32 public constant BURNER_ROLE = keccak256("BURNER_ROLE");

    string private _digitalKey; // The key that allows Game Manager functions

    constructor(string memory digitalKey) ERC20("MetaToken", "METR") {

        // Assign the digital key
        _digitalKey = digitalKey;

        // Address that deploys this contract can mint and burn
        _setupRole(DEFAULT_ADMIN_ROLE, msg.sender);
        _setupRole(MINTER_ROLE, msg.sender);
        _setupRole(BURNER_ROLE, msg.sender);
    }

    // Mints MetaTokens to an inputted address
    function mintToken(address account, uint256 amount) external {
        if(!hasRole(MINTER_ROLE, msg.sender)) revert InvalidRole();
        if(amount < 1) revert NotPositiveValue();
        _mint(account, amount);
        emit METRMinted(account, amount);
    }

    // Burns MetaTokens from an inputted address
    function burnToken(address account, uint256 amount) external {
        if(!hasRole(BURNER_ROLE, msg.sender)) revert InvalidRole();
        if(amount < 1) revert NotPositiveValue();
        _burn(account, amount);
        emit METRBurned(account, amount);
    }

    function grantRolesToGameItem(address account, string memory digitalKey) external {
        if(keccak256(bytes(_digitalKey)) != keccak256(bytes(digitalKey))) revert InvalidDigitalKey();
        _setupRole(MINTER_ROLE, account);
        _setupRole(BURNER_ROLE, account);
    }


}

