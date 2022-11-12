import { loadFixture } from "@nomicfoundation/hardhat-network-helpers";
import { expect } from "chai";
import deployFixture from "./deployFixture";

describe("GameManager purchaseVitalityItem tests", () => {
  it("Should reject invalid key", async () => {
    const { GameManagerContract, Alice } = await loadFixture(deployFixture);
    await expect(
      GameManagerContract.purchaseVitalityItem(
        Alice.address,
        "Health",
        "InvalidKey"
      )
    ).be.revertedWithCustomError(GameManagerContract, "InvalidDigitalKey");
  });

  it("Should mint Vitality Item", async () => {
    const { GameManagerContract, Alice, digitalKey, MINT_AMOUNT } =
      await loadFixture(deployFixture);
    await GameManagerContract.mintMETR(Alice.address, MINT_AMOUNT, digitalKey);
    await expect(
      GameManagerContract.purchaseVitalityItem(
        Alice.address,
        "Health",
        digitalKey
      )
    ).not.be.reverted;
  });
});
