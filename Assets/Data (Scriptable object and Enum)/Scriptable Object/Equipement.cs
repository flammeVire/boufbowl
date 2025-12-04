using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Equipement", order = 2)]


public class Equipement : ScriptableObject
{
    // nom de l'objet, pas utile mais pratique
    public string name;

    // XXX
    public EquipementType type;

    // Définit si l'equipement se casse aprés avoir perdu ses charges
    [SerializeField] private bool IsBreakable;

    // Nombre de charges restantes avant que l'équipement se casse /  
    public int charges;

    // Listes des effet de l'objet ramassé, un autre scriptable object
    public Effect[] effects;

    // La prefab de l'objet quand il sera sur le terrain
    public GameObject model;

    // Ici l'icone qui montrera au joueur qu'il possede eventuellement à mettre sur l'ui
    public Texture2D icon;
}
