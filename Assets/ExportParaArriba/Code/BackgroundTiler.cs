using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DefaultExecutionOrder(50)]
public class BackgroundTiler : MonoBehaviour
{
    public static BackgroundTiler Instance { get; private set; }

    [SerializeField] private List<TileData> sprites;
    [SerializeField] private GameObject backgroundTile;
    [SerializeField] private float tilesNeeded;
    [SerializeField] private float height;
    [SerializeField] private float offsetActivation;
    [SerializeField] private Transform container;
    private float left;
    private Queue<BackgroundTile> unloaded;
    private List<BackgroundTile> loaded;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        unloaded = new();
        loaded = new();
        left = VisibilityChecker.Instance.left;
        SetNeededTiles();
    }

    private void Update()
    {
        Check();
    }

    private void Check()
    {
        if(VisibilityChecker.Instance.left + VisibilityChecker.Instance.width >= left - offsetActivation)
        {
            Load();
        }
    }

    private void SetNeededTiles()
    {
        for(int i = 0; i < tilesNeeded; i++)
        {
            InstantiateTile();
        }
    }

    private void InstantiateTile()
    {
        var go = Instantiate(backgroundTile, container);
        var tile = go.GetComponent<BackgroundTile>();

        Load(tile);
    }

    public void Unload(BackgroundTile tile)
    {
        if (!loaded.Contains(tile)) return;

        loaded.Remove(tile);
        unloaded.Enqueue(tile);
        tile.gameObject.SetActive(false);
    }

    private void Load(BackgroundTile tile = null)
    {
        if(tile == null && unloaded.Count > 0)
        {
            tile = unloaded.Dequeue();
        }

        if(tile == null) return;

        tile.gameObject.SetActive(true);
        left += tile.Paint(left, height, GetRandomSprite());

        loaded.Add(tile);
    }

    public Sprite GetRandomSprite()
    {
        int total = sprites.Sum(sprite => sprite.weight);
        int random = UnityEngine.Random.Range(0, total);
        int carry = 0;
        foreach(var sprite in sprites)
        {
            carry += sprite.weight;
            if(random < carry)
            {
                return sprite.sprite;
            }
        }
        return null;
    }
}

[Serializable]
public struct TileData
{
    public int weight;
    public Sprite sprite;
}
