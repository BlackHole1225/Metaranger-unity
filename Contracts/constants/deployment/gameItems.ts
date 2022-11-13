import {
  BlasterAccuracyToken,
  BlasterAimSpeedToken,
  BlasterCooldownToken,
  BlasterRapidFireToken,
  BlasterStoppingPowerToken,
} from "./blasterTokens";
import {
  DiscLauncherAimSpeedToken,
  DiscLauncherBaseToken,
  DiscLauncherChargeSpeed,
  DiscLauncherCooldownToken,
  DiscLauncherStoppingPower,
} from "./discLauncherTokens";
import {
  JetpackBaseToken,
  JetpackCooldownToken,
  JetpackDurationToken,
  JetpackFlightSpeedToken,
} from "./jetpackTokens";
import {
  ShotgunAimSpeedToken,
  ShotgunBaseToken,
  ShotgunCooldownToken,
  ShotgunExtraBarrelToken,
  ShotgunSpreadshotToken,
  ShotgunStoppingPowerToken,
} from "./shotgunTokens";
import {
  SniperAimSpeedToken,
  SniperBaseToken,
  SniperCooldownToken,
  SniperStoppingPowerToken,
  SniperZoomToken,
} from "./sniperTokens";

export const BlasterContract = {
  contractName: "BlasterContract",
  gameItems: [
    BlasterAccuracyToken,
    BlasterAimSpeedToken,
    BlasterCooldownToken,
    BlasterRapidFireToken,
    BlasterStoppingPowerToken,
  ],
};

export const DiscLauncherContract = {
  contractName: "DiscLauncherContract",
  gameItems: [
    DiscLauncherAimSpeedToken,
    DiscLauncherBaseToken,
    DiscLauncherChargeSpeed,
    DiscLauncherCooldownToken,
    DiscLauncherStoppingPower,
  ],
};

export const ShotgunContract = {
  contractName: "ShotgunContract",
  gameItems: [
    ShotgunAimSpeedToken,
    ShotgunBaseToken,
    ShotgunCooldownToken,
    ShotgunExtraBarrelToken,
    ShotgunSpreadshotToken,
    ShotgunStoppingPowerToken,
  ],
};

export const SniperContract = {
  contractName: "SniperContract",
  gameItems: [
    SniperAimSpeedToken,
    SniperBaseToken,
    SniperCooldownToken,
    SniperStoppingPowerToken,
    SniperZoomToken,
  ],
};

export const JetpackContract = {
  contractName: "JetpackContract",
  gameItems: [
    JetpackBaseToken,
    JetpackCooldownToken,
    JetpackDurationToken,
    JetpackFlightSpeedToken,
  ],
};
