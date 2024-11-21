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
    public static GameObject Server;
    //public static GameObject item_lottery;
    public static PlayerScript robber_player;
    public static PlayerScript ghost_player;

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

   
}
