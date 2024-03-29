using System.Collections.Generic;
using UnityEngine;

public class ShipBase : MonoBehaviour {
    private int currentTile;
    private int health = 3;
    public Naves nave;
    void Start () {

    }
    public void SetTile (int i, GameGrid grid) {
        currentTile = i;
        transform.position = grid.GetCell (currentTile);
    }
    public void Set (Naves n) {
        nave = n;
        health = n.life;
        SpriteRenderer sp = GetComponent<SpriteRenderer> ();
        sp.sprite = n.sprite;
        sp.color = n.color;
    }
    public int getSpeed () {
        return nave.speed;
    }
    public float Life () {
        return (float) health / (float) nave.life;
    }
    public void Damage (int i) {
        health -= i;
        if (health <= 0) Destroy (gameObject);
    }
    public void SetTile (int i) {
        currentTile = i;
    }
    void Update () {

    }
    public int Tile () {
        return currentTile;
    }
    public void Move (int i, GameGrid grid, List<ShipBase> list) {
        if (i == -1 && currentTile % grid.countX == 0) return;
        if (i == 1 && currentTile % grid.countX == grid.countX - 1) return;
        if (i == -grid.countX && currentTile < grid.countX) return;
        if (i == grid.countX && currentTile > grid.cellCount - grid.countX - 1) return;
        int prev = currentTile;
        currentTile += i;
        if (currentTile < 0) currentTile = 0;
        for (int j = 0; j < list.Count; j++) {
            if (list[j] != this && list[j].currentTile == currentTile) {
                if (prev < 0) currentTile++;
                else currentTile = prev;
                j = 0;
            }
        }
        transform.position = grid.GetCell (currentTile);
    }
    public bool Outside () {
        return currentTile < 0;
    }
}