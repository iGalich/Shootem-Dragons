using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Actor : MonoBehaviour
{
    [SerializeField] private int minHealth;

    [SerializeField] protected int health;

    protected float firerate;

    protected Rigidbody2D rb;

    protected BoxCollider2D boxCollider2D;

    protected bool isAlive = true;

    public int MinHealth => minHealth;
    public bool CurrState => isAlive;
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    public virtual void OnHitByBullet()
    {
        Debug.Log(this.name + " received damaged! Ouch!");
    }
    protected virtual void Death()
    {
        Debug.Log("Death was not implemented in " + this);
    }
}
