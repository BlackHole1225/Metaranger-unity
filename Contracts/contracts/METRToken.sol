// SPDX-License-Identifier: MIT
pragma solidity ^0.8.14;

import "@openzeppelin/contracts/token/ERC20/presets/ERC20PresetMinterPauser.sol";

contract METRToken is ERC20PresetMinterPauser {
    constructor(unit256 initialSupply) ERC20PresetMinterPauser("MetaToken", "METR") {
        _mint(msg.sender, initialSupply);
    }
}

