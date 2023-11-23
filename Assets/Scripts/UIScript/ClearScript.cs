using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EasyTransition;

public class ClearScript : MonoBehaviour
{
    public TransitionSettings transition;
    public float loadDelay;

    public GameObject ClearMenu;
    PlayerScript playerscript;
    public int StageNum;

    SaveData savedata;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("Astronaut");
        playerscript = obj.GetComponent<PlayerScript>();
        
        obj = GameObject.Find("SaveDataManager");
        savedata = obj.GetComponent<SaveDataScript>().savedata;
        savedata.SelectedStage = StageNum;
        ClearMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerscript.GoalFlag == true){
            
            ClearMenu.SetActive(true);
        }   
    }

    public void OnClick_NextStage(){
        if (StageNum == 0){
            TransitionManager.Instance().Transition("Scenes/Stage1", transition, loadDelay);
        }
        else if (StageNum == 1){
            TransitionManager.Instance().Transition("Scenes/Stage2", transition, loadDelay);
        }
        else if(StageNum == 2){
            TransitionManager.Instance().Transition("Scenes/Stage3", transition, loadDelay);
        }
        else if(StageNum == 3){
            TransitionManager.Instance().Transition("Scenes/StageEx", transition, loadDelay);
        }
    }
    public void OnClick_Retry(){
        if (StageNum == 0){
            TransitionManager.Instance().Transition("Scenes/Tutorial", transition, loadDelay);
        }
        else if (StageNum == 1){
            TransitionManager.Instance().Transition("Scenes/Stage1", transition, loadDelay);
        }
        else if(StageNum == 2){
            TransitionManager.Instance().Transition("Scenes/Stage2", transition, loadDelay);
        }
        else if(StageNum == 3){
            TransitionManager.Instance().Transition("Scenes/Stage3", transition, loadDelay);
        }
        else if(StageNum == 4){
            TransitionManager.Instance().Transition("Scenes/StageEx", transition, loadDelay);
        }

    }
    public void OnClick_StageSelect(){
        TransitionManager.Instance().Transition("Scenes/StageSelect", transition, loadDelay);
    }

}
