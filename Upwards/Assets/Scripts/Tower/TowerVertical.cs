using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Structure))]
public class TowerVertical : MonoBehaviour
{
    // Even right after finishing it i can barely comprehend the horrors i've created within this god forsaken script.

    public float doorRadius;

    public Sprite towerEntrance;
    public Sprite towerBody;

    public Transform aboveCheck;
    public Transform belowCheck;
    public Transform[] platformChecks;

    public LayerMask ground;
    public LayerMask tower;

    public GameObject interactPrompt;
    public GameObject movePrompt;

    [HideInInspector]
    public bool playerIsInside = false;
    [HideInInspector]
    public bool playerWasSent = false;
    [HideInInspector]
    public bool playerWasMoving = false;

    private SpriteRenderer spriteRen;
    private GameObject platform;
    private GameObject player;
    private DoorManager doorManager;
    private Structure structure;

    private bool insideTracker;
    void Start()
    {
        spriteRen = GetComponent<SpriteRenderer>();
        platform = GetComponentInChildren<PlatformEffector2D>().gameObject;
        structure = GetComponent<Structure>();

        player = FindObjectOfType<PlayerMovement>().gameObject;
        doorManager = FindObjectOfType<DoorManager>();
    }

    void LateUpdate()
    {
        interactPrompt.SetActive(isDoor());
        if(isDoor()){
            gameObject.tag = "Door";

            platform.GetComponent<Collider2D>().isTrigger = !structure.built;

            bool playerIsClose = Vector3.Distance(transform.position,player.transform.position) <= doorRadius && structure.built;
            interactPrompt.SetActive(playerIsClose);
            if(playerIsClose){
                playerIsInside ^= Input.GetButtonDown("Interact");
                player.GetComponent<SpriteRenderer>().enabled = !playerIsInside;
            }

            movePrompt.SetActive(playerIsInside);
            if(playerIsInside){
                player.transform.position = transform.position;
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                int move = (int)(Input.GetAxisRaw("Horizontal"));
                if(move != 0 && !playerWasSent && !playerWasMoving){
                    playerIsInside = false;
                    int destinationIndex = Modulo((System.Array.IndexOf(doorManager.doors, this) + move), (doorManager.doors.Length));
                    doorManager.doors[destinationIndex].playerIsInside = true;
                    doorManager.doors[destinationIndex].playerWasSent = true;
                    doorManager.doors[destinationIndex].playerWasMoving = true;
                } else if(move == 0 && playerWasMoving){
                    playerWasMoving = false;
                }
                playerWasSent = false;
            }
            spriteRen.sprite = towerEntrance;
        }else{
            gameObject.tag = "Untagged";
            spriteRen.sprite = towerBody;
        }
    }

    public bool isDoor(){
        bool isOnGround = Physics2D.OverlapCircle(belowCheck.position, 0f, ground);
        bool isHighest = !Physics2D.OverlapCircle(aboveCheck.position, 0f, tower);
        
        bool hasPlatforms = false;
        foreach(Transform check in platformChecks){
            hasPlatforms |= Physics2D.OverlapCircle(check.position, .5f, tower);
        }
        return isOnGround || isHighest || hasPlatforms;
    }

    int Modulo (int x, int m)
	{
		int r = x % m;
		return r < 0 ? r + m : r;
	}
}
