using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoHelp : MonoBehaviour
{
    // Start is called before the first frame update
    public void ToHelp(){
        SceneManager.LoadScene("Help");
    }    
}
