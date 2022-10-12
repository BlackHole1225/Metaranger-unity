import { loadFixture } from "@nomicfoundation/hardhat-network-helpers";
import { expect } from "chai";
import deployFixture from "./deployFixture";

describe("GameItem ownsGameItem tests", () => {
  it("Should identify that user doesn't currently own specified token", async () => {
    const { GameItemContract, Alice } = await loadFixture(deployFixture);

    expect(await GameItemContract.ownsGameItem(Alice.address, "TokenA")).be.eq(
      false
    );
  });

  it("Should identify that user does currently own specified token", async () => {
    const { GameItemContract, METR, Alice, MINT_AMOUNT } = await loadFixture(
      deployFixture
    );

    await METR.mintToken(Alice.address, MINT_AMOUNT);
    await GameItemContract.mintGameItem(Alice.address, "TokenA");

    expect(await GameItemContract.ownsGameItem(Alice.address, "TokenA")).be.eq(
      true
    );
  });

  it("Should identify if checking a token that doesn't exist", async () => {
    const { GameItemContract, Alice } = await loadFixture(deployFixture);
    await expect(
      GameItemContract.ownsGameItem(Alice.address, "TokenDoesntExist")
    ).be.revertedWithCustomError(GameItemContract, "ItemDoesntExist");
  });
});
