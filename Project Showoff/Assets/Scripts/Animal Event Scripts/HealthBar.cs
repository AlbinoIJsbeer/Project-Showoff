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

    private void Awake()
    {
        rescueActive = false;
        timer = 5;
    }

    void Start()
    {
        healthBar.minValue = 0;
        healthBar.maxValue = 100;
        healthBar.wholeNumbers = true;
        healthBar.value = 0;

        UpdatePosition();
        timer = 5f;
        rescueActive = false;
    }

    void Update()
    {
        ActivateRescuing();
        ActivateTimer();     
        UpdateTimerText();
        UpdatePosition();
        DestroyObjectOnTime();
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
        FindObjectOfType<AudioManager>().Play("Tap");
    }

    private void UpdatePosition()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(targetPos.position);
        UIObject.transform.position = Vector3.Lerp(UIObject.transform.position, pos, 1);
    }

    private void ActivateRescuing()
    {
        boatPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        int index = GameObject.FindGameObjectWithTag("Player").GetComponent<BoatUpgrade>().BoatIndex;

        if (BoatController.boatCurrentState == BoatController.BoatState.RESCUE)
        {
            if ((index == 0 && Vector3.Distance(boatPos, transform.position) < 20) ||
                (index == 1 && Vector3.Distance(boatPos, transform.position) < 35) ||
                (index == 2 && Vector3.Distance(boatPos, transform.position) < 45))
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
            GameObject.FindGameObjectWithTag("Player").GetComponent<BoatController>().showBirdFail = true;         
            BoatController.boatCurrentState = BoatController.BoatState.SAIL;
            rescueActive = false;
            Destroy(gameObject);
        }
        else if (healthBar.value >= healthBar.maxValue)
        {
            FindObjectOfType<AudioManager>().Play("BirdFlyingAway");
            GameObject.FindGameObjectWithTag("Player").GetComponent<BoatStats>().Score += 300;
            GameObject.FindGameObjectWithTag("Player").GetComponent<BoatStats>().Money += 200;
            GameObject.FindGameObjectWithTag("Player").GetComponent<BoatStats>().Trash += 20;
            GameObject.FindGameObjectWithTag("Player").GetComponent<BoatController>().showBirdRescue = true;
            FindObjectOfType<SpawnAnimalEvent>().showFact = true;
            BoatController.boatCurrentState = BoatController.BoatState.SAIL;
            rescueActive = false;
            Destroy(gameObject);
        }
    }
}
