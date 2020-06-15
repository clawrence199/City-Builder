using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private City city;
    [SerializeField]
    private Text dayText;
    [SerializeField]
    private Text cityText;
    void Start()
    {
        city = GetComponent<City>();
        UpdateDayCount();
        UpdateCityData();
    }

    public void UpdateCityData()
    {
        cityText.text = string.Format
            (" Jobs: {0}/{1}\n Cash: {2}\n Population: {3}/{4}\n Food: {5}",
            city.CurrentJobs, city.JobsCeiling, city.Cash, city.CurrentPopulation, city.PopulationCeiling, city.Food);
    }
    public void UpdateDayCount()
    {
        dayText.text = string.Format("Day {0}", city.Day);
    }

}
