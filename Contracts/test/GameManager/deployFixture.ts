import { SignerWithAddress } from "@nomiclabs/hardhat-ethers/signers";
import { ethers } from "hardhat";
import { BURNER_ROLE, MINTER_ROLE } from "../../constants/roles";
import { GameManager, METRToken, VitalityItem } from "../../typechain";
require("dotenv").config({ path: __dirname + "/.env" });

let Deployer: SignerWithAddress;
let Alice: SignerWithAddress;
let Bob: SignerWithAddress;

let GameManagerContract: GameManager;
let METR: METRToken;
let VitalityItems: VitalityItem;

const MINT_AMOUNT = ethers.utils.parseEther("1000");
const MINT_AMOUNT_2 = ethers.utils.parseEther("3000");
const price200 = ethers.utils.parseEther("200");
const price500 = ethers.utils.parseEther("500");
const price1000 = ethers.utils.parseEther("500");

const TokenA1 = {
  itemName: "TokenA1",
  prereqs: [],
  price: price200,
  exists: true,
};

const TokenA2 = {
  itemName: "TokenA2",
  prereqs: ["TokenA1"],
  price: price500,
  exists: true,
};

const TokenA3 = {
  itemName: "TokenA3",
  prereqs: ["TokenA1"],
  price: price500,
  exists: true,
};

const TokenA4 = {
  itemName: "TokenA4",
  prereqs: ["TokenA2", "TokenA3"],
  price: price1000,
  exists: true,
};

const TokenB1 = {
  itemName: "TokenB1",
  prereqs: [],
  price: price200,
  exists: true,
};

const TokenB2 = {
  itemName: "TokenB2",
  prereqs: ["TokenB1"],
  price: price500,
  exists: true,
};

const TokenB3 = {
  itemName: "TokenB3",
  prereqs: ["TokenB1"],
  price: price500,
  exists: true,
};

const TokenB4 = {
  itemName: "TokenB4",
  prereqs: ["TokenB2", "TokenB3"],
  price: price1000,
  exists: true,
};

const TokenA = {
  contractName: "TokenA",
  gameItems: [TokenA1, TokenA2, TokenA3, TokenA4],
  exists: true,
};

const TokenB = {
  contractName: "TokenB",
  gameItems: [TokenB1, TokenB2, TokenB3, TokenB4],
  exists: true,
};

const deployFixture = async () => {
  const { DIGITAL_KEY } = process.env;

  const digitalKey = DIGITAL_KEY as string;

  [Deployer, Alice, Bob] = await ethers.getSigners();

  // Instance of the METR Contract
  const METR_CONTRACT = await ethers.getContractFactory("METRToken", Deployer);
  METR = await METR_CONTRACT.deploy(digitalKey);

  // Instance of the VitalityItem Contract
  const VitalityItemContract = await ethers.getContractFactory(
    "VitalityItem",
    Deployer
  );
  VitalityItems = await VitalityItemContract.deploy(METR.address);

  // Instance of the GameManager Contract
  const GameManager = await ethers.getContractFactory("GameManager", Deployer);
  GameManagerContract = await GameManager.deploy(
    digitalKey,
    METR.address,
    VitalityItems.address
  );

  // Deployments
  await METR.deployed();
  await VitalityItems.deployed();
  await GameManagerContract.deployed();

  // Roles
  await METR.grantRole(MINTER_ROLE, Deployer.address);
  await METR.grantRole(BURNER_ROLE, Deployer.address);
  await METR.grantRole(MINTER_ROLE, GameManagerContract.address);
  await METR.grantRole(BURNER_ROLE, GameManagerContract.address);
  await METR.grantRole(BURNER_ROLE, VitalityItems.address);
  await VitalityItems.grantRole(MINTER_ROLE, GameManagerContract.address);

  return {
    GameManagerContract,
    Deployer,
    Alice,
    Bob,
    MINT_AMOUNT,
    MINT_AMOUNT_2,
    digitalKey,
    TokenA,
    TokenB,
  };
};

export default deployFixture;
