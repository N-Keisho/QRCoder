using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EasyTransition;

public class ReturnSelect : MonoBehaviour
{
    public TransitionSettings transition;
    public float loadDelay;

    public void OnClick_ReturnSelect(){
        TransitionManager.Instance().Transition("Scenes/StageSelect", transition, loadDelay);
    }
}
