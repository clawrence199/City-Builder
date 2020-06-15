using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    public int Cash { get; set; }
    public int Day { get; set; }
    public float CurrentPopulation { get; set; }
    public float PopulationCeiling { get; set; }
    public int CurrentJobs { get; set; }
    public int JobsCeiling { get; set; }
    public float Food { get; set; } // Note to self: type prop and press tab twice for property shortcut

    public int[] buildingCounts = new int[4];
    private UIController uicontroller;

    // Start used for initialization
    void Awake()
    {
        uicontroller = GetComponent<UIController>();
        Cash = 35;
        Food = 0;
        JobsCeiling = 0;
        Day = 0;
    }

    public void EndTurn()
    {
        // Potentially change order of claculations 
        Day++;
        CalculatePopulation();
        CalculateJobs();
        CalculateFood();
        CalculateCash();
        uicontroller.UpdateCityData();
        uicontroller.UpdateDayCount();
        Debug.Log("Day ended");
        Debug.LogFormat
            ("Jobs: {0}/{1}, Cash: {2}, pop: {3}/{4}, Food: {5}",
            CurrentJobs, JobsCeiling, Cash, CurrentPopulation, PopulationCeiling, Food);
    }

    void CalculateJobs()
    {
        JobsCeiling = buildingCounts[3] * 10;
        CurrentJobs = Mathf.Min((int)CurrentPopulation, JobsCeiling);
    }
    void CalculateCash()
    {
        Cash += CurrentJobs * 2;
    }

    public void DepositCash(int cash)
    {
        Cash += cash;
    }

    void CalculateFood()
    {
        Food += buildingCounts[2] * 4f;
    }

    void CalculatePopulation()
    {
        PopulationCeiling = buildingCounts[1] * 5;
        if (Food >= CurrentPopulation && CurrentPopulation < PopulationCeiling)
        {
            Food -= CurrentPopulation * .25f;
            CurrentPopulation = Mathf.Min(CurrentPopulation += Food * .25f, PopulationCeiling);
        }
        else if (Food < CurrentPopulation)
        {
            CurrentPopulation -= (CurrentPopulation - Food) * .5f;
        }
    }
}
