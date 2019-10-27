using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField]
    Sprite empty;
    [SerializeField]
    private GameGrid grid;
    [SerializeField]
    private int squadSize=4;
    [SerializeField]
    Text actionCounter;
    [SerializeField]
    Button fireButtom;
    [SerializeField]
    Button confirmButtom;
    [SerializeField]
    private GameObject moveButtons;
    [SerializeField]
    private ShipBase selected;
    Camera cam;
    private int turnId;
    private int actions;
    private int selectedTile;
    private int moveCount;
    private HashSet<int> moves=new HashSet<int>();
    public List<ShipBase> squad=new List<ShipBase>();
    public List<ShipBase> enemySquad=new List<ShipBase>();
    public delegate void Del();
    private Del update;
    [SerializeField]
    private bool confirmed;
    void Start () {
        cam=GetComponent<Camera>();
        for(int i=1;i<=squadSize;i++){
            GameObject go=new GameObject("Ship"+i);
            ShipBase ship=go.AddComponent<ShipBase>();
            go.AddComponent<SpriteRenderer>().sprite=empty;
            go.AddComponent<BoxCollider2D>();
            squad.Add(ship);
            ship.SetTile(-i);
            go.transform.position=grid.GetCell(-i);
        }
        update=PlaceShip;
    }
    void Update () {
        update?.Invoke();
    }
    void PlaceShip(){
        if(selected){
            if(Input.GetKeyDown(KeyCode.RightArrow))selected.Move(1,grid,squad);
            if(Input.GetKeyDown(KeyCode.LeftArrow))selected.Move(-1,grid,squad);
            if(Input.GetKeyDown(KeyCode.UpArrow))selected.Move(grid.countX,grid,squad);
            if(Input.GetKeyDown(KeyCode.DownArrow))selected.Move(-grid.countX,grid,squad);
            if(Input.GetKeyDown(KeyCode.Space))selected=null;
        }
        else{
            foreach(ShipBase s in squad){
                if(s.Outside()){
                    selected=s;
                    return;
                }
            }
            update=PlayerTurn;
            actions=5;
            actionCounter.text="Actions:"+actions;
        }
    }
    ShipBase GetInLine(int x){
        ShipBase target=null;
        foreach(ShipBase enemy in enemySquad){
           if(enemy.transform.position.x==x && (!target || target.transform.position.y>enemy.transform.position.y))target=enemy;
        }
        return target;
    }
    public void Fire(){
        actions--;
        actionCounter.text="Actions:"+actions;
        Debug.Log("firing "+selected.name);
    }
    public void ConfirmMove(){
        if(selectedTile!=selected.Tile()){
            actions--;
            actionCounter.text="Actions:"+actions;
            selectedTile=selected.Tile();
            confirmButtom.interactable=actions>0;
            moves.Clear();
            moves.Add(selectedTile);
        }
    }
    public void Move(int m){
         if(selected){
            fireButtom.interactable=actions>0;
            int i=selected.Tile();
            if(m==0)selected.Move(1,grid,squad);
            if(m==1)selected.Move(-1,grid,squad);
            if(m==2)selected.Move(grid.countX,grid,squad);
            if(m==3)selected.Move(-grid.countX,grid,squad);
            if(i!=selected.Tile()){
                if(moves.Contains(selected.Tile()))moves.Remove(i);
                else if(moves.Count<=selected.getSpeed())moves.Add(selected.Tile());
                        else selected.SetTile(i,grid);
            }
        }
    }
    void PlayerTurn(){
        if(selected && Input.GetMouseButton(0)){
            //fireButtom.interactable=actions>0;
            selected.transform.localScale=Vector3.one*(1+Mathf.Abs(Mathf.Sin(Time.time))/2);
            Vector3 v=cam.ScreenToWorldPoint(Input.mousePosition)/grid.celSize;
            v+=Vector3.one/2;
            if(v.x>=0 && v.x<grid.countX && v.y>=0 && v.y<grid.countY){
                int i=(int)v.x+(int)v.y*grid.countX;
                Debug.Log(i);
                if(i!=selected.Tile()){
                    if(moves.Contains(i)){
                        moves.Remove(selected.Tile());
                        selected.SetTile(i,grid);
                    }
                    else if(moves.Count<=selected.getSpeed()){
                            moves.Add(i);
                            selected.SetTile(i,grid);
                        }
                }
            }
            /*int i=selected.Tile();
            if(Input.GetKeyDown(KeyCode.RightArrow))selected.Move(1,grid,squad);
            if(Input.GetKeyDown(KeyCode.LeftArrow))selected.Move(-1,grid,squad);
            if(Input.GetKeyDown(KeyCode.UpArrow))selected.Move(grid.countX,grid,squad);
            if(Input.GetKeyDown(KeyCode.DownArrow))selected.Move(-grid.countX,grid,squad);
            if(i!=selected.Tile()){
                if(moves.Contains(selected.Tile()))moves.Remove(i);
                else if(moves.Count<=selected.getSpeed())moves.Add(selected.Tile());
                        else selected.SetTile(i,grid);
            }*/
        }
        if(Input.GetMouseButtonDown(0)){
            Vector3 v=cam.ScreenToWorldPoint(Input.mousePosition);
            Collider2D col=Physics2D.OverlapPoint(new Vector2(v.x,v.y));
            if(col){
                ShipBase ship=col.GetComponent<ShipBase>();
                if(ship){
                    if(ship.gameObject==col.gameObject){
                        if(selected){
                            selected.transform.localScale=Vector3.one;
                            selected.SetTile(selectedTile,grid);
                        }
                        selected=ship;
                        selectedTile=selected.Tile();
                        moves.Clear();
                        moves.Add(selectedTile);
                    }
                }
            }
            moveButtons.SetActive(selected);
        }
    }
}