import { actual500, actual750, actual1000 } from "./prices";

export const DiscLauncherBaseToken = {
  itemName: "DiscLauncherBase",
  prereqs: [],
  price: actual1000,
  exists: true,
};

export const DiscLauncherAimSpeedToken = {
  itemName: "DiscLauncherAimSpeed",
  prereqs: ["DiscLauncherBase"],
  price: actual500,
  exists: true,
};

export const DiscLauncherChargeSpeed = {
  itemName: "DiscLauncherChargeSpeed",
  prereqs: ["DiscLauncherBase"],
  price: actual500,
  exists: true,
};

export const DiscLauncherStoppingPower = {
  itemName: "DiscLauncherStoppingPower",
  prereqs: ["DiscLauncherChargeSpeed"],
  price: actual750,
  exists: true,
};

export const DiscLauncherCooldownToken = {
  itemName: "DiscLauncherCooldown",
  prereqs: ["DiscLauncherAimSpeed"],
  price: actual750,
  exists: true,
};
