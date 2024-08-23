using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsakuraGroundedState : AsakuraState
{
    public AsakuraGroundedState(Entity _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.J))
        {
            stateMachine.ChangeState(asakuraPlayer.attackState);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            stateMachine.ChangeState(asakuraPlayer.jumpState);
        }

    }
}
