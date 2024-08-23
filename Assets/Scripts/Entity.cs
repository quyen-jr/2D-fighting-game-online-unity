using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine.UIElements;
using System;
using UnityEngine.TextCore.Text;

public class Entity : MonoBehaviour
{
    public GameObject playerTagName;
    protected SpriteRenderer spriteRenderer;
    [Header("Move Info")]
    public int speed;
    public int jumpForce;
    public bool cantJumpAndUseSkill;
    [Header("Dash Info")]
    public float dashDuration;
    public float dashSpeed;
    private bool isBusy;
    [Header("Primary Attack Info")]
    public float attackPrimarySpeed;
    [NonSerialized]public CapsuleCollider2D cd;
    [Header("Collision Info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask WhatIsLayer;
    [Header("Attack Check")]
    public Transform attackCheck;
    public float attackCheckRadius;
    [Header("Stats Info")]
    private int maxHealth = 100;
    public int currentHealth = 100;
    [Header("HealthUI")]
    [NonSerialized]public HeathBarUi healthBarUI;
    public GameObject leftHealthUI;
    public GameObject rightHealthUI;
    public GameObject leftSkillUI;
    public GameObject rightSkillUI;
    public bool gameEnd;
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    [NonSerialized]public PhotonView photonView;

    public PlayerStateMachine stateMachine;

    [Header("Audio")]
    [SerializeField] private AudioClip runClip;
    [SerializeField] private AudioClip attackClip;
    protected AudioSource attackSound = new AudioSource();
    protected AudioSource runSound = new AudioSource();
    protected virtual void Awake()
    {
        attackSound = gameObject.AddComponent<AudioSource>();
        runSound = gameObject.AddComponent<AudioSource>();

        attackSound.clip = attackClip;
        runSound.clip = runClip;
        photonView = GetComponent<PhotonView>();
    }
    protected virtual void Start()
    {
        if (playerTagName != null&& photonView.IsMine)
            playerTagName.SetActive(true);
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new PlayerStateMachine();

        // change side and order
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        cd = GetComponent<CapsuleCollider2D>();
        if (photonView.IsMine && spriteRenderer) spriteRenderer.sortingOrder = 1;
        else if (!photonView.IsMine && spriteRenderer) spriteRenderer.sortingOrder = 0;


        DisplayHealthUI();

    }
    protected virtual void DisplayHealthUI()
    {
        if(PhotonNetwork.IsMasterClient)
            photonView.RPC("RPC_SyncHealthUI", RpcTarget.AllBuffered);
        DisplaySkillCoolDownUI();
    }

    private void DisplaySkillCoolDownUI()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (photonView.IsMine)
            {
                leftSkillUI.SetActive(true);
            }
        }
        else
        {
            if(photonView.IsMine)
            {
                rightSkillUI.SetActive(true);
            }
        }

    }
    [PunRPC]
    public void RPC_SyncHealthUI()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (photonView.IsMine)
            {
                rightHealthUI.SetActive(false);
            }
            else
            {
                leftHealthUI.SetActive(false);
            }
        }
        else
        {
            if (!photonView.IsMine)
            {
                rightHealthUI.SetActive(false);
            }
            else
            {
                leftHealthUI.SetActive(false);
            }
        }
   
    }
    public virtual void Flip()
    {

        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }
    public virtual void SetZeroVelocity()
    {
        rb.velocity = new Vector2(0, 0);
    }
    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        Flip();
    }
    public bool isGroundedDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, WhatIsLayer);
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    public virtual void AnimationFinishTrigger()
    {
        stateMachine.currentState.AnimationTrigger();
    }
    public virtual void Update()
    {

    }
    protected void CheckIfLoss() {
        if (currentHealth < 0&&photonView.IsMine)
        {
            GameManager.instance.EnableResultUI(false);
            GameManager.instance.view.RPC("RPC_EnableResultUI", RpcTarget.Others, true);
            photonView.RPC("RPC_GamEnd", RpcTarget.AllBuffered);
        }
        if (currentHealth < 0 && !photonView.IsMine)
        {
            GameManager.instance.EnableResultUI(true);
            GameManager.instance.view.RPC("RPC_EnableResultUI", RpcTarget.Others, false);
            photonView.RPC("RPC_GamEnd", RpcTarget.AllBuffered);
        }
    }
    [PunRPC]
    public void RPC_TakeDamage(int _damage)
    {
        takeDamage(_damage);
    }
    [PunRPC]
    public void RPC_GamEnd()
    {
        gameEnd=true;
    }
    [PunRPC]
    public void RPC_UpdateHealthUI()
    {
        healthBarUI = GetComponentInChildren<HeathBarUi>();
        if(healthBarUI != null ) 
            healthBarUI.UpdateHealthUI();
    }
    public virtual void takeDamage(int _damage)
    {

    }

    public int GetMaxHealthValue()
    {
        return maxHealth;
    }

    public static implicit operator Entity(GameObject v)
    {
        throw new NotImplementedException();
    }
    [PunRPC]
    public void RPC_SwapPosBetweenTwoPlayers(bool _isMaster, float x, float y)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(_isMaster);
        if (objectsWithTag.Length < 2) return;
        Entity character1 = (Entity)objectsWithTag[0].GetComponent<Entity>();
        Entity character2 = (Entity)objectsWithTag[1].GetComponent<Entity>();
        Vector2 pos = new Vector2(x, y);
        if (_isMaster)
        {
            // client
            if (!PhotonNetwork.IsMasterClient && character1.photonView.IsMine)
            {                
                character1.transform.position= pos;
            }
            if (!PhotonNetwork.IsMasterClient && character2.photonView.IsMine)
            {
                character2.transform.position = pos;
            }
        }
        else
        {
            // master
            if (PhotonNetwork.IsMasterClient && character1.photonView.IsMine)
            {
                character1.transform.position = pos;
            }
            if (PhotonNetwork.IsMasterClient && character2.photonView.IsMine)
            {
                character2.transform.position = pos;
            }
        }
    }
}
