import { SignerWithAddress } from "@nomiclabs/hardhat-ethers/signers";
import { ethers } from "hardhat";
import { BURNER_ROLE, MINTER_ROLE } from "../../constants/roles";
import { GameItem, METRToken } from "../../typechain";

let Deployer: SignerWithAddress;
let Alice: SignerWithAddress;
let Bob: SignerWithAddress;

let GameItemContract: GameItem;
let METR: METRToken;

// The interfaceID for a ERC1155 according to this:
// https://stackoverflow.com/questions/69706835/how-to-check-if-the-token-on-opensea-is-erc721-or-erc1155-using-node-js
const ERC1155interfaceID = 0xd9b67a26;
const ERC721interfaceID = 0x80ac58cd;

const MINT_AMOUNT = ethers.utils.parseEther("1000");
const MINT_AMOUNT_2 = ethers.utils.parseEther("3000");
const TokenAPrice = ethers.utils.parseEther("200");
const TokenBPrice = ethers.utils.parseEther("500");
const TokenCPrice = ethers.utils.parseEther("500");
const TokenDPrice = ethers.utils.parseEther("1000");

const TokenA = {
  itemName: "TokenA",
  prereqs: [],
  price: TokenAPrice,
  exists: true,
};

const TokenB = {
  itemName: "TokenB",
  prereqs: ["TokenA"],
  price: TokenBPrice,
  exists: true,
};

const TokenC = {
  itemName: "TokenC",
  prereqs: ["TokenA"],
  price: TokenCPrice,
  exists: true,
};

const TokenD = {
  itemName: "TokenD",
  prereqs: ["TokenB", "TokenC"],
  price: TokenDPrice,
  exists: true,
};

const deployFixture = async () => {
  // Relevant addresses
  [Deployer, Alice, Bob] = await ethers.getSigners();

  // Instance of the METR contract
  const METR_CONTRACT = await ethers.getContractFactory("METRToken", Deployer);
  METR = await METR_CONTRACT.deploy();

  // Instance of the GameItem contract
  const GameItem_Contract = await ethers.getContractFactory(
    "GameItem",
    Deployer
  );

  GameItemContract = await GameItem_Contract.deploy(
    [TokenA, TokenB, TokenC, TokenD],
    METR.address
  );

  // Deployments
  await METR.deployed();
  await GameItemContract.deployed();

  // Roles
  await METR.grantRole(MINTER_ROLE, Deployer.address);
  await METR.grantRole(BURNER_ROLE, Deployer.address);
  await GameItemContract.grantRole(MINTER_ROLE, Deployer.address);
  await METR.grantRole(BURNER_ROLE, GameItemContract.address);

  // Variables relevant for the tests
  return {
    GameItemContract,
    METR,
    Deployer,
    Alice,
    Bob,
    MINT_AMOUNT,
    MINT_AMOUNT_2,
    TokenA,
  };
};

export default deployFixture;
