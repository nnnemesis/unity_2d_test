using UnityEngine;
using System.Collections.Generic;

public enum WeaponType
{
    Hand,
    Axe,
    Pistol,
    Automat
}

public enum WeaponUseState
{
    Idle,
    Use,
    Reload
}

public interface IWeapon : IHasEventTire
{
    void StartUsing();
    void StopUsing();
    void Recharge();
}

public class WeaponUseStateChangedEvent : TireEvent
{
    public WeaponUseState NewState;

    public WeaponUseStateChangedEvent()
    {
        Type = TireEventType.WeaponUseStateChangedEvent;
    }

    public override string ToString()
    {
        return "Type " + Type + " WeaponUseState " + NewState;
    }

}