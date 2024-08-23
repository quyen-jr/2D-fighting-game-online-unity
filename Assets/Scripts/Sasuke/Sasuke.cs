using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Sasuke : Entity
{
    public SasukeIdleState idleState { get; private set; }
    public SasukeRunState runState { get; private set; }
    public SasukeJumpState jumpState { get; private set; }
    public SasukeAirState airState { get; private set; }
    public SasukeAttackState attackState { get; private set; }
    public SasukeChidoriSkillState chidoriSkillState {  get; private set; }
    public SasukeSwapPosSkillState swapPosSkillState { get; private set; }
    public Sasuke_SkillManager skill { get; private set; }
    
    public bool isUseSkill;
    public bool isUseSusano;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        skill = GetComponentInChildren<Sasuke_SkillManager>();
        idleState = new SasukeIdleState(this, stateMachine, "Idle");
        runState = new SasukeRunState(this, stateMachine, "Run");
        jumpState = new SasukeJumpState(this, stateMachine, "Jump");
        airState = new SasukeAirState(this, stateMachine, "Jump");
        attackState = new SasukeAttackState(this, stateMachine, "Attack");

        chidoriSkillState = new SasukeChidoriSkillState(this, stateMachine, "ChidoriSkill");
        swapPosSkillState = new SasukeSwapPosSkillState(this, stateMachine, "SwapPosSkill");
       // chidoriSkillAttackState = new SasukeChidoriSkillAttackState(this, stateMachine, "ChidoriSkillAttack");
        //dashState = new AsakuraDashState(this, stateMachine, "Dash");
        //slashSkillState = new AsakuraSlashSkillState(this, stateMachine, "SlashSkill");
        //sweepSkillState = new AsakuraSweepSkillState(this, stateMachine, "SweepSkill");
        //getDamageState = new AsakuraGetDamageState(this, stateMachine, "GetDamage");
        //ultiSkillState = new AsakuraUltiSkillState(this, stateMachine, "UltiSkill");
        stateMachine.Initialize(idleState);

    }

    public override void Update()
    {
        if (gameEnd) return;
        CheckIfLoss();
        if (!photonView.IsMine) return;
        stateMachine.currentState.Update();

        if (isUseSusano) SetVelocity(0, rb.velocity.y);
        if (cantJumpAndUseSkill) return;

        if (Input.GetKeyDown(KeyCode.L)&&isGroundedDetected() &&!isUseSkill&&skill.chidoriSkill.CanUseSkill())
        {               
            stateMachine.ChangeState(chidoriSkillState);
            return;
        }
        else if(Input.GetKeyDown(KeyCode.L) && !isGroundedDetected() && !isUseSkill && skill.chidoriSkill.CanUseSkill())
        {            
            stateMachine.ChangeState(chidoriSkillState);
            chidoriSkillState.ChangeQuicklyToChidoriAttackMode();
            return;
        }
        if (Input.GetKeyDown(KeyCode.I) && !isUseSkill && skill.swapPosSkill.CanUseSkill())
        {
            isUseSkill=true;
            skill.swapPosSkill.CreateSkillEffect();
            UseChidoriSkillAfter(0.3f);
        }
        if (Input.GetKeyDown(KeyCode.O) && !isUseSkill&&skill.susanoSkill.CanUseSkill())
        {
            skill.susanoSkill.UseSkill();
            StartCoroutine("AvoidMoveWhenEnableSusanoSkill");
        }

    }
    IEnumerator AvoidMoveWhenEnableSusanoSkill()
    {
        isUseSusano = true;
        yield return new WaitForSeconds(3f);
        isUseSusano = false;
    }
    public override void takeDamage(int _damage)
    {
        //if (cantJumpAndUseSkill)
        //    _damage = (int)_damage / 4;
        currentHealth -= _damage;
        //stateMachine.ChangeState(getDamageState);
        StartCoroutine("ChangeOrderSorting", 0.15f);
        Debug.Log(currentHealth);
    }

    protected IEnumerator ChangeOrderSorting(float _second)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = -1;
        }
        else
        {
            Debug.LogWarning("Renderer component not found!");
        }
        yield return new WaitForSeconds(_second);
        if (spriteRenderer != null)
        {
            if (photonView.IsMine && spriteRenderer) spriteRenderer.sortingOrder = 1;
            else if (!photonView.IsMine && spriteRenderer) spriteRenderer.sortingOrder = 0;
        }
    }
    public void UseChidoriSkillAfter(float _seconds)
    {
        StartCoroutine("UseChidoriSkillAfterCouroutine", _seconds);
    }
    IEnumerator UseChidoriSkillAfterCouroutine(float _seconds)
    {
        yield return new WaitForSeconds(_seconds);
        stateMachine.ChangeState(swapPosSkillState);
    }
    [PunRPC]
    public void IgnoreCollider(bool _isEnable)
    {
        Physics2D.IgnoreLayerCollision(3, 3, _isEnable);
    }
    [PunRPC]
    public void RPC_EnableSasukeRunSound(bool _isEnable)
    {
        if (!_isEnable) runSound.Stop();
        else
        if (runSound != null&&!runSound.isPlaying)
        {
            runSound.Play();
        }
    }
    [PunRPC]
    public void RPC_EnableSasukeAttackSound(bool _isEnable)
    {
        if (!_isEnable) attackSound.Stop();
        else
        if (attackSound != null && !attackSound.isPlaying)
        {
            attackSound.Play();
        }
    }


    protected override void DisplayHealthUI()
    {
        base.DisplayHealthUI();
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}
