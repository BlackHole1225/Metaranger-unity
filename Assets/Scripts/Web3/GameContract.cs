using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContract
{
    public string Address { get; set; }
    public string ABI { get; set; }

    public GameContract(string address, string abi)
    {
        this.Address = address;
        this.ABI = abi;
    }
}

