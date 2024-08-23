using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsakuraSweepSkillState : AsakuraState
{
    public AsakuraSweepSkillState(Entity _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        asakuraPlayer.photonView.RPC("RPC_EnableAsakuraSweepSkillSound", RpcTarget.AllViaServer, true);
        //asakuraPlayer.photonView.RPC("IgnoreCollider", RpcTarget.AllBuffered, false);
    }

    public override void Exit()
    {
        base.Exit();
        asakuraPlayer.SetZeroVelocity();
        asakuraPlayer.photonView.RPC("RPC_EnableAsakuraSweepSkillSound", RpcTarget.AllViaServer, false);
        //  asakuraPlayer.photonView.RPC("IgnoreCollider", RpcTarget.AllBuffered, true);
    }

    public override void Update()
    {
        //base.Update();
        asakuraPlayer.SetVelocity(asakuraPlayer.transform.localScale.x * asakuraPlayer.skill.sweepSkill.sweepSpeed, rb.velocity.y);
        if (triggerCalled)
        {
            stateMachine.ChangeState(asakuraPlayer.idleState);
        }
    }
}
