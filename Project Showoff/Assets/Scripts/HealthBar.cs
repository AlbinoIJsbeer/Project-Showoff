using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform targetPos;
    [SerializeField] private GameObject UIObject;
    [SerializeField] private TMP_Text timeUI;

    public Slider healthBar;

    private float timer;
    private Vector3 boatPos;
    [HideInInspector] public bool rescueActive;

    void Start()
    {
        healthBar.minValue = 0;
        healthBar.maxValue = 100;
        healthBar.wholeNumbers = true;
        healthBar.value = 0;

        UpdatePosition();
        timer = 10f;
        rescueActive = false;
    }

    void Update()
    {
        ActivateRescuing();
        ActivateTimer();
        DestroyObjectOnTime();
        UpdateTimerText();
        UpdatePosition();
    }

    private void UpdateTimerText()
    {
        timeUI.text = timer.ToString("00");

        if (timer <= 3)
            timeUI.color = new Color(1, 0, 0, 1);
        else
            timeUI.color = new Color(1, 1, 1, 1);
    }

    public void FillBar(int increment)
    {
        healthBar.value += increment;
    }

    private void UpdatePosition()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(targetPos.position);
        UIObject.transform.position = Vector3.Lerp(UIObject.transform.position, pos, 1);
    }

    private void ActivateRescuing()
    {
        boatPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        if (Vector3.Distance(boatPos, transform.position) < 20 && BoatController.boatCurrentState == BoatController.BoatState.RESCUE)
        {
            rescueActive = true;
            UIObject.SetActive(true);
        }
        else
        {
            rescueActive = false;
            UIObject.SetActive(false);
        }
    }

    private void ActivateTimer()
    {
        if (rescueActive)
        {
            timer -= Time.deltaTime;
        }
    }

    private void DestroyObjectOnTime()
    {
        if (timer <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<BoatStats>().Score -= 100;
            rescueActive = false;
            SpawnAnimalEvent.numberOfSpawns--;
            Destroy(gameObject);
        }
        else if (healthBar.value >= healthBar.maxValue)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<BoatStats>().Score += 300;
            //GameObject.FindGameObjectWithTag("RescueNotification").SetActive(true);
            rescueActive = false;
            SpawnAnimalEvent.numberOfSpawns--;
            Destroy(gameObject);
        }
    }
}
