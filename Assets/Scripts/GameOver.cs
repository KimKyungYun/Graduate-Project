using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.getPresentGamePhaseName()=="TheEnd" ||  GameManager.Instance.getPresentGamePhaseName().Equals("BadEnding") || GameManager.Instance.getPresentGamePhaseName().Equals("SuddenEnd")){
            Invoke("GoBack",5);
            Invoke("deactiveCanvas", 4.5f);
        }
    }
    public void GoBack(){
        SceneManager.LoadScene("MainMenu");
    }
    
    private void deactiveCanvas()
    {
        Destroy(GameObject.Find("Canvas"));
    }
}
