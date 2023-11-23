using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EasyTransition;

public class ConfigScript : MonoBehaviour
{
    
    public GameObject configMenu;
    bool configOpened = false;

    // public TMP_InputField serverAdressInput;
    public TMP_InputField idInput;
    public TMP_InputField stepSizeInput;
    public TMP_InputField intervalInput;
    public TMP_InputField rotateSpeedInput;
    public TMP_InputField passInput;
    

    public TransitionSettings transition;
    public float loadDelay;

    SaveDataScript savedatamanager;
    SaveData savedata;

    void Start()
    {
        GameObject obj = GameObject.Find("SaveDataManager");
        savedatamanager = obj.GetComponent<SaveDataScript>();
        configMenu.SetActive(false);

        // serverAdressInput.text = savedatamanager.savedata.ServerUrl;
        idInput.text = savedatamanager.savedata.Id;
        stepSizeInput.text = savedatamanager.savedata.StepSize.ToString();
        intervalInput.text = savedatamanager.savedata.Interval.ToString();
        rotateSpeedInput.text = savedatamanager.savedata.RotateSpeed.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickConfig(){
        if (configOpened){
            ConfigClose();
            configOpened = false;
        }
        else{
            ConfigOpen();
            configOpened = true;
        }

    }

    public void ConfigOpen(){
        configMenu.SetActive(true);
    }

    public void ConfigClose(){
        configMenu.SetActive(false);
    }


    // 変更記録系
    // public void ServerAdressChaged(){
    //     savedatamanager.savedata.ServerUrl = serverAdressInput.text;
    //     savedatamanager.Save();
    // }

    public void IdChaged(){
        savedatamanager.savedata.Id = idInput.text;
        savedatamanager.Save();
    }

    public void StepSizeChaged(){
        savedatamanager.savedata.StepSize = float.Parse(stepSizeInput.text);
        savedatamanager.Save();
    }

    public void IntervalChaged(){
        savedatamanager.savedata.Interval = float.Parse(intervalInput.text);
        savedatamanager.Save();
    }

    public void RotateSpeedChaged(){
        savedatamanager.savedata.RotateSpeed = float.Parse(rotateSpeedInput.text);
        savedatamanager.Save();
    }

    public void PassChaged(){
        if ( passInput.text == "KogCoder"){
            TransitionManager.Instance().Transition("Scenes/StageEx", transition, loadDelay);
        }
    }

}
