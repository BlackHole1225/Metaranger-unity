import { actual500, actual750, actual1000, actual2500 } from "./prices";

export const ShotgunBaseToken = {
  itemName: "ShotgunBase",
  prereqs: [],
  price: actual2500,
  exists: true,
};

export const ShotgunSpreadshotToken = {
  itemName: "ShotgunSpreadshot",
  prereqs: ["ShotgunBase"],
  price: actual500,
  exists: true,
};

export const ShotgunCooldownToken = {
  itemName: "ShotgunCooldown",
  prereqs: ["ShotgunBase"],
  price: actual500,
  exists: true,
};

export const ShotgunStoppingPowerToken = {
  itemName: "ShotgunStoppingPower",
  prereqs: ["ShotgunCooldown"],
  price: actual750,
  exists: true,
};

export const ShotgunExtraBarrelToken = {
  itemName: "ShotgunExtraBarrel",
  prereqs: ["ShotgunSpreadshot"],
  price: actual750,
  exists: true,
};

export const ShotgunAimSpeedToken = {
  itemName: "ShotgunAimSpeed",
  prereqs: ["ShotgunStoppingPower", "ShotgunExtraBarrel"],
  price: actual1000,
  exists: true,
};
