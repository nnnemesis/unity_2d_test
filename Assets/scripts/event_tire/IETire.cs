using System;

public enum TireEventType  // Tire event type
{
    ControlEvent,
    ChangedDirectionEvent,
    ChangedMoveStateEvent,
    ChangedVerticalMoveStateEvent,
    ChangedJumpStateEvent,
    WeaponUseStateChangedEvent,
    WeaponCurrentAmmoChangedEvent,
    WeaponCurrentTotalAmmoChangedEvent,
    ChangedCurrentWeapon,
    ChangedHealthEvent,
    ChangedAiTarget,
    ChangedUnitMoveControlType,
    ChangedCanUseLadder,
    SaveWeaponStateEvent,
    LoadWeaponStateEvent,
    ChangedCanPickupAmmoEvent,
    AmmoPickupEvent,
    ChangedShiftWalkEvent,
    ChangedSitDownEvent,
    ChangedCanPickupWeaponEvent,
    WeaponPickupEvent,
    ReplacedCurrentWeaponEvent,
}

public enum TEPath   // Event path
{
    Local,
    Up,
    Down,
    UpDown
}

public interface IEventTire // I Event tire
{
    void SendEvent(TEPath path, TireEventType type, Object param);
    void AddEventListener(TireEventType type, Action<Object> handler);
    void RemoveEventListener(TireEventType type, Action<Object> handler);
}

public interface IETireHierarchy : IEventTire
{
    void AddChild(IEventTire child);
}

