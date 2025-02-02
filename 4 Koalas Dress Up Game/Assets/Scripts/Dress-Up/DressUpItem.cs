using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DressUpItem : MonoBehaviour
{
    public ClothingItem data;
    public Image image;
    public Image frame;

    public void ChangeFrameColor(Color color)
    {
        frame.color = color;
    }

#if UNITY_EDITOR
    //OnValidate is called whenever the script is loaded or a value changes in the Inspector (Editor-only)
    private void OnValidate()
    {
        if (data == null || image == null) { return; }

        image.sprite = data.sprite;
    }
#endif
}