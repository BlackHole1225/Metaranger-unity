import { loadFixture } from "@nomicfoundation/hardhat-network-helpers";
import { expect } from "chai";
import deployFixture from "./deployFixture";

describe("GameManager mintMETR tests", () => {
  it("Should reject invalid key", async () => {
    const { GameManagerContract, Alice, MINT_AMOUNT } = await loadFixture(
      deployFixture
    );

    await expect(
      GameManagerContract.mintMETR(Alice.address, MINT_AMOUNT, "InvalidKey")
    ).be.revertedWithCustomError(GameManagerContract, "InvalidDigitalKey");
  });
  it("Should allow minting of METR", async () => {
    const { GameManagerContract, Alice, MINT_AMOUNT, digitalKey } =
      await loadFixture(deployFixture);
    await expect(
      GameManagerContract.mintMETR(Alice.address, MINT_AMOUNT, digitalKey)
    ).not.be.reverted;
  });
});
