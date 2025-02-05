using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClothingDisplay : MonoBehaviour
{
    //Image components for each clothing item
    public Image hatImage;
    public Image shirtImage;
    public Image pantsImage;
    public Image shoesImage;

    // Start is called before the first frame update
    void Start()
    {
        //Subscribe to OutfitChanged event
        PlayerManager.Instance.OutfitChanged += ChangeOutfit;

        //Set initial image sprites
        hatImage.sprite = PlayerManager.Instance.hat.sprite;
        shirtImage.sprite = PlayerManager.Instance.shirt.sprite;
        pantsImage.sprite = PlayerManager.Instance.pants.sprite;
        shoesImage.sprite = PlayerManager.Instance.shoes.sprite;
    }

    //Change Outfit event handler, must be subscribed to the PlayerManager's OutfitChanged event
    private void ChangeOutfit(object sender, ClothingType type)
    {
        if (sender.GetType() != typeof(PlayerManager)) { return; }

        switch (type)
        {
            case ClothingType.Hat:
                hatImage.sprite = ((PlayerManager)sender).hat.sprite;
                break;

            case ClothingType.Shirt:
                shirtImage.sprite = ((PlayerManager)sender).shirt.sprite;
                break;

            case ClothingType.Pants:
                pantsImage.sprite = ((PlayerManager)sender).pants.sprite;
                break;

            case ClothingType.Shoes:
                shoesImage.sprite = ((PlayerManager)sender).shoes.sprite;
                break;
        }
    }
}
