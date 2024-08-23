using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public TextMeshProUGUI roomName;
    public ConnectToSever lobbyManager;

    private void Start()
    {
        lobbyManager= FindObjectOfType<ConnectToSever>();
    }
    public void SetRoomName(string _roomName)
    {
        roomName.text = _roomName;
    }
    public void OnlickRoomItem()
    {
        lobbyManager.JoinRoom(roomName.text);
    }
}
