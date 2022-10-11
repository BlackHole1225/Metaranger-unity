// SPDX-License-Identifier: MIT
pragma solidity 0.8.17;

import "../interfaces/IGameItem.sol";
import "@openzeppelin/contracts/token/ERC1155/ERC1155.sol";
import "@openzeppelin/contracts/access/AccessControl.sol";
import "./METRToken.sol";

contract GameItem is ERC1155, AccessControl, IGameItem {
    bytes32 public constant MINTER_ROLE = keccak256("MINTER_ROLE");

    // Represents the available game items on this contract
    mapping(string => ItemValues) private gameItems;

    // Internal instance of the METRToken contract
    METRToken METRTokenContract; 

    constructor(ItemInitialiser[] memory itemsToInitialise, address metrContractAddress) ERC1155(""){
        // Address that deploys this contract can mint
        _setupRole(DEFAULT_ADMIN_ROLE, msg.sender);

        // Enables this contract to find balances of the METRToken contract
        // ! Figure out how do I check this?
        // ! Or check if this is the right implementation?
        METRTokenContract = METRToken(metrContractAddress);

        // Cannot call this contract without items to initialise
        if(itemsToInitialise.length < 1) revert NoItemsToInitialise();

        // Populates contract's internal gameItems based on deployment variables
        for(uint i = 0; i < itemsToInitialise.length; i++){
            ItemInitialiser memory item = itemsToInitialise[i]; // Abbreviation for readibility
            gameItems[item.itemName] = ItemValues(i,item.prereqs,item.price, true);
        }
    }

    // Gets a game item's price
    function getPrice(string memory itemName) public view returns(uint256 price){
        if(!gameItems[itemName].exists) revert ItemDoesntExist();
        return gameItems[itemName].price;
    }

    // Gets a game item's index 
    function getIndex(string memory itemName) internal view returns(uint256 index){
        if(!gameItems[itemName].exists) revert ItemDoesntExist();
        return gameItems[itemName].index;
    }

    // Gets a game item's prereqs
    function getPrereqs(string memory itemName) public view returns(string[] memory prereqs){
        if(!gameItems[itemName].exists) revert ItemDoesntExist();
        return gameItems[itemName].prereqs;
    }

    // Checks if the buyer owns enough METR to make the purchase
    function hasEnoughMETR(address account, string memory itemName) internal view returns(bool valid) {
        uint256 price = getPrice(itemName);
        uint256 usersBalance = METRTokenContract.balanceOf(account);
        if(price > usersBalance) return false;
        return true;
    }

    // Checks if buyer owns prerequisite game item token
    function ownsPreReqs(address account, string memory itemName) internal view returns(bool valid) {
        string[] memory prereqs = getPrereqs(itemName);
        if(prereqs.length == 0) return true;

        for(uint i = 0; i < prereqs.length; i++){
            uint256 prereqIndex = getIndex(prereqs[i]);
            uint256 buyerBalance = this.balanceOf(account, prereqIndex);
            if(buyerBalance < 1) return false;
        }
        
        return true;
    }

    // Mints game item token to inputted address
    function mintGameItem(address account, string calldata itemName) external {
        if(!hasRole(MINTER_ROLE, msg.sender)) revert InvalidRole();
        if(!gameItems[itemName].exists) revert ItemDoesntExist();
        if(this.balanceOf(account, getIndex(itemName)) > 0) revert AlreadyOwned();
        if(!hasEnoughMETR(account,itemName)) revert InadequateMETR();
        if(!ownsPreReqs(account, itemName)) revert InadequatePreReqs();

        METRTokenContract.burnToken(account, getPrice(itemName));
        _mint(account, getIndex(itemName), 1, "");
        emit ItemMinted(account, itemName);
    }

    // Checks to see if a game token is owned by a specific address
    function ownsGameItem(address account, string calldata itemName) external view returns(bool owns) {
        if(!gameItems[itemName].exists) revert ItemDoesntExist();
        uint256 index = getIndex(itemName);
        uint256 gameTokenBalance = balanceOf(account, index);
        return gameTokenBalance > 0;
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