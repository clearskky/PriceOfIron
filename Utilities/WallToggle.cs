using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallToggle : MonoBehaviour
{
    public GameObject Player;

    void FollowPlayer()
    {
        if (Player != null)
        {
            transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 3f, Player.transform.position.z - 2.5f); // 0.15x 12.56y 7.7z
        }
    }
    void FixedUpdate()
    {
        FollowPlayer();
    }
}
