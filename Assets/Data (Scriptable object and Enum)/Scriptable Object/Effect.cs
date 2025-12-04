using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Effects", order = 3)]

public class Effect : ScriptableObject
{
    // Effet de l'objet
    public EffectType effect;

    // Portée de l'effet en particulier
    public int minRange;

    // Portée de l'effet en particulier
    public int maxRange;
}
