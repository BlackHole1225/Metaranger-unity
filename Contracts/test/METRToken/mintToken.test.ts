import { loadFixture } from "@nomicfoundation/hardhat-network-helpers";
import { expect } from "chai";
import { parseEther } from "ethers/lib/utils";
import deployFixture from "./deployFixture";

describe("METRToken mintToken Tests", () => {
  const MINT_AMOUNT = parseEther("100");

  it("Should allow deployer to mint METR tokens", async () => {
    const { METR, Alice } = await loadFixture(deployFixture);
    expect(await METR.balanceOf(Alice.address)).to.equal(0);
    await expect(METR.mintToken(Alice.address, MINT_AMOUNT)).not.be.reverted;
    expect(await METR.balanceOf(Alice.address)).to.equal(MINT_AMOUNT);
  });

  it("Should revert if another address tries to mint METR tokens", async () => {
    const { METR, Bob, Carol } = await loadFixture(deployFixture);
    expect(await METR.balanceOf(Carol.address)).to.equal(0);
    await expect(
      METR.connect(Bob).mintToken(Carol.address, MINT_AMOUNT)
    ).be.revertedWithCustomError(METR, "InvalidRole");
    expect(await METR.balanceOf(Carol.address)).to.equal(0);
  });

  it("Should revert if attempting to mint non-positive number of tokens", async () => {
    const { METR, Alice } = await loadFixture(deployFixture);
    await expect(METR.mintToken(Alice.address, 0)).be.revertedWithCustomError(
      METR,
      "NotPositiveValue"
    );
  });
});
