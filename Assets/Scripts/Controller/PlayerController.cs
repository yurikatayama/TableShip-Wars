using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    GameGrid grid;
    public int currentCell;
    void Start () {

    }
    public void Move (int i) {
        switch (i) {
            case 0:
                currentCell++;
                break;
            case 1:
                currentCell--;
                break;
            case 2:
                currentCell += grid.countX;
                break;
            case 3:
                currentCell -= grid.countX;
                break;
        }
        transform.position = grid.GetCell (currentCell);
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown (KeyCode.RightArrow)) Move (0);
        if (Input.GetKeyDown (KeyCode.LeftArrow)) Move (1);
        if (Input.GetKeyDown (KeyCode.UpArrow)) Move (2);
        if (Input.GetKeyDown (KeyCode.DownArrow)) Move (3);
    }

    public Vector2 LerpMovement (Vector2 posOrigin, Vector2 posDestiny) {
        return Vector2.Lerp (posOrigin, posDestiny, 1.0f);
    }
}