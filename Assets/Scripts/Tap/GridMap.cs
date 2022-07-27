using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridMap<T>
{
    private Dictionary<Vector2Int, T> grid;

    public GridMap()
    {
        grid = new Dictionary<Vector2Int, T>();
    }

    public T GetItemAtCoord(Vector2Int coords)
    {
        if(ContainsItemAtCoord(coords))
            return grid[coords];
        else
            return default(T);
    }

    public void SetItemAtCoord(Vector2Int coords, T item)
    {
        if(ContainsItemAtCoord(coords))
            grid[coords] = item;
        else
            grid.Add(coords, item);
    }

    public bool ContainsItemAtCoord(Vector2Int coords)
    {
        if (grid.ContainsKey(coords))
            return true;
        else
            return false;
    }

    public bool RemoveItemAtCoord(Vector2Int coords)
    {
        if (ContainsItemAtCoord(coords))
        {
            grid.Remove(coords);
            return true;
        }
        return false;
    }
}
