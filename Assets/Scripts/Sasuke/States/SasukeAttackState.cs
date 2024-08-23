using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SasukeAttackState : SasukeState
{
    private float lastTimeAttacked;
    private float comboWindow = 2;
    private int comboCounter;
    public SasukeAttackState(Entity _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        if (comboCounter > 2 || lastTimeAttacked + comboWindow < Time.time)
            comboCounter = 0;
        sasukePlayer.anim.SetInteger("ComboCounter", comboCounter);
        
    }

    public override void Exit()
    {
        base.Exit();
        comboCounter++;
        lastTimeAttacked = Time.time;
        //sasukePlayer.photonView.RPC("RPC_EnableSasukeAttackSound", RpcTarget.AllViaServer, fa);
    }

    public override void Update()
    {
        base.Update();
        sasukePlayer.SetVelocity(sasukePlayer.attackPrimarySpeed*sasukePlayer.transform.localScale.x, rb.velocity.y);
        if (triggerCalled)
        {
            stateMachine.ChangeState(sasukePlayer.idleState);
        }
    }
}
