using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RobberScript : MonoBehaviour
{
    public GameObject flashlight;
    public GameObject ItemPickUpAura;
    bool nightvision_on;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
            Debug.Log("FlashlightOn");
        }
        else
        {
            Debug.Log("FlashlightOff");
            flashlight.SetActive(false);
        }
    }

    public void NightVision(bool is_on) 
    {
        if (is_on)
        {
            Game.camera.GetComponent<CameraScript>().CameraMode(camera_mode.ROBBER_SPECIAL);
        }
        else
        {
            Game.camera.GetComponent<CameraScript>().CameraMode(camera_mode.ROBBER);
        }

        nightvision_on = is_on;
        Game.camera.GetComponent<CameraScript>().filter.SetActive(is_on);
    }
}
