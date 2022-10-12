import { loadFixture } from "@nomicfoundation/hardhat-network-helpers";
import { expect } from "chai";
import { parseEther } from "ethers/lib/utils";
import deployFixture from "./deployFixture";

describe("METRToken burnToken Tests", () => {
  const MINT_AMOUNT = parseEther("100");
  const BURN_AMOUNT = parseEther("50");

  it("Should allow deployer to burn METR tokens", async () => {
    const { METR, Alice } = await loadFixture(deployFixture);
    await METR.mintToken(Alice.address, MINT_AMOUNT);
    await expect(METR.burnToken(Alice.address, BURN_AMOUNT)).not.be.reverted;
    expect(await METR.balanceOf(Alice.address)).to.equal(BURN_AMOUNT);
  });

  it("Should revert if another address tried to burn METR tokens", async () => {
    const { METR, Alice, Bob } = await loadFixture(deployFixture);
    await expect(
      METR.connect(Bob).burnToken(Alice.address, BURN_AMOUNT)
    ).be.revertedWithCustomError(METR, "InvalidRole");
  });
});
