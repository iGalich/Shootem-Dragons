using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Actor
{
    public static Player Instance { get; private set; }

    private Animator anim;

    private FirePlayerBullets firePlayerBullets;

    private int damage = 1;

    private float lastHit = float.MinValue;
    private float iFrames = 0.5f;

    public FirePlayerBullets FirePlayerBullets { get => firePlayerBullets; set => firePlayerBullets = value; }
    public bool IsAlive => isAlive;
    public int Health { get => health; set => health = value; }
    public int Damage { get => damage; set => damage = value; }
    private void Awake()
    {
        if (Instance is null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        firePlayerBullets = GetComponent<FirePlayerBullets>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            OnHitByBullet();
        }
    }
    public override void OnHitByBullet()
    {
        if (!isAlive || GameManager.Instance.GodMode || Time.time - lastHit < iFrames) return;

        lastHit = Time.time;
        ScoreManager.Instance.Combo = 0;
        Hud.Instance.RemoveHeart();
        CameraManager.Instance.TriggerShake(0.3f, 0.05f);
        health--;
        if (health <= 0)
            Death();
    }
    protected override void Death()
    {
        Hud.Instance.ShowDeathMenu();
        isAlive = false;
        rb.gravityScale = 0.5f;
        anim.SetTrigger("Dead");
        ScoreManager.Instance.CheckScore();
    }
}