using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHandler : MonoBehaviour
{

    [SerializeField]
    private City city;
    [SerializeField]
    private UIController uiController;
    [SerializeField]
    private Building[] buildings;
    [SerializeField]
    private Board board;
    private Building selectedBuilding;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Mouse is being HELD DOWN and shift key
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift))
        {
            InteractWithBoard(0);
        }
        // If mouse is clicked 
        else if (Input.GetMouseButtonDown(0) && selectedBuilding != null)
        {
            //Debug.Log("building selected and mouse clicked");
            InteractWithBoard(0);
        }

        if (Input.GetMouseButtonDown(1))
        {
            InteractWithBoard(1);
        }
    }

    void InteractWithBoard(int action)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 gridPosition = board.CalculateGridPosition(hit.point);
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                if (action == 0 && board.CheckForBuildingAtPosition(gridPosition) == null)
                {
                    if (city.Cash >= selectedBuilding.cost)
                    {
                        city.DepositCash(-selectedBuilding.cost);
                        city.buildingCounts[selectedBuilding.id]++;
                        uiController.UpdateCityData();
                        board.AddBuilding(selectedBuilding, gridPosition);
                    }
                }
                else if (action == 1 && board.CheckForBuildingAtPosition(gridPosition) != null)
                {
                    city.DepositCash(board.CheckForBuildingAtPosition(gridPosition).cost / 2);
                    board.RemoveBuilding(gridPosition);
                    uiController.UpdateCityData();
                }
            }
        }
    }
    // Int is passed in the inspector under the buttons On Click() method (road, house, farm, factory)
    public void EnableBuilder(int building)
    {
        selectedBuilding = buildings[building];
        //Debug.Log("Selected Building: " + selectedBuilding.buildingName);
            
    }
}
