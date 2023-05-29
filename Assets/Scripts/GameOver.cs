using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.getPresentGamePhaseName()=="TheEnd"){
            Invoke("GoBack",5);
        }
    }
    public void GoBack(){
        SceneManager.LoadScene("MainMenu");
    }    
}
