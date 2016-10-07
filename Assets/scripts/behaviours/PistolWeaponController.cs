using UnityEngine;
using System.Collections.Generic;

public class PistolWeaponController : MonoBehaviour
{
    private WeaponState State;
    private IEventTire EventTire;
    public GameObject BulletPrefab;
    public Transform BulletSpawnPosition;

    void Start()
    {
        State = GetComponent<WeaponState>();
        EventTire = this.GetEventTire();
        EventTire.AddEventListener(TireEventType.ControlEvent, OnControlEvent);
    }

    void OnControlEvent(object param)
    {
        Dictionary<ControlAction, bool> actions = (Dictionary<ControlAction, bool>)param;
        if (actions[ControlAction.MainAttack])
        {
            StartUsing();
        }
    }

    void StartUsing()
    {
        if (State.UseState == WeaponUseState.Idle && State.CurrentMagazineAmmo > 0)
        {
            State.UseState = WeaponUseState.Use;
            MakeShot();
            Invoke("UsingDone", State.OneUseTime);
        }            
    }

    void MakeShot()
    {
        State.CurrentMagazineAmmo -= 1;
        var camera = Camera.main;
        var screenMousePosition = Input.mousePosition;
        var worldMousePosition = camera.ScreenToWorldPoint(screenMousePosition);
        worldMousePosition.z = 0;
        //Debug.Log("worldMousePosition " + worldMousePosition);
        var spawnPosition = BulletSpawnPosition.position;
        //Debug.Log("spawnPosition " + spawnPosition);
        //var angle = Vector3.Angle(Vector3.right, worldMousePosition - spawnPosition);
        //Debug.Log("angle " + angle);
        GameObject bullet = (GameObject)Instantiate(BulletPrefab, BulletSpawnPosition.position, Quaternion.FromToRotation(Vector3.right, worldMousePosition - spawnPosition));
    }

    void UsingDone()
    {
        State.UseState = WeaponUseState.Idle;
    }

}