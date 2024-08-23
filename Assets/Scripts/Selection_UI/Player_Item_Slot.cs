
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player_Item_Slot : MonoBehaviour
{
    public GameObject PlayerPrefab;
    private Image Image=>GetComponent<Image>();
    public Sprite playerImage;
    // public int playerItemIndex;
    [SerializeField] GameObject frameImageWhenChoose;
    [SerializeField] GameObject lockedFrame;
    public  int price;

    private void Start()
    {
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //GameManager.instance.playerSelected=PlayerPrefab.gameObject;
        //UpdatePlayerChoseImaged(playerItemIndex);
    }
    private void SetPlayerSelectedImage(bool _isMaster)
    {

        if (_isMaster)
        {
            GameManager.instance.selectionMenu.MasterSelectedPlayerImg.sprite = playerImage;
        }
        else
        {
            GameManager.instance.selectionMenu.ClientSelectedPlayerImg.sprite = playerImage;
        }
    }
    private void Update()
    {

    }
    public void EnableFrameWhenChoosePlayerItem(bool _isChoose)
    {
        frameImageWhenChoose.SetActive(_isChoose);
        if (_isChoose)
            GetComponentInChildren<FrameWhenChoosePlayer>().startCouroutineColorChange();
    }
    public void SelectedThisPlayer() {
        Image.color = Color.gray;
        //Image.color.a= 
    }
    public void LockedThisPlayer()
    {
        Image.color = Color.black;
        lockedFrame.SetActive(true);
        //Image.color.a= 
    }
    public void UnlockThisPlayer()
    {
        Image.color = Color.white;
        lockedFrame.SetActive(false);
    }
    public void UpdatePlayerChoseImaged(int _playerItemIndex)
    {
        EnableFrameWhenChoosePlayerItem(true);
        if (lockedFrame.activeSelf == true) return;
        bool isMaster = GameManager.instance.IsMaster();
        SetPlayerSelectedImage(isMaster);
        
        GameManager.instance.view.RPC("RPC_SendPlayerSelectedToOther", RpcTarget.AllViaServer, isMaster, _playerItemIndex);
    }
}
