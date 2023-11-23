using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    GameObject MainCamera;
    GameObject SubCamera;
    GameObject FPCamera;

    bool Main = true;
    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GameObject.Find("MainCamera");
        SubCamera = GameObject.Find("SubCamera");
        FPCamera = GameObject.Find("FPCamera");

        MainCamera.SetActive(true);
        FPCamera.SetActive(false);
    }

    // Update is called once per frame
    public void OnClick_CameraChage(){
        if (Main){
            MainCamera.SetActive(false);
            FPCamera.SetActive(true);
            Main = false;
        }
        else {
            MainCamera.SetActive(true);
            FPCamera.SetActive(false);
            Main = true;
        }
    }
}
