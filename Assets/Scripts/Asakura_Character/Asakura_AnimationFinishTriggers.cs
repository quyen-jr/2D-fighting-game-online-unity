using Photon.Pun;
using UnityEngine;

public class Asakura_AnimationFinishTriggers : MonoBehaviour
{
    [SerializeField] private GameObject hitEffectPrefab;
    Entity entity => GetComponentInParent<Entity>();
    private void AnimationFinishTrigger()
    {
        if(!entity.photonView.IsMine) return;
        entity.AnimationFinishTrigger();
    }
    private void SpawnSlashSkill()
    {
        if (!entity.photonView.IsMine) return;

        Asakura asakura = entity as Asakura;
        asakura.skill.slashSkill.UseSkill();
    }
    private void AttackTrigger()
    {
        if (!entity.photonView.IsMine) return;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(entity.attackCheck.position, entity.attackCheckRadius);
        foreach (Collider2D collider in colliders)
        {
            Entity enemy = collider.GetComponent<Entity>();
            if (enemy != null && !enemy.photonView.IsMine)
            {
                GameObject hitEffect= PhotonNetwork.Instantiate(hitEffectPrefab.name,enemy.transform.position,Quaternion.identity);
                hitEffect.GetComponent<HitEffect>().SetUpEffect(entity.transform.localScale.x);
                enemy.photonView.RPC("RPC_TakeDamage", RpcTarget.AllViaServer, 10);
            }
        }
    }
    private void AsakuraUltiAttackTrigger()
    {
        if (!entity.photonView.IsMine) return;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(entity.attackCheck.position, 3f);
        foreach (Collider2D collider in colliders)
        {
            Entity enemy = collider.GetComponent<Entity>();
            if (enemy != null && !enemy.photonView.IsMine)
            {
                GameObject hitEffect = PhotonNetwork.Instantiate(hitEffectPrefab.name, enemy.transform.position, Quaternion.identity);
                enemy.photonView.RPC("RPC_TakeDamage", RpcTarget.AllViaServer, 30);
            }
        }
    }
    private void SweepSkillTrigger()
    {
        if (!entity.photonView.IsMine) return;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(entity.attackCheck.position, entity.attackCheckRadius);
        foreach (Collider2D collider in colliders)
        {
            Entity enemy = collider.GetComponent<Entity>();
            if (enemy != null && !enemy.photonView.IsMine)
            {
                enemy.photonView.RPC("RPC_TakeDamage", RpcTarget.AllViaServer, 5);
            }
        }
    }

}
