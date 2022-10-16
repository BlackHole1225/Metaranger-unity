import { SignerWithAddress } from "@nomiclabs/hardhat-ethers/signers";
import { ethers } from "hardhat";
import { BURNER_ROLE, MINTER_ROLE } from "../../constants/roles";
import { METRToken } from "../../typechain";

let Deployer: SignerWithAddress;
let Alice: SignerWithAddress;
let Bob: SignerWithAddress;
let Carol: SignerWithAddress;

let METR: METRToken;

const digitalKey =
  "2cd347f69a4cbb6545677cf5b3f50019370cdb858579315d08f15b23e89f4b15e4773d3eda46f393c98e57ef179babcd096c415301955bf043faa30c058807cbe233290dc1f69d9e77e5b5e222a27ca681b50d548b639875ae74844ee338cc567d0ce4b2e9f79fc19656fc601d23ff0180a0dda2d8a961bb9b378fa36b49e4d10fde93c8927a2a94be7ef4d41cff87878d8a104ade3d38a9c82e66148214568f27f4e995907407e10b271409cba8daf1f5be1c93929f38d3a8da3df97eab90d909482986edb05eec";

// Variables / State to be loaded for fixture before each test is run
const deployFixture = async () => {
  [Deployer, Alice, Bob, Carol] = await ethers.getSigners();

  const METR_CONTRACT = await ethers.getContractFactory("METRToken", Deployer);
  METR = await METR_CONTRACT.deploy(digitalKey);

  await METR.grantRole(MINTER_ROLE, Deployer.address);
  await METR.grantRole(BURNER_ROLE, Deployer.address);

  await METR.deployed();

  return { METR, Deployer, Alice, Bob, Carol, digitalKey };
};

export default deployFixture;
