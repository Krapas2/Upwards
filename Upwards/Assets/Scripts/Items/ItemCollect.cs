using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    [System.Serializable]
    public struct Item
    {
        public string name;
        public Sprite sprite;
    }

    public Item[] items;

    public LayerMask player;

    private int ItemIndex;

    private Rigidbody2D rb;
    private PlayerInventory inventory;

    private AudioManager audioManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = FindObjectOfType<PlayerInventory>();
        audioManager = FindObjectOfType<AudioManager>();

        rb.velocity = new Vector2(Random.Range(-.25f, .25f), 1).normalized * 5f;
    }

    void Update()
    {
        bool isOnResource = Physics2D.OverlapCircle(transform.position, 0.25f, player);

        transform.up = rb.velocity;

        if (isOnResource){
            inventory.AddItem(ItemIndex, 1);
            string n = ((int)(Random.Range(1, 3))).ToString();
            if (audioManager) // only play sound if audio manager exists. otherwise game crashes
                audioManager.Play("pop" + n);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        rb.velocity = Vector2.zero;
    }

    public void AssignItem(string name){
        int index = FindObjectOfType<PlayerInventory>().ItemIndexFromName(name);
        ItemIndex = index;
        GetComponent<SpriteRenderer>().sprite = items[index].sprite;
    }
}
