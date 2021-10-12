using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Actor
{
    private Animator anim;

    private FirePlayerBullets firePlayerBullets;

    private ParticleSystem jetParticles;

    private int damage = 1;

    private float lastHit = float.MinValue;
    private float iFrames = 0.5f;

    public FirePlayerBullets FirePlayerBullets { get => firePlayerBullets; set => firePlayerBullets = value; }
    public bool IsAlive { get => isAlive; set => isAlive = value; }
    public int Health { get => health; set => health = value; }
    public int Damage { get => damage; set => damage = value; }

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        firePlayerBullets = GetComponent<FirePlayerBullets>();
        jetParticles = GetComponentInChildren<ParticleSystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            OnHitByBullet();
            collision.GetComponent<Bullet>().Destroy();
        }
    }
    public override void OnHitByBullet()
    {
        if (!isAlive || GameManager.Instance.GodMode || Time.time - lastHit < iFrames) return;

        AudioManager.Instance.Play("PlayerHit");
        lastHit = Time.time;
        ScoreManager.Instance.Combo = 0;
        GameManager.Instance.hud.RemoveHeart();
        GameManager.Instance.cameraManager.TriggerShake(0.3f, 0.05f);
        health--;
        if (health <= 0)
            Death();
    }
    protected override void Death()
    {
        AudioManager.Instance.ChangeVolume(0f, 1.5f);
        jetParticles.Stop();
        boxCollider2D.enabled = false;
        GameManager.Instance.hud.ShowDeathMenu();
        isAlive = false;
        rb.gravityScale = 0.5f;
        anim.SetTrigger("Dead");
        ScoreManager.Instance.CheckScore();
    }
}