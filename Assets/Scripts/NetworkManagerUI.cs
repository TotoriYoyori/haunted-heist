using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using System.Net;
using Unity.Netcode.Transports.UTP;

public class NetworkManagerUI : NetworkBehaviour
{
    [SerializeField] Button serverButton;
    [SerializeField] Button hostButton;
    [SerializeField] Button clientButton;
    [SerializeField] bool host_auto_connect;

    private void Start()
    {
        //Debug.Log("UI");
        serverButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
        });
        hostButton.onClick.AddListener(() =>
        {
            Debug.Log("Trying to create host");
            NetworkManager.Singleton.StartHost();
            //GetComponent<PlayerSelectionHandler>().RequestSpawnWithSelectedPrefabServerRpc(1);
            DisableConnectionUIServerRpc();
        });
        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData("10.11.148.114", 7777);
            NetworkManager.Singleton.StartClient();

            //to do, find a way to disable it for the ghost/client as well
        });

        if (host_auto_connect)
        {
            Debug.Log("Trying to create host");
            NetworkManager.Singleton.StartHost();
            //GetComponent<PlayerSelectionHandler>().RequestSpawnWithSelectedPrefabServerRpc(1);
            DisableConnectionUIServerRpc();
        }
    }

    [ClientRpc] // It does turn on the camera but not on the correct client
    public void DisableConnectionUIClientRpc(ClientRpcParams rpcParams = default)
    {
        // Ensure this ClientRpc only targets the specified client
        this.gameObject.SetActive(false);
    }

    [ServerRpc(RequireOwnership = false)]
    public void DisableConnectionUIServerRpc(ServerRpcParams rpcParams = default)
    {
        Debug.Log("ConnectionUI was disabled");
        ulong callingClientId = rpcParams.Receive.SenderClientId;

        // Set up ClientRpcParams to target the requesting client
        var clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { callingClientId } // Only send to this client
            }
        };

        DisableConnectionUIClientRpc(clientRpcParams);
    }

}
