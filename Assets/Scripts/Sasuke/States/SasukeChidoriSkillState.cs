using Photon.Pun;
using UnityEngine;

public class SasukeChidoriSkillState : SasukeState
{
    public float timeOfrun;
    public int ChidoriStateNumber;
    private float timeToPrepare;
    private bool reduceTimePrepare;
    public SasukeChidoriSkillState(Entity _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }
    public override void Enter()
    {
        base.Enter();
        sasukePlayer.SetZeroVelocity();
        sasukePlayer.isUseSkill = true;
        ChidoriStateNumber= 0;
        timeOfrun = 0.2f;
        sasukePlayer.photonView.RPC("IgnoreCollider", RpcTarget.AllBuffered, true);

        /// usse skill in air
    }

    public override void Exit()
    {
        base.Exit();
        ChidoriStateNumber = 0;
        sasukePlayer.anim.SetInteger("ChidoriStateNumber", ChidoriStateNumber);
        sasukePlayer.photonView.RPC("IgnoreCollider", RpcTarget.AllBuffered, false);
        sasukePlayer.isUseSkill= false;
        reduceTimePrepare = false;
    }

    public override void Update()
    {
        if(ChidoriStateNumber == 0)
        {
            sasukePlayer.SetZeroVelocity();
            if (reduceTimePrepare)
            {
                timeToPrepare -= Time.deltaTime;
                if(timeToPrepare < 0)
                {
                    IncreaseStateOfChidori();
                }
            }
        }
        if(ChidoriStateNumber == 1)// trang thai chay chidori
        {
            timeOfrun -= Time.deltaTime;
            sasukePlayer.SetVelocity(sasukePlayer.transform.localScale.x * sasukePlayer.speed * 4, rb.velocity.y);

            // sau khi trang thai chay hoan tat chuyen qua trang thai tan cong chidori
            if (timeOfrun < 0)
            {
                IncreaseStateOfChidori();
                sasukePlayer.skill.chidoriSkill.TurnIntoChidoriAttack();
            }
        }
        
        // sau khi trang thai chay ket thuc
        if (timeOfrun < 0&&ChidoriStateNumber == 2)
        {
            sasukePlayer.SetZeroVelocity();
        }
        base.Update();
        if(triggerCalled)
        {
            stateMachine.ChangeState(sasukePlayer.idleState);
        }
    }
    //  de chidori thuc hien lan luot 3 animation
    public void IncreaseStateOfChidori()
    {
        ChidoriStateNumber++;
        sasukePlayer.anim.SetInteger("ChidoriStateNumber", ChidoriStateNumber);
    }
    public void ChangeQuicklyToChidoriAttackMode()
    {
        reduceTimePrepare=true;
        timeToPrepare = 0.2f;
      // sasukePlayer.skill.chidoriSkill.CreateChidoriPrepare();
      // sasukePlayer.chidoriSkillState.IncreaseStateOfChidori();
    }
}
