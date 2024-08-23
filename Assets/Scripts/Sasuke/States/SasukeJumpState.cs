using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SasukeJumpState : SasukeState
{
    public SasukeJumpState(Entity _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2(rb.velocity.x * 0.7f, sasukePlayer.jumpForce);
        sasukePlayer.photonView.RPC("RPC_EnableSasukeRunSound", RpcTarget.AllViaServer, false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (xInput != 0 && !sasukePlayer.isUseSusano)
        {
            sasukePlayer.SetVelocity(sasukePlayer.speed * 0.7f * xInput, rb.velocity.y);
        }

        if (turnCanJump < 2)
        {
            if (Input.GetKeyDown(KeyCode.K) && !sasukePlayer.cantJumpAndUseSkill)
            {
                turnCanJump++;
                rb.velocity = new Vector2(rb.velocity.x * 0.7f, sasukePlayer.jumpForce);
            }
        }

        if (rb.velocity.y < 0)
            stateMachine.ChangeState(sasukePlayer.airState);

    }
}
