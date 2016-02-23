using UnityEngine;
using System.Collections;

public class AutomatWeaponController : MonoBehaviour, IWeapon
{
    private WeaponState State;
    private IEventTire EventTire;
    public GameObject BulletPrefab;
    public Transform BulletSpawnPosition;

    private Coroutine ReloadRoutine;
    private Coroutine AutoShootRoutine;

    void Awake()
    {
        EventTire = GetComponent<IEventTire>();
        State = GetComponent<WeaponState>();
    }

    public void StartUsing()
    {
        if (State.UseState == WeaponUseState.Idle && State.CurrentMagazineAmmo > 0)
        {
            State.UseState = WeaponUseState.Use;
            AutoShootRoutine = StartCoroutine(AutoShootCoroutine());
        }            
    }

    IEnumerator AutoShootCoroutine()
    {
        while(State.CurrentMagazineAmmo > 0)
        {
            MakeShot();
            yield return new WaitForSeconds(State.OneUseTime);
        }
        State.UseState = WeaponUseState.Idle;
    }

    private void MakeShot()
    {
        State.CurrentMagazineAmmo -= 1;
        var camera = Camera.main;
        var screenMousePosition = Input.mousePosition;
        var worldMousePosition = camera.ScreenToWorldPoint(screenMousePosition);
        worldMousePosition.z = 0;
        //Debug.Log("worldMousePosition " + worldMousePosition);
        var spawnPosition = BulletSpawnPosition.position;
        //Debug.Log("spawnPosition " + spawnPosition);
        var angle = Vector3.Angle(Vector3.right, worldMousePosition - spawnPosition);
        //Debug.Log("angle " + angle);
        GameObject bullet = (GameObject)Instantiate(BulletPrefab, BulletSpawnPosition.position, Quaternion.FromToRotation(Vector3.right, worldMousePosition - spawnPosition));
    }

    public void StopUsing()
    {
        if(State.UseState == WeaponUseState.Use)
        {
            State.UseState = WeaponUseState.Idle;
            StopAutoShootingRoutine();
        }
    }

    void StopAutoShootingRoutine()
    {
        if (AutoShootRoutine != null)
        {
            StopCoroutine(AutoShootRoutine);
            AutoShootRoutine = null;
        }
    }

    public void Recharge()
    {
        if (State.UseState != WeaponUseState.Reload)
        {
            State.UseState = WeaponUseState.Reload;
            StopAutoShootingRoutine();
            ReloadRoutine = StartCoroutine(ReloadCoroutine());
        }
    }

    IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(State.ReloadTime);
        DoRecharge();
        State.UseState = WeaponUseState.Idle;
    }

    void DoRecharge()
    {
        var currentTotal = State.CurrentTotalAmmo;
        if (currentTotal > 0)
        {
            var currentInMagazine = State.CurrentMagazineAmmo;
            var restTotal = currentTotal + currentInMagazine - State.MaxMagazineAmmo;
            if (restTotal >= 0)
            {
                State.CurrentMagazineAmmo = State.MaxMagazineAmmo;
                State.CurrentTotalAmmo = restTotal;
            }
            else
            {
                State.CurrentMagazineAmmo = currentTotal + currentInMagazine;
                State.CurrentTotalAmmo = 0;
            }
        }
    }

    public IEventTire GetTire()
    {
        return EventTire;
    }

}