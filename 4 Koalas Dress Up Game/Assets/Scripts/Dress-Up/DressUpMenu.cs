using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum Direction
{
    Left,
    Right,
    Up,
    Down
}

public class DressUpMenu : MonoBehaviour
{
    public GameObject dressUpInventory;

    private DressUpItem[,] _clothingItems;
    private Vector2Int _selectedIndex;

    // Start is called before the first frame update
    void Start()
    {
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

        SetSelectedItem(new(0, 0));
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    public void ShiftSelection(Direction direction)
    {
        Vector2Int newIndex;

        switch (direction)
        {
            case Direction.Left:
                newIndex = new(_selectedIndex.x - 1, _selectedIndex.y);
                if (IsValidIndex(newIndex))
                {
                    SetSelectedItem(newIndex);
                }
                break;

            case Direction.Right:
                newIndex = new(_selectedIndex.x + 1, _selectedIndex.y);
                if (IsValidIndex(newIndex))
                {
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

    private void SetSelectedItem(Vector2Int index)
    {
        if (_selectedIndex != null) { _clothingItems[_selectedIndex.x, _selectedIndex.y].ChangeFrameColor(Color.black); }
        _selectedIndex = index;
        _clothingItems[_selectedIndex.x, _selectedIndex.y].ChangeFrameColor(Color.gray);
    }
}
