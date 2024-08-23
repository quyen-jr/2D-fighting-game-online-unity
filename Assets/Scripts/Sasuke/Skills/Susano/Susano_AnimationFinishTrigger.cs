using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
[Serializable]
public class Susano_AnimationFinishTrigger : MonoBehaviour
{
    [SerializeField] private GameObject hitEffectPrefab;
    SusanoSkillController susano=>GetComponentInParent<SusanoSkillController>();
    private void ChangeToIdleState()
    {
        if (!susano.view.IsMine) return;
        susano.anim.SetBool("Active", false);
    }

    private void AttackFinishTrigger()
    {
        if (!susano.view.IsMine) return;
        susano.anim.SetBool("Attack", false);
    }
    private void AttackTrigger()
    {
        if (!susano.view.IsMine) return;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(susano.attackCheckObject.position, susano.attackCheckRadius);
        foreach (Collider2D collider in colliders)
        {
            Entity enemy = collider.GetComponent<Entity>();
            if (enemy != null && !enemy.photonView.IsMine)
            {
                GameObject hitEffect = PhotonNetwork.Instantiate(hitEffectPrefab.name, enemy.transform.position, Quaternion.identity);
                hitEffect.GetComponent<HitEffect>().SetUpEffect(susano.transform.localScale.x);
                enemy.photonView.RPC("RPC_TakeDamage", RpcTarget.AllViaServer, 20);
            }
        }
    }

}
