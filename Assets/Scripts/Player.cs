using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BulletPro;

public class Player : Actor
{
    public static Player Instance { get; private set; }

    private int damage = 1;

    private void Awake()
    {
        if (Instance is null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public override void OnHitByBullet(Bullet bullet, Vector3 hitPoint)
    {
        if (!isAlive) return;

        CameraManager.Instance.TriggerShake(0.3f, 0.05f);
        health--;
        if (health < 0)
            Death();
    }
    protected override void Death()
    {
        isAlive = false;
    }
}