using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static GameObject player;
    public static GameObject robber;
    public static GameObject ghost;
    public static GameObject robber_camera;
    public static GameObject ghost_camera;
    public static GameObject level;

    public GameObject robberPrefab;
    public GameObject ghostPrefab;
    // Start is called before the first frame update
    void Start()
    {
        //player = robber;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F)) SwitchCharacters();
    }

    private void FixedUpdate()
    {
        //if (player != null) camera.GetComponent<CameraScript>().to_follow = player.transform.position;
    }

    void SwitchCharacters()
    {
        player = (player == robber) ? ghost : robber;
        GetComponent<Camera>().GetComponent<CameraScript>().to_follow = player.transform.position;

        if (player == robber)
        {
            GetComponent<Camera>().GetComponent<CameraScript>().CameraMode(camera_mode.ROBBER);
        }
        else
        {
            GetComponent<Camera>().GetComponent<CameraScript>().CameraMode(camera_mode.GHOST);
        }
    }

    /*
        [ServerRpc(RequireOwnership = false)]
        public void RequestSpawnWithSelectedPrefabServerRpc(int prefabIndex, ServerRpcParams rpcParams = default)
        {
            GameObject prefabToSpawn = prefabIndex == 0 ? robberPrefab : ghostPrefab;
            GameObject playerInstance = Instantiate(prefabToSpawn);

            playerInstance.GetComponent<NetworkObject>().SpawnAsPlayerObject(rpcParams.Receive.SenderClientId);
        }*/
}
