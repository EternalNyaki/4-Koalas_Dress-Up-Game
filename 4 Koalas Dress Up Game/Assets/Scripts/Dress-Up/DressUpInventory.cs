using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DressUpInventory : MonoBehaviour
{
    public ClothingItem[] startingItems;

    private List<ClothingItem> _clothingInventory;

    // Start is called before the first frame update
    void Start()
    {
        _clothingInventory = new List<ClothingItem>();

        foreach (ClothingItem item in startingItems) { _clothingInventory.Add(item); }
    }
}
