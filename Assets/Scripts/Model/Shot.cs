using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {
    public Vector3 target;
    public Del confirm;
    public delegate void Del (Vector3 v);
    void Start () {

    }
    public void Set (Vector3 vector, Del d) {
        target = vector;
        confirm = d;
    }
    void Update () {
        transform.position = Vector3.MoveTowards (transform.position, target, 5 * Time.deltaTime);
        if (Vector3.Distance (transform.position, target) < 0.5) {
            Destroy (gameObject);
            confirm?.Invoke (target + Vector3.back);
        }
    }
}