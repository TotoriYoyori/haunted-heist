using Unity.Netcode;
using UnityEngine;

public class PlayerSelectionHandler : NetworkBehaviour
{
    public GameObject robberPrefab;
    public GameObject ghostPrefab;

    [ServerRpc(RequireOwnership = false)]
    public void RequestSpawnWithSelectedPrefabServerRpc(int prefabIndex, ServerRpcParams rpcParams = default)
    {
        GameObject prefabToSpawn = prefabIndex == 0 ? robberPrefab : ghostPrefab;
        GameObject playerInstance = Instantiate(prefabToSpawn);

        playerInstance.GetComponent<NetworkObject>().SpawnAsPlayerObject(rpcParams.Receive.SenderClientId);
    }
}
