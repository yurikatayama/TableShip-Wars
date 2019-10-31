using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour {
    public int countX = 10, countY = 20;
    public int cellCount;
    [Range (0.1f, 5)]
    public float celSize = 1;
    public bool show;
    public Sprite tile;
    private List<SpriteRenderer> tiles = new List<SpriteRenderer> ();
    void Start () {
        cellCount = countX * countY;
        for (int i = 0; i < cellCount; i++) {
            GameObject go = new GameObject ("tler" + i);
            SpriteRenderer sp = go.AddComponent<SpriteRenderer> ();
            sp.sprite = tile;
            sp.color = Color.green;
            sp.sortingLayerName = "Grid";
            tiles.Add (sp);
            go.transform.position = GetCell (i);
            go.transform.parent = transform;
            go.transform.localScale = Vector3.one * celSize;
        }
    }
    public SpriteRenderer GetRenderer (int i) {
        return tiles[i];
    }
    public Vector2 GetCell (int i) {
        return new Vector2 (i % countX * celSize, i / countX * celSize);
    }
    void OnDrawGizmos () {
        if (!show) return;
        for (int i = 0; i < cellCount; i++) {
            Vector2 vector = GetCell (i);
            Gizmos.DrawCube (new Vector3 (vector.x, vector.y), Vector3.one / 4);
        }
    }
    void Update () {

    }
}