using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//Enum for 2D input directions
public enum Direction
{
    Left,
    Right,
    Up,
    Down
}

public class DressUpMenu : MonoBehaviour
{
    //Object which holds the outfits and clothing items in the inventory
    public GameObject dressUpInventory;

    [Tooltip("Width of the inventory viewport in menu cells (number of outfits displayed at a time)")]
    public int viewWidth = 2;
    //Reference to ScrollView scrollbar
    public Scrollbar scrollbar;

    public AudioClip selectSound;

    //2D array of items in the inventory
    private DressUpItem[,] _clothingItems;
    //Currently selected index in the inventory
    private Vector2Int _selectedIndex;

    //Array of scroll values for each column of the inventory for auto-scrolling
    private int[] _scrollValuesForOutfits;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //Get components
        _audioSource = GetComponent<AudioSource>();

        //Initialize clothing items array
        _clothingItems = new DressUpItem[dressUpInventory.GetComponentsInChildren<VerticalLayoutGroup>().Length,
                                        dressUpInventory.transform.GetChild(0).GetComponentsInChildren<DressUpItem>().Length];
        for (int i = 0; i < _clothingItems.GetLength(0); i++)
        {
            GameObject outfit = dressUpInventory.GetComponentsInChildren<VerticalLayoutGroup>()[i].gameObject;
            for (int j = 0; j < _clothingItems.GetLength(1); j++)
            {
                _clothingItems[i, j] = outfit.GetComponentsInChildren<DressUpItem>()[j];
            }
        }

        //Set scroll values array
        _scrollValuesForOutfits = new int[_clothingItems.GetLength(0)];
        for (int i = 0; i < _clothingItems.GetLength(0); i++)
        {
            if (i < viewWidth)
            {
                _scrollValuesForOutfits[i] = 0;
            }
            else
            {
                _scrollValuesForOutfits[i] = (i - viewWidth) / (_clothingItems.GetLength(0) - viewWidth);
            }
        }

        //Set initial selection index
        SetSelectedItem(new(0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        //Move the selected index based on input
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ShiftSelection(Direction.Left);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ShiftSelection(Direction.Right);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ShiftSelection(Direction.Up);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ShiftSelection(Direction.Down);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            ConfirmSelection();
            _audioSource.PlayOneShot(selectSound);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerManager.Instance.SetScene("Overworld City");
        }
    }

    //Shift Selection method
    //Move the selected index in the menu in the given direction
    public void ShiftSelection(Direction direction)
    {
        Vector2Int newIndex;

        switch (direction)
        {
            case Direction.Left:
                newIndex = new(_selectedIndex.x - 1, _selectedIndex.y);
                if (IsValidIndex(newIndex))
                {
                    ScrollInventory(Direction.Left);
                    SetSelectedItem(newIndex);
                }
                break;

            case Direction.Right:
                newIndex = new(_selectedIndex.x + 1, _selectedIndex.y);
                if (IsValidIndex(newIndex))
                {
                    ScrollInventory(Direction.Right);
                    SetSelectedItem(newIndex);
                }
                break;

            case Direction.Up:
                newIndex = new(_selectedIndex.x, _selectedIndex.y - 1);
                if (IsValidIndex(newIndex))
                {
                    SetSelectedItem(newIndex);
                }
                break;

            case Direction.Down:
                newIndex = new(_selectedIndex.x, _selectedIndex.y + 1);
                if (IsValidIndex(newIndex))
                {
                    SetSelectedItem(newIndex);
                }
                break;
        }
    }

    //Scroll Inventory method
    //Handles automatically scrolling the menu so the selected item is always visible
    private void ScrollInventory(Direction direction)
    {
        if (direction == Direction.Up || direction == Direction.Down) { return; }

        switch (direction)
        {
            case Direction.Left:
                scrollbar.value = _scrollValuesForOutfits[_selectedIndex.x];
                break;

            case Direction.Right:
                scrollbar.value = _scrollValuesForOutfits[_selectedIndex.x + 1];
                break;
        }
    }

    //Is Valid Index method
    //Return true if the given index is a valid index into the inventory
    private bool IsValidIndex(Vector2Int index)
    {
        if (index.x < 0 || index.y < 0)
        {
            return false;
        }
        else if (index.x >= _clothingItems.GetLength(0))
        {
            return false;
        }
        else if (index.y >= _clothingItems.GetLength(1))
        {
            return false;
        }

        return true;
    }

    //Set Selected Item method
    //Sets the selected index and adjusts the visuals to match the new selected index
    private void SetSelectedItem(Vector2Int index)
    {
        if (_selectedIndex != null) { _clothingItems[_selectedIndex.x, _selectedIndex.y].ChangeFrameColor(Color.black); }
        _selectedIndex = index;
        _clothingItems[_selectedIndex.x, _selectedIndex.y].ChangeFrameColor(Color.gray);
    }

    //Confirm Selection method
    //Sets the player's current clothing based on the currently selected inventory item
    public void ConfirmSelection()
    {
        PlayerManager.Instance.SetClothing(_clothingItems[_selectedIndex.x, _selectedIndex.y].data);
    }
}
