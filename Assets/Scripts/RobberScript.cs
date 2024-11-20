using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RobberScript : MonoBehaviour
{
    public GameObject flashlight;
    public GameObject ItemPickUpAura;
    public GameObject DarkFilter;
    public GameObject player;
    GameObject RobberCamera;
    bool nightvision_on;

    // Start is called before the first frame update
    private void Awake()
    {
        DarkFilter = GameObject.Find("BaseDark");
    }
    void Start()
    {
        RobberCamera = player.GetComponent<PlayerScript>().player_camera;
        RobberCamera.GetComponent<CameraScript>().CameraMode(camera_mode.ROBBER);
        player.transform.position = GameObject.Find("RobberSpawnPoint").transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) GetComponent<Footstep_script>().FootstepCreationCheck();
    }

    public void Flashlight(bool is_on)
    {
        if (is_on)
        {
            flashlight.SetActive(true);
            DarkFilter.SetActive(false);
            Debug.Log("FlashlightOn");
        }
        else
        {
            Debug.Log("FlashlightOff");
            DarkFilter.SetActive(true);
            flashlight.SetActive(false);
        }
    }

    public void NightVision(bool is_on) 
    {
        if (is_on)
        {
            RobberCamera.GetComponent<CameraScript>().CameraMode(camera_mode.ROBBER_SPECIAL);
            DarkFilter.SetActive(false);
        }
        else
        {
            RobberCamera.GetComponent<CameraScript>().CameraMode(camera_mode.ROBBER);
            DarkFilter.SetActive(true);
        }

        nightvision_on = is_on;
        RobberCamera.GetComponent<CameraScript>().filter.SetActive(is_on);
    }
}
