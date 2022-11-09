using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItemContract
{
    public string Address { get; set; }
    public string ABI { get; set; }

    public GameItemContract(string address, string abi)
    {
        this.Address = address;
        this.ABI = abi;
    }
}