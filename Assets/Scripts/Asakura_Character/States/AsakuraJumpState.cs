using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsakuraJumpState : AsakuraState
{
   
    public AsakuraJumpState(Entity _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        asakuraPlayer.photonView.RPC("RPC_EnableAsakuraRunSound", RpcTarget.AllViaServer, false);
        rb.velocity = new Vector2(rb.velocity.x * 0.7f, asakuraPlayer.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (xInput != 0)
        {
            asakuraPlayer.SetVelocity(asakuraPlayer.speed * 0.7f * xInput, rb.velocity.y);
        }

        if (turnCanJump < 2)
        {    
            if (Input.GetKeyDown(KeyCode.K))
            {
                turnCanJump++;
                rb.velocity = new Vector2(rb.velocity.x * 0.7f, asakuraPlayer.jumpForce);
            }
        }

        if (rb.velocity.y < 0)
            stateMachine.ChangeState(asakuraPlayer.airState);
       
    }
}
