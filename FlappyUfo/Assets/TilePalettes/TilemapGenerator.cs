using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TilemapGenerator : MonoBehaviour
{
    public GameObject ParentObject;
    public Tilemap Tilemap;
    public TileBase baseTile;
    public TileBase terrainTile;
    public TileBase upperLayerTile;
    public int mapWidth = 50;
    public int mapHeight = 50;
    public int seed = 42;
    public float terrainThreshold = 0.6f;
    public float terrainOffset = 0f;
    public float terrainScale = 0.1f;
    public float frequency = 0.1f;
    public float amplitude = 5f;

    void Start()
    {
        GenerateTilemap();
    }
    public void SetSeed(int seed)
    {
        seed = seed;
    }
    public void GenerateTilemap()
    {
        Random.InitState(seed);
        float randomAmplitude = amplitude + Random.Range(4f, 7f);
        float randomFreuqency = amplitude + Random.Range(0.5f, 0.9f);
        GenerateBaseLayer();
        GenerateUpperLayer(6, 3, randomAmplitude, randomFreuqency);
        GenerateUpperLayer(6, 0, randomAmplitude, randomFreuqency);
        GenerateUpperLayer(12, 6, randomAmplitude, randomFreuqency);
    }

    void GenerateBaseLayer()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight * 1.5f; y++)
            {
                Vector3Int basePosition = new Vector3Int(x, y, 1);
                Tilemap.SetTile(basePosition, baseTile);
            }
        }
    }

    void GenerateUpperLayer(int zLayer, int offset, float randomAmplitude, float randomFreuqency)
    {
        for (int x = 0; x < mapWidth; x++)
        {
            float yTop = randomAmplitude * Mathf.Sin(randomFreuqency * x);
            int startYTop = Mathf.FloorToInt(yTop + mapHeight + offset);

            for (int y = startYTop; y < mapHeight * 1.5f; y++)
            {
                float noiseValue = Mathf.PerlinNoise(x * terrainScale + terrainOffset, y * terrainScale + terrainOffset);
                if (noiseValue > terrainThreshold)
                {
                    Vector3Int terrainPosition = new Vector3Int(x, y, zLayer);
                    Tilemap.SetTile(terrainPosition, terrainTile);
                }
            }
        }
    }

}
