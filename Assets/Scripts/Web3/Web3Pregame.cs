using System;
using System.Collections;
using UnityEngine;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using System.Threading.Tasks;



public class Web3Pregame : MonoBehaviour
{
    [Tooltip("Reference to the Web3Manager")]
    public Web3Manager Web3Manager;

    Health playerHealth;

    float onChainHealth;
    float onChainArmour;
    float onChainShields;

    async void Start()
    {
        PlayerCharacterController playerCharacterController =
            GameObject.FindObjectOfType<PlayerCharacterController>();
        playerHealth = playerCharacterController.GetComponent<Health>();
        await AddOnChainValues();
        playerHealth.MaxArmour += onChainArmour;
        playerHealth.MaxHealth += onChainHealth;
        playerHealth.MaxShields += onChainShields;
        Debug.Log("\n\tplayerHealth.MaxShields " + playerHealth.MaxShields);
        Debug.Log("\n\tplayerHealth.MaxHealth " + playerHealth.MaxHealth);
        Debug.Log("\n\tplayerHealth.MaxArmour " + playerHealth.MaxArmour);
    }

    async Task AddOnChainValues()
    {
        Debug.Log("Started querying blockchain");
        string health = await Web3Manager.getBalance("Health");
        string armour = await Web3Manager.getBalance("Armour");
        string shields = await Web3Manager.getBalance("Shields");

        Debug.Log("\n\thealth " + health);
        Debug.Log("\n\tarmour " + armour);
        Debug.Log("\n\tshields " + shields);

        onChainHealth = float.Parse(health);
        onChainArmour = float.Parse(armour);
        onChainShields = float.Parse(shields);

        Debug.Log("\n\tonChainHealth " + onChainHealth);
        Debug.Log("\n\tonChainArmour " + onChainArmour);
        Debug.Log("\n\tonChainShields " + onChainShields);
    }

    void Update()
    {

    }
}