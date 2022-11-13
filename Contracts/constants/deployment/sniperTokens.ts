import { actual500, actual750, actual5000 } from "./prices";

export const SniperBaseToken = {
  itemName: "SniperBase",
  prereqs: [],
  price: actual5000,
  exists: true,
};

export const SniperAimSpeedToken = {
  itemName: "SniperAimSpeed",
  prereqs: ["SniperBase"],
  price: actual500,
  exists: true,
};

export const SniperCooldownToken = {
  itemName: "SniperCooldown",
  prereqs: ["SniperBase"],
  price: actual500,
  exists: true,
};

export const SniperStoppingPowerToken = {
  itemName: "SniperStoppingPower",
  prereqs: ["SniperCooldown"],
  price: actual750,
  exists: true,
};

export const SniperZoomToken = {
  itemName: "SniperZoom",
  prereqs: ["SniperAimSpeed"],
  price: actual750,
  exists: true,
};
