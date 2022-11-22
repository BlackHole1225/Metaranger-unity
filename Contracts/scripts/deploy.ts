import { ethers } from "hardhat";
import {
  BlasterContract,
  DiscLauncherContract,
  JetpackContract,
  ShotgunContract,
  SniperContract,
} from "../constants/deployment/gameItems";
import { BURNER_ROLE, MINTER_ROLE } from "../constants/roles";
require("dotenv").config({ path: __dirname + "/.env" });

async function main() {
  const { DIGITAL_KEY } = process.env;

  if (DIGITAL_KEY) {
    console.log("\n\tThere is a digital key, starting deployment");

    // Instantiate the METR Contract
    const METR_CONTRACT = await ethers.getContractFactory("METRToken");
    const METR = await METR_CONTRACT.deploy(DIGITAL_KEY);
    console.log("\n\tMETR contract instantiated ", METR.address);

    // Instantiate VitalityItem Contract
    const VitalityItemContract = await ethers.getContractFactory(
      "VitalityItem"
    );
    const VitalityItems = await VitalityItemContract.deploy(METR.address);
    console.log(
      "\n\tVitalityItems contract instantiated ",
      VitalityItems.address
    );

    // Instantiate the GameManager Contract
    const GameManagerContract = await ethers.getContractFactory("GameManager");
    const GameManager = await GameManagerContract.deploy(
      DIGITAL_KEY,
      METR.address,
      VitalityItems.address
    );
    console.log("\n\tGameManager contract instantiated ", GameManager.address);

    // Deployments
    await METR.deployed();
    await VitalityItems.deployed();
    await GameManager.deployed();
    console.log("\n\tAll initial contracts deployed");

    // Roles
    await METR.grantRole(MINTER_ROLE, GameManager.address);
    await METR.grantRole(BURNER_ROLE, GameManager.address);
    await METR.grantRole(BURNER_ROLE, VitalityItems.address);
    await VitalityItems.grantRole(MINTER_ROLE, GameManager.address);
    console.log("\n\tAll relevant roles assigned");

    // Deploy Game Items
    await GameManager.createGameItem(BlasterContract, DIGITAL_KEY);
    console.log("\n\tBlaster Contract deployed");
    const blasterReference = await GameManager.gameItemContracts(
      "BlasterContract"
    );
    console.log(
      "\tBlaster Game Item Address ",
      blasterReference.contractAddress
    );

    await GameManager.createGameItem(DiscLauncherContract, DIGITAL_KEY);
    console.log("\n\tDiscLauncher Contract deployed");
    const discLauncherReference = await GameManager.gameItemContracts(
      "DiscLauncherContract"
    );
    console.log(
      "\tDiscLauncher Game Item Address ",
      discLauncherReference.contractAddress
    );

    await GameManager.createGameItem(ShotgunContract, DIGITAL_KEY);
    console.log("\n\tShotgun Contract deployed");
    const shotgunReference = await GameManager.gameItemContracts(
      "ShotgunContract"
    );
    console.log(
      "\tShotgun Game Item Address ",
      shotgunReference.contractAddress
    );

    await GameManager.createGameItem(SniperContract, DIGITAL_KEY);
    console.log("\n\tSniper Contract deployed");
    const sniperReference = await GameManager.gameItemContracts(
      "SniperContract"
    );
    console.log("\tSniper Game Item Address ", sniperReference.contractAddress);

    await GameManager.createGameItem(JetpackContract, DIGITAL_KEY);
    console.log("\n\tJetpack Contract deployed");
    const jetpackReference = await GameManager.gameItemContracts(
      "JetpackContract"
    );
    console.log(
      "\tJetpack Game Item Address ",
      jetpackReference.contractAddress
    );
  } else {
    console.log("Something went wrong with the digital key");
    console.log("DIGITAL_KEY", DIGITAL_KEY);
  }
}

// We recommend this pattern to be able to use async/await everywhere
// and properly handle errors.
main().catch((error) => {
  console.error(error);
  process.exitCode = 1;
});
