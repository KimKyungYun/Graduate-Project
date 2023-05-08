using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScene : MonoBehaviour
{
    // Start is called before the first frame update
    public BUtton myButton;
    
    void Start () {
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}
    public void TaskOnClick()
    {
        // 문자열 이용해서 씬 전환
        //SceneManager.LoadScene("Stage1");        // OK
    }

}
