using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.Netcode;
using UnityEngine;
public enum camera_mode
{
    ROBBER,
    GHOST,
    ROBBER_SPECIAL,
    GHOST_SPECIAL
}
public class CameraScript : NetworkBehaviour
{
    //public NetworkVariable<Vector2> transform_to_follow = new NetworkVariable<Vector2>();
    public Vector2 to_follow;
    [SerializeField] float speed;
    [SerializeField] bool is_robber_camera;
    public GameObject filter;

    float camera_left_border;
    float camera_right_border;
    float camera_top_border;
    float camera_bottom_border;

    float camera_height;
    float camera_width;
    // Start is called before the first frame update
    void Start()
    {
        if (is_robber_camera)
        {
            Game.robber_camera = this.gameObject;
            CameraMode(camera_mode.ROBBER);

        }
        else
        {
            Game.ghost_camera = this.gameObject;
            CameraMode(camera_mode.GHOST);
        }
        
    }

    private void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Follow();
        CameraBorders();
    }

    void CameraBorders()
    {
        camera_height = GetComponent<Camera>().orthographicSize;
        camera_width = GetComponent<Camera>().aspect * camera_height;

        camera_left_border = transform.position.x - camera_width / 2;
        camera_bottom_border = transform.position.y - camera_height / 2;
        camera_right_border = transform.position.x + camera_width / 2;
        camera_top_border = transform.position.y + camera_height / 2;

        if (camera_left_border < Game.level.GetComponent<Level>().left_level_border) transform.position =
                new Vector3(Game.level.GetComponent<Level>().left_level_border + camera_width / 2, transform.position.y, transform.position.z);
        if (camera_bottom_border < Game.level.GetComponent<Level>().bottom_level_border) transform.position =
                new Vector3(transform.position.x, Game.level.GetComponent<Level>().bottom_level_border + camera_height / 2, transform.position.z);
        if (camera_right_border > Game.level.GetComponent<Level>().right_level_border) transform.position =
                new Vector3(Game.level.GetComponent<Level>().right_level_border - camera_width / 2, transform.position.y, transform.position.z);
        if (camera_top_border > Game.level.GetComponent<Level>().top_level_border) transform.position =
                new Vector3(transform.position.x, Game.level.GetComponent<Level>().top_level_border - camera_height / 2, transform.position.z);
    }
    void Follow()
    {
        if (to_follow == null) return;
        Vector2 new_position = new Vector2(to_follow.x, to_follow.y); 
        transform.position = Vector2.Lerp(transform.position, to_follow, speed);
        transform.position += new Vector3(0,0,-10f); // Fix this!!!
    }

    public void CameraMode(camera_mode mode)
    {
        switch (mode)
        {
            case camera_mode.ROBBER:
                GetComponent<Camera>().cullingMask = LayerMask.GetMask("Default", "UI", "UI_for_robber", "Robber");
                break;
            case camera_mode.GHOST:
                GetComponent<Camera>().cullingMask = LayerMask.GetMask("Default", "UI", "Ghost", "UI_for_ghost", "Robber");
                break;
            case camera_mode.ROBBER_SPECIAL:
                GetComponent<Camera>().cullingMask = LayerMask.GetMask("Default", "UI", "UI_for_robber", "Messages", "Nightvision", "Ghost", "Robber");
                break;
            case camera_mode.GHOST_SPECIAL:
                GetComponent<Camera>().cullingMask = LayerMask.GetMask("Default", "UI", "Ghost", "StepVision", "Messages", "Robber");
                break;

        }
    }
}
