using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : ScriptableObject
{
    // Effet de l'objet
    public EffectType effect;

    // Portée de l'effet en particulier
    public int minRange;

    // Portée de l'effet en particulier
    public int maxRange;
}
