using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AsakuraSlashSkill : AsakuraSkill
{
    [SerializeField] private GameObject slashSkillPrefab;
    [Header("Skill info")]
    [SerializeField] private int moveSpeed;
    private Vector2 faceDir;
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        if (!player.photonView.IsMine) return;
        GameObject slashSkillObject=PhotonNetwork.Instantiate(slashSkillPrefab.name,player.transform.position,Quaternion.identity);
        faceDir = player.transform.localScale;
        slashSkillObject.GetComponent<SlashSkillController>().SetUpSkill(moveSpeed,faceDir);
        
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
