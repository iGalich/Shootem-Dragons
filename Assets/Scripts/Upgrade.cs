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

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (upgradeType)
            {
                case UpgradeType.Health:
                    Player.Instance.Health++;
                    Hud.Instance.AddHeart();
                    break;
                case UpgradeType.Damage:
                    Player.Instance.Damage += 2;
                    break;
                case UpgradeType.Spread:
                    UpgradeSpread();
                    break;
            }
            UpgradeSpawner.Instance.DestroyUpgrades();
        }
    }
    private void UpgradeSpread()
    {
        Player.Instance.FirePlayerBullets.IncreaseBulletsAmount(2);
        Player.Instance.FirePlayerBullets.ChangeAngle(15f);
    }
}
