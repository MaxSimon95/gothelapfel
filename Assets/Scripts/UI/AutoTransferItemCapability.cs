using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTransferItemCapability : MonoBehaviour
{
    public GameObject itemAutoTransferTargetParent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrepareAutoTransferTarget()
    {
        // set auto transfer target parent, so the auto transfer from the inventory knows where to put items when this canvas has been opened
        if(itemAutoTransferTargetParent != null)
        {
           // Debug.Log("AUTOPARENT FATHER SET");
            InventoryItemHandler.SetAutoTransferTargetParent(itemAutoTransferTargetParent);
        }
    }
}
