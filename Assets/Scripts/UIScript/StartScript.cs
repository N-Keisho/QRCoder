using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyTransition;

public class StartScript : MonoBehaviour
{
    public TransitionSettings transition;
    public float loadDelay;

    void Start(){
        GameObject obj = GameObject.Find("SaveDataManager");
        SaveDataScript SaveDataScript = obj.GetComponent<SaveDataScript>();
        SaveDataScript.savedata.SelectedStage = 0;
        SaveDataScript.savedata.Id = "";
        SaveDataScript.Save();
    }

    void Update(){
        if (Input.anyKeyDown) OnClick_Start();
    }

    public void OnClick_Start(){
        TransitionManager.Instance().Transition("Scenes/StageSelect", transition, loadDelay);
    }
}
