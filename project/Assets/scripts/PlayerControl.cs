using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject bulletprefab;

    public float movespeed = .2f;
    public float bulletspeed = 3f;
    public float guncooldown = .3f;

    Animator anim;
    List<SpriteRenderer> rends;
    float mousex;

    int movedir;

    float cooldown;

    float iframes;


    private void Start()
    {
        rends = GetComponentsInChildren<SpriteRenderer>().ToList();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (iframes > 0)
        {
            iframes -= Time.deltaTime;
            for (int i = 0; i < rends.Count; i++)
            {
                rends[i].enabled = !rends[i].enabled;
            }
        }
        else if (iframes != -10)
        {
            iframes = -10;
            for (int i = 0; i < rends.Count; i++)
            {
                rends[i].enabled = true;
            }
        }

        mousex = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;


        movedir = Mathf.Clamp(Mathf.RoundToInt(transform.position.x - mousex),-1,1);

        if (cooldown >= 0)
            cooldown -= Time.deltaTime;

        if(Input.GetMouseButton(0) && cooldown < 0)
        {
            anim.SetTrigger("shoot");
            cooldown = guncooldown;
        }
    }

    public void spawnbullet()
    {
        GameObject go = Instantiate(bulletprefab);
        go.transform.position = transform.position + Vector3.up;
        go.GetComponent<Rigidbody2D>().velocity = (Vector2.up * bulletspeed) + (Vector2.right * -movedir * 5);
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, new Vector2(Mathf.Clamp(mousex, -11f,11f), transform.position.y), movespeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ball") && iframes <= 0)
        {
            iframes = .6f;
            var list = FindObjectsByType<Ball>(FindObjectsSortMode.None);
            for (int i = 0; i < list.Length; i++)
            {
                list[i].die(true);
            }
            FindObjectOfType<BallSpawner>().spawn();
            ScoreTracker.instance.ResetScore();
        }
    }
}
