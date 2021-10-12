using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBossBullets : MonoBehaviour
{
    private float angle = 180f;

    private Coroutine fireCoroutine;

    private bool functionTimerStarted;

    private void FixedUpdate()
    {
        if (fireCoroutine == null && !functionTimerStarted)
        {
            functionTimerStarted = true;
            FunctionTimer.Create(() => fireCoroutine = StartCoroutine(FireCo()), 3f);
        }
    }
    private IEnumerator FireCo()
    {
        while (angle < 360f)
        {
            AudioManager.Instance.Play("DragonFire");

            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = GameManager.Instance.bossBulletPool.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetMoveDirection(bulDir);

            angle += 10f;

            //yield return null;
            yield return new WaitForSecondsRealtime(0.1f);
        }

        FunctionTimer.Create(() => AudioManager.Instance.Play("BossRoar"), 2f);
        angle = 360f;
        FunctionTimer.Create(() => fireCoroutine = StartCoroutine(FireCoAgain()), 3f);
        //}
    }
    private IEnumerator FireCoAgain()
    {
        while (angle > 180f)
        {
            AudioManager.Instance.Play("DragonFire");

            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = GameManager.Instance.bossBulletPool.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetMoveDirection(bulDir);

            angle -= 10f;

            //yield return null;
            yield return new WaitForSecondsRealtime(0.1f);
        }

        FunctionTimer.Create(() => AudioManager.Instance.Play("BossRoar"), 2f);
        angle = 180f;
        FunctionTimer.Create(() => fireCoroutine = StartCoroutine(FireCo()), 3f);
    }
    public void StopFiring()
    {
        StopCoroutine(fireCoroutine);
    }
}