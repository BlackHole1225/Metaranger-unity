import { loadFixture } from "@nomicfoundation/hardhat-network-helpers";
import { expect } from "chai";
import deployFixture from "./deployFixture";

describe("GameManager purchaseGameItem tests", () => {
  it("Should reject invalid key", async () => {
    const { GameManagerContract, Alice } = await loadFixture(deployFixture);
    await expect(
      GameManagerContract.purchaseGameItem(
        Alice.address,
        "TokenA",
        "TokenA1",
        "NotValidKey"
      )
    ).be.revertedWithCustomError(GameManagerContract, "InvalidDigitalKey");
  });
  it("Should reject invalid contract name", async () => {
    const { GameManagerContract, Alice, digitalKey } = await loadFixture(
      deployFixture
    );
    await expect(
      GameManagerContract.purchaseGameItem(
        Alice.address,
        "ContractThatDoesntExist",
        "TokenA",
        digitalKey
      )
    ).be.revertedWithCustomError(GameManagerContract, "ContractDoesntExist");
  });
  it("Should mint game item", async () => {
    const { GameManagerContract, Alice, digitalKey, MINT_AMOUNT } =
      await loadFixture(deployFixture);
    await GameManagerContract.mintMETR(Alice.address, MINT_AMOUNT, digitalKey);
    await expect(
      GameManagerContract.purchaseGameItem(
        Alice.address,
        "TokenA",
        "TokenA1",
        digitalKey
      )
    ).not.be.reverted;
  });
});
