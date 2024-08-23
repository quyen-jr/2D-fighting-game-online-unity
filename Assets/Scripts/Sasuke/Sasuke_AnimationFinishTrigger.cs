using Photon.Pun;
using UnityEngine;

public class Sasuke_AnimationFinishTrigger : MonoBehaviour
{
    [SerializeField] private GameObject hitEffectPrefab;
    Entity entity => GetComponentInParent<Entity>();

    private void CreateChidoriPrepare()
    {
        //Debug.Log(1);
        if (!entity.photonView.IsMine) return;
        Sasuke sasuke = entity as Sasuke;
        sasuke.skill.chidoriSkill.CreateChidoriPrepare();
    }
    private void IncreaseStateNumberOfChidoriAnimation()
    {
        // qua moi  state thi player se thuc hien cac animation khac nhau cua skill chidori
        Sasuke sasuke = entity as Sasuke;
        if (sasuke != null)
        {
                sasuke.chidoriSkillState.IncreaseStateOfChidori();
        }
    }
    private void AnimationFinishTrigger()
    {
        if (!entity.photonView.IsMine) return;
        entity.AnimationFinishTrigger();
    }
    private void EnableAttackSound()
    {
        if (!entity.photonView.IsMine) return;
        Sasuke sasuke = entity as Sasuke;
        sasuke.photonView.RPC("RPC_EnableSasukeAttackSound", RpcTarget.AllViaServer, true);
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
                GameObject hitEffect = PhotonNetwork.Instantiate(hitEffectPrefab.name, enemy.transform.position, Quaternion.identity);
                hitEffect.GetComponent<HitEffect>().SetUpEffect(entity.transform.localScale.x);
                enemy.photonView.RPC("RPC_TakeDamage", RpcTarget.AllViaServer, 10);
            }
        }
    }
    private void CheckIfCanTurnToChidoriAttackMode()
    {
        if (!entity.photonView.IsMine) return;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(entity.attackCheck.position, entity.attackCheckRadius);
        foreach (Collider2D collider in colliders)
        {
            Entity enemy = collider.GetComponent<Entity>();
            if (enemy != null && !enemy.photonView.IsMine)
            {
                Sasuke sasuke = entity as Sasuke;
                if (sasuke != null)
                {
                    // phong truong hop  tang vuot
                    if (sasuke.chidoriSkillState.ChidoriStateNumber == 1)
                    {
                        Debug.Log(1);
                        sasuke.SetZeroVelocity();
                        sasuke.chidoriSkillState.timeOfrun = -1;
                        //sasuke.chidoriSkillState.ChidoriStateNumber = 2;
                      //  sasuke.anim.SetInteger("ChidoriStateNumber", sasuke.chidoriSkillState.ChidoriStateNumber);
                        //sasuke.skill.chidoriSkill.TurnIntoChidoriAttack();
                    }

                }

            }
        }
    }

}
