﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Naves", menuName = "TableShip-Wars/Naves", order = 0)]
public class Naves : ScriptableObject {

    public int life, speed, damage;
    public Vector2 position;

}