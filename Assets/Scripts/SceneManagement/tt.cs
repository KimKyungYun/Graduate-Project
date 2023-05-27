using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class tt : MonoBehaviour
{
    // Start is called before the first frame update
    public void GOGO(SelectEnterEventArgs args){
        SceneManager.LoadScene("MyGcode");
    }
}
