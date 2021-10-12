using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    public static int EnemyCount = 0;

    private Animator anim;

    private bool moved;

    private FireEnemyBullets fireEnemyBullets;

    public int Health { get => health; set => health = value; }
    public bool IsAlive => isAlive;
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        fireEnemyBullets = GetComponent<FireEnemyBullets>();
    }
    private void FixedUpdate()
    {
        if (!moved)
            MoveIntoScreen();
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

        AudioManager.Instance.Play("BossDragonDie");
        iTween.ShakePosition(this.gameObject , Vector3.one * 0.08f, 0.2f);
        //health -= Player.Instance.Damage;
        health -= GameManager.Instance.player.Damage;
        if (health <= 0)
            Death();
    }
    protected override void Death()
    {
        ScoreManager.Instance.ScoreUp();
        Destroy(fireEnemyBullets);
        isAlive = false;
        rb.gravityScale = 0.5f;
        anim.SetTrigger("Dead");
        Enemy.EnemyCount--;
        if (Enemy.EnemyCount == 0)
        {
            GameManager.Instance.SpawningEnemies = false;
            GameManager.Instance.FunctionTimerCreated = false;
        }
        FunctionTimer.Create(() => Destroy(gameObject), 1f);
    }
    private void MoveIntoScreen()
    {
        moved = true;
        rb.AddForce(new Vector2(-2f, 0f));
    }
}
