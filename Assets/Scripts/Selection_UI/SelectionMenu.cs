using Photon.Pun;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button playButton;
    [SerializeField] public Image MasterSelectedPlayerImg;
    [SerializeField] public Image ClientSelectedPlayerImg;
    [SerializeField] public GameObject buySection;
    [SerializeField] public GameObject dontHaveMoneySection;
    [SerializeField] private AudioSource changePlayerAudio;
    [SerializeField] private AudioSource SelectPlayerAudio;
    public List<Player_Item_Slot> playerItemList;
    private int maxPlayerCanChoose=0;
    private int initalSlotPlayerChosen = 0;
    private bool selectedPlayerItem;
    [SerializeField] private TextMeshProUGUI totalCoinText;
    void Start()
    {
        initalSlotPlayerChosen = 0;
      UpdatePlayerHasBought();
    }
    public void UpdatePlayerHasBought()
    {
        maxPlayerCanChoose =0;
        for (int i = 0; i < playerItemList.Count; i++)
        {
            if (!DataSaver.Instance.dts.playerBought[i])
            {
                playerItemList[i].LockedThisPlayer();
            }
            else
            {
                playerItemList[i].UnlockThisPlayer();
            }
            maxPlayerCanChoose++;
        }
        totalCoinText.text="Coin :"+ DataSaver.Instance.dts.totalCoin.ToString();

    }
    // Update is called once per frame
    void Update()
    {
        // ready to start game 
        if (selectedPlayerItem && PhotonNetwork.IsMasterClient && GameManager.instance.clientSelectedPlayerItem)
        {
            GameManager.instance.view.RPC("RPC_StartGame", RpcTarget.AllViaServer);
            GameManager.instance.clientSelectedPlayerItem = false;
        }
        if (selectedPlayerItem) return;
        if (Input.GetKeyDown(KeyCode.J) && PhotonNetwork.IsMasterClient)
        {
            if(SelectPlayerAudio!=null)
            {
                SelectPlayerAudio.Play();
            }
            if (!DataSaver.Instance.dts.playerBought[initalSlotPlayerChosen]) return;
            selectedPlayerItem = true;
            GameManager.instance.playerSelected = playerItemList[initalSlotPlayerChosen].PlayerPrefab.gameObject;
            playerItemList[initalSlotPlayerChosen].SelectedThisPlayer();
        }
        if (Input.GetKeyDown(KeyCode.J) && !PhotonNetwork.IsMasterClient)
        {
            if (SelectPlayerAudio != null)
            {
                SelectPlayerAudio.Play();
            }
            if (!DataSaver.Instance.dts.playerBought[initalSlotPlayerChosen]) return;
            selectedPlayerItem = true;
            GameManager.instance.playerSelected = playerItemList[initalSlotPlayerChosen].PlayerPrefab.gameObject;
            playerItemList[initalSlotPlayerChosen].SelectedThisPlayer();
            GameManager.instance.view.RPC("RPC_ClientSelectedPlayerItem", RpcTarget.AllViaServer);

        }
        // change player item
        if (Input.GetKeyDown(KeyCode.D))
        {
            if(changePlayerAudio!=null)
            {
                changePlayerAudio.Play();
            }
            //Debug.Log(initalSlotPlayerChosen);
            playerItemList[initalSlotPlayerChosen].EnableFrameWhenChoosePlayerItem(false);
            initalSlotPlayerChosen = initalSlotPlayerChosen + 1;
            if (initalSlotPlayerChosen > maxPlayerCanChoose - 1)
            {
                initalSlotPlayerChosen = 0;
                playerItemList[initalSlotPlayerChosen].UpdatePlayerChoseImaged(initalSlotPlayerChosen);
            }
            else
            {
                playerItemList[initalSlotPlayerChosen].UpdatePlayerChoseImaged(initalSlotPlayerChosen);
            }

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (changePlayerAudio != null)
            {
                changePlayerAudio.Play();
            }
            playerItemList[initalSlotPlayerChosen].EnableFrameWhenChoosePlayerItem(false);
            initalSlotPlayerChosen = initalSlotPlayerChosen - 1;
            if (initalSlotPlayerChosen < 0)
            {
                initalSlotPlayerChosen = maxPlayerCanChoose - 1;
                playerItemList[initalSlotPlayerChosen].UpdatePlayerChoseImaged(initalSlotPlayerChosen);
            }
            else
            {
                playerItemList[initalSlotPlayerChosen].UpdatePlayerChoseImaged(initalSlotPlayerChosen);
            }

        }

        /// buy 
        if (Input.GetKeyDown(KeyCode.B))
        {
            buySection.SetActive(true);
        }
    }

    public void BuyThisPlayer()
    {
        Debug.Log("click");
        if(DataSaver.Instance.dts.totalCoin>= playerItemList[initalSlotPlayerChosen].price)
        {
            DataSaver.Instance.dts.totalCoin -= playerItemList[initalSlotPlayerChosen].price;
            DataSaver.Instance.dts.playerBought[initalSlotPlayerChosen] = true;
            DataSaver.Instance.SaveDateFn(DataSaver.Instance.dts);
            UpdatePlayerHasBought();
            playerItemList[initalSlotPlayerChosen].UpdatePlayerChoseImaged(initalSlotPlayerChosen);
            buySection.SetActive(false);
        }
        else
        {
            //Debug.Log("Dont have Enough Money");
            dontHaveMoneySection.SetActive(true);
            buySection.SetActive(false);
        }
    }
    public void CancelBuyProcess()
    {
        buySection.SetActive(false);
    }
    public void DisableDontHaveMoneySection()
    {
        dontHaveMoneySection.SetActive(false);
    }
}
