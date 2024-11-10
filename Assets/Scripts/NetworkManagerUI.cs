using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class NetworkManagerUI : NetworkBehaviour
{
    [SerializeField] Button serverButton;
    [SerializeField] Button hostButton;
    [SerializeField] Button clientButton;

    private void Awake()
    {
        serverButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
        });
        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            //GetComponent<PlayerSelectionHandler>().RequestSpawnWithSelectedPrefabServerRpc(1);
        });
        clientButton.onClick.AddListener(() =>
        {
        NetworkManager.Singleton.StartClient();
            //GetComponent<PlayerSelectionHandler>().RequestSpawnWithSelectedPrefabServerRpc(0);
        });
    }

}
