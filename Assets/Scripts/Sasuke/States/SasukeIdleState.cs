using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SasukeIdleState : SasukeGroundedState
{
    public SasukeIdleState(Entity _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        sasukePlayer.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        sasukePlayer.SetVelocity(0, rb.velocity.y);
        if (!sasukePlayer.isGroundedDetected())
        {
            stateMachine.ChangeState(sasukePlayer.airState);
        }
        if (xInput != 0&&!sasukePlayer.isUseSusano)
        {
            stateMachine.ChangeState(sasukePlayer.runState);
        }
    }
}
