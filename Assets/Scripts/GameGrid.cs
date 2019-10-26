using UnityEngine;

public class GameGrid : MonoBehaviour {
    public int countX = 10, countY = 20;
    public int cellCount;
    [Range (0.1f, 5)]
    public float celSize = 1;
    public bool show;
    void Start () {
        cellCount = countX * countY;
    }
    public Vector3 GetCell (int i) {
        return new Vector3 (i % countX * celSize, i / countX * celSize);
    }
    void OnDrawGizmos () {
        if (!show) return;
        for (int i = 0; i < cellCount; i++) {
            Vector2 vector = GetCell (i);
            Gizmos.DrawCube (new Vector3 (vector.x, vector.y, 0), Vector3.one / 4);
        }
    }
    void Update () {

    }
}