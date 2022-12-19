using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class Paddle : NetworkBehaviour{

    public float speed;

    public override void OnNetworkSpawn(){
        if(!IsOwner) return;
        if(IsServer){
            SendPlayerNameServerRpc(0, NetworkUI.singleton.playerName);
            transform.position += (Vector3.right * -7f);
        }
        else{
            SendPlayerNameServerRpc(1, NetworkUI.singleton.playerName);
            transform.position += (Vector3.right * 7f);
        }
    }

    void Update(){
        if(!IsOwner) return;
        PlayerInput();
        Constrain();
    }

    void PlayerInput(){
        transform.Translate(Input.GetAxis("Vertical") * Vector3.up * speed * Time.deltaTime);
    }

    void Constrain(){
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -5f, 5f), 0f);
    }

    [ServerRpc(RequireOwnership = false)]
    public void SendPlayerNameServerRpc(int num, string name){
        NetworkUI.singleton.players[num] = name;
        UpdatePlayerNamesClientRpc(NetworkUI.singleton.players[0], NetworkUI.singleton.players[1]);
    }

    [ClientRpc]
    public void UpdatePlayerNamesClientRpc(string p1, string p2){
        
        NetworkUI.singleton.players = new string[2]{p1, p2};
        
        for(int i = 0; i < 2; i++){
            NetworkUI.singleton.playerText[i].text = NetworkUI.singleton.players[i];
        }
    }
}