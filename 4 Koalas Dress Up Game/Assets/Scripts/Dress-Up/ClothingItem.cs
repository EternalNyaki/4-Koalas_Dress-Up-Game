using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum ClothingType
{
    Hat,
    Shirt,
    Pants,
    Shoes
}

[CreateAssetMenu(fileName = "ClothingItem.asset", menuName = "Dress-Up/Clothing Item")]
public class ClothingItem : ScriptableObject
{
    public ClothingType type;
    public Sprite sprite;
    public Sprite icon;
}
