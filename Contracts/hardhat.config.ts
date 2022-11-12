import { HardhatUserConfig } from "hardhat/config";
import "@nomicfoundation/hardhat-toolbox";
require("dotenv").config();
require("hardhat-tracer");

const { API_URL, PRIVATE_KEY } = process.env;

const config: HardhatUserConfig = {
  solidity: "0.8.17",
  typechain: {
    outDir: "typechain",
    target: "ethers-v5",
  },
  // networks: {
  //   mumbai: {
  //     url: API_URL,
  //     accounts: [`0x${PRIVATE_KEY}`],
  //   },
  // },
};

export default config;
