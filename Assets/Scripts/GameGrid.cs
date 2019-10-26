using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour {

    Vector2 gridSize;
    int cellSize = 10;

    // Start is called before the first frame update
    void Start () {
        gridSize = new Vector2 (cellSize * 10, cellSize * 20);
    }

    // Update is called once per frame
    void Update () {

    }

    void GenerateGrid () {

    }
}