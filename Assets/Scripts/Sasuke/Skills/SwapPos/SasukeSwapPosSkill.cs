using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SasukeSwapPosSkill : SasukeSkill
{
    [SerializeField] private GameObject SasukeSkillEffect;
    public Transform characterPlayerBody;
    public Transform enemyPlayerBody;
    private Transform mid;
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public  void CreateSkillEffect()
    {
        Debug.Log("dataora");
        GameObject effect= PhotonNetwork.Instantiate(SasukeSkillEffect.name,new Vector2(0,0),Quaternion.identity);
        effect.GetComponent<SwapPosSkillEffect_Controller>().Active();
    }
    public override void UseSkill()
    {
        base.UseSkill();
        SwapPosBetweenTwoPlayers();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
    private void SwapPosBetweenTwoPlayers()
    {
        if (!player.photonView.IsMine) return;
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Player");
        if (objectsWithTag.Length < 2) return;

        if (objectsWithTag[0].GetComponent<Entity>().photonView.IsMine)
        {
            characterPlayerBody = objectsWithTag[0].transform;
            enemyPlayerBody = objectsWithTag[1].transform;
        }
        else
        {
            characterPlayerBody = objectsWithTag[1].transform;
            enemyPlayerBody = objectsWithTag[0].transform;
        }
        Vector2 posChar = characterPlayerBody.position;
        Vector2 posEnemy = enemyPlayerBody.position;
        if(PhotonNetwork.IsMasterClient)
        {
            player.photonView.RPC("RPC_SwapPosBetweenTwoPlayers", RpcTarget.AllBuffered, false, posEnemy.x, posEnemy.y);
            player.photonView.RPC("RPC_SwapPosBetweenTwoPlayers", RpcTarget.AllBuffered, true, posChar.x, posChar.y);
        }
        else
        {
            player.photonView.RPC("RPC_SwapPosBetweenTwoPlayers", RpcTarget.AllBuffered, false, posChar.x, posChar.y);
            player.photonView.RPC("RPC_SwapPosBetweenTwoPlayers", RpcTarget.AllBuffered, true, posEnemy.x, posEnemy.y);
        }


    }
}
