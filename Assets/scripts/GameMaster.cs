using UnityEngine;
using UnityEngine.Tilemaps;

public class GameMaster : MonoBehaviour
{
    public Tilemap floorTilemap;          // 床Tilemap
    public Tile exitTile;                 // Tile Paletteの出口Tile
    public GameObject exitTriggerPrefab;  // BoxCollider2D付きTriggerプレハブ
    private bool exitSpawned = false;

    void Update()
    {
        if (!exitSpawned && AllEnemiesDefeated())
        {
            SpawnExit();
        }
    }

    bool AllEnemiesDefeated()
    {
        EnemyAI[] enemies = FindObjectsOfType<EnemyAI>();
        foreach (var e in enemies)
        {
            if (e.IsAlive) return false;
        }
        return true;
    }

    void SpawnExit()
    {
        Vector3Int tilePos = FindRandomFloorTile();

        // Tilemapに出口Tileを置く
        floorTilemap.SetTile(tilePos, exitTile);

        // Tileの位置にTriggerを置く
        Vector3 worldPos = floorTilemap.CellToWorld(tilePos) + new Vector3(0.5f, 0.5f, 0);
        Instantiate(exitTriggerPrefab, worldPos, Quaternion.identity);

        exitSpawned = true;
        Debug.Log("出口生成！位置: " + tilePos);
    }

    Vector3Int FindRandomFloorTile()
    {
        BoundsInt bounds = floorTilemap.cellBounds;

        while (true)
        {
            int x = Random.Range(bounds.xMin, bounds.xMax);
            int y = Random.Range(bounds.yMin, bounds.yMax);
            Vector3Int pos = new Vector3Int(x, y, 0);

            TileBase tile = floorTilemap.GetTile(pos);
            if (tile != null) return pos; // 床タイルならOK
        }
    }
}
