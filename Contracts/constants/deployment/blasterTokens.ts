import { actual500, actual750, actual1000 } from "./prices";

export const BlasterAccuracyToken = {
  itemName: "BlasterAccuracy",
  prereqs: [],
  price: actual500,
  exists: true,
};

export const BlasterStoppingPowerToken = {
  itemName: "BlasterStoppingPower",
  prereqs: [],
  price: actual500,
  exists: true,
};

export const BlasterRapidFireToken = {
  itemName: "BlasterRapidFire",
  prereqs: ["BlasterStoppingPower"],
  price: actual750,
  exists: true,
};

export const BlasterAimSpeedToken = {
  itemName: "BlasterAimSpeed",
  prereqs: ["BlasterAccuracy"],
  price: actual750,
  exists: true,
};

export const BlasterCooldownToken = {
  itemName: "BlasterCooldown",
  prereqs: ["BlasterRapidFire"],
  price: actual1000,
  exists: true,
};
