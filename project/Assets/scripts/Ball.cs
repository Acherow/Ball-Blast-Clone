using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class Ball : MonoBehaviour
{
    public int health;
    int hp;
    public GameObject textbox;
    public GameObject dust;
    public GameObject explosionParticles;
    public GameObject nextTier;
    public int spawncount;

    Animator anim;
    GameObject box;
    TMP_Text txt;
    Rigidbody2D rb;
    Collider2D col;

    float iframes = .3f;

    private void Start()
    {
        GameObject canvas = FindObjectOfType<WorldCanvas>().gameObject;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box = Instantiate(textbox);
        box.transform.SetParent(canvas.transform);
        txt = box.GetComponent<TMP_Text>();
        col = GetComponent<Collider2D>();
        col.isTrigger = true;
        hp = health;
        txt.text = hp.ToString();
    }

    private void Update()
    {
        if (iframes > 0)
            iframes -= Time.deltaTime;
        else
            col.isTrigger = false;

        box.transform.position = transform.position;
    }

    public void takedamage()
    {
        hp -= 1;
        anim.SetTrigger("damage");
        txt.text = hp.ToString();
        if (hp <= 0)
        {
            die();
        }
    }

    public void die(bool delete = false)
    {
        if(!delete)
            ScoreTracker.instance.AddScore(health);

        if (!delete && nextTier)
        {
            for (int i = 0; i < spawncount; i++)
            {
                GameObject go = Instantiate(nextTier);
                go.transform.position = transform.position;
                go.GetComponent<Rigidbody2D>().velocity = (Vector2.up * 8) + (Vector2.right * Random.Range(-1f,1f) * 8);
            }
        }
        anim.SetTrigger("death");
    }

    public void queuefree()
    {
        var go = Instantiate(explosionParticles);
        go.transform.position = transform.position;
        Destroy(box);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.GetComponent<Ball>())
        {
            GameObject go = Instantiate(dust);
            go.transform.position = collision.contacts[0].point;
        }
    }
}
