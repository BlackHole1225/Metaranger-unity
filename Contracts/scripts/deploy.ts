import { ethers } from "hardhat";

async function main() {
  // const currentTimestampInSeconds = Math.round(Date.now() / 1000);
  // const ONE_YEAR_IN_SECS = 365 * 24 * 60 * 60;
  // const unlockTime = currentTimestampInSeconds + ONE_YEAR_IN_SECS;

  // const lockedAmount = ethers.utils.parseEther("1");

  // const Lock = await ethers.getContractFactory("Lock");
  // const lock = await Lock.deploy(unlockTime, { value: lockedAmount });

  // await lock.deployed();

  // console.log(`Lock with 1 ETH and unlock timestamp ${unlockTime} deployed to ${lock.address}`);

  const { DIGITAL_KEY } = process.env;

  //  BLASTER GAME ITEMS
  // ! There is no Blaster Base item, because the Blaster is unlocked by default

  const BlasterAccuracyToken = {
    itemName: "BlasterAccuracy",
    prereqs: [],
    price: 500,
    exists: true,
  };

  const BlasterStoppingPowerToken = {
    itemName: "BlasterStoppingPower",
    prereqs: [],
    price: 500,
    exists: true,
  };

  const BlasterRapidFireToken = {
    itemName: "BlasterRapidFire",
    prereqs: ["BlasterStoppingPower"],
    price: 750,
    exists: true,
  };

  const BlasterAimSpeedToken = {
    itemName: "BlasterAimSpeed",
    prereqs: ["BlasterAccuracy"],
    price: 750,
    exists: true,
  };

  const BlasterCooldownToken = {
    itemName: "BlasterCooldown",
    prereqs: ["BlasterRapidFire"],
    price: 1000,
    exists: true,
  };

  //  DISC LAUNCHER GAME ITEMS

  const DiscLauncherBaseToken = {
    itemName: "DiscLauncherBase",
    prereqs: [],
    price: 1000,
    exists: true,
  };

  const DiscLauncherAimSpeedToken = {
    itemName: "DiscLauncherAimSpeed",
    prereqs: ["DiscLauncherBase"],
    price: 500,
    exists: true,
  };

  const DiscLauncherChargeSpeed = {
    itemName: "DiscLauncherChargeSpeed",
    prereqs: ["DiscLauncherBase"],
    price: 500,
    exists: true,
  };

  const DiscLauncherStoppingPower = {
    itemName: "DiscLauncherStoppingPower",
    prereqs: ["DiscLauncherChargeSpeed"],
    price: 750,
    exists: true,
  };

  const DiscLauncherCooldownToken = {
    itemName: "DiscLauncherCooldown",
    prereqs: ["DiscLauncherAimSpeed"],
    price: 750,
    exists: true,
  };

  //  SHOTGUN GAME ITEMS

  const ShotgunBaseToken = {
    itemName: "ShotgunBase",
    prereqs: [],
    price: 2500,
    exists: true,
  };

  const ShotgunSpreadshotToken = {
    itemName: "ShotgunSpreadshot",
    prereqs: ["ShotgunBase"],
    price: 500,
    exists: true,
  };

  const ShotgunCooldownToken = {
    itemName: "ShotgunCooldown",
    prereqs: ["ShotgunBase"],
    price: 500,
    exists: true,
  };

  const ShotgunStoppingPowerToken = {
    itemName: "ShotgunStoppingPower",
    prereqs: ["ShotgunCooldown"],
    price: 750,
    exists: true,
  };

  const ShotgunExtraBarrelToken = {
    itemName: "ShotgunExtraBarrel",
    prereqs: ["ShotgunSpreadshot"],
    price: 750,
    exists: true,
  };

  const ShotgunAimSpeedToken = {
    itemName: "ShotgunAimSpeed",
    prereqs: ["ShotgunStoppingPower", "ShotgunExtraBarrel"],
    price: 1000,
    exists: true,
  };

  //  SNIPER GAME ITEMS

  const SniperBaseToken = {
    itemName: "SniperBase",
    prereqs: [],
    price: 5000,
    exists: true,
  };

  const SniperAimSpeedToken = {
    itemName: "SniperAimSpeed",
    prereqs: ["SniperBase"],
    price: 500,
    exists: true,
  };

  const SniperCooldownToken = {
    itemName: "SniperCooldown",
    prereqs: ["SniperBase"],
    price: 500,
    exists: true,
  };

  const SniperStoppingPowerToken = {
    itemName: "SniperStoppingPower",
    prereqs: ["SniperCooldown"],
    price: 750,
    exists: true,
  };

  const SniperZoomToken = {
    itemName: "SniperZoom",
    prereqs: ["SniperAimSpeed"],
    price: 750,
    exists: true,
  };

  //  JETPACK GAME ITEMS

  const JetpackBaseToken = {
    itemName: "JetpackBase",
    prereqs: [],
    price: 1000,
    exists: true,
  };

  const JetpackFlightSpeedToken = {
    itemName: "JetpackFlightSpeed",
    prereqs: ["JetpackBase"],
    price: 500,
    exists: true,
  };

  const JetpackDurationToken = {
    itemName: "JetpackDuration",
    prereqs: ["JetpackBase"],
    price: 500,
    exists: true,
  };

  const JetpackCooldownToken = {
    itemName: "JetpackCooldown",
    prereqs: ["JetpackDuration, JetpackFlightSpeed"],
    price: 750,
    exists: true,
  };

  // ALL THE TOKENS TOGETHER

  const GameItemContracts = [
    {
      contractName: "BlasterContract",
      gameItems: [
        BlasterAccuracyToken,
        BlasterAimSpeedToken,
        BlasterCooldownToken,
        BlasterRapidFireToken,
        BlasterStoppingPowerToken,
      ],
      exists: true,
    },
    {
      contractName: "DiscLauncherContract",
      gameItems: [
        DiscLauncherAimSpeedToken,
        DiscLauncherBaseToken,
        DiscLauncherChargeSpeed,
        DiscLauncherCooldownToken,
        DiscLauncherStoppingPower,
      ],
      exists: true,
    },
    {
      contractName: "ShotgunContract",
      gameItems: [
        ShotgunAimSpeedToken,
        ShotgunBaseToken,
        ShotgunCooldownToken,
        ShotgunExtraBarrelToken,
        ShotgunSpreadshotToken,
        ShotgunStoppingPowerToken,
      ],
      exists: true,
    },
    {
      contractName: "SniperContract",
      gameItems: [
        SniperAimSpeedToken,
        SniperBaseToken,
        SniperCooldownToken,
        SniperStoppingPowerToken,
        SniperZoomToken,
      ],
      exists: true,
    },
    {
      contractName: "JetpackContract",
      gameItems: [
        JetpackBaseToken,
        JetpackCooldownToken,
        JetpackDurationToken,
        JetpackFlightSpeedToken,
      ],
      exists: true,
    },
  ];
}

// We recommend this pattern to be able to use async/await everywhere
// and properly handle errors.
main().catch((error) => {
  console.error(error);
  process.exitCode = 1;
});
