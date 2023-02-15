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
    public Transform towerParent;
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
    private PlayerCollect playerCollect;

    void Start()
    {
        //assign components
        cam = FindObjectOfType<Camera>();
        inventory = GetComponent<PlayerInventory>();
        playerCollect = GetComponent<PlayerCollect>();
    }

    void Update()
    {
        buildMode ^= (Input.GetButtonDown("Build"));
        playerCollect.collectMode = !buildMode;

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            changeStructure(1);
        } else if(Input.GetKeyDown(KeyCode.Alpha2)){
            changeStructure(2);
        }

        if(buildMode){
            if(Input.GetButtonDown("Build"))
                GetComponent<PlayerCollect>().collectMode = false;

            if(Input.GetButtonDown("Build") || currentStructure == null){ 
                //called at the first frame of buildmode being true
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
        buildPoint = new Vector3( 
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
        if(snapPoint != new Vector3()){
            buildPoint = snapPoint;
        } else{
            Vector2 spriteSize = currentStructure.GetComponent<SpriteRenderer>().sprite.bounds.extents * 2;
            //look mom i did a branchless programing on c#. are you proud?
            buildPoint += new Vector3(.5f * spriteSize.x % 2, -.5f * spriteSize.x % 2, 0);
        }
    }

    void changeStructure(int index){
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
