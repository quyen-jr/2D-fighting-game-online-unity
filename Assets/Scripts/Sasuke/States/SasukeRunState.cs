using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SasukeRunState : SasukeGroundedState
{
    public SasukeRunState(Entity _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        sasukePlayer.photonView.RPC("RPC_EnableSasukeRunSound", RpcTarget.AllViaServer, false);
    }

    public override void Update()
    {
        base.Update();
        
        sasukePlayer.SetVelocity(xInput * sasukePlayer.speed, rb.velocity.y);
        sasukePlayer.photonView.RPC("RPC_EnableSasukeRunSound", RpcTarget.AllViaServer,true);
        if (!sasukePlayer.isGroundedDetected())
        {
            sasukePlayer.photonView.RPC("RPC_EnableSasukeRunSound", RpcTarget.AllViaServer, false);
            stateMachine.ChangeState(sasukePlayer.airState);
        }
        if (xInput == 0||sasukePlayer.isUseSusano)
        {
            sasukePlayer.photonView.RPC("RPC_EnableSasukeRunSound", RpcTarget.AllViaServer, false);
            stateMachine.ChangeState(sasukePlayer.idleState);
        }
    }
}
