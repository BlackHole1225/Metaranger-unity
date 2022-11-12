import { loadFixture } from "@nomicfoundation/hardhat-network-helpers";
import { expect } from "chai";
import deployFixture from "./deployfixture";

describe("VitalityItem mintVitalityItem tests", () => {
  // Doesn't have minter role
  it("Should revert if another address tries to mint VitalityItem", async () => {
    const { VitalityItemContract, Alice } = await loadFixture(deployFixture);
    await expect(
      VitalityItemContract.connect(Alice).mintVitalityItem(
        Alice.address,
        "Health"
      )
    ).be.revertedWithCustomError(VitalityItemContract, "InvalidRole");
  });

  // Item doesn't exist
  it("Should revert if attempting to mint item that doens't exist", async () => {
    const { VitalityItemContract, Alice } = await loadFixture(deployFixture);
    await expect(
      VitalityItemContract.mintVitalityItem(Alice.address, "ItemDoesntExist")
    ).be.revertedWithCustomError(VitalityItemContract, "ItemDoesntExist");
  });

  // Not enough METR
  it("Should revert if trying to mint a token when the buyer doesn't have enough METR", async () => {
    const { VitalityItemContract, Alice } = await loadFixture(deployFixture);
    await expect(
      VitalityItemContract.mintVitalityItem(Alice.address, "Health")
    ).be.revertedWithCustomError(VitalityItemContract, "InadequateMETR");
  });

  // Should correctly mint a vitality item
  it("Should mint item", async () => {
    const { VitalityItemContract, METR, Alice, BIG_MINT_AMOUNT } =
      await loadFixture(deployFixture);

    await METR.mintToken(Alice.address, BIG_MINT_AMOUNT);

    await expect(VitalityItemContract.mintVitalityItem(Alice.address, "Health"))
      .not.be.reverted;
  });
});
