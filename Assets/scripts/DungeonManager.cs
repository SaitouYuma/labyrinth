using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager Instance;

    [Header("Tilemap & Exit")]
    public Tilemap floorTilemap;
    public Tile exitTile;
    public GameObject exitTriggerPrefab;

    private bool exitSpawned = false;
    private int totalEnemies = 0;
    private int defeatedEnemies = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        //totalEnemies = FindObjectsOfType<EnemyAI>().Length;
    }

    public void EnemyDefeated()
    {
        defeatedEnemies++;
        Debug.Log($"ìGì|ÇµÇΩ: {defeatedEnemies}/{totalEnemies}");

        if (!exitSpawned && defeatedEnemies >= totalEnemies)
        {
            SpawnExit();
        }
    }

    void SpawnExit()
    {
        Vector3Int tilePos = FindRandomFloorTile();

        floorTilemap.SetTile(tilePos, exitTile);

        Vector3 worldPos = floorTilemap.CellToWorld(tilePos) + new Vector3(0.5f, 0.5f, 0);
        Instantiate(exitTriggerPrefab, worldPos, Quaternion.identity);

        exitSpawned = true;
        Debug.Log("èoå˚ê∂ê¨ÅIà íu: " + tilePos);
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
            if (tile != null) return pos;
        }
    }

    public void RegisterEnemy()
    {
        totalEnemies++;
        Debug.Log($"ìGìoò^: {totalEnemies}");
    }
}
