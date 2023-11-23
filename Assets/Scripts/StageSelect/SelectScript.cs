using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using EasyTransition;

public class SelectScript : MonoBehaviour
{
    public TransitionSettings transition;
    public TransitionSettings totitle;
    public float loadDelay;

    int SelectedStage;
    int numOfStage = 4;
    public float Speed = 1;

    public TMP_Text text;

    SaveData savedata;

    // Start is called before the first frame update
    void Start()
    {   GameObject obj = GameObject.Find("SaveDataManager");
        SelectedStage = obj.GetComponent<SaveDataScript>().savedata.SelectedStage;
        if (SelectedStage == 0 || SelectedStage == 4){
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (SelectedStage == 1){
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (SelectedStage == 2){
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (SelectedStage == 3){
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SelectedStage == 0 || SelectedStage == 4)
            text.text = "Tutorial";
        else if (SelectedStage == 1)
            text.text = "Stage1";
        else if(SelectedStage == 2)
            text.text ="Stage2";
        else if (SelectedStage == 3)
            text.text ="Stage3";

        if (Input.GetKeyDown(KeyCode.RightArrow)) OnClick_TurnRight();
        if (Input.GetKeyDown(KeyCode.LeftArrow)) OnClick_TurnLeft();
        if (Input.GetKeyDown(KeyCode.Return)) OnClick_Select();
        if (Input.GetKeyDown(KeyCode.T)) OnClick_ToTitle();
        if (Input.GetKeyDown(KeyCode.Q)) EndGame();
    }

// 回転系
//------------------------------------------------------------------
    public void OnClick_TurnRight(){
        StartCoroutine(TurnRight());
        SelectedStage = (SelectedStage + 1) % numOfStage;
    }

    public void OnClick_TurnLeft(){
        StartCoroutine(TurnLeft());
        SelectedStage = (SelectedStage - 1 + 4) % numOfStage;
    }

    IEnumerator TurnRight(){
        for (int i = 0 ; i < 360/numOfStage ; i++){
            transform.Rotate(0, 1f, 0);
            yield return new WaitForSeconds(Speed * 0.001f);
        }
    }

    IEnumerator TurnLeft(){
        for (int i = 0 ; i < 360/numOfStage ; i++){
            transform.Rotate(0, -1f, 0);
            yield return new WaitForSeconds(Speed * 0.001f);
        }
        
    }
//------------------------------------------------------------------

// 選択系
    public void OnClick_Select(){
        // Debug.Log(SelectedStage);
        if (SelectedStage == 0){
            TransitionManager.Instance().Transition("Scenes/Tutorial", transition, loadDelay);
        }
        else if (SelectedStage == 1){
            TransitionManager.Instance().Transition("Scenes/Stage1", transition, loadDelay);
        }
        else if(SelectedStage == 2){
            TransitionManager.Instance().Transition("Scenes/Stage2", transition, loadDelay);
        }
        else if(SelectedStage == 3){
            TransitionManager.Instance().Transition("Scenes/Stage3", transition, loadDelay);
        }
    }

    // タイトルへ戻る
    public void OnClick_ToTitle(){
        TransitionManager.Instance().Transition("Scenes/Title", totitle, loadDelay);
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
}
