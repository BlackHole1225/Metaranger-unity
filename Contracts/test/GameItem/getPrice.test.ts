import { loadFixture } from "@nomicfoundation/hardhat-network-helpers";
import { expect } from "chai";
import { ethers } from "hardhat";
import deployFixture from "./deployFixture";

describe("GameItem getPrice tests", () => {
  it("Should get price of existing Token A", async () => {
    const { GameItemContract } = await loadFixture(deployFixture);
    expect(await GameItemContract.getPrice("TokenA")).to.equal(
      ethers.utils.parseEther("200")
    );
  });

  it("Should get price of existing Token B", async () => {
    const { GameItemContract } = await loadFixture(deployFixture);
    expect(await GameItemContract.getPrice("TokenB")).to.equal(
      ethers.utils.parseEther("500")
    );
  });

  it("Should get price of existing Token D", async () => {
    const { GameItemContract } = await loadFixture(deployFixture);
    expect(await GameItemContract.getPrice("TokenD")).to.equal(
      ethers.utils.parseEther("1000")
    );
  });

  it("getPrice should identify that token doesn't exist on contract", async () => {
    const { GameItemContract } = await loadFixture(deployFixture);
    await expect(
      GameItemContract.getPrice("TokenDoesntExist")
    ).be.revertedWithCustomError(GameItemContract, "ItemDoesntExist");
  });
});
