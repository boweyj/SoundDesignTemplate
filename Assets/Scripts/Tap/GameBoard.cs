using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    private GridMap<GameObject> grid_map;

    [SerializeField] Vector2Int board_size;
    [SerializeField] int board_resolution;

    [SerializeField] GameObject tile_prefab, unit_prefab;


    private void Awake()
    {
        grid_map = new GridMap<GameObject>();

        SpawnBoard();
    }

    public void SpawnBoard()
    {
        for (int i = 0; i < board_size.x; i++)
        {
            for (int j = 0; j < board_size.y; j++)
            {
                Vector2Int coords = new Vector2Int(i, j);
                Vector2Int world_coords = coords * board_resolution;

                GameObject tile = Instantiate(tile_prefab, (Vector2)world_coords, Quaternion.identity);
                GameObject piece = Instantiate(unit_prefab, (Vector2)world_coords, Quaternion.identity);
                grid_map.SetItemAtCoord(coords, piece);
            }
        }
    }

    public GameObject GetPieceAtCoords(Vector2Int coord)
    {
        if (grid_map.ContainsItemAtCoord(coord))
            return grid_map.GetItemAtCoord(coord);
        else
            return null;
    }
}
