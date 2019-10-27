using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu (fileName = "Nave", menuName = "TableShip Wars/Nave")]
// public class Naves : ScriptableObject {

//     public int life=2, speed=2, damage=2;

// }


struct Naves {
    public int life, speed, damage;

    public Naves (int life, int speed, int damage) {
        this.life = life;
        this.speed = speed;
        this.damage = damage;
    }
}