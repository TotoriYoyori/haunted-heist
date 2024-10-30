using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static GameObject player;
    public static GameObject robber;
    public static GameObject ghost;
    public static GameObject camera;
    public static GameObject level;
    // Start is called before the first frame update
    void Start()
    {
        player = robber;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) SwitchCharacters();
    }

    private void FixedUpdate()
    {
        camera.GetComponent<CameraScript>().to_follow = player.transform.position;
    }

    void SwitchCharacters()
    {
        player = (player == robber) ? ghost : robber;
        camera.GetComponent<CameraScript>().to_follow = player.transform.position;

        if (player == robber)
        {
            camera.GetComponent<CameraScript>().CameraMode(camera_mode.ROBBER);
        }
        else
        {
            camera.GetComponent<CameraScript>().CameraMode(camera_mode.GHOST);
        }
    }
}
