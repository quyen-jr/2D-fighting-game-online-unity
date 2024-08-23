using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsakuraUltiSkill : AsakuraSkill
{
    [SerializeField] private GameObject UltiEffectPrefab;

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        GameObject ultiEffect = PhotonNetwork.Instantiate(UltiEffectPrefab.name, player.transform.position, Quaternion.identity);
        ultiEffect.GetComponent<Ulti_Effect>().SetUpEffect();
    }

    protected override void Update()
    {
        base.Update();
    }

}
