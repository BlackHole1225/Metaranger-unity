import { SignerWithAddress } from "@nomiclabs/hardhat-ethers/signers";
import { ethers } from "hardhat";
import { GameManager } from "../../typechain";

let Deployer: SignerWithAddress;
let Alice: SignerWithAddress;
let Bob: SignerWithAddress;

let GameManagerContract: GameManager;

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

const TokenDetails = [
  {
    contractName: "TokenA",
    gameItems: [TokenA1, TokenA2, TokenA3, TokenA4],
    exists: true,
  },
  {
    contractName: "TokenB",
    gameItems: [TokenB1, TokenB2, TokenB3, TokenB4],
    exists: true,
  },
];

const digitalKey =
  "2cd347f69a4cbb6545677cf5b3f50019370cdb858579315d08f15b23e89f4b15e4773d3eda46f393c98e57ef179babcd096c415301955bf043faa30c058807cbe233290dc1f69d9e77e5b5e222a27ca681b50d548b639875ae74844ee338cc567d0ce4b2e9f79fc19656fc601d23ff0180a0dda2d8a961bb9b378fa36b49e4d10fde93c8927a2a94be7ef4d41cff87878d8a104ade3d38a9c82e66148214568f27f4e995907407e10b271409cba8daf1f5be1c93929f38d3a8da3df97eab90d909482986edb05eec";

const deployFixture = async () => {
  [Deployer, Alice, Bob] = await ethers.getSigners();

  const GameManager = await ethers.getContractFactory("GameManager", Deployer);
  GameManagerContract = await GameManager.deploy(TokenDetails, digitalKey);
  await GameManagerContract.deployed();

  return {
    GameManagerContract,
    Deployer,
    Alice,
    Bob,
    MINT_AMOUNT,
    MINT_AMOUNT_2,
    digitalKey,
  };
};

export default deployFixture;
