using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSourceType : MonoBehaviour
{
    public ItemSource[] itemSources;

    public int IndexFromDensity(float density){
        return (int)(density*itemSources.Length);
    }
}
