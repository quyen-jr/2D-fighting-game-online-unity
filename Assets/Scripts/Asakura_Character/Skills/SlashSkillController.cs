using Photon.Pun;
using System.Collections;
using UnityEngine;

public class SlashSkillController : MonoBehaviour
{
    private int moveSpeed;
    private PhotonView view => GetComponent<PhotonView>();
    private Vector2 faceDir;
    public float timeLife;

    private Animator anim => GetComponentInChildren<Animator>();
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    private bool canExplouse = false;
    public void SetUpSkill(int _moveSpeed, Vector2 _faceDir)
    {
        moveSpeed = _moveSpeed;
        faceDir = _faceDir;
        rb.velocity = new Vector2(_moveSpeed * faceDir.x, 0);
        transform.localScale = new Vector2(faceDir.x, transform.localScale.x);
        //anim.SetBool("SlashRun", true);
        //StartCoroutine("selfDestroy", 2f);
    }
    void Update()
    {
        if (!view.IsMine) return;
        timeLife -= Time.deltaTime;
        if (timeLife < 0&&!canExplouse)
        {
            selfDestroy();
        }
        if (canExplouse)
        {
            anim.SetBool("SlashExplouse", true);
            StartCoroutine("selfDestroyAfter", 0.4f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Collider2D[] colliders = Physics2D.OverlapCircleAll(entity.attackCheck.position, entity.attackCheckRadius);
        //foreach (Collider2D collider in colliders)
        //{
        if (!view.IsMine) return;
        Entity enemy = collision.GetComponent<Entity>();
        if (enemy != null && !enemy.photonView.IsMine)
        {
            Debug.Log("attack player");
            canExplouse = true;
            rb.velocity = new Vector2(0, 0);
            transform.position = new Vector2(enemy.transform.position.x,enemy.transform.position.y);
            //      if (PhotonNetwork.IsMasterClient)
            enemy.photonView.RPC("RPC_TakeDamage", RpcTarget.AllViaServer,20);
            // else 
            //selfDestroy();
        }
        //}
    }
    IEnumerator selfDestroyAfter(float _second)
    {
        yield return new WaitForSeconds(_second);
        selfDestroy();
    }
    private void selfDestroy()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
