using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkController : MonoBehaviourPunCallbacks
{
    public static NetworkController instance;
    void Start()
    {
        if (instance)
        {
            Debug.LogError("Already got NetworkController Instance!");
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " Server!");
    }

    public void Kill()
    {
        RoomController.roomController.Kill();
        if (PhotonNetwork.NetworkClientState != Photon.Realtime.ClientState.Disconnected && PhotonNetwork.NetworkClientState != Photon.Realtime.ClientState.Disconnecting)
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
        }

        Destroy(this.gameObject);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
