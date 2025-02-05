using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DressUpItem : MonoBehaviour
{
    //The associated clothing item
    public ClothingItem data;

    //Image reference for the clothing sprite and frame
    public Image image;
    public Image frame;

    //Change Frame Color method
    //Sets the color of the frame to the given color
    public void ChangeFrameColor(Color color)
    {
        frame.color = color;
    }

#if UNITY_EDITOR
    //OnValidate is called whenever the script is loaded or a value changes in the Inspector (Editor-only)
    private void OnValidate()
    {
        if (data == null || image == null) { return; }

        //Set the menu sprite to the sprite for the associated clothing item
        image.sprite = data.sprite;
    }
#endif
}