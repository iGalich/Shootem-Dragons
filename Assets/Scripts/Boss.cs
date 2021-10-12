using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Actor
{
    private int maxHealth;

    private float healthRatio;
    private float lastScream = float.MinValue;
    private float screamCooldown = 10f;

    private FireBossBullets fireBossBullets;

    public FireBossBullets FireBossBullets { get => fireBossBullets; set => fireBossBullets = value; }
    protected override void Start()
    {
        base.Start();
        maxHealth = minHealth;
        fireBossBullets = GetComponent<FireBossBullets>();
    }
    private void FixedUpdate()
    {
        if (isAlive)
            transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time) * 0.001f, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnHitByBullet();
            collision.GetComponent<Bullet>().Destroy();
        }
    }
    public override void OnHitByBullet()
    {
        if (!isAlive) return;

        ScoreManager.Instance.BossHit();
        health -= GameManager.Instance.player.Damage;
        HitPointChange();
        if (health <= 0)
            Death();
    }
    private void HitPointChange()
    {
        healthRatio = (float)health / (float)maxHealth;
        GameManager.Instance.hud.HpBarFront.localScale = new Vector3(healthRatio, 1, 1);
    }
    protected override void Death()
    {
        fireBossBullets.enabled = false;
        Destroy(fireBossBullets);
        GameManager.Instance.player.Health++;
        screamCooldown = float.MaxValue;
        ScoreManager.Instance.BossKilled();
        fireBossBullets.StopFiring();
        isAlive = false;
        rb.gravityScale = 0.05f;
        GameManager.Instance.hud.DisableBossHealthBar();
        AudioManager.Instance.ChangeVolume(0f, 10f);
    }
}
