using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BulletPro;

public class Enemy : Actor
{
    public override void OnHitByBullet(Bullet bullet, Vector3 hitPoint)
    {
        base.OnHitByBullet(bullet, hitPoint);
    }
}
