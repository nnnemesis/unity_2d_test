using UnityEngine;
using System.Collections;

public class BaseBulletController : MonoBehaviour
{
    public float BulletSpeed = 5f;
    public float DamageAmount = 5f;
    public float LifeTime = 15f;

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.LogWarning("OnTriggerEnter");
        var damagable = other.GetComponent<IDamageble>();
        if (damagable != null)
        {
            damagable.InflictDamage(DamageType.Bullet, DamageAmount);
            DestroyOnHit();
        }
        else if (!other.isTrigger)
        {
            DestroyOnHit();
        }
    }

    void DestroyOnHit()
    {
        Destroy(gameObject);
    }

    void DestroyOnTime()
    {
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        transform.position = transform.position + transform.rotation * new Vector3(BulletSpeed, 0f) * Time.fixedDeltaTime;
        LifeTime -= Time.fixedDeltaTime;
        if (LifeTime < 0)
        {
            DestroyOnTime();
        }
    }
}