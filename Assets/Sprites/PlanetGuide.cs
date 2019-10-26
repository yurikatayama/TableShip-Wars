using UnityEngine;

public class PlanetGuide : MonoBehaviour
{
    [SerializeField]
    Vector2 dir;
    [SerializeField]
    float speed;
    [SerializeField]
    float size;
    [SerializeField]
    float shrink;
    float time;

    void Update()
    {
        time+=Time.deltaTime;
        transform.Translate(dir*speed*Time.deltaTime);
        transform.localScale=Vector3.one* (size-time*shrink);
    }
}
