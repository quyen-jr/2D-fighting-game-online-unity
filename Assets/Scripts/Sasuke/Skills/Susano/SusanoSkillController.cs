using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SusanoSkillController : MonoBehaviour
{
    private Entity player;
    public PhotonView view=>GetComponent<PhotonView>();
    public Transform attackCheckObject;
    public float attackCheckRadius;
    public Animator anim=>GetComponentInChildren<Animator>();
    private float timeLife = 12.5f;

    private float lastTimeAttacked;
    private float comboWindow = 1;
    private int AttackCombo=0;
    private float timeCanToNextAttack;

    [Header("Audio")]
    [SerializeField] private AudioClip attackClip;
    [SerializeField] private AudioClip appearClip;
    public AudioSource attackSound= new AudioSource();
    private AudioSource appearSound = new AudioSource();


    void Start()
    {
        attackSound = gameObject.AddComponent<AudioSource>();
        appearSound = gameObject.AddComponent<AudioSource>();

        attackSound.clip = attackClip;
        appearSound.clip = appearClip;
        if(appearSound!=null)
            appearSound.Play();
        if (!view.IsMine) return;
        anim.SetBool("Active", true);
    }
    public void SetUpSkill(Entity _player)
    {
        if (!view.IsMine) return;
        player = _player;
        player.cantJumpAndUseSkill = true;
    }
    void Update()
    {
        if (!view.IsMine) return;

        timeLife -= Time.deltaTime;
        if (timeLife < 0)
        {
            PhotonNetwork.Destroy(gameObject);
            player.cantJumpAndUseSkill = false;
        }
        if (timeCanToNextAttack >= 0)
        {
            timeCanToNextAttack-=Time.deltaTime;
        }
        // attack
        if ( Input.GetKeyDown(KeyCode.J)&&timeCanToNextAttack<0)
        {
            view.RPC("RPC_EnableAttackAudio", RpcTarget.AllViaServer);
            anim.SetInteger("AttackCombo", AttackCombo);
            anim.SetBool("Attack", true);
            AttackCombo++;
            lastTimeAttacked = Time.time;
            timeCanToNextAttack = 0.5f;
        }
        if (AttackCombo > 1 || lastTimeAttacked + comboWindow < Time.time)
        {
            AttackCombo = 0;
        }
    }
    [PunRPC]
    public void MatchSkillForPlayerTransfrom(bool _isMaster)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(_isMaster);
        if (objectsWithTag.Length < 2) return;
        Entity character1 = (Entity)objectsWithTag[0].GetComponent<Entity>();
        Entity character2 = (Entity)objectsWithTag[1].GetComponent<Entity>();
        //// master  tao ra 

        ///
        if (_isMaster)
        {
            // master
            if (PhotonNetwork.IsMasterClient && character1.photonView.IsMine)
            {
                transform.SetParent(character1.transform);
            }
            if (PhotonNetwork.IsMasterClient && character2.photonView.IsMine)
            {
                transform.SetParent(character2.transform);
            }
            // client
            if (!PhotonNetwork.IsMasterClient && character1.photonView.IsMine)
            {
                transform.SetParent(character2.transform);
            }
            if (!PhotonNetwork.IsMasterClient && character2.photonView.IsMine)
            {
                transform.SetParent(character1.transform);
            }
        }
        else
        {
            /// client tao ra
            // client 
            if (!PhotonNetwork.IsMasterClient && character1.photonView.IsMine)
            {
                transform.SetParent(character1.transform);
            }
            if (!PhotonNetwork.IsMasterClient && character2.photonView.IsMine)
            {
                transform.SetParent(character2.transform);
            }
            // master
            if (PhotonNetwork.IsMasterClient && character1.photonView.IsMine)
            {
                transform.SetParent(character2.transform);
            }
            if (PhotonNetwork.IsMasterClient && character2.photonView.IsMine)
            {
                transform.SetParent(character1.transform);
            }
        }
    }
    public void MatchSkillForPlayerCreated(bool _isMaster)
    {
        view.RPC("MatchSkillForPlayerTransfrom", RpcTarget.AllViaServer, _isMaster);
    }
    protected virtual void OnDrawGizmos()
    {
        //Gizmos.DrawLine(at.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawWireSphere(attackCheckObject.position, attackCheckRadius);
    }

    [PunRPC]
    public void RPC_EnableAttackAudio()
    {
            if (attackSound != null)
            {
                attackSound.Play();

            }
    }
}
