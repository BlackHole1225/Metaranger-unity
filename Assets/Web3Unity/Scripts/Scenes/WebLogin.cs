using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_WEBGL
public class WebLogin : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void Web3Connect();

    [DllImport("__Internal")]
    private static extern string ConnectAccount();

    [DllImport("__Internal")]
    private static extern void SetConnectAccount(string value);

    private int expirationTime;
    private string account; 

    public Web3Manager Web3Manager;

    public void OnLogin()
    {
        Web3Connect();
        OnConnected();
    }

    async private void OnConnected()
    {
        account = ConnectAccount();
        while (account == "") {
            await new WaitForSeconds(1f);
            account = ConnectAccount();
        };

        Debug.Log("Account in OnConnected " + account);
        // save account for next scene
        PlayerPrefs.SetString("Account", account);
        // reset login message
        // SetConnectAccount("");

        await Web3Manager.getMETRBalance();
    }

    public void OnSkip()
    {
        // burner account for skipped sign in screen
        // PlayerPrefs.SetString("Account", "");
    }
}
#endif
