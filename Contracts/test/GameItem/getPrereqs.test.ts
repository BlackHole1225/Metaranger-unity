import { loadFixture } from "@nomicfoundation/hardhat-network-helpers";
import { expect } from "chai";
import deployFixture from "./deployFixture";

describe("GameItem getPrereqs tests", () => {
  it("Should get prereqs of existing Token A", async () => {
    const { GameItemContract } = await loadFixture(deployFixture);
    expect(await GameItemContract.getPrereqs("TokenA")).to.deep.equal([]);
  });

  it("Should get prereqs of existing Token B", async () => {
    const { GameItemContract } = await loadFixture(deployFixture);
    expect(await GameItemContract.getPrereqs("TokenB")).to.deep.equal([
      "TokenA",
    ]);
  });

  it("Should get prereqs of existing Token D", async () => {
    const { GameItemContract } = await loadFixture(deployFixture);
    expect(await GameItemContract.getPrereqs("TokenD")).to.deep.equal([
      "TokenB",
      "TokenC",
    ]);
  });

  it("getPrereqs should identify that token doesn't exist on contract", async () => {
    const { GameItemContract } = await loadFixture(deployFixture);
    await expect(
      GameItemContract.getPrereqs("TokenDoesntExist")
    ).be.revertedWithCustomError(GameItemContract, "ItemDoesntExist");
  });
});
