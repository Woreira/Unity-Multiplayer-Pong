using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Collections;

public class NetworkUI : MonoBehaviour{

    public Button host, client;
    public Text[] playerText;
    public GameObject panel;

    public string playerName;

    public static NetworkUI singleton;

    public string[] players = new string[2];

    void Awake(){

        singleton = this;


        host.onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
            panel.SetActive(false);
        });

        client.onClick.AddListener(() => {
            NetworkManager.Singleton.StartClient();
            panel.SetActive(false);
        });

        
    }

    void Update(){
        host.interactable = !string.IsNullOrEmpty(playerName);
        client.interactable = !string.IsNullOrEmpty(playerName);
    }

    public void SetPlayerName(string newName){
        playerName = newName;
    }

}