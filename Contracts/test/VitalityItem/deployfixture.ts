import { SignerWithAddress } from "@nomiclabs/hardhat-ethers/signers";
import { ethers } from "hardhat";
import { BURNER_ROLE, MINTER_ROLE } from "../../constants/roles";
import { VitalityItem, METRToken } from "../../typechain";

let Deployer: SignerWithAddress;
let Alice: SignerWithAddress;
let Bob: SignerWithAddress;

let VitalityItemContract: VitalityItem;
let METR: METRToken;

const BIG_MINT_AMOUNT = ethers.utils.parseEther("1000");
const SMALL_MINT_AMOUNT = ethers.utils.parseEther("50");

const digitalKey =
  "2cd347f69a4cbb6545677cf5b3f50019370cdb858579315d08f15b23e89f4b15e4773d3eda46f393c98e57ef179babcd096c415301955bf043faa30c058807cbe233290dc1f69d9e77e5b5e222a27ca681b50d548b639875ae74844ee338cc567d0ce4b2e9f79fc19656fc601d23ff0180a0dda2d8a961bb9b378fa36b49e4d10fde93c8927a2a94be7ef4d41cff87878d8a104ade3d38a9c82e66148214568f27f4e995907407e10b271409cba8daf1f5be1c93929f38d3a8da3df97eab90d909482986edb05eec";

const deployFixture = async () => {
  // Relevant addresses
  [Deployer, Alice, Bob] = await ethers.getSigners();

  // Instance of the METR contract
  const METR_CONTRACT = await ethers.getContractFactory("METRToken", Deployer);
  METR = await METR_CONTRACT.deploy(digitalKey);

  // Instance of the VitalityItem contract
  const VitalityItem_Contract = await ethers.getContractFactory(
    "VitalityItem",
    Deployer
  );

  VitalityItemContract = await VitalityItem_Contract.deploy(METR.address);

  // Deployments
  await METR.deployed();
  await VitalityItemContract.deployed();

  // Roles
  await METR.grantRole(MINTER_ROLE, Deployer.address);
  await METR.grantRole(BURNER_ROLE, Deployer.address);
  await VitalityItemContract.grantRole(MINTER_ROLE, Deployer.address);
  await METR.grantRole(BURNER_ROLE, VitalityItemContract.address);

  // Variables relevant for the tests
  return {
    VitalityItemContract,
    METR,
    Deployer,
    Alice,
    Bob,
    BIG_MINT_AMOUNT,
    SMALL_MINT_AMOUNT,
  };
};

export default deployFixture;
