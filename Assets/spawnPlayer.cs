using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class spawnPlayer : MonoBehaviour
{
    public GameObject player;
    public int minX,minY, maxX, maxY;
    void Start()
    {
        Vector2 randomPos= new Vector2(Random.Range(minX,maxX),Random.Range(minY,maxY));
      //  GameObject gameObject= Instantiate(playerPrefab,randomPos,Quaternion.identity);
        PhotonNetwork.Instantiate(player.name, randomPos, Quaternion.identity);
    }
}
