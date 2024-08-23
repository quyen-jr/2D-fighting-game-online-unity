using Photon.Pun;
using System.Collections;
using UnityEngine;

public class Asakura : Entity
{
    public AsakuraIdleState idleState { get; private set; }
    public AsakuraRunState runState { get; private set; }
    public AsakuraJumpState jumpState { get; private set; }
    public AsakuraAirState airState { get; private set; }
    public AsakuraAttackState attackState { get; private set; }
    public AsakuraDashState dashState { get; private set; }
    public AsakuraSlashSkillState slashSkillState { get; private set; }
    public AsakuraSweepSkillState   sweepSkillState { get; private set; }
    public AsakuraGetDamageState getDamageState { get; private set; }
    public AsakuraUltiSkillState ultiSkillState { get; private set; }
    public Asakura_SkillManager skill { get; private set; }


    [SerializeField] private AudioClip sweep1SkillClip;
    private AudioSource sweep1SkillSource = new AudioSource();
    [SerializeField] private AudioClip sweep2SkillClip;
    private AudioSource sweep2SkillSource = new AudioSource();
    [SerializeField] private AudioClip sweep3SkillClip;
    private AudioSource sweep3SkillSource = new AudioSource();
    protected override void Awake()
    {
        base.Awake();
        sweep1SkillSource = gameObject.AddComponent<AudioSource>();
        sweep1SkillSource.clip= sweep1SkillClip;

        sweep2SkillSource = gameObject.AddComponent<AudioSource>();
        sweep2SkillSource.clip = sweep2SkillClip;

        sweep3SkillSource = gameObject.AddComponent<AudioSource>();
        sweep3SkillSource.clip = sweep3SkillClip;
    }
    protected override void Start()
    {
        base.Start();
        skill = GetComponentInChildren<Asakura_SkillManager>();
        idleState = new AsakuraIdleState(this, stateMachine, "Idle");
        runState = new AsakuraRunState(this, stateMachine, "Run");
        jumpState = new AsakuraJumpState(this, stateMachine, "Jump");
        airState = new AsakuraAirState(this, stateMachine, "Jump");
        attackState = new AsakuraAttackState(this, stateMachine, "Attack");
        dashState = new AsakuraDashState(this, stateMachine, "Dash");
        slashSkillState = new AsakuraSlashSkillState(this, stateMachine, "SlashSkill");
        sweepSkillState = new AsakuraSweepSkillState(this, stateMachine, "SweepSkill");
        getDamageState = new AsakuraGetDamageState(this, stateMachine, "GetDamage");
        ultiSkillState = new AsakuraUltiSkillState(this, stateMachine, "UltiSkill");
        stateMachine.Initialize(idleState);

    }

    public override void Update()
    {
        if (gameEnd) return;
        CheckIfLoss();
        if (!photonView.IsMine) return;
        stateMachine.currentState.Update();
        if (Input.GetKeyDown(KeyCode.L)&&skill.dashSkill.CanUseSkill())
        {
            stateMachine.ChangeState(dashState);
            return;
        }
        if (Input.GetKeyDown(KeyCode.U) && skill.slashSkill.CanUseSkill())
        {
            stateMachine.ChangeState(slashSkillState);
            return;
        }
        if (Input.GetKeyDown(KeyCode.I) && skill.sweepSkill.CanUseSkill())
        {
            stateMachine.ChangeState(sweepSkillState);
            return;
        }
        if (Input.GetKeyDown(KeyCode.O) && skill.ultiSkill.CanUseSkill())
        {
            stateMachine.ChangeState(ultiSkillState);
            return;
        }

    }
    public override void takeDamage(int _damage)
    {
        currentHealth -= _damage;
        stateMachine.ChangeState(getDamageState);
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
    [PunRPC]
    public void IgnoreCollider(bool _isEnable)
    {
        Physics2D.IgnoreLayerCollision(3, 3, _isEnable);
    }
    [PunRPC]
    public void RPC_EnableAsakuraRunSound(bool _isEnable)
    {
        if (!_isEnable) runSound.Stop();
        else
        if (runSound != null && !runSound.isPlaying)
        {
            runSound.Play();
        }
    }
    [PunRPC]
    public void RPC_EnableAsakuraAttackSound(bool _isEnable)
    {
        if (!_isEnable) attackSound.Stop();
        else
        if (attackSound != null && !attackSound.isPlaying)
        {
            attackSound.Play();
        }
    }
    [PunRPC]
    public void RPC_EnableAsakuraSweepSkillSound(bool _isEnable)
    {
        if (!_isEnable)
        {
            sweep1SkillSource.Stop();
            sweep2SkillSource.Stop();
            sweep3SkillSource.Stop();
            return;
        }
        if (sweep1SkillSource != null && !sweep1SkillSource.isPlaying)
        {
            sweep1SkillSource.Play();
        }
        if (sweep2SkillSource != null && !sweep2SkillSource.isPlaying)
        {
            sweep2SkillSource.Play();
        }
        if (sweep3SkillSource != null && !sweep3SkillSource.isPlaying)
        {
            sweep3SkillSource.Play();
        }
    }
    protected override void DisplayHealthUI()
    {
        base.DisplayHealthUI();
    }
    protected  override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}
