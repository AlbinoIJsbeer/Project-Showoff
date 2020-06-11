using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatFuel : MonoBehaviour
{
    private float maxFuel;
    public float MaxFuel { get { return maxFuel; } set { maxFuel = value; } }
    private float fuel;
    public float Fuel { get { return fuel; } set { fuel = value; } }

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

    private void SetFuel()
    {
        maxFuel = 100;
        fuel = maxFuel;
    }

    private void Refuel()
    {
        fuel += 50;
    }

    private void FuelGauge()
    {
        // Fuel gauge needle shows how much fuel is left
        fuel = Mathf.Clamp(fuel, 0, maxFuel);
        float fuelPercentage = fuel / maxFuel;
        fuelNdeedle.transform.rotation = Quaternion.Euler(0, 0, (90 - (180 * fuelPercentage)));     
    }

    private void LowFuelNotification()
    {
        // Notification for when the fuel is low
        if (90 >= fuelNdeedle.eulerAngles.z && fuelNdeedle.eulerAngles.z >= 45)
            lowFuel.SetActive(true);
        else
            lowFuel.SetActive(false);
    }
}
