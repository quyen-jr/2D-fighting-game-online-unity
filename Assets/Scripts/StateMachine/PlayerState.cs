using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected float xInput;
    protected float yInput;
    protected PlayerStateMachine stateMachine;
    private Entity player;
    public string animBoolName;
    protected bool triggerCalled;
    public float stateTimer;
    public PlayerState(Entity _player,PlayerStateMachine _stateMachine, string _animBoolName)
    {
        player = _player;
        stateMachine = _stateMachine;
        animBoolName = _animBoolName;
    }
    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        triggerCalled = false;
    }
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }
    public virtual void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        stateTimer -= Time.deltaTime;
    }
    public virtual void AnimationTrigger()
    {
        triggerCalled= true;
    }
}
