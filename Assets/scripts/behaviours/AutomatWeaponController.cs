using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutomatWeaponController : MonoBehaviour,ITireEventListener
{
    private WeaponState State;
    private IEventTire EventTire;
    public GameObject BulletPrefab;
    public Transform BulletSpawnPosition;

    private Coroutine ReloadRoutine;
    private Coroutine AutoShootRoutine;

    void Awake()
    {
        State = GetComponent<WeaponState>();
    }

    void Start()
    {
        EventTire = GetComponent<IEventTire>();
        EventTire.AddEventListener(TireEventType.ControlEvent, this);
    }

    public void OnTireEvent(TireEvent ev)
    {
        if (ev.Type == TireEventType.ControlEvent)
        {
            OnControlEvent((ControlEvent)ev);
        }
    }

    void OnControlEvent(ControlEvent e)
    {
        Dictionary<ControlAction, bool> actions = e.Actions;
        if (actions[ControlAction.MainAttack])
        {
            StartUsing();
        }
        else
        {
            StopUsing();
        }
    }

    void StartUsing()
    {
        if (State.UseState == WeaponUseState.Idle && State.CurrentMagazineAmmo > 0)
        {
            State.UseState = WeaponUseState.Use;
            AutoShootRoutine = StartCoroutine(AutoShootCoroutine());
        }            
    }

    IEnumerator AutoShootCoroutine()
    {
        while(State.CurrentMagazineAmmo > 0 && State.UseState == WeaponUseState.Use)
        {
            MakeShot();
            yield return new WaitForSeconds(State.OneUseTime);
        }
        State.UseState = WeaponUseState.Idle;
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
        var angle = Vector3.Angle(Vector3.right, worldMousePosition - spawnPosition);
        //Debug.Log("angle " + angle);
        GameObject bullet = (GameObject)Instantiate(BulletPrefab, BulletSpawnPosition.position, Quaternion.FromToRotation(Vector3.right, worldMousePosition - spawnPosition));
    }

    void StopUsing()
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

}