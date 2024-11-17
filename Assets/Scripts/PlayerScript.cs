using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerScript : NetworkBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool is_robber;
    public GameObject player_camera;
    [SerializeField] GameObject this_robber;
    [SerializeField] GameObject this_ghost;
    [SerializeField] GameObject pointer;

    public bool frozen;
    // Start is called before the first frame update
    void Awake()
    {
        //if (is_robber) Game.robber = this.gameObject;
        if (Game.robber == null)
        {
            Game.robber = this_robber;
            this_robber.GetComponent<RobberScript>().player = this.gameObject;
            player_camera = GameObject.Find("GhostCamera");
            Game.robber_camera = player_camera;
            is_robber = true;
            this_robber.SetActive(true);
        }
        else
        {
            Game.ghost = this_ghost;
            this_ghost.GetComponent<GhostScript>().player = this.gameObject;
            player_camera = GameObject.Find("RobberCamera");
            Game.ghost_camera = player_camera;
            this_ghost.SetActive(true);          
        }

        if (Game.robber != null && Game.ghost != null)
        {

        }
        //Instantiate(player_camera);
        //Game.player = this.gameObject;
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += MovementInput();

    }

    private void Update()
    {
        AbilitiesInput();
        player_camera.GetComponent<CameraScript>().to_follow = transform.position;
    }
    
    void AbilitiesInput()
    {
        if (!IsLocalPlayer) return;
        // Left Mouse Abilities (NightVision and ChargeAttack)
        if (Input.GetButtonDown("Fire1"))
        {
            if (is_robber) this_robber.GetComponent<RobberScript>().NightVision(true);
            else this_ghost.GetComponent<GhostScript>().ChargeAttack(true);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            if (is_robber) this_robber.GetComponent<RobberScript>().NightVision(false);
            else this_ghost.GetComponent<GhostScript>().ChargeAttack(false);
        }

        // Right Mouse Abilities (Flashlight and StepVision)
        if (Input.GetButtonDown("Fire2"))
        {
            if (is_robber) this_robber.GetComponent<RobberScript>().Flashlight(true);
            else this_ghost.GetComponent<GhostScript>().StepVision(true);
        }
        if (Input.GetButtonUp("Fire2"))
        {
            if (is_robber) this_robber.GetComponent<RobberScript>().Flashlight(false);
            else this_ghost.GetComponent<GhostScript>().StepVision(false);
        }

        // C button abilities (Item pickUp and walking through walls)
        if (Input.GetButtonDown("Jump"))
        {
            if (is_robber) this_robber.GetComponent<RobberScript>().ItemPickUpAura.GetComponent<ItemPickUp>().StartPicking(true);
            //else GetComponent<GhostScript>().StepVision(false);
        }
        if (Input.GetButtonUp("Jump"))
        {
           if (is_robber) this_robber.GetComponent<RobberScript>().ItemPickUpAura.GetComponent<ItemPickUp>().StartPicking(false);
            //else GetComponent<GhostScript>().StepVision(false);
        }
    }

    Vector3 MovementInput()
    {
        if (frozen || !IsLocalPlayer) return Vector3.zero;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");


        Vector3 Movement = new Vector3(x, y, 0) * speed;
        if (Movement.magnitude > 1) Movement.Normalize();

        return Movement;
    }

    
}
