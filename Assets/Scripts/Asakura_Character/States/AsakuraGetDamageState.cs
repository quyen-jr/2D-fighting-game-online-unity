using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsakuraGetDamageState : AsakuraState
{
    public AsakuraGetDamageState(Entity _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        if (triggerCalled)
        {
            stateMachine.ChangeState(asakuraPlayer.idleState);
        }
    }
}
