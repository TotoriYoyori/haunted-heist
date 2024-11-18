using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class ServerRpcScript : NetworkBehaviour
{
    private void Start()
    {
        Game.Server = this.gameObject;
    }
    /*

    [ServerRpc(RequireOwnership = false)]
    public void PlayerJoinServerRpc(ServerRpcParams rpcParams = default)
    {
        Debug.Log("A player has joined");
        // Disabling cameras
        if (Game.robber_camera.activeSelf && Game.ghost_camera.activeSelf)
        {
            Game.robber_camera.SetActive(false);
            Game.ghost_camera.SetActive(false);
        }

        ulong callingClientId = rpcParams.Receive.SenderClientId;

        if (Game.robber != null) Game.robber.GetComponent<PlayerScript>().ActivateCameraClientRpc(callingClientId);
        if (Game.ghost != null) Game.ghost.GetComponent<PlayerScript>().ActivateCameraClientRpc(callingClientId);

        // GetPlayerObjectByClientID(callingClientID)// ActivateCameraClientRpc(callingClientId);
    }*/
}
