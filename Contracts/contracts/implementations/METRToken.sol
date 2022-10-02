// SPDX-License-Identifier: MIT
pragma solidity 0.8.17;

import "../interfaces/IMETRToken.sol";
import "@openzeppelin/contracts/token/ERC20/ERC20.sol";
import "@openzeppelin/contracts/token/ERC20/extensions/ERC20Burnable.sol";
import "@openzeppelin/contracts/access/AccessControl.sol";

contract METRToken is ERC20, ERC20Burnable, AccessControl, IMETRToken {
    bytes32 public constant MINTER_ROLE = keccak256("MINTER_ROLE");
    bytes32 public constant BURNER_ROLE = keccak256("BURNER_ROLE");

    constructor() ERC20("MetaToken", "METR") {
        // Address that deploys this contract can mint and burn
        _setupRole(DEFAULT_ADMIN_ROLE, msg.sender);
    }

    // Mints MetaTokens to an inputted address
    function mintToken(TokenArgs calldata tokenArgs) external {
        if(!hasRole(MINTER_ROLE, msg.sender)) revert InvalidRole();
        _mint(tokenArgs.account, tokenArgs.amount);
        emit METRMinted(tokenArgs.account, tokenArgs.amount);

    }

    // Burns MetaTokens from an inputted address
    function burnToken(TokenArgs calldata tokenArgs) external {
        if(!hasRole(BURNER_ROLE, msg.sender)) revert InvalidRole();
        _burn(tokenArgs.account, tokenArgs.amount);
        emit METRBurned(tokenArgs.account, tokenArgs.amount);
    }

}

