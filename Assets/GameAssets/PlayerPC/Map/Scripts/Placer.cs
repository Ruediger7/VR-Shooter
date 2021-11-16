using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{
    //private CustomGrid grid;
    private GridMap grid;
    public GameObject wall;
    public GameObject tree;
    private GameObject toPlace;
    private int size;
    private void Awake()
    {
        size = 2;
        grid = new GridMap(20, 20, size);
        //grid = FindObjectOfType<CustomGrid>();
        Debug.Log("Awake");
        toPlace = wall;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            toPlace = wall;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            toPlace = tree;
        }
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                Debug.Log("Place at" + hitInfo.point);
                PlaceObject(hitInfo.point);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                Debug.Log("Delete at " + hitInfo.point);
                DeleteObject(hitInfo.point);
            }
        }
    }
    private void PlaceObject(Vector3 nearPoint)
    {
        Vector3 placeArray = grid.GetXZVector(nearPoint);
        if (grid.checkPosition(placeArray))
        {
            Debug.Log("Placed not free.");
        }
        else
        {

            Vector3 placeGrid = new Vector3(placeArray.x * size, placeArray.y * size, placeArray.z * size);
            Vector3 finalPosition = grid.ObjectPlacement(placeGrid);

            Debug.Log("Place " + placeGrid);
            Debug.Log("FinalPlace " + finalPosition);

            GameObject c = GameObject.Instantiate(toPlace);

            c.transform.localScale = new Vector3(size, size, size);


            c.transform.position = finalPosition;

            grid.saveGameObject(placeArray, c);

            Debug.Log("Placed a " + toPlace.name);
        }
    }

    private void DeleteObject(Vector3 nearPoint)
    {
        Vector3 placeArray = grid.GetXZVector(nearPoint);
        if (grid.checkPosition(placeArray))
        {
            grid.removeGameObject(placeArray);
        }
        else
        {
            Debug.Log("Placed is not in use.");

        }
    }
}
