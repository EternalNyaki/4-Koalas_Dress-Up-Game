using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public ClothingItem defaultHat;
    public ClothingItem defaultShirt;
    public ClothingItem defaultPants;
    public ClothingItem defaultShoes;

    public ClothingItem hat { get; private set; }
    public ClothingItem shirt { get; private set; }
    public ClothingItem pants { get; private set; }
    public ClothingItem shoes { get; private set; }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (defaultHat != null && defaultHat.type != ClothingType.Hat)
        {
            Debug.LogError("Hat must be a hat");
            defaultHat = null;
        }
        if (defaultShirt != null && defaultShirt.type != ClothingType.Shirt)
        {
            Debug.LogError("Shirt must be a shirt");
            defaultShirt = null;
        }
        if (defaultPants != null && defaultPants.type != ClothingType.Pants)
        {
            Debug.LogError("Pants must be pants");
            defaultPants = null;
        }
        if (defaultShoes != null && defaultShoes.type != ClothingType.Shoes)
        {
            Debug.LogError("Shoes must be shoes");
            defaultShoes = null;
        }
    }
#endif

    protected override void Initialize()
    {
        base.Initialize();

        hat = defaultHat;
        shirt = defaultShirt;
        pants = defaultPants;
        shoes = defaultShoes;
    }

    public void SetClothing(ClothingItem item)
    {
        switch (item.type)
        {
            case ClothingType.Hat:
                hat = item;
                break;

            case ClothingType.Shirt:
                shirt = item;
                break;

            case ClothingType.Pants:
                pants = item;
                break;

            case ClothingType.Shoes:
                shoes = item;
                break;
        }
    }
}
