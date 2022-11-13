import { actual500, actual750, actual1000 } from "./prices";

export const JetpackBaseToken = {
  itemName: "JetpackBase",
  prereqs: [],
  price: actual1000,
  exists: true,
};

export const JetpackFlightSpeedToken = {
  itemName: "JetpackFlightSpeed",
  prereqs: ["JetpackBase"],
  price: actual500,
  exists: true,
};

export const JetpackDurationToken = {
  itemName: "JetpackDuration",
  prereqs: ["JetpackBase"],
  price: actual500,
  exists: true,
};

export const JetpackCooldownToken = {
  itemName: "JetpackCooldown",
  prereqs: ["JetpackDuration, JetpackFlightSpeed"],
  price: actual750,
  exists: true,
};
