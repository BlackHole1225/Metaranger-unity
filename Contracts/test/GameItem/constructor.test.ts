import { loadFixture } from "@nomicfoundation/hardhat-network-helpers";
import { expect } from "chai";
import { ethers } from "hardhat";
import { DEFAULT_ADMIN_ROLE, MINTER_ROLE } from "../../constants/roles";
import { GameItem, GameItem__factory } from "../../typechain";
import deployFixture from "./deployFixture";

describe("GameItem constructor tests", () => {
  it("Should assign deployer the MINTER_ROLE", async () => {
    const { Deployer, GameItemContract } = await loadFixture(deployFixture);

    expect(await GameItemContract.hasRole(MINTER_ROLE, Deployer.address)).to.eq(
      true
    );
  });

  it("Should assign deployer the DEFAULT_ADMIN_ROLE", async () => {
    const { Deployer, GameItemContract } = await loadFixture(deployFixture);

    expect(
      await GameItemContract.hasRole(DEFAULT_ADMIN_ROLE, Deployer.address)
    ).to.eq(true);
  });

  it("Should not assign another address the MINTER_ROLE", async () => {
    const { Alice, GameItemContract } = await loadFixture(deployFixture);

    expect(await GameItemContract.hasRole(MINTER_ROLE, Alice.address)).to.eq(
      false
    );
  });

  it("Should not assign another address the DEFAULT_ADMIN_ROLE", async () => {
    const { Alice, GameItemContract } = await loadFixture(deployFixture);

    expect(
      await GameItemContract.hasRole(DEFAULT_ADMIN_ROLE, Alice.address)
    ).to.eq(false);
  });

  it("Should identify invalid token price", async () => {
    const { METR, Deployer } = await loadFixture(deployFixture);

    const GameItem_Contract = await ethers.getContractFactory(
      "GameItem",
      Deployer
    );

    const InvalidPriceToken = {
      itemName: "InvalidPriceToken",
      prereqs: [],
      price: 0,
      exists: true,
    };

    await expect(
      GameItem_Contract.deploy([InvalidPriceToken], METR.address)
    ).be.revertedWithCustomError(GameItem_Contract, "InadequatePrice");
  });

  it("Should identify there are no tokens to initialise", async () => {
    const { METR, Deployer } = await loadFixture(deployFixture);

    const GameItem_Contract = (await ethers.getContractFactory(
      "GameItem",
      Deployer
    )) as GameItem__factory;

    await expect(
      GameItem_Contract.deploy([], METR.address, {
        gasLimit: 30000000,
      })
    ).be.revertedWithCustomError(GameItem_Contract, "NoItemsToInitialise");
  });
});
