import { SignerWithAddress } from "@nomiclabs/hardhat-ethers/signers";
import { ethers } from "hardhat";
import { BURNER_ROLE, MINTER_ROLE } from "../../constants/roles";
import { METRToken } from "../../typechain";

let Deployer: SignerWithAddress;
let Alice: SignerWithAddress;
let Bob: SignerWithAddress;
let Carol: SignerWithAddress;

let METR: METRToken;

// Variables / State to be loaded for fixture before each test is run
const deployFixture = async () => {
  [Deployer, Alice, Bob, Carol] = await ethers.getSigners();

  const METR_CONTRACT = await ethers.getContractFactory("METRToken", Deployer);
  METR = await METR_CONTRACT.deploy();

  await METR.grantRole(MINTER_ROLE, Deployer.address);
  await METR.grantRole(BURNER_ROLE, Deployer.address);

  await METR.deployed();

  return { METR, Deployer, Alice, Bob, Carol };
};

export default deployFixture;
