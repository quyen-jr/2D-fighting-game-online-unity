using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class SasukeChidoriSkill : SasukeSkill
{
    [SerializeField] private GameObject chidoriPrefab;
    public GameObject chidoriPrefabObject=null;
    private bool turnToAttackMode;
    private Vector2 faceDir;

    public void CreateChidoriPrepare()
    {
        if (!player.photonView.IsMine) return;
        Vector2 postition = new Vector2(player.transform.position.x - 0.5f * player.transform.localScale.x, player.transform.position.y + 0.67f);
        faceDir = player.transform.localScale;
        chidoriPrefabObject = PhotonNetwork.Instantiate(chidoriPrefab.name, postition, Quaternion.identity);

        bool isMaster = PhotonNetwork.IsMasterClient;
        ChidoriSkillController chidoriPrefabObjectScript = chidoriPrefabObject.GetComponent<ChidoriSkillController>();
        chidoriPrefabObjectScript.SetUpSkill(player.transform.localScale);
        chidoriPrefabObjectScript.MatchSkillForPlayerCreated(isMaster);
    }
    public void TurnIntoChidoriAttack()
    {
        if (!player.photonView.IsMine) return;

        if (chidoriPrefabObject == null) return;
        turnToAttackMode = true;
        bool isMaster = PhotonNetwork.IsMasterClient;
        chidoriPrefabObject.GetComponent<ChidoriSkillController>().MatchSkillForPlayerCreated(isMaster);
        chidoriPrefabObject.GetComponent<ChidoriSkillController>().TurnToAttackMode(turnToAttackMode);
       
    }

    public override void UseSkill()
    {
        base.UseSkill();
    }
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
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
