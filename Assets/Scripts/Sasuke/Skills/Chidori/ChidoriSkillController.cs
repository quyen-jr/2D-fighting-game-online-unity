using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChidoriSkillController : MonoBehaviour
{
    private bool turnToAttackMode;
    public PhotonView view => GetComponent<PhotonView>();
    private Vector2 faceDir;
    public float timeLife;
    private Animator anim => GetComponentInChildren<Animator>();
    public Entity entity;

    [Header("Audio")]
    [SerializeField] private AudioClip attackClip;
    [SerializeField] private AudioClip appearClip;
    public AudioSource attackSound = new AudioSource();
    private AudioSource appearSound = new AudioSource();
    private void Start()
    {
        attackSound = gameObject.AddComponent<AudioSource>();
        appearSound = gameObject.AddComponent<AudioSource>();

        attackSound.clip = attackClip;
        appearSound.clip = appearClip;
        if (appearSound != null)
            appearSound.Play();
    }
    public void SetUpSkill(Vector2 _faceDir)
    {
        //if (!view.IsMine) return;
        faceDir = _faceDir;
        transform.localScale = new Vector2(faceDir.x, transform.localScale.y);
        //transform.SetParent(entity.transform);
       
    }
    [PunRPC]
    public void MatchSkillForPlayerTransfrom(bool _isMaster)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(_isMaster);
        if (objectsWithTag.Length < 2) return;
        Entity character1 =(Entity) objectsWithTag[0].GetComponent<Entity>();
        Entity character2 = (Entity)objectsWithTag[1].GetComponent<Entity>();
        //// master  tao ra 
        
        ///
        if (_isMaster)
        {
            // master
            if (PhotonNetwork.IsMasterClient  && character1.photonView.IsMine)
            {
                transform.SetParent(character1.transform);
            }
            if (PhotonNetwork.IsMasterClient  && character2.photonView.IsMine)
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
            if (PhotonNetwork.IsMasterClient  && character1.photonView.IsMine)
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
    public void TurnToAttackMode(bool _turnToAttackMode )
    {
        if (!view.IsMine) return;
        turnToAttackMode = _turnToAttackMode;
        if (turnToAttackMode)
            anim.SetBool("AttackAnimation", true);
        view.RPC("RPC_EnableAttackAudio", RpcTarget.AllViaServer);
        transform.position = new Vector2(transform.position.x + 0.83f * faceDir.x, transform.position.y - 0.3f);
    }

    private void Update()
    {
        if (!view.IsMine) return;
        if (turnToAttackMode)
        {
            timeLife -= Time.deltaTime;
            if(timeLife < 0)
                PhotonNetwork.Destroy(gameObject); 
        }
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
