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
    private ShipBase selected;
    Camera cam;
    private int turnId;
    private int actions;
    private int selectedTile;
    private int moveCount;
    private HashSet<int> moves=new HashSet<int>();
    private HashSet<SpriteRenderer> path=new HashSet<SpriteRenderer>();
    public List<ShipBase> squad=new List<ShipBase>();
    public List<ShipBase> enemySquad=new List<ShipBase>();
    public delegate void Del();
    private Del update;
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
            foreach(int i in moves)
                grid.GetRenderer(i).color=Color.green;                
            moves.Clear();
            confirmButtom.interactable=false;
            moves.Add(selectedTile);
            grid.GetRenderer(selectedTile).color=Color.yellow;
        }
    }
    void PlayerTurn(){
        if(selected && Input.GetMouseButton(0)){
            //fireButtom.interactable=actions>0;
            selected.transform.localScale=Vector3.one*(1+Mathf.Abs(Mathf.Sin(Time.time))/2);
            Vector3 v=cam.ScreenToWorldPoint(Input.mousePosition)/grid.celSize;
            int difx=(int)(v.x+0.5f)-selected.Tile()%grid.countX;
            int dify=(int)(v.y+0.5f)-selected.Tile()/grid.countX;
            v+=Vector3.one/2;
            if(v.x>=0 && v.x<grid.countX && v.y>=0 && v.y<grid.countY && Mathf.Abs(dify)+Mathf.Abs(difx)<2){
                int i=(int)v.x+(int)v.y*grid.countX;
                if(i!=selected.Tile()){
                    if(moves.Contains(i)){
                        moves.Remove(selected.Tile());
                        grid.GetRenderer(selected.Tile()).color=Color.green;
                        selected.SetTile(i,grid);
                    }
                    else if(moves.Count<=selected.getSpeed()){
                        moves.Add(i);
                        selected.SetTile(i,grid);
                        grid.GetRenderer(i).color=Color.yellow;
                    }
                }
            }
            confirmButtom.interactable=moves.Count>1;
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
                        foreach(int i in moves)
                            grid.GetRenderer(i).color=Color.green; 
                        moves.Clear();
                        grid.GetRenderer(selectedTile).color=Color.yellow;
                        moves.Add(selectedTile);
                    }
                }
            }
        }
    }
}