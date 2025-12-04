using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/StatCharacter", order = 1)]
public class StatCharacter : ScriptableObject
{
    //public
    public float pv = 0f;
    public float strength = 0f;
    public int pointMouvementMax = 0;
    public float rangeThrow = 0f;
    public bool isStun = false;
    public bool isLunch = false;
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
