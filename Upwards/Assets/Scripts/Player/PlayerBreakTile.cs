using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerBreakTile : MonoBehaviour
{
    [System.Serializable]
    public struct Item
    {
        public string name;
        public TileBase tile;
    }


    public Item[] items;
    public ItemDrop item;

    public float breakTime;
    public TileBreak tileBreak;
    private TileBreak curTileBreak;

    [HideInInspector]
    public bool collectMode = true;

    private PlayerInventory playerInventory;
    private Tilemap tilemap;
    private Camera cam;

    void Start()
    {
        playerInventory = GetComponent<PlayerInventory>();
        tilemap = GameObject.FindGameObjectWithTag("CollectableMap").GetComponent<Tilemap>();
        cam = Camera.main;
    }
    void Update()
    {
        Vector3Int mousePos = Vector3Int.FloorToInt(cam.ScreenToWorldPoint(Input.mousePosition));
        mousePos.z = 0;


        if (collectMode)
        {

            bool tileSelectedIsCollectable = false;
            Item tileSelected = new Item();
            foreach (Item item in items)
            {
                if (item.tile == tilemap.GetTile(Vector3Int.FloorToInt(mousePos)))
                {
                    tileSelected = item;
                    tileSelectedIsCollectable |= true;
                }
            }

            if (curTileBreak != null)
            {
                if (curTileBreak.transform.position != mousePos + tilemap.tileAnchor)
                    Destroy(curTileBreak.gameObject);
            }
            else if (Input.GetButton("Fire1") && tileSelectedIsCollectable)
            {
                curTileBreak = Instantiate(tileBreak, mousePos + tilemap.tileAnchor, Quaternion.identity);
                curTileBreak.breakTime = breakTime;
                curTileBreak.item = item;
                curTileBreak.itemName = tileSelected.name;
            }
        }
    }
}
