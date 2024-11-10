using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUp : MonoBehaviour                          // Solve all the bugs with turning off flashlight at inappropriate times
{
    public GameObject closest_item;
    GameObject SelectionAura;
    [SerializeField] float pick_up_speed_default;
    bool is_picking;
    float picking_progress;
    // Start is called before the first frame update
    private void Awake()
    {
        SelectionAura = GameObject.Find("WorldCanvas/SelectionAura");
    }
    void Start()
    {
        SelectionAura.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ItemPicking();
    }

    void ItemPicking()
    {
        if (is_picking && Game.robber.GetComponent<RobberScript>().flashlight.activeSelf == true)
        {
            float pick_up_speed = pick_up_speed_default;
            switch (closest_item.GetComponent<ItemScript>().level)
            {
                case item_level.Medium:
                    pick_up_speed = pick_up_speed_default / 2;
                    break;
                case item_level.Hard:
                    pick_up_speed = pick_up_speed_default / 4;
                    break;
            }

            picking_progress -= pick_up_speed;
            if (picking_progress < 0) FinishPicking();
            SelectionAura.GetComponent<Image>().fillAmount = picking_progress;
        }
        else if (Game.robber.GetComponent<RobberScript>().flashlight.activeSelf == false)
        {
            SelectionAura.SetActive (false);
            closest_item = null;
            is_picking = false;
        }
    }

    void FinishPicking()
    {
        SelectionAura.GetComponent<Image>().fillAmount = 1;
        closest_item.SetActive(false);                      // False pickup
        closest_item = null;
        is_picking = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Item" || Game.robber.GetComponent<RobberScript>().flashlight.activeSelf != true) return;
        
        if (closest_item == null) 
        {
            LockOnItem(collision.gameObject);
        }
        else if (Vector2.Distance(transform.position, closest_item.transform.position) > Vector2.Distance(transform.position, collision.transform.position))
        {
            LockOnItem(collision.gameObject);
        }
    }

    public void StartPicking(bool picking)
    {
        if (closest_item == null) return;
        Debug.Log("StartedPicking");
        picking_progress = 1;
        SelectionAura.GetComponent<Image>().fillAmount = 1f;
        is_picking = picking;
    }
    void LockOnItem(GameObject item)
    {
        if (item == null) SelectionAura.SetActive(false); 
        else
        {
            closest_item = item;
            SelectionAura.SetActive(true);
            SelectionAura.transform.position = closest_item.transform.position;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Item") return;

        if (closest_item != null && closest_item == collision.gameObject)
        {
            LockOnItem(null);
            closest_item = null;
        }
    }

}
