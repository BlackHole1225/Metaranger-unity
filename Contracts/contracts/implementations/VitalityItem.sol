// SPDX-License-Identifier: MIT
pragma solidity 0.8.17;

import "./METRToken.sol";
import "../interfaces/IVitalityItem.sol";
import "@openzeppelin/contracts/token/ERC1155/ERC1155.sol";
import "@openzeppelin/contracts/access/AccessControl.sol";

contract VitalityItem is ERC1155, AccessControl, IVitalityItem {
    bytes32 public constant MINTER_ROLE = keccak256("MINTER_ROLE");

    // Represents available vitality items on this contract
    mapping(string => ItemValues) private vitalityItems;

    // Internal instance of the METRToken contract
    METRToken private METRTokenContract; 

    // Internal instance of the METRToken contract
    constructor(address metrContractAddress) ERC1155(""){

        // Address that deploys this contract can mint

        _setupRole(DEFAULT_ADMIN_ROLE, msg.sender);
        _setupRole(MINTER_ROLE, msg.sender);

        // Enables this contract to find balances of the METRToken contract
        METRTokenContract = METRToken(metrContractAddress);

        // Explicitly declare and populate the vitality items
        vitalityItems["Health"] = ItemValues(0, 200 ether, true);
        vitalityItems["Armour"] = ItemValues(1, 210 ether, true);
        vitalityItems["Shields"] = ItemValues(2, 220 ether, true);
    }

    // Gets a vitality item's price
    function getPrice(string memory itemName) public view returns (uint256 price){
        if(!vitalityItems[itemName].exists) revert ItemDoesntExist();
        return vitalityItems[itemName].price;
    }

    // Gets a vitality item's index
    function getIndex(string memory itemName) internal view returns(uint256 index){
        return vitalityItems[itemName].index;
    }

    // Checks if the buyer owns enough METR to make the purchase
    function hasEnoughMETR(address account, string memory itemName) internal view returns(bool valid){
        uint256 price = getPrice(itemName);
        uint256 usersBalance = METRTokenContract.balanceOf(account);
        if(price > usersBalance) return false;
        return true;
    }

    // Gets the balance of a specific vitality token
    function getBalance(address account, string memory itemName) public view returns (uint256 balance){
        if(!vitalityItems[itemName].exists) revert ItemDoesntExist();
        return this.balanceOf(account, getIndex(itemName));
    }

    // Mints vitality item to inputted address
    function mintVitalityItem(address account, string calldata itemName) external {
        if(!hasRole(MINTER_ROLE, msg.sender)) revert InvalidRole();
        if(!vitalityItems[itemName].exists) revert ItemDoesntExist();
        if(!hasEnoughMETR(account, itemName)) revert InadequateMETR();

        METRTokenContract.burnToken(account, getPrice(itemName));
        _mint(account, getIndex(itemName), 1, "");
        emit ItemMinted(account, itemName, itemName);
    }

    // The following functions are overrides required by Solidity.
    function supportsInterface(bytes4 interfaceId)
        public
        view
        override(ERC1155, AccessControl)
        returns (bool)
    {
        return super.supportsInterface(interfaceId);
    }
}
