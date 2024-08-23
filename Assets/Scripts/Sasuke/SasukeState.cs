using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SasukeState : PlayerState
{
    protected Sasuke sasukePlayer;
    protected Rigidbody2D rb;
    protected static int turnCanJump = 1;
    public SasukeState(Entity _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
        sasukePlayer = (Sasuke)_player;
        rb = sasukePlayer.rb;
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
        sasukePlayer.anim.SetFloat("yVelocity", rb.velocity.y);
    }
}
