using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsakuraRunState : AsakuraGroundedState
{
    public AsakuraRunState(Entity _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        asakuraPlayer.photonView.RPC("RPC_EnableAsakuraRunSound", RpcTarget.AllViaServer, false);
    }

    public override void Update()
    {
        base.Update();
        asakuraPlayer.photonView.RPC("RPC_EnableAsakuraRunSound", RpcTarget.AllViaServer, true);
        asakuraPlayer.SetVelocity(xInput* asakuraPlayer.speed,rb.velocity.y);
        if (!asakuraPlayer.isGroundedDetected())
        {
            asakuraPlayer.photonView.RPC("RPC_EnableAsakuraRunSound", RpcTarget.AllViaServer, false);
            stateMachine.ChangeState(asakuraPlayer.airState);
        }
        if (xInput==0)
        {
            asakuraPlayer.photonView.RPC("RPC_EnableAsakuraRunSound", RpcTarget.AllViaServer, false);
            stateMachine.ChangeState(asakuraPlayer.idleState);
        }
    }
}
