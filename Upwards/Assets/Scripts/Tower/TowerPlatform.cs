using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlatform : MonoBehaviour
{

    // This script only handles the visuals of platforms
    // if you're looking for behaviour it's in the platformEffector2D and Structure components


    public Transform leftCheck;
    public Transform rightCheck;

    public Sprite LeftEdge;
    public Sprite RightEdge;
    public Sprite Middle;
    public Sprite Alone;

    public LayerMask platform;

    private SpriteRenderer spriteRen;
    private Structure structure;


    void Start()
    {
        spriteRen = GetComponent<SpriteRenderer>();
        structure = GetComponent<Structure>();
    }

    // Update is called once per frame
    void Update()
    {
        if(structure != null)
            GetComponent<Collider2D>().isTrigger = !structure.built;

        bool hasPlatformOnLeft = Physics2D.OverlapCircle(leftCheck.position, .3f, platform);
        bool hasPlatformOnRight = Physics2D.OverlapCircle(rightCheck.position, .3f, platform);;

        if(hasPlatformOnLeft && hasPlatformOnRight)
            spriteRen.sprite = Middle;
        if(hasPlatformOnLeft && !hasPlatformOnRight)
            spriteRen.sprite = RightEdge;
        if(!hasPlatformOnLeft && hasPlatformOnRight)
            spriteRen.sprite = LeftEdge;
        if(!hasPlatformOnLeft && !hasPlatformOnRight)
            spriteRen.sprite = Alone;

    }
}
