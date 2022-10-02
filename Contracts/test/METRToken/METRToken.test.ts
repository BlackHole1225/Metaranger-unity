import { ethers } from "hardhat";
import { SignerWithAddress } from "@nomiclabs/hardhat-ethers/signers";
import { expect } from "chai";
import { MINTER_ROLE, BURNER_ROLE } from "../../constants/roles";
import { METRToken } from "../../typechain-types";
import { loadFixture } from "@nomicfoundation/hardhat-network-helpers";
import { parseEther } from "ethers/lib/utils";

describe("METRToken Tests", () => {
  let Deployer: SignerWithAddress;
  let Alice: SignerWithAddress;
  let Bob: SignerWithAddress;
  let Carol: SignerWithAddress;

  let METR: METRToken;

  const MINT_AMOUNT = parseEther("100");
  const BURN_AMOUNT = parseEther("50");

  // Variables / State to be loaded for fixture before each test is run
  const deployFixture = async () => {
    [Deployer, Alice, Bob, Carol] = await ethers.getSigners();

    const METR_CONTRACT = await ethers.getContractFactory(
      "METRToken",
      Deployer
    );
    METR = await METR_CONTRACT.deploy();

    await METR.grantRole(MINTER_ROLE, Deployer.address);
    await METR.grantRole(BURNER_ROLE, Deployer.address);

    await METR.deployed();

    return { METR, Deployer, Alice, Bob, Carol };
  };

  it("Should allow deployer to mint METR tokens", async () => {
    const { METR, Alice } = await loadFixture(deployFixture);
    expect(await METR.balanceOf(Alice.address)).to.equal(0);
    await expect(
      METR.mintToken({ account: Alice.address, amount: MINT_AMOUNT })
    ).not.be.reverted;
    expect(await METR.balanceOf(Alice.address)).to.equal(MINT_AMOUNT);
  });

  it("Should revert if another address tries to mint METR tokens", async () => {
    const { METR, Bob, Carol } = await loadFixture(deployFixture);
    expect(await METR.balanceOf(Carol.address)).to.equal(0);
    await expect(
      METR.connect(Bob).mintToken({
        account: Carol.address,
        amount: MINT_AMOUNT,
      })
    ).be.revertedWithCustomError(METR, "InvalidRole");
    expect(await METR.balanceOf(Carol.address)).to.equal(0);
  });

  it("Should allow deployer to burn METR tokens", async () => {
    const { METR, Alice } = await loadFixture(deployFixture);
    await METR.mintToken({ account: Alice.address, amount: MINT_AMOUNT });
    await expect(
      METR.burnToken({ account: Alice.address, amount: BURN_AMOUNT })
    ).not.be.reverted;
    expect(await METR.balanceOf(Alice.address)).to.equal(BURN_AMOUNT);
  });

  it("Should revert if another address tried to burn METR tokens", async () => {
    const { METR, Alice, Bob } = await loadFixture(deployFixture);
    await expect(
      METR.connect(Bob).burnToken({
        account: Alice.address,
        amount: BURN_AMOUNT,
      })
    ).be.revertedWithCustomError(METR, "InvalidRole");
  });
});
