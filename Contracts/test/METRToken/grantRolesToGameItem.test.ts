import { loadFixture } from "@nomicfoundation/hardhat-network-helpers";
import { expect } from "chai";
import deployFixture from "./deployFixture";

describe("METRToken grantRolesToGameItem Tests", () => {
  it("Should not accept invalid digital key", async () => {
    const { METR, Alice } = await loadFixture(deployFixture);
    await expect(
      METR.grantRolesToGameItem(Alice.address, "InvalidKey")
    ).be.revertedWithCustomError(METR, "InvalidDigitalKey");
  });

  it("Should allow granting of role", async () => {
    const { METR, Alice, digitalKey } = await loadFixture(deployFixture);
    await expect(METR.grantRolesToGameItem(Alice.address, digitalKey)).not.be
      .reverted;
  });
});
