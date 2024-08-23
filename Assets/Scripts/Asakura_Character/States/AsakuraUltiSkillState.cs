using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsakuraUltiSkillState : AsakuraState
{
    
    public AsakuraUltiSkillState(Entity _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        triggerCalled = false;
        asakuraPlayer.SetZeroVelocity();
        int faceDir = asakuraPlayer.transform.localScale.x >= 0 ? 1 : -1;
        asakuraPlayer.transform.localScale= new Vector2(1*faceDir, 1);
        asakuraPlayer.skill.ultiSkill.UseSkill();
    }

    public override void Exit()
    {
        base.Exit();
        int faceDir = asakuraPlayer.transform.localScale.x > 0 ? 1 : -1;
        asakuraPlayer.transform.localScale = new Vector2(faceDir, 1);
    }

    public override void Update()
    {
        //base.Update();
        asakuraPlayer.SetZeroVelocity();
        ChangeScale();
        if (triggerCalled)
        {
            stateMachine.ChangeState(asakuraPlayer.airState);
        }
    }
    private void ChangeScale()
    {
        int faceDir = asakuraPlayer.transform.localScale.x >= 0 ? 1 : -1;
        if (faceDir != 1 && faceDir != -1) Debug.Log("deo scale");
        Vector2 newSize = new Vector2(1 * faceDir, 1) * 3;
        asakuraPlayer.transform.localScale = Vector2.Lerp(asakuraPlayer.transform.localScale, newSize, Time.deltaTime * 2);
    }
}
