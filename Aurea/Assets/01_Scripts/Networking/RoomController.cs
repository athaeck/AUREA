using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomController : MonoBehaviourPunCallbacks
{
    public static RoomController roomController;
    public List<GameObject> players = new List<GameObject>();
    private void Awake()
    {
        if (roomController)
            Debug.LogError("Already got a room controller");

        roomController = this;
    }
    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom()
    {
        if(players.Count > 1) {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
        }
        StartGame();
    }

    public void Kill() {
        players = new List<GameObject>();
        
    }

    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(1);

        GameObject photonGameobject = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Player"), Vector3.zero, Quaternion.identity);


        players.Add(photonGameobject);
    }

}
