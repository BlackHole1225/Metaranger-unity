import { loadFixture } from "@nomicfoundation/hardhat-network-helpers";
import { expect } from "chai";
import deployFixture from "./deployFixture";

describe("GameManager createGameItem tests", () => {
  it("Should reject invalid key", async () => {
    const { GameManagerContract, TokenA } = await loadFixture(deployFixture);

    await expect(
      GameManagerContract.createGameItem(TokenA, "InvalidKey")
    ).be.revertedWithCustomError(GameManagerContract, "InvalidDigitalKey");
  });

  it("Should reject game item that already exists", async () => {
    const { GameManagerContract, TokenA, digitalKey } = await loadFixture(
      deployFixture
    );
    await GameManagerContract.createGameItem(TokenA, digitalKey);

    await expect(
      GameManagerContract.createGameItem(TokenA, digitalKey)
    ).be.revertedWithCustomError(GameManagerContract, "GameItemAlreadyExists");
  });

  it("Should create game item", async () => {
    const { GameManagerContract, TokenA, digitalKey } = await loadFixture(
      deployFixture
    );
    await expect(GameManagerContract.createGameItem(TokenA, digitalKey)).not.be
      .reverted;
  });
});
