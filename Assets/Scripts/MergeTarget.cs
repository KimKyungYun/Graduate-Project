using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeTarget : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mergeTarget;
    public GameObject removeTarget1;
    public GameObject removeTarget2;
    public GameObject SwapTarget;

    void Update(){
        if(Vector3.Distance(mergeTarget.transform.position, transform.position)<=0.5f&&GameManager.Instance.getPresentGamePhaseName()=="합치기페이즈")
        {
            removeTarget1.SetActive(false);
            removeTarget2.SetActive(false);
            gameObject.SetActive(false);
            SwapTarget.SetActive(true);
        }
    }
}
