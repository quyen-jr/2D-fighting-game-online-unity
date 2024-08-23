using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Realtime;
using Firebase.Database;

public class ConnectToSever : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public static ConnectToSever Instance;
    public  InputField createRoomInput;
    public InputField joinRoomInput;
    //
    public RoomItem roomItemPrefab;
    private List<RoomItem> roomItemsList= new List<RoomItem>();
    public Transform contentScrollRect;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        //Instantiate(roomItemPrefab, contentScrollRect);
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    //public override void OnConnectedToMaster()
    //{
    //     SceneManager.LoadScene("mainmenu");
    //}
    private void Update()
    {
       // if(PhotonNetwork.pl)
    }
    public void CreateRoom()
    {
        Debug.Log("create room");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(createRoomInput.text, roomOptions);
    }
    public void JoinRoom(string _roomName)
    {
        PhotonNetwork.JoinRoom(_roomName);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("game");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
       
        UpdateRoomList(roomList);
    }
    private void UpdateRoomList(List<RoomInfo> list)
    {
        foreach (RoomItem item in roomItemsList)
        {
            Destroy(item.gameObject);   
        }
        roomItemsList.Clear();
        foreach (RoomInfo room in list)
        {
            if (room.PlayerCount == 0) continue;
            RoomItem newRoom= Instantiate(roomItemPrefab,contentScrollRect);
            newRoom.SetRoomName(room.Name);
            roomItemsList.Add(newRoom);
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Đã kết nối đến Master Server.");
        // Tham gia vào lobby mặc định
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Đã tham gia vào Lobby.");
        // Bạn có thể thêm mã để hiển thị danh sách phòng hoặc các thao tác khác tại đây
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log("Player left the room: " + otherPlayer.NickName);
        if(!GameManager.instance.resultCanvasUI.gameObject.activeSelf)
            GameManager.instance.EnableResultUI(true);
        // Kiểm tra nếu không còn ai trong phòng, đóng phòng
        if (PhotonNetwork.CurrentRoom.PlayerCount == 0)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LeaveRoom();
        }
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Debug.Log("You left the room");
        // Cập nhật danh sách phòng khi rời khỏi phòng

        //PhotonNetwork.JoinLobby();
    }
}
