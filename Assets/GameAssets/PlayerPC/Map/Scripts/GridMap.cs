using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour
{

    private const int sortingOrderDefault = 5000;

    private int width;
    private int height;
    private int cellSize;
    private int[,] gridArray;
    private bool[,] gridFull;

    private GameObject[,] gameObjectArray;
    private TextMesh[,] debugTextArray;

    public GridMap(int w, int h, int size)
    {
        width = w;
        height = h;
        cellSize = size;

        gridArray = new int[width, height];
        gridFull = new bool[width, height];
        gameObjectArray = new GameObject[width, height];
        debugTextArray = new TextMesh[width, height];

        for(int x = 0; x< gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridArray.GetLength(1); z++)
            {
                debugTextArray[x,z] = CreateWorldText(gridArray[x, z].ToString(), null, Getposition(x, z) + new Vector3(cellSize/2, 0, cellSize/2), 20, Color.black, TextAnchor.MiddleCenter);
                // Nur Tempörär für 100s
                Debug.DrawLine(Getposition(x, z), Getposition(x, z + 1), Color.black, 100f);
                Debug.DrawLine(Getposition(x, z), Getposition(x + 1, z), Color.black, 100f);
            }
        }

        Debug.DrawLine(Getposition(0, height), Getposition(width, height), Color.black, 100f);
        Debug.DrawLine(Getposition(width, 0), Getposition(width, height), Color.black, 100f);
    }

    private Vector3 Getposition(int x, int y)
    {
        return new Vector3(x, 0, y) * cellSize;
    }

    public bool checkPosition(Vector3 placeGrid)
    {
        return gridFull[(int)placeGrid.x, (int)placeGrid.z];
    }

    public void savePosition(Vector3 placeGrid)
    {
        gridFull[(int)placeGrid.x, (int)placeGrid.z] = true;
    }
    public void saveGameObject(Vector3 place, GameObject thing)
    {
        gameObjectArray[(int)place.x, (int)place.z] = thing;
        savePosition(place);
    }
    public void freePosition(Vector3 placeGrid)
    {
        gridFull[(int)placeGrid.x, (int)placeGrid.z] = false;
    }
    public void removeGameObject(Vector3 place)
    {
        GameObject thing = gameObjectArray[(int)place.x, (int)place.z];
        Debug.Log("Removed " + thing.name);
        Destroy(thing);
        freePosition(place);
    }

    public void GetXZ(Vector3 position, out int x, out int z)
    {
        x = Mathf.FloorToInt(position.x / cellSize);
        z = Mathf.FloorToInt(position.z / cellSize);
    }

    public Vector3 GetXZVector(Vector3 position)
    {
        position.x = Mathf.FloorToInt(position.x / cellSize);
        position.z = Mathf.FloorToInt(position.z / cellSize);
        return position;
    }

    public Vector3 ObjectPlacement(Vector3 position)
    {
        position.x += cellSize / 2;
        position.z += cellSize / 2;
        return position;
    }

    public void SetValue(int x, int z, int value)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {
            gridArray[x, z] = value;
            debugTextArray[x, z].text = gridArray[x, z].ToString();
        }
    }

    public void SetValue(Vector3 position, int value)
    {
        int x, z;
        GetXZ(position, out x, out z);
        SetValue(x, z, value);
    }

    public int GetValue(int x, int z)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {
            return gridArray[x, z];
        }
        else
        {
            return 0;
        }
    }

    public int GetValue(Vector3 position)
    {
        int x, z;
        GetXZ(position, out x, out z);
        return GetValue(x, z);
    }




    public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = sortingOrderDefault)
    {
        if (color == null) color = Color.white;
        return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
    }

    // Create Text in the World
    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));

        gameObject.transform.rotation = Quaternion.Euler(90, -90f, -90);

        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }
}
