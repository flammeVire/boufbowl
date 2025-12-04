using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/StatCharacter", order = 1)]
public class StatCharacter : ScriptableObject
{
    //health of players
    public float pv = 0f;

    // ???
    public float strength = 0f;

    // Player Movement
    public int pointMouvementMax = 0;

    // Range of the throws
    public float rangeThrow = 0f;

    // Is the player in a stunned state
    public bool isStun = false;

    // Number of action point the player have left
    public int actionPoint = 1;

    // Number of movement point left
    public int movementPoint = 1;

    public CharacterClass characterClass;
    [System.Serializable]
    public enum CharacterClass {
        Captain,
        Striker1,
        Striker2,
        Defender}
    public CharacterClassArms characterClassArms;
    [System.Serializable]
    public enum CharacterClassArms {
        Glove,
        Shield}
}
