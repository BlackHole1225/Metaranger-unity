import { ethers } from "hardhat";
import { expect } from "chai";
import { MINTER_ROLE, BURNER_ROLE } from "../../constants/roles";
import { GameItem, METRToken } from "../../typechain";
import { loadFixture } from "@nomicfoundation/hardhat-network-helpers";
import { SignerWithAddress } from "@nomiclabs/hardhat-ethers/signers";

describe("GameItem Tests", () => {
  let Deployer: SignerWithAddress;
  let Alice: SignerWithAddress;
  let Bob: SignerWithAddress;

  let GameItemContract: GameItem;
  let METR: METRToken;

  const MINT_AMOUNT = 1000;
  const MINT_AMOUNT_2 = 2000;
  const TokenAPrice = 200;
  const TokenBPrice = 500;
  const TokenCPrice = 500;
  const TokenDPrice = 1000;

  const TokenA = {
    itemName: "TokenA",
    prereqs: [],
    price: TokenAPrice,
    exists: true,
  };

  const TokenB = {
    itemName: "TokenB",
    prereqs: ["TokenA"],
    price: TokenBPrice,
    exists: true,
  };

  const TokenC = {
    itemName: "TokenC",
    prereqs: ["TokenA"],
    price: TokenCPrice,
    exists: true,
  };

  const TokenD = {
    itemName: "TokenD",
    prereqs: ["TokenB", "TokenC"],
    price: TokenDPrice,
    exists: true,
  };

  // Variables / State to be loaded for fixture before each test is run
  const deployFixture = async () => {
    // Relevant addresses
    [Deployer, Alice, Bob] = await ethers.getSigners();

    // Instance of the METR contract
    const METR_CONTRACT = await ethers.getContractFactory(
      "METRToken",
      Deployer
    );
    METR = await METR_CONTRACT.deploy();

    // Instance of the GameItem contract
    const GameItem_Contract = await ethers.getContractFactory(
      "GameItem",
      Deployer
    );

    GameItemContract = await GameItem_Contract.deploy(
      [TokenA, TokenB, TokenC, TokenD],
      METR.address
    );

    // Deployments
    await METR.deployed();
    await GameItemContract.deployed();

    // Roles
    await METR.grantRole(MINTER_ROLE, Deployer.address);
    await METR.grantRole(BURNER_ROLE, Deployer.address);
    await GameItemContract.grantRole(MINTER_ROLE, Deployer.address);
    await METR.grantRole(BURNER_ROLE, GameItemContract.address);

    // await METR.connect(Deployer).mintToken(Alice.address, MINT_AMOUNT);
    // await GameItemContract.mintGameItem(Alice.address, "TokenA");

    // Variables relevant for the tests
    return { GameItemContract, METR, Deployer, Alice, Bob };
  };

  // Internal function tests

  // Token Prices
  it("Should get price of existing Token A", async () => {
    const { GameItemContract } = await loadFixture(deployFixture);
    expect(await GameItemContract.getPrice("TokenA")).to.equal(200);
  });

  it("Should get price of existing Token B", async () => {
    const { GameItemContract } = await loadFixture(deployFixture);
    expect(await GameItemContract.getPrice("TokenB")).to.equal(500);
  });

  it("Should get price of existing Token D", async () => {
    const { GameItemContract } = await loadFixture(deployFixture);
    expect(await GameItemContract.getPrice("TokenD")).to.equal(1000);
  });

  it("getPrice should identify that token doesn't exist on contract", async () => {
    const { GameItemContract } = await loadFixture(deployFixture);
    await expect(
      GameItemContract.getPrice("TokenDoesntExist")
    ).be.revertedWithCustomError(GameItemContract, "ItemDoesntExist");
  });

  // Token Prereqs
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

  it("Should allow deployed to mint TokenA", async () => {
    const { GameItemContract, METR, Alice, Deployer, Bob } = await loadFixture(
      deployFixture
    );

    console.log(await METR.hasRole(MINTER_ROLE, Deployer.address));
    console.log(await GameItemContract.hasRole(MINTER_ROLE, Deployer.address));
    const _METR = METR.connect(Deployer);
    await _METR.mintToken(Alice.address, ethers.utils.parseEther("1000"));

    await METR.connect(Deployer).mintToken(Alice.address, MINT_AMOUNT);

    // await expect(METR.balanceOf(Alice.address)).not.be.reverted;
    console.log("Alice's balance ", await METR.balanceOf(Alice.address));
    await expect(
      GameItemContract.connect(Deployer).mintGameItem(Alice.address, "TokenA")
    ).be.revertedWithCustomError(GameItemContract, "ItemDoesntExist");
  });

  // Already owns the token
  // Not enough METR
  // Doesn't have prereqs
});
