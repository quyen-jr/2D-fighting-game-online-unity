using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsakuraIdleState : AsakuraGroundedState
{
    public AsakuraIdleState(Entity _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        asakuraPlayer.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        asakuraPlayer.SetVelocity(0, rb.velocity.y);
        if (!asakuraPlayer.isGroundedDetected())
        {
            stateMachine.ChangeState(asakuraPlayer.airState);
        }
        if (xInput != 0 )
        {
            stateMachine.ChangeState(asakuraPlayer.runState);
        }
    }
}
