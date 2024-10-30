using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum item_level
{
    Easy,
    Medium,
    Hard
}
public class ItemScript : MonoBehaviour
{
    public item_level level;
    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
