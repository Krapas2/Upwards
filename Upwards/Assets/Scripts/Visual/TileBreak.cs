using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileBreak : MonoBehaviour
{
    [HideInInspector]
    public float breakTime;
    [HideInInspector]
    public string itemName;
    [HideInInspector]
    public ItemDrop item;

    private Tilemap tilemap;


    private AudioManager audioManager;
    
    void Start()
    {
        tilemap = GameObject.FindGameObjectWithTag("CollectableMap").GetComponent<Tilemap>();
        audioManager = FindObjectOfType<AudioManager>();
        StartCoroutine(Die());
    }

    void Update()
    {
        if(Input.GetButtonUp("Fire1")){
            Destroy(gameObject);
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds (breakTime);

        tilemap.SetTile(Vector3Int.FloorToInt(transform.position), null);
        ItemDrop curItem = Instantiate(item, transform.position, Quaternion.identity);
        curItem.AssignItem(itemName);
        if (audioManager) // only play sound if audio manager exists. otherwise game crashes
            audioManager.Play("Pop");

        Destroy(gameObject);
    }
}
