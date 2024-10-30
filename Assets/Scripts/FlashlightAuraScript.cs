using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightAuraScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Item") return;
        collision.gameObject.GetComponent<ItemScript>().sprite.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Item") return;
        collision.gameObject.GetComponent<ItemScript>().sprite.enabled = false;
    }

}
