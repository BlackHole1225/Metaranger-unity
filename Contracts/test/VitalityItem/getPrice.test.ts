import { loadFixture } from "@nomicfoundation/hardhat-network-helpers";
import { expect } from "chai";
import { ethers } from "hardhat";
import deployFixture from "./deployfixture";

describe("VitalityItem getPrice tests", () => {
  it("Should get price of Health Item", async () => {
    const { VitalityItemContract } = await loadFixture(deployFixture);
    expect(await VitalityItemContract.getPrice("Health")).to.equal(
      ethers.utils.parseEther("200")
    );
  });

  it("Should get price of Armour Item", async () => {
    const { VitalityItemContract } = await loadFixture(deployFixture);
    expect(await VitalityItemContract.getPrice("Armour")).to.equal(
      ethers.utils.parseEther("210")
    );
  });

  it("Should get price of Shields Item", async () => {
    const { VitalityItemContract } = await loadFixture(deployFixture);
    expect(await VitalityItemContract.getPrice("Shields")).to.equal(
      ethers.utils.parseEther("220")
    );
  });

  it("Should identify that item doesn't exist on contract", async () => {
    const { VitalityItemContract } = await loadFixture(deployFixture);

    await expect(
      VitalityItemContract.getPrice("ItemDoesntExist")
    ).be.revertedWithCustomError(VitalityItemContract, "ItemDoesntExist");
  });
});
