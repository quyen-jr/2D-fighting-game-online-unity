using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playerSelected;
    [SerializeField] public SelectionMenu selectionMenu;
    [SerializeField] private int minX, minY, maxX, maxY;
    public PhotonView view;
    public bool clientSelectedPlayerItem;
    [SerializeField] public ResultCanvasUI resultCanvasUI;
    public AudioSource AudioWhenChoosePlayer;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
            instance = this;
    }
    void Start()
    {
        view = GetComponent<PhotonView>();
        StartCoroutine(EnableAudioWhenChoosePlayer());
    }

    void Update()
    {
    }
    public void InstantiatePlayer()
    {
        Vector2 posWhenStart = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        if (!PhotonNetwork.IsMasterClient) posWhenStart = new Vector2(15.5f, -1.4f);
        else posWhenStart = new Vector2(-3f, -1.4f);
        PhotonNetwork.Instantiate(playerSelected.name, posWhenStart, Quaternion.identity);
    }
    [PunRPC]
    public void RPC_SendPlayerSelectedToOther(bool _isMaster, int _imageSlotIndex)
    {
        Debug.Log(_imageSlotIndex);
        if (PhotonNetwork.IsMasterClient && !_isMaster)
        {
            selectionMenu.ClientSelectedPlayerImg.sprite = selectionMenu.playerItemList[_imageSlotIndex].playerImage;
        }
        else if (!PhotonNetwork.IsMasterClient && _isMaster)
        {
            selectionMenu.MasterSelectedPlayerImg.sprite = selectionMenu.playerItemList[_imageSlotIndex].playerImage;
        }

    }
    public bool IsMaster() => PhotonNetwork.IsMasterClient;

    [PunRPC]
    public void RPC_ClientSelectedPlayerItem()
    {
        clientSelectedPlayerItem = true;
    }
    [PunRPC]
    public void RPC_StartGame()
    {
        if (selectionMenu.gameObject == null) return;
        selectionMenu.gameObject.SetActive(false);
        AudioWhenChoosePlayer.Stop();
        InstantiatePlayer();
    }

    public void CombackToMainMenuScene()
    {
        PhotonNetwork.LeaveRoom();
        ///PhotonNetwork.LeaveLobby();
        //PhotonNetwork.Disconnect();
        Destroy(ConnectToSever.Instance.gameObject);
        SceneManager.LoadScene("mainmenu");

    }
    public void EnableResultUI(bool _isWin)
    {
        resultCanvasUI.gameObject.SetActive(true);
        string resultText = (_isWin) ? "YOU WIN !" : "YOU LOSS !";
        DataSaver.Instance.IncreaseCoin(_isWin);
        resultCanvasUI.setResultText(resultText);
    }
    [PunRPC]
    public void  RPC_EnableResultUI(bool _isWin)
    {
        if (!view.IsMine) return;
        EnableResultUI(_isWin);
    }


    IEnumerator EnableAudioWhenChoosePlayer()
    {
        yield return new WaitForSeconds(3f);
        AudioWhenChoosePlayer.Play();
    }
}
