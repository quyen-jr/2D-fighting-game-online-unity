using Photon.Pun.Demo.PunBasics;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Skill : MonoBehaviour
{
    public float coolDown;
    public float coolDownTimer;
    [NonSerialized]public Entity player;

    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {
        if (!player) return;
        if (!player.photonView.IsMine) return;
        coolDownTimer -= Time.deltaTime;
    }
    public virtual bool CanUseSkill()
    {
        if (!player) return false;
        if (!player.photonView.IsMine) return false;
        if (coolDownTimer <=0)
        {
            ///UseSkill();
            coolDownTimer = coolDown;
            return true;
        }
        return false;
    }

    public virtual void UseSkill()
    {
        if (!player) return;
        if (!player.photonView.IsMine) return;
        //useskill;
    }

}
