using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AfterProcess : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] targets;
    public Material target_material;
    public GameObject door;
    public GameObject door_button;
    public int delay_time=5;
    public bool onProgress=false;

// &&GameManager.Instance.getPresentGamePhaseName()=="AfterProcess"
    public void FirstProcess(SelectEnterEventArgs args){

        if(GameManager.Instance.getPresentGamePhaseName().Equals("MeetLuther"))
        {
            if (door.transform.position.y < 0.5f)
            {

                GameObject target = identifyTarget();


                gameObject.GetComponents<AudioSource>()[0].Play();
                gameObject.GetComponents<AudioSource>()[1].Play();
                target_material.SetColor("_Color", target.GetComponent<Renderer>().material.color);
                target.GetComponent<Renderer>().material = target_material;
                BlockOpen();
                Invoke("FreeOpen", delay_time);
                GameManager.Instance.setGamePhase("SteamProcess");
            }
        }
    }
// &&GameManager.Instance.getPresentGamePhaseName()=="AfterProcess"
    public void SecondProcess(SelectEnterEventArgs args){

        if(GameManager.Instance.getPresentGamePhaseName().Equals("MeetDavid"))
        {
            if (door.transform.position.y < 0.5f)
            {

                GameObject target = identifyTarget();

                gameObject.GetComponents<AudioSource>()[0].Play();
                gameObject.GetComponents<AudioSource>()[1].Play();
                target.GetComponent<Renderer>().material = target_material;
                BlockOpen();
                Invoke("FreeOpen", delay_time);

                //save ColorInfo
                SavedGameInfo.Instance.SelectedColorType = (ColorType)Enum.Parse(typeof(ColorType), target_material.name);

                GameManager.Instance.setGamePhase("SecondProcess");

            }
        }
    }
    public void ThirdProcess(SelectEnterEventArgs args){

        if(GameManager.Instance.getPresentGamePhaseName().Equals("MeetChulsu"))
        {
            if (door.transform.position.y < 0.5f)
            {

                GameObject target = identifyTarget();


                gameObject.GetComponents<AudioSource>()[0].Play();
                gameObject.GetComponents<AudioSource>()[1].Play();
                target.GetComponent<Renderer>().material = target_material;
                BlockOpen();
                Invoke("FreeOpen", delay_time);
                GameManager.Instance.setGamePhase("FinalProcess");
            }
        }

    }
    public void BlockOpen(){
        onProgress=true;
        door_button.GetComponent<MoveGlass>().enabled=false;
    }

    public void FreeOpen(){
        onProgress=false;
        door_button.GetComponent<MoveGlass>().enabled=true;
    }

    private GameObject identifyTarget()
    {
        foreach(GameObject target in targets)
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= 4.0f)
            {
                return target;
            }
        }

        throw new ArgumentException("근처에 Target이 없습니다.");
    }
}
