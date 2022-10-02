import { ethers } from "ethers";

export const generateRole = (role: string) =>
  ethers.utils.keccak256(ethers.utils.toUtf8Bytes(role));

export const MINTER_ROLE = generateRole("MINTER_ROLE");
export const BURNER_ROLE = generateRole("BURNER_ROLE");
