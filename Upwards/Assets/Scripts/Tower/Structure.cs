using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{

    public bool built = false;
    public LayerMask collisions; // objects that don't allow the structure to be built on same position as them
    public Color canBuildColor;
    public Color cannotBuildColor;

    public Transform groundCheck;
    public LayerMask ground;

    public Transform[] snapChecks; // position to check for the structure having support (ground or other)

    private SpriteRenderer spriteRen;
    private SpriteRenderer[] childrenRen; // all components in children with sprite renderer. used for changing color prior to being built

    public List<GameObject> snapChildren; // positions for other structures to nap to while being built

    public PlayerBuild playerBuild;

    private AudioManager audioManager;

    void Start()
    {
        spriteRen = GetComponent<SpriteRenderer>();
        childrenRen = GetComponentsInChildren<SpriteRenderer>();
        playerBuild = FindObjectOfType<PlayerBuild>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if (!built)
        {
            if (CanBeBuilt() && playerBuild.canBeAfforded())
            {
                // visual for structure can be built
                spriteRen.color = canBuildColor;
                foreach (SpriteRenderer childRen in childrenRen)
                {
                    childRen.color = canBuildColor;
                }

                if (Input.GetButton("Fire1"))
                {
                    // visual for structure is built
                    spriteRen.sortingOrder = -3;
                    spriteRen.color = new Color(1, 1, 1);
                    foreach (SpriteRenderer childRen in childrenRen)
                    {
                        spriteRen.sortingOrder = -2;
                        childRen.color = new Color(1, 1, 1);
                    }

                    // background changes
                    gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
                    playerBuild.applyCost();
                    playerBuild.currentStructure = null;
                    built = true;

                    if (audioManager) // only play sound if audio manager exists. otherwise game crashes
                        audioManager.Play("Construir");
                }
            }
            else
            {
                // visual can't be built
                foreach (SpriteRenderer childRen in childrenRen)
                {
                    childRen.color = cannotBuildColor;
                }
                spriteRen.color = cannotBuildColor;
            }
        }
    }

    //structures can be built if the space they occupy isn't already used and they are either on the ground or built on other structures
    private bool CanBeBuilt()
    {

        Collider2D[] cols = Physics2D.OverlapBoxAll(transform.position, spriteRen.sprite.bounds.extents * 2 - new Vector3(.125f, .125f, 0), 0, collisions);
        bool blocked = false;
        foreach (Collider2D col in cols)
        {
            blocked |= col.gameObject != gameObject;
        }

        bool based = Physics2D.OverlapCircle(groundCheck.position, 0f, ground);

        bool snapped = false;
        foreach (Structure structure in FindObjectsOfType<Structure>())
        {
            foreach (Transform snapCheck in snapChecks)
            {
                snapped |= structure.transform.position == snapCheck.position;
            }
        }

        return !blocked && (based || snapped) && !FindObjectOfType<DoorManager>().playerIsInDoor;
    }
}
