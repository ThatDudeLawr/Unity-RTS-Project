﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour {
    public int Cash { get; set; }
    public int Day { get; set; }
    public int Hour { get; set; }
    public float PopulationCurrent { get; set; }
    public float PopulationCeiling { get; set; }
    public int JobsCurrent { get; set; }
    public int JobsCeiling { get; set; }
    public float Food { get; set; }

    public int[] buildingCounts = new int[4];
    private UIController uiController;


    // Use this for initialization
    void Start () {
        uiController = GetComponent<UIController>();
        Cash = 50;
        Day = 1;
        Hour = 0;
    }

	public void EndTurn()
    {
        Hour++;
        if(Hour==24)
        {
            Day++;
            Hour = 0;
            Debug.Log("Day ended.");
        }

        CalculateCash();
        CalculatePopulation();
        CalculateJobs();
        CalculateFood();
        

        uiController.UpdateCityData();
        uiController.UpdateDayCount();
        Debug.LogFormat
            ("Jobs: {0}/{1}, Cash: {2}, pop: {3}/{4}, Food: {5}",
            JobsCurrent, JobsCeiling, Cash, PopulationCurrent, PopulationCeiling, Food);
    }

    void CalculateJobs()
    {
        JobsCeiling = buildingCounts[3] * 10;
        JobsCurrent = Mathf.Min((int)PopulationCurrent, JobsCeiling);
    }

    void CalculateCash()
    {
        Cash += JobsCurrent * 2;
    }
    public void DepositCash(int cash)
    {
        Cash += cash;
    }
    void CalculateFood()
    {
        Food += buildingCounts[2] * 10f;
    }
    
    void CalculatePopulation()
    {
        PopulationCeiling = buildingCounts[1] * 5;
        if (Food >= PopulationCurrent && PopulationCurrent <= PopulationCeiling)
        {
            Food -= PopulationCurrent* 1.2f;
            PopulationCurrent = Mathf.Min(PopulationCurrent += Food * .25f, PopulationCeiling);
        }
        else if (Food < PopulationCurrent)
        {
            PopulationCurrent -= (PopulationCurrent - Food) * 0.75f;
        }
    }

    #region TimeOfDayUpdate
    private float time;
    public float timeBeforeUpdate=3f;

    private void Update()
    {
        time += Time.deltaTime;

        if (time >= timeBeforeUpdate)
        {
            time = 0.0f;
            EndTurn();
        }
        
    }
    #endregion
}

