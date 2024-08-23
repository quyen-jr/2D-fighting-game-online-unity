using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsakuraState : PlayerState
{
    protected Asakura asakuraPlayer;
    protected Rigidbody2D rb;
    protected  static int  turnCanJump = 1;
    public AsakuraState(Entity _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
        asakuraPlayer=(Asakura)_player;
        rb=asakuraPlayer.rb;
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
        asakuraPlayer.anim.SetFloat("yVelocity",rb.velocity.y);
    }
}
