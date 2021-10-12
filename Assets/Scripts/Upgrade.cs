using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    private enum UpgradeType
    {
        Health,
        Damage,
        Spread
    }

    [SerializeField] private UpgradeType upgradeType;

    private BoxCollider2D boxCollider2D;
    private ParticleSystem particles;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        particles = GetComponentInChildren<ParticleSystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.Play("Upgrade");

            switch (upgradeType)
            {
                case UpgradeType.Health:
                    GameManager.Instance.player.Health++;
                    GameManager.Instance.hud.AddHeart();
                    break;
                case UpgradeType.Damage:
                    GameManager.Instance.player.Damage++;
                    break;
                case UpgradeType.Spread:
                    UpgradeSpread();
                    break;
            }
            GameManager.Instance.upgradeSpawner.DestroyUpgrades();
        }
    }
    private void UpgradeSpread()
    {
        GameManager.Instance.player.FirePlayerBullets.IncreaseBulletsAmount(2);
        GameManager.Instance.player.FirePlayerBullets.ChangeAngle(15f);
    }
    private void OnDestroy()
    {
       var system = Instantiate(particles, transform.position, transform.rotation);
        system.Play();
    }
}
