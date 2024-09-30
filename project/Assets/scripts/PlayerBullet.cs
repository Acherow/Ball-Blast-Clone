using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            collision.gameObject.GetComponent<Ball>().takedamage();
            Destroy(gameObject);
        }
    }
}
