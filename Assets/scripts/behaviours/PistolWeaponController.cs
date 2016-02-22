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

    public void StartUsing()
    {
        if (State.UseState == WeaponUseState.Idle && State.CurrentMagazineAmmo > 0)
        {
            State.UseState = WeaponUseState.Use;
            MakeShot();
            Invoke("UsingDone", State.OneUseTime);
        }            
    }

    private void MakeShot()
    {
        State.CurrentMagazineAmmo -= 1;
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
        State.UseState = WeaponUseState.Idle;
    }

    public void StopUsing()
    {
        // no reaction, pistol will travel to idle after one use

    }

    public void Recharge()
    {
        if (State.UseState != WeaponUseState.Reload)
        {
            State.UseState = WeaponUseState.Reload;
            DoRecharge();
            Invoke("UsingDone", State.ReloadTime);
        }
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