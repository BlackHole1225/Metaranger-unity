import { loadFixture } from "@nomicfoundation/hardhat-network-helpers";
import { expect } from "chai";
import { BytesLike } from "ethers";
import { PromiseOrValue } from "../../typechain/common";
import deployFixture from "./deployFixture";

// The interfaceID for a ERC1155 and ERC721 according to this:
// https://stackoverflow.com/questions/69706835/how-to-check-if-the-token-on-opensea-is-erc721-or-erc1155-using-node-js
const ERC1155interfaceID = 0xd9b67a26;
const ERC721interfaceID = 0x80ac58cd;

describe("GameItem supportInterface tests", () => {
  it("Should identify that this contract supports the ERC1155 interface", async () => {
    const { GameItemContract } = await loadFixture(deployFixture);

    expect(
      await GameItemContract.supportsInterface(
        ERC1155interfaceID as unknown as PromiseOrValue<BytesLike>
      )
    ).be.eq(true);
  });

  it("Should identify that this contract doesn't support the ERC721 interface", async () => {
    const { GameItemContract } = await loadFixture(deployFixture);

    expect(
      await GameItemContract.supportsInterface(
        ERC721interfaceID as unknown as PromiseOrValue<BytesLike>
      )
    ).be.eq(false);
  });
});
