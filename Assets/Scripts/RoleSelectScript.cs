using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class RoleSelectScript : NetworkBehaviour
{/*
    [SerializeField] Button robberButton;
    [SerializeField] Button ghostButton;
    GameObject game;

    private void Awake()
    {
        game = GameObject.Find("Game");

        robberButton.onClick.AddListener(() =>
        {
            game.GetComponent<Game>().RequestSpawnWithSelectedPrefabServerRpc(1);
            this.gameObject.SetActive(false);   
        });
        ghostButton.onClick.AddListener(() =>
        {
            game.GetComponent<Game>().RequestSpawnWithSelectedPrefabServerRpc(0);
            this.gameObject.SetActive(false);
        });
    }
    */
}
