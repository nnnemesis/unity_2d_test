using UnityEngine;
using System.Collections;

public class PistolWeaponController : MonoBehaviour, IWeapon
{
    private WeaponState State;
    private IEventTire EventTire;
    public GameObject BulletPrefab;
    public Transform BulletSpawnPosition;

    void Awake()
    {
        EventTire = GetComponent<IEventTire>();
        State = GetComponent<WeaponState>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.LogWarning("OnTriggerEnter");
        if(State.UseState == WeaponUseState.Use)
        {
            var damagable = other.gameObject.GetComponent<IDamageble>();
            if(damagable != null)
            {
                damagable.InflictDamage(DamageType.Hit, State.DamageAmount);
            }
        }
            
    }

    public void StartUsing()
    {
        if (State.UseState == WeaponUseState.Idle)
        {
            State.UseState = WeaponUseState.Use;
            MakeShot();
            Invoke("UsingDone", State.OneUseTime);
        }            
    }

    private void MakeShot()
    {
        var camera = Camera.main;
        var screenMousePosition = Input.mousePosition;
        var worldMousePosition = camera.ScreenToWorldPoint(screenMousePosition);
        worldMousePosition.z = 0;
        Debug.Log("worldMousePosition " + worldMousePosition);
        var spawnPosition = BulletSpawnPosition.position;
        Debug.Log("spawnPosition " + spawnPosition);
        var angle = Vector3.Angle(Vector3.right, worldMousePosition - spawnPosition);
        Debug.Log("angle " + angle);
        GameObject bullet = (GameObject)Instantiate(BulletPrefab, BulletSpawnPosition.position, Quaternion.FromToRotation(Vector3.right, worldMousePosition - spawnPosition));
    }

    private void UsingDone()
    {
        if (State.UseState == WeaponUseState.Use)
            State.UseState = WeaponUseState.Idle;
    }

    public void StopUsing()
    {
        // no reaction, pistol will travel to idle after one use

    }

    public void Recharge()
    {
        //need recharge
    }

    public IEventTire GetTire()
    {
        return EventTire;
    }

}