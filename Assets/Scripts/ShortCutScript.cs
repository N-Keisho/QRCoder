using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyTransition;

public class ShortCutScript : MonoBehaviour
{
    public TransitionSettings transition;
    public TransitionSettings totitle;
    public float loadDelay;

    SaveData savedata;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("SaveDataManager");
        savedata = obj.GetComponent<SaveDataScript>().savedata;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))    ToTitle();  
        if (Input.GetKeyDown(KeyCode.S))    ReturnSelect();  
        if (Input.GetKeyDown(KeyCode.Q))    EndGame();  
        if (Input.GetKeyDown(KeyCode.A))    Retry();   
    }

    // タイトルへ戻る
    void ToTitle(){
        TransitionManager.Instance().Transition("Scenes/Title", totitle, loadDelay);
    }

    // セレクトに戻る
    void ReturnSelect(){
        TransitionManager.Instance().Transition("Scenes/StageSelect", transition, loadDelay);
    }

    // ゲームの終了
    void EndGame()
    {     
        #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
        #else
            Application.Quit();//ゲームプレイ終了
        #endif
    }

    // 再読み込み
    void Retry(){
        if (savedata.SelectedStage == 0){
            TransitionManager.Instance().Transition("Scenes/Tutorial", transition, loadDelay);
        }
        else if (savedata.SelectedStage == 1){
            TransitionManager.Instance().Transition("Scenes/Stage1", transition, loadDelay);
        }
        else if(savedata.SelectedStage == 2){
            TransitionManager.Instance().Transition("Scenes/Stage2", transition, loadDelay);
        }
        else if(savedata.SelectedStage == 3){
            TransitionManager.Instance().Transition("Scenes/Stage3", transition, loadDelay);
        }
        else if(savedata.SelectedStage == 4){
            TransitionManager.Instance().Transition("Scenes/StageEx", transition, loadDelay);
        }

    }
}
