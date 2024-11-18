using Unity.Netcode;
using UnityEngine;

public class NetworkScript : NetworkBehaviour
{
    private void Start()
    {
        Game.Server = this.gameObject;
    }
    

    [ServerRpc(RequireOwnership = false)]
    public void PlayerJoinServerRpc(ServerRpcParams rpcParams = default)
    {
        Debug.Log("A player has joined");
        // Disabling cameras
        /*
        if (Game.robber_camera.activeSelf && Game.ghost_camera.activeSelf)
        {
            Game.robber_camera.SetActive(false);
            Game.ghost_camera.SetActive(false);
        }*/

        ulong callingClientId = rpcParams.Receive.SenderClientId;

        // Set up ClientRpcParams to target the requesting client
        var clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { callingClientId } // Only send to this client
            }
        };

        if (Game.robber_player != null) Game.robber_player.ActivateCameraClientRpc(clientRpcParams);
        if (Game.ghost_player != null) Game.ghost_player.ActivateCameraClientRpc(clientRpcParams);
        
        // GetPlayerObjectByClientID(callingClientID)// ActivateCameraClientRpc(callingClientId);
    }
    /*
    [ClientRpc] // It does turn on the camera but not on the correct client
    public void ActivateCameraClientRpc(ClientRpcParams rpcParams = default)
    {
        Debug.Log("Camera is supposed to be activated");
        // Ensure this ClientRpc only targets the specified client

        Game.robber_camera.SetActive(false);
        Game.ghost_camera.SetActive(false);

        if (IsHost) Game.robber_camera.SetActive(true);
        else if (IsClient) Game.ghost_camera.SetActive(true);
        Debug.Log("Camera was activated");

    }*/
}
