using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Deck", menuName = "TableShip-Wars/Deck", order = 0)]
public class Deck : ScriptableObject {

    public int[] decklist = new int[10];
}