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
    [SerializeField] private GameObject pointer;
    [SerializeField] private GameObject emptyFuelTank;

    void Start()
    {
        SetFuel();
        BoatStats.OnRefuel += Refuel;
    }

    void Update()
    {
        EmptyFuelTank();
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
        {
            lowFuel.SetActive(true);
            if (BoatController.boatCurrentState == BoatController.BoatState.SAIL)
                pointer.SetActive(true);
            else if (BoatController.boatCurrentState == BoatController.BoatState.DOCKED)
                pointer.SetActive(false);
        }
        else
            lowFuel.SetActive(false);
    }

    // Show empty fuel tank notification
    private void EmptyFuelTank()
    {
        if (fuelNdeedle.eulerAngles.z == 90 && BoatController.boatCurrentState == BoatController.BoatState.SAIL)
        {
            emptyFuelTank.SetActive(true);
            PauseMenu.GameIsPaused = true;
        }
    }
}
