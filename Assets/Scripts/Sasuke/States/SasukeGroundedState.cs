using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SasukeGroundedState : SasukeState
{
    public SasukeGroundedState(Entity _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        if (Input.GetKeyDown(KeyCode.J) && !sasukePlayer.cantJumpAndUseSkill)
        {
            stateMachine.ChangeState(sasukePlayer.attackState);
        }
        if (Input.GetKeyDown(KeyCode.K)&&!sasukePlayer.cantJumpAndUseSkill)
        {
            stateMachine.ChangeState(sasukePlayer.jumpState);
        }

    }
}
