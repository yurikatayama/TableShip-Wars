using UnityEngine;

public class GameGrid : MonoBehaviour
{
    public int countX=10,countY=20;
    public int cellCount;
    [Range(0.1f,5)]
    public float celSize=1;
    public bool show;
    public GameObject tile;
    void Start()
    {
        cellCount=countX*countY;
        for(int i=0;i<cellCount;i++){
            GameObject go= Instantiate(tile,GetCell(i),Quaternion.identity);
            go.name="tile"+i;
            go.transform.parent=transform;
            go.transform.localScale=Vector3.one*celSize;
        }
        tile.SetActive(false);
    }
    public Vector2 GetCell(int i){
        return new Vector2(i%countX*celSize,i/countX*celSize);
    }
    void OnDrawGizmos()
    {
        if(!show)return;
        for(int i=0;i<cellCount;i++){
            Vector2 vector=GetCell(i);
            Gizmos.DrawCube(new Vector3(vector.x,vector.y),Vector3.one/4);
        }
    }
    void Update()
    {
        
    }
}
