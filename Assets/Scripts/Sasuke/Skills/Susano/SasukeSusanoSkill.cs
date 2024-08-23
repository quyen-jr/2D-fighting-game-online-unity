using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SasukeSusanoSkill : SasukeSkill
{
    [SerializeField] private GameObject SusanoPrefab;
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        if (!player.photonView.IsMine) return;
        Vector2 pos= new Vector2( player.transform.position.x,player.transform.position.y+2);
        GameObject  susanoObject= PhotonNetwork.Instantiate(SusanoPrefab.name,pos, quaternion.identity);
        susanoObject.transform.localScale= player.transform.localScale;
        SusanoSkillController susanoSkillScript = susanoObject.GetComponent<SusanoSkillController>();
        susanoSkillScript.SetUpSkill(player);
        susanoSkillScript.MatchSkillForPlayerCreated(PhotonNetwork.IsMasterClient);
        //susanoObject.transform.SetParent(player.transform);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
