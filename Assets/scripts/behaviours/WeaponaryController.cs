using UnityEngine;
using System.Collections.Generic;

public class WeaponaryController : MonoBehaviour {

    private UnitState UnitState;
    private IEventTire EventTire;
    public Transform LeftHandTransform;
    public GameObject InitWeapon;   // test

    void Awake()
    {
        EventTire = GetComponent<IEventTire>();
        UnitState = GetComponent<UnitState>();
    }

    void Start() {
        GameObject testWeapon = (GameObject)Instantiate(InitWeapon, Vector3.zero, Quaternion.identity);
        SingleEventTireProxy proxy = testWeapon.AddComponent<SingleEventTireProxy>();
        proxy.Instance = EventTire;
        testWeapon.transform.SetParent(LeftHandTransform, false);
        UnitState.CurrentWeapon = testWeapon;
    }
	
}
