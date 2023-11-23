using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    // 移動距離と移動間隔。実行間隔ともリンクしている。
    public float StepSize;
    public float Interval;
    public float RotateSpeed;

    // リスポーン位置
    private Vector3 RespornPosition;
    private Vector3 RespornRotation;
    float ResetHigh;

    // 前後左右地面の判定
    public bool FrontSide = false;
    public bool BackSide = false;
    public bool RightSide = false;
    public bool LeftSide = false;
    public bool Grounded = false;

    // 物体に衝突
    public bool ButtonPushed = false;

    // アニメーション用
    bool f_GoStraight = false;
    bool f_GoRight = false;
    bool f_GoLeft = false;
    bool f_GoBack = false;
    bool f_TurnRight = false;
    bool f_TurnLeft = false;
    bool f_TurnBack = false;
    
    // 終了判定ズ
    public bool GoalFlag = false;
    public bool StopFlag = false;

    private Animator animator;
    private SaveData savedata;

    // 回転用
    private Vector3 NowAngles;
    private Vector3 R_StartAngles;
    private Vector3 L_StartAngles;
    private Vector3 B_StartAngles;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        GameObject obj = GameObject.Find("SaveDataManager");
        savedata = obj.GetComponent<SaveDataScript>().savedata;
        RespornPosition = transform.position;
        RespornRotation = transform.eulerAngles;
        ResetHigh = RespornPosition.y - 10;
    }

    // Update is called once per frame
    void Update()
    {
        StepSize = savedata.StepSize;
        Interval = savedata.Interval;
        RotateSpeed = savedata.RotateSpeed;
        NowAngles = this.transform.eulerAngles;
        
        if (GoalFlag == false && StopFlag == false){
            // 移動
            if (f_GoStraight == true)   transform.position += transform.TransformDirection(Vector3.forward * StepSize * Time.deltaTime);
            if (f_GoRight == true)      transform.position += transform.TransformDirection(Vector3.right * StepSize * Time.deltaTime);
            if (f_GoLeft == true)       transform.position += transform.TransformDirection(Vector3.left * StepSize * Time.deltaTime);
            if (f_GoBack == true)       transform.position += transform.TransformDirection(Vector3.back * StepSize * Time.deltaTime);
        
            // 回転
            if (f_TurnRight == true){
                if (NowAngles.y < R_StartAngles.y)
                    NowAngles.y += 360f;
                if (NowAngles.y >= R_StartAngles.y + 90f){
                    f_TurnRight = false;
                    NowAngles.y = R_StartAngles.y + 90f;
                }
                else
                    NowAngles.y += RotateSpeed * Time.deltaTime;
                this.transform.eulerAngles = NowAngles;  
            }
            if (f_TurnLeft == true){
                if (NowAngles.y > L_StartAngles.y)
                    NowAngles.y -= 360f;
                if (NowAngles.y <= L_StartAngles.y - 90f){
                    f_TurnLeft = false;
                    NowAngles.y = L_StartAngles.y - 90f;
                }
                else
                    NowAngles.y -= RotateSpeed * Time.deltaTime;
                this.transform.eulerAngles = NowAngles;  
            }
            if (f_TurnBack == true){
                if (NowAngles.y < B_StartAngles.y)
                    NowAngles.y += 360f;
                if (NowAngles.y >= B_StartAngles.y + 180f){
                    f_TurnBack = false;
                    NowAngles.y = B_StartAngles.y + 180f;
                }
                else
                    NowAngles.y += 2 * RotateSpeed * Time.deltaTime;
                this.transform.eulerAngles = NowAngles;  
            }
        }
        
        // 落下時リスポーン
        if (transform.position.y < ResetHigh)
            Reset();

        // 地面についているかどうか
        if (Grounded == true) animator.SetBool("Grounded", true);
        else{
            animator.SetBool("Grounded", false);
            animator.SetBool("canStraight", true);
        }

        // 壁にぶつかった！
        if (FrontSide == true){
            animator.SetBool("canStraight", false);
            f_GoStraight = false;
        }
        else {
            animator.SetBool("canStraight", true);
        }
    }


    //-------------------------------------------------------------
    // 移動系統
    public IEnumerator GoStraight(){
        if (FrontSide == false){
            f_GoStraight = true;
            animator.SetBool("isRunning", true);
            yield return new WaitForSeconds(Interval);
            f_GoStraight = false;
            animator.SetBool("isRunning", false);
        }
    }
    public IEnumerator GoRight(){
        if (RightSide == false){
            f_GoRight = true;
            yield return new WaitForSeconds(Interval);
            f_GoRight = false;
        }
    }
    public IEnumerator GoLeft(){
        if (LeftSide == false){
            f_GoLeft = true;
            yield return new WaitForSeconds(Interval);
            f_GoLeft = false;
        }
    }
    public IEnumerator GoBack(){
        if (BackSide == false){
            f_GoBack = true;
            yield return new WaitForSeconds(Interval);
            f_GoBack = false;
        }
    }

    //-------------------------------------------------------------
    // 回転系統
    public void TrunRight(){
        R_StartAngles = NowAngles;
        f_TurnRight = true;
    }

    public void TrunLeft(){
        L_StartAngles = NowAngles;
        f_TurnLeft = true;
    }

    public void TrunBack(){
        B_StartAngles = NowAngles;
        f_TurnBack = true;
    }

    //-------------------------------------------------------------
    // 初期化系
    public void Reset(){
        StopFlag = true;
        if (savedata.SelectedStage == 3){
            ButtonPushed = false;
        };

        f_GoStraight = false;
        f_GoRight = false;
        f_GoLeft = false;
        f_GoBack = false;
        f_TurnRight = false;
        f_TurnLeft = false;
        f_TurnBack = false;

        transform.position = RespornPosition;
        transform.rotation = Quaternion.Euler(0, RespornRotation.y, 0);
    }
}