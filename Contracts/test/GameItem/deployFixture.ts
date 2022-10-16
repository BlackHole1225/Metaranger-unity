import { SignerWithAddress } from "@nomiclabs/hardhat-ethers/signers";
import { ethers } from "hardhat";
import { BURNER_ROLE, MINTER_ROLE } from "../../constants/roles";
import { GameItem, METRToken } from "../../typechain";

let Deployer: SignerWithAddress;
let Alice: SignerWithAddress;
let Bob: SignerWithAddress;

let GameItemContract: GameItem;
let METR: METRToken;

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

const digitalKey =
  "2cd347f69a4cbb6545677cf5b3f50019370cdb858579315d08f15b23e89f4b15e4773d3eda46f393c98e57ef179babcd096c415301955bf043faa30c058807cbe233290dc1f69d9e77e5b5e222a27ca681b50d548b639875ae74844ee338cc567d0ce4b2e9f79fc19656fc601d23ff0180a0dda2d8a961bb9b378fa36b49e4d10fde93c8927a2a94be7ef4d41cff87878d8a104ade3d38a9c82e66148214568f27f4e995907407e10b271409cba8daf1f5be1c93929f38d3a8da3df97eab90d909482986edb05eec";

const deployFixture = async () => {
  // Relevant addresses
  [Deployer, Alice, Bob] = await ethers.getSigners();

  // Instance of the METR contract
  const METR_CONTRACT = await ethers.getContractFactory("METRToken", Deployer);
  METR = await METR_CONTRACT.deploy(digitalKey);

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
