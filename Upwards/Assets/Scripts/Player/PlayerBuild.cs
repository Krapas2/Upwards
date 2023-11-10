using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuild : MonoBehaviour
{

    [System.Serializable]
    public struct structure
    {
        public string name;
        public Structure prefab;
        public int cost;
        public float snapRadius;
    }

    // build
    public Transform towerParent; // gameobject that keeps all pieces of the tower as children
    [HideInInspector]
    public bool buildMode = false;
    private int currentIndex = 0;

    //basic
    public int money;

    public structure[] structures;

    [HideInInspector]
    public Structure currentStructure;

    //position for building
    private Vector3 buildPoint;
    private Vector3 buildOffset;

    // components
    private Camera cam;
    private PlayerInventory inventory;
    private PlayerBreakTile playerBreakTile;

    void Start()
    {
        //assign components
        cam = Camera.main;
        inventory = GetComponent<PlayerInventory>();
        playerBreakTile = GetComponent<PlayerBreakTile>();
    }

    void Update()
    {
        buildMode ^= (Input.GetButtonDown("Build")); // !buildmode on button press
        playerBreakTile.collectMode = !buildMode; // only one can be active at a time

        if(Input.GetKeyDown(KeyCode.Alpha1)){ // should add input on editor for this
            changeStructure(1);
        } else if(Input.GetKeyDown(KeyCode.Alpha2)){ // same here
            changeStructure(2);
        }

        if(buildMode){
            if(Input.GetButtonDown("Build"))
                playerBreakTile.collectMode = false; //might be redundant with line 51, should be checked

            if(Input.GetButtonDown("Build") || currentStructure == null){ 
                //called on the first frame of buildmode being true, creates a currentStructure to be built
                currentStructure = Instantiate(structures[currentIndex].prefab, buildPoint, Quaternion.identity);
                currentStructure.transform.parent = towerParent;
            } else if(currentStructure != null){
                Snap(); //snap buildPoint towards mouse or snappoint
                currentStructure.transform.position = buildPoint;
            }
        } else if(currentStructure != null){
            Destroy(currentStructure.gameObject);
        }

    }

    void Snap(){
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //snap to grid 
        buildPoint = new Vector3( //could check out using vectorint here
            Mathf.Round(mousePos.x),
            Mathf.Round(mousePos.y),
            0
        );
        
        //snap to BuildSnap point
        Vector3 snapPoint = new Vector3();
        foreach (GameObject curSnap in GameObject.FindGameObjectsWithTag("BuildSnap")){ // find snappable points
            float curDistance = Vector3.Distance(curSnap.transform.position, mousePos);

            if( 
                curDistance < structures[currentIndex].snapRadius &&
                curDistance < Vector3.Distance(snapPoint, mousePos) &&
                !currentStructure.snapChildren.Contains(curSnap)
                )
            {
                snapPoint = curSnap.transform.position;
            }
        }
        if(snapPoint != new Vector3()){     // if a snappable point has been found, use it
            buildPoint = snapPoint;
        } else{                             // otherwise use mouse position
            Vector2 spriteSize = currentStructure.GetComponent<SpriteRenderer>().sprite.bounds.extents * 2; // used to put center, not edge, of structure on mouse
            buildPoint += new Vector3(.5f * spriteSize.x % 2, -.5f * spriteSize.x % 2, 0);
        }
    }

    // handles changing structure type 
    void changeStructure(int index){ //index currently only works with 1 for tower and 2 for platform. could maybe be improved with an enum
        if(currentStructure != null)
            Destroy(currentStructure.gameObject);
        currentIndex = index-1; 
        currentStructure = Instantiate(structures[currentIndex].prefab, buildPoint, Quaternion.identity);
    }

    public bool canBeAfforded(){
        return inventory.money >= structures[currentIndex].cost;
    }

    public void applyCost(){
        inventory.money -= structures[currentIndex].cost;
    }
}
