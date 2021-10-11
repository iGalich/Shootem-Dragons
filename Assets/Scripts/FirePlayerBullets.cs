using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlayerBullets : MonoBehaviour
{
    [SerializeField] private int bulletsAmount = 10;

    [SerializeField] private float startAngle = 90f, endAngle = 270f;

    private Vector2 bulletMoveDirection;

    private float bulletCooldown = 0.3f;
    private float lastBullet = float.MinValue;

    private void Update()
    {
        if (Input.GetButton("Jump") && Time.time - lastBullet > bulletCooldown && Player.Instance.IsAlive)
        {
            Fire();
            lastBullet = Time.time;
        }
    }
    private void Fire()
    {
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = PlayerBulletsPool.Instance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetMoveDirection(bulDir);

            angle += angleStep;
        }
    }
    public void IncreaseBulletsAmount(int amount)
    {
        bulletsAmount += amount;
    }
    public void ChangeAngle(float angle)
    {
        startAngle -= angle;
        endAngle += angle;
    }
}
