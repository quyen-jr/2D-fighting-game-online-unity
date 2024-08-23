using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsakuraDashState : AsakuraState
{
    public AsakuraDashState(Entity _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = asakuraPlayer.dashDuration;
        asakuraPlayer.photonView.RPC("IgnoreCollider", RpcTarget.AllBuffered,true);
    }

    public override void Exit()
    {
        base.Exit();
        asakuraPlayer.photonView.RPC("IgnoreCollider", RpcTarget.AllBuffered, false);
    }

    public override void Update()
    {
        base.Update();
        rb.velocity = new Vector2(asakuraPlayer.dashSpeed * asakuraPlayer.transform.localScale.x,0);
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(asakuraPlayer.idleState);
        }
    }


}
