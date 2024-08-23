using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SasukeSwapPosSkillState : SasukeState
{
    public SasukeSwapPosSkillState(Entity _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        sasukePlayer.isUseSkill = true;
    }

    public override void Exit()
    {
        base.Exit();
        sasukePlayer.isUseSkill = false;
        sasukePlayer.skill.swapPosSkill.UseSkill();
    }

    public override void Update()
    {
        base.Update();
        sasukePlayer.SetZeroVelocity();
        if (triggerCalled)
        {
             stateMachine.ChangeState(sasukePlayer.airState);
        }
    }

}
