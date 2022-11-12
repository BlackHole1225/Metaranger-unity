import { loadFixture } from "@nomicfoundation/hardhat-network-helpers";
import { expect } from "chai";
import deployFixture from "./deployfixture";

describe("VitalityItem getBalance tests", () => {
  // Item doesn't exist
  it("Should revert if the item doesn't exist", async () => {
    const { VitalityItemContract, Alice } = await loadFixture(deployFixture);
    await expect(
      VitalityItemContract.getBalance(Alice.address, "ItemDoesntExist")
    ).be.revertedWithCustomError(VitalityItemContract, "ItemDoesntExist");
  });

  // User has 0 balance of the item
  it("Should identify if the user doesn't have any of the item", async () => {
    const { VitalityItemContract, Alice } = await loadFixture(deployFixture);
    expect(
      await VitalityItemContract.getBalance(Alice.address, "Health")
    ).to.equal(0);
  });

  // User's balance > 0
  it("Should identify if the user has more than zero of the item", async () => {
    const { VitalityItemContract, Alice, METR, BIG_MINT_AMOUNT } =
      await loadFixture(deployFixture);
    await METR.mintToken(Alice.address, BIG_MINT_AMOUNT);

    await VitalityItemContract.mintVitalityItem(Alice.address, "Health");

    expect(
      await VitalityItemContract.getBalance(Alice.address, "Health")
    ).to.equal(1);
  });
});
