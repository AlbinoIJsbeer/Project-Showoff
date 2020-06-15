using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatFuel : MonoBehaviour
{
    private float maxFuel;
    public float MaxFuel { get { return maxFuel; } set { maxFuel = value; } }
    private float fuel;
    public float Fuel { get { return fuel; } set { fuel = value; } }

    // Fuel gauge needle and lowFuel notification
    [SerializeField] private Transform fuelNdeedle;
    [SerializeField] private GameObject lowFuel;

    void Start()
    {
        SetFuel();
        BoatStats.OnRefuel += Refuel;
    }

    void Update()
    {
        FuelGauge();
        LowFuelNotification();
    }

    // Set fuel
    private void SetFuel()
    {
        maxFuel = 100;
        fuel = maxFuel;
    }

    // Refuel
    private void Refuel()
    {
        fuel += 50;
    }

    // Fuel gague for showing remaining fuel
    private void FuelGauge()
    {       
        fuel = Mathf.Clamp(fuel, 0, maxFuel);
        float fuelPercentage = fuel / maxFuel;
        fuelNdeedle.transform.rotation = Quaternion.Euler(0, 0, (90 - (180 * fuelPercentage)));     
    }

    // Notification for when the fuel is low
    private void LowFuelNotification()
    {      
        if (90 >= fuelNdeedle.eulerAngles.z && fuelNdeedle.eulerAngles.z >= 45)
            lowFuel.SetActive(true);
        else
            lowFuel.SetActive(false);
    }
}
