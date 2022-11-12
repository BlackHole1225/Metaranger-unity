import { loadFixture } from "@nomicfoundation/hardhat-network-helpers";
import { expect } from "chai";
import { DEFAULT_ADMIN_ROLE, MINTER_ROLE } from "../../constants/roles";
import deployFixture from "./deployfixture";

describe("VitalityItem constructor tests", () => {
  it("Should assign deployer the MINTER_ROLE", async () => {
    const { Deployer, VitalityItemContract } = await loadFixture(deployFixture);

    expect(
      await VitalityItemContract.hasRole(MINTER_ROLE, Deployer.address)
    ).to.eq(true);
  });

  it("Should assign deployer the DEFAULT_ADMIN_ROLE", async () => {
    const { Deployer, VitalityItemContract } = await loadFixture(deployFixture);

    expect(
      await VitalityItemContract.hasRole(DEFAULT_ADMIN_ROLE, Deployer.address)
    ).to.eq(true);
  });

  it("Should not assign another address the MINTER_ROLE", async () => {
    const { Alice, VitalityItemContract } = await loadFixture(deployFixture);

    expect(
      await VitalityItemContract.hasRole(MINTER_ROLE, Alice.address)
    ).to.eq(false);
  });

  it("Should not assign another address the DEFAULT_ADMIN_ROLE", async () => {
    const { Alice, VitalityItemContract } = await loadFixture(deployFixture);

    expect(
      await VitalityItemContract.hasRole(DEFAULT_ADMIN_ROLE, Alice.address)
    ).to.eq(false);
  });
});
