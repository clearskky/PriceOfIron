using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    public GameObject Player;
    GameObject wall;
    RaycastHit raycastHit;

    void FollowPlayer()
    {
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 5f, Player.transform.position.z - 3f);
    }

    void Update()
    {
        FollowPlayer();
    }
}
