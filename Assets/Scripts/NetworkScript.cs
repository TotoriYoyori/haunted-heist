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
}
