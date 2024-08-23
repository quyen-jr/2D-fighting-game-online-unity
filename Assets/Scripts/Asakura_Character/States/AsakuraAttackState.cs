using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsakuraAttackState : AsakuraState
{
    private float lastTimeAttacked;
    private float comboWindow = 2;
    private int comboCounter;
    public AsakuraAttackState(Entity _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (comboCounter > 2 || lastTimeAttacked + comboWindow < Time.time)
            comboCounter = 0;
        asakuraPlayer.anim.SetInteger("ComboCounter", comboCounter);
        asakuraPlayer.photonView.RPC("RPC_EnableAsakuraAttackSound", RpcTarget.AllViaServer, true);
    }

    public override void Exit()
    {
        base.Exit();
        comboCounter++;
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
      //  asakuraPlayer.SetVelocity(asakuraPlayer.attackPrimarySpeed*asakuraPlayer.transform.localScale.x, rb.velocity.y);
        if (triggerCalled)
        {
            stateMachine.ChangeState(asakuraPlayer.idleState);
        }
    }
}
