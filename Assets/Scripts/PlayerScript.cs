using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool is_robber;
    public bool frozen;
    // Start is called before the first frame update
    void Awake()
    {
        if (is_robber) Game.robber = this.gameObject;
        else Game.ghost = this.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += MovementInput();

    }

    private void Update()
    {
        AbilitiesInput();
    }

    void AbilitiesInput()
    {
        if (Game.player != this.gameObject) return;
        // Left Mouse Abilities (NightVision and ChargeAttack)
        if (Input.GetButtonDown("Fire1"))
        {
            if (is_robber) GetComponent<RobberScript>().NightVision(true);
            else GetComponent<GhostScript>().ChargeAttack(true);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            if (is_robber) GetComponent<RobberScript>().NightVision(false);
            else GetComponent<GhostScript>().ChargeAttack(false);
        }

        // Right Mouse Abilities (Flashlight and StepVision)
        if (Input.GetButtonDown("Fire2"))
        {
            if (is_robber) GetComponent<RobberScript>().Flashlight(true);
            else GetComponent<GhostScript>().StepVision(true);
        }
        if (Input.GetButtonUp("Fire2"))
        {
            if (is_robber) GetComponent<RobberScript>().Flashlight(false);
            else GetComponent<GhostScript>().StepVision(false);
        }

        // C button abilities (Item pickUp and walking through walls)
        if (Input.GetButtonDown("Jump"))
        {
            if (is_robber) GetComponent<RobberScript>().ItemPickUpAura.GetComponent<ItemPickUp>().StartPicking(true);
            //else GetComponent<GhostScript>().StepVision(false);
        }
        if (Input.GetButtonUp("Jump"))
        {
           if (is_robber) GetComponent<RobberScript>().ItemPickUpAura.GetComponent<ItemPickUp>().StartPicking(false);
            //else GetComponent<GhostScript>().StepVision(false);
        }
    }

    Vector3 MovementInput()
    {
        if (Game.player != this.gameObject || frozen) return Vector3.zero;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");


        Vector3 Movement = new Vector3(x, y, 0) * speed;
        if (Movement.magnitude > 1) Movement.Normalize();

        return Movement;
    }

    
}
