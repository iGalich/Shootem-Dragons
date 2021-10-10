using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BulletPro;


public class Actor : MonoBehaviour
{

    [SerializeField] protected int health;

    protected float firerate;

    private Rigidbody2D rb;

    protected bool isAlive = true;

    public bool CurrState => isAlive;
    public virtual void OnHitByBullet(Bullet bullet, Vector3 hitPoint)
    {
        Debug.Log(this.name + " received damaged! Ouch!");
    }
    protected virtual void Death()
    {
        Debug.Log("Death was not implemented in " + this);
    }
}
