using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GhostScript : MonoBehaviour
{
    bool stepvision_on;
    bool is_aiming;
    [SerializeField] GameObject aiming_arrow;
    [SerializeField] GameObject ghost_hiding;
    [SerializeField] GameObject ghost_attacking;
    public GameObject player;
    GameObject GhostCamera;
    Vector2 mouse_position;
    Vector2 charge_target_position = Vector2.zero;
    float charge_time;
    [SerializeField] float charge_duration;
    [SerializeField] float charge_length;
    Vector3 charge_starting_position;
    // Start is called before the first frame update
    void Start()
    {
        GhostCamera = player.GetComponent<PlayerScript>().player_camera;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 screen_mouse_position = Input.mousePosition;
        //screen_mouse_position.z = Camera.main.nearClipPlane; 
        mouse_position = Camera.main.ScreenToWorldPoint(screen_mouse_position);

        if (is_aiming) AimForCharge(mouse_position);
        if (charge_target_position != Vector2.zero) Charging();
    }

    void AimForCharge(Vector2 target_position)
    {
        Vector2 direction = new Vector3(target_position.x, target_position.y, 0) - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        aiming_arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Charging()
    {
        charge_time += Time.deltaTime;
        float progress = ((charge_time / charge_duration) > 1f) ? 1 : charge_time / charge_duration;
        Debug.Log(progress);

        float coolT = Mathf.Pow(progress, 2);


        transform.position = Vector3.Lerp(charge_starting_position, charge_target_position, coolT);

        if (progress == 1) EndCharge();
    }

    void StartCharge()
    {
        charge_target_position = transform.position + aiming_arrow.transform.up * charge_length;
        charge_starting_position = transform.position;
        charge_time = 0f;
        Hide(false);

        Debug.Log("Charge Started");
    }

    void EndCharge()
    {
        charge_target_position = Vector3.zero;
        Hide(true);
        Debug.Log("Charge Ended");
    }

    void Hide(bool is_hiding)
    {
        player.GetComponent<PlayerScript>().frozen = !is_hiding;
        ghost_hiding.SetActive(is_hiding);
        ghost_attacking.SetActive(!is_hiding);
    }
    public void ChargeAttack(bool is_on)
    {
        is_aiming = is_on;
        aiming_arrow.SetActive(is_aiming);
        Hide(!is_on);

        if (is_on == false && Vector2.Distance(mouse_position, transform.position) > 0f)
        {
            StartCharge();
        }

    }

    public void StepVision(bool is_on)
    {
        if (is_on)
        {
            GhostCamera.GetComponent<CameraScript>().CameraMode(camera_mode.GHOST_SPECIAL);
        }
        else
        {
            GhostCamera.GetComponent<CameraScript>().CameraMode(camera_mode.GHOST);
        }

        stepvision_on = is_on;
        GhostCamera.GetComponent<CameraScript>().filter.SetActive(is_on);
    }


}
