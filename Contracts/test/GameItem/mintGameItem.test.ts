import { loadFixture } from "@nomicfoundation/hardhat-network-helpers";
import { expect } from "chai";
import deployFixture from "./deployFixture";

describe("GameItem mintGameItem tests", () => {
  // Doesn't have minter role
  it("Should revert if another address tries to mint GameItem", async () => {
    const { GameItemContract, Alice } = await loadFixture(deployFixture);
    await expect(
      GameItemContract.connect(Alice).mintGameItem(Alice.address, "TokenA")
    ).be.revertedWithCustomError(GameItemContract, "InvalidRole");
  });

  // Item doesn't exist
  it("Should revert if attempting to mint token that doesn't exist", async () => {
    const { GameItemContract, Alice } = await loadFixture(deployFixture);
    await expect(
      GameItemContract.mintGameItem(Alice.address, "TokenDoesntExist")
    ).be.revertedWithCustomError(GameItemContract, "ItemDoesntExist");
  });

  // Already owns the token
  it("Should revert if trying to mint a token that the buyer already owns", async () => {
    const { GameItemContract, METR, Alice, MINT_AMOUNT } = await loadFixture(
      deployFixture
    );

    await METR.mintToken(Alice.address, MINT_AMOUNT);
    await GameItemContract.mintGameItem(Alice.address, "TokenA");

    await expect(
      GameItemContract.mintGameItem(Alice.address, "TokenA")
    ).be.revertedWithCustomError(GameItemContract, "AlreadyOwned");
  });

  // Not enough METR
  it("Should revert if trying to mint a token when the buyer doesn't have enough METR", async () => {
    const { GameItemContract, Alice } = await loadFixture(deployFixture);

    await expect(
      GameItemContract.mintGameItem(Alice.address, "TokenA")
    ).be.revertedWithCustomError(GameItemContract, "InadequateMETR");
  });

  // Doesn't have prereqs
  it("Should revert if trying to mint a token when the buyer doesn't have any of the prereq tokens", async () => {
    const { GameItemContract, METR, Alice, MINT_AMOUNT } = await loadFixture(
      deployFixture
    );

    await METR.mintToken(Alice.address, MINT_AMOUNT);

    await expect(
      GameItemContract.mintGameItem(Alice.address, "TokenD")
    ).be.revertedWithCustomError(GameItemContract, "InadequatePreReqs");
  });

  it("Should revert if trying to mint a token when the buyer doesn't have all of the prereq tokens", async () => {
    const { GameItemContract, METR, Alice, MINT_AMOUNT_2 } = await loadFixture(
      deployFixture
    );

    await METR.mintToken(Alice.address, MINT_AMOUNT_2);
    await GameItemContract.mintGameItem(Alice.address, "TokenA");
    await GameItemContract.mintGameItem(Alice.address, "TokenB");

    await expect(
      GameItemContract.mintGameItem(Alice.address, "TokenD")
    ).be.revertedWithCustomError(GameItemContract, "InadequatePreReqs");
  });

  // Should correctly mint a token
  it("Should mint token if they have the right amount of METR", async () => {
    const { GameItemContract, METR, Alice, MINT_AMOUNT } = await loadFixture(
      deployFixture
    );

    await METR.mintToken(Alice.address, MINT_AMOUNT);

    await expect(GameItemContract.mintGameItem(Alice.address, "TokenA")).not.be
      .reverted;
  });

  it("Should mint token if they have the right amount of METR and prereq tokens", async () => {
    const { GameItemContract, METR, Alice, MINT_AMOUNT } = await loadFixture(
      deployFixture
    );

    await METR.mintToken(Alice.address, MINT_AMOUNT);
    await GameItemContract.mintGameItem(Alice.address, "TokenA");

    await expect(GameItemContract.mintGameItem(Alice.address, "TokenB")).not.be
      .reverted;
  });
});
