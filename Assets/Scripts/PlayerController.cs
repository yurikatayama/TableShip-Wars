using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public Vector2 LerpMovement (Vector2 posOrigin, Vector2 posDestiny) {
        return Vector2.Lerp (posOrigin, posDestiny, 1.0f);
    }
}