using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SourceScript : MonoBehaviour
{
    PlayerScript playerscript;
    GetScript getscript;

    TMP_Text Console;
    // data
    int[] datas;
    int datas_len;
   
    // 動作間隔
    float StopTime;

    // ループ用変数0
    bool isLoop_0 = false;
    bool LoopBreak_0 = false;
    bool LoopEnd_0 = false;
    // ループ用変数1
    bool isLoop_1 = false;
    bool LoopBreak_1 = false;
    bool LoopEnd_1 = false;
    // ループ用変数2
    bool isLoop_2 = false;
    bool LoopBreak_2 = false;
    bool LoopEnd_2 = false;

    // エラー管理
    bool ErrorFlag = false;

    // Start is called before the first frame update
    void Start(){
        GameObject obj = GameObject.Find("Astronaut");
        playerscript = obj.GetComponent<PlayerScript>();
        getscript = obj.GetComponent<GetScript>();
        obj = GameObject.Find("Console");
        Console = obj.GetComponent<TMP_Text>();
        Console.text = "";
;    }

    //////////////////////////////////

    public void OnClick_GetAndExe(){
        playerscript.StopFlag = false;
        StartCoroutine(GetAndExe());
    }

    public IEnumerator GetAndExe(){
        Console.text = "Loading...";
        getscript.GetSuccess = 0;
        StartCoroutine(getscript.GetCode());
        yield return new WaitUntil(() => getscript.GetSuccess != 0);
        if (getscript.GetSuccess == -1){
            // Console.text = "Cannot connect to destination host";
            yield break;
        }
        else {
            Console.text = "";
            // ループ用変数0
            isLoop_0 = false;
            LoopBreak_0 = false;
            LoopEnd_0 = false;
            // ループ用変数1
            isLoop_1 = false;
            LoopBreak_1 = false;
            LoopEnd_1 = false;
            // ループ用変数2
            isLoop_2 = false;
            LoopBreak_2 = false;
            LoopEnd_2 = false;
            // エラー管理
            ErrorFlag = false;
            // データの取得とその他設定
            datas = getscript.sourcedata.source;
            datas_len = getscript.sourcedata.source.Length;
            StopTime = playerscript.Interval;
            StartCoroutine(Exe(0));
        }
    }

    // 実行部
    public IEnumerator Exe(int index){  
        if (playerscript.GoalFlag == true || playerscript.StopFlag == true){ 
            // ゴールしていたら終了
            LoopBreak_0 = true;
            LoopBreak_1 = true;
            LoopBreak_2 = true;
            if (playerscript.GoalFlag == true)
                ConsoleOut("Goal!", false);
            else if (playerscript.StopFlag == true)
                ConsoleOut("Stop", false);
            yield break;
        }  
        else{
            // データがNullのときの処理
            if (datas_len == 0){
                playerscript.StopFlag = true;
                ConsoleOut("Source Code is Null", true);
                yield break;
            }
            int data = datas[index];            // 使用データの取り出し
            int type = data / 10000;            // 系統
            int code = data / 100 - type * 100; // コードの種類
            int times = data % 100;             // 回数
            // Debug.Log(data + " " + type + " " + code + " " + times);

            // 基本動作
            if (type == 1){
                // nマス進む
                if (code == 1){
                    ConsoleOut(times.ToString() + " times go straight", false);
                    for (int i = 0 ; i < times ; i++){
                        StartCoroutine(playerscript.GoStraight());
                        yield return new WaitForSeconds(StopTime);
                    }
                    StartCoroutine(Exe(index+1));
                }
                else{
                    // 右に向く
                    if(code == 2){
                        ConsoleOut("Turn Right", false);
                        // StartCoroutine(playerscript.TrunRight());
                        playerscript.TrunRight();
                    }
                    // 左に向く
                    else if (code == 3){
                        ConsoleOut("Turn Left", false);
                        // StartCoroutine(playerscript.TrunLeft());
                        playerscript.TrunLeft();
                    }
                    // 後ろに向く
                    else if (code == 4){
                        ConsoleOut("Turn Back", false);
                        // StartCoroutine(playerscript.TrunBack());  
                        playerscript.TrunBack();
                    }

                    yield return new WaitForSeconds(StopTime);
                    StartCoroutine(Exe(index+1));
                }
            }

            // ループ系
            else if (type == 2){
                if (code == 0 || code == 1){
                    StartCoroutine(LoopContent(index, code, times));
                }
                // break;
                else if (code == 98){
                    ConsoleOut("Loop break", false);
                    if (isLoop_2 == true){
                        LoopBreak_2 = true;
                        LoopEnd_2 =true;
                    }
                    else if (isLoop_1 == true){
                        LoopBreak_1 = true;
                        LoopEnd_1 =true;
                    }
                    else if (isLoop_0 == true){
                        LoopBreak_0 = true;
                        LoopEnd_0 =true; 
                    }
                }
                // ループここまで 
                else if (code == 99){
                    ConsoleOut("Loop End", false);
                    if (isLoop_2 == true){
                        LoopEnd_2 = true;
                    }
                    else if (isLoop_1 == true){
                        LoopEnd_1 = true;
                    }
                    else if (isLoop_0 == true){
                        LoopEnd_0 = true; 
                    }
                }
            }

            // 条件系
            else if (type == 3){
                // 前に進めないなら
                if (code >= 1 && code <= 97){
                    StartCoroutine(IfContent(index, code));
                }
                // そうじゃないなら
                else if (code == 98){
                    // 普通に読まれた場合は、直後のendの次まで行く！
                    ConsoleOut("Else", false);
                    int ifEndPoint;
                    for (ifEndPoint = index + 1 ; ifEndPoint < datas_len ; ifEndPoint++){
                        if (datas[ifEndPoint] == 39900)
                            break;
                    }
                    if (ifEndPoint >= datas_len){
                        playerscript.StopFlag = true;
                        ConsoleOut("Cannot find end of if", true);
                        yield break;
                    }
                    else
                        StartCoroutine(Exe(ifEndPoint+1));
                }
                // 条件終了
                else if (code == 99){
                    ConsoleOut("If end", false);
                    StartCoroutine(Exe(index+1));
                }

            }
            else ConsoleOut("Source Code Finished", false);
        }
    }

    ///////////////////////////////////////////////

    /// ループ部
    public IEnumerator LoopContent (int index, int code, int times){
        /// <summary> ループを司る関数
        /// <para> int index, int code, int times
        /// </summary>
        ConsoleOut("Loop Start", false);
        if (isLoop_0 == false || isLoop_1 == false || isLoop_2 == false){
            // 一番外
            if (isLoop_0 == false){
                isLoop_0 = true;
                if (code == 0){
                    ConsoleOut("Eternal Loop Start", false);
                    while(true){
                        if (LoopBreak_0 == true){
                            LoopBreak_0 = false;
                            break;
                        }
                        StartCoroutine(Exe(index+1));
                        yield return new WaitUntil(() => LoopEnd_0 == true);
                        LoopEnd_0 = false;
                    }
                }
                else if (code == 1){
                    ConsoleOut(times.ToString() + " times Loop Start", false);
                    for (int i = 0 ; i < times ; i++){
                        if (LoopBreak_0 == true){
                            LoopBreak_0 = false;
                            break;
                        }
                        StartCoroutine(Exe(index+1));
                        yield return new WaitUntil(() => LoopEnd_0 == true);
                        LoopEnd_0 = false; 
                    }
                }
                isLoop_0 = false;
            }

            // 二番目
            else if (isLoop_1 == false){
                isLoop_1 = true;
                if (code == 0){
                    ConsoleOut("Eternal Loop Start", false);
                    while(true){
                        if (LoopBreak_1 == true){
                            LoopBreak_1 = false;
                            break;
                        }
                        StartCoroutine(Exe(index+1));
                        yield return new WaitUntil(() => LoopEnd_1 == true);
                        LoopEnd_1 = false;    
                    }
                }
                else if (code == 1){
                    ConsoleOut(times.ToString() + " times Loop Start", false);
                    for (int i = 0 ; i < times ; i++){
                        if (LoopBreak_1 == true){
                            LoopBreak_1 = false;
                            break;
                        }
                        StartCoroutine(Exe(index+1));
                        yield return new WaitUntil(() => LoopEnd_1 == true);
                        LoopEnd_1 = false; 
                    }
                }
                isLoop_1 = false;
            }

            // 3番目
            else if (isLoop_2 == false){
                isLoop_2 = true;
                if (code == 0){
                    ConsoleOut("Eternal Loop Start", false);
                    while(true){
                        if (LoopBreak_2 == true){
                            LoopBreak_2 = false;
                            break;
                        }
                        StartCoroutine(Exe(index+1));
                        yield return new WaitUntil(() => LoopEnd_2 == true);
                        LoopEnd_2 = false;    
                    }
                }
                else if (code == 1){
                    ConsoleOut(times.ToString() + " times Loop Start", false);
                    for (int i = 0 ; i < times ; i++){
                        if (LoopBreak_2 == true){
                            LoopBreak_2 = false;
                            break;
                        }
                        StartCoroutine(Exe(index+1));
                        yield return new WaitUntil(() => LoopEnd_2 == true);
                        LoopEnd_2 = false; 
                    }
                }
                isLoop_2 = false;
            }

            // 共通部分
            // 自分のループエンドを探す
            int LoopCount = 1; // 自分自身はカウント済み
            int EndCount = 0;
            int LoopEndPoint; // 探索用変数
            for (LoopEndPoint = index+1 ; LoopEndPoint < datas_len ; LoopEndPoint++)
            {
                if (datas[LoopEndPoint] == 20000 || ( datas[LoopEndPoint] >= 20100 && datas[LoopEndPoint] <= 20199)) 
                    LoopCount++;
                if (datas[LoopEndPoint] == 29900)
                {
                    EndCount++;
                    if (LoopCount == EndCount) break;
                }
            }

            if (LoopEndPoint >= datas_len){
                playerscript.StopFlag = true;
                ConsoleOut("Cannot find end of Loop", true);
                yield break;
            }
            else
                StartCoroutine(Exe(LoopEndPoint+1));
        }
        else
            ConsoleOut("Loop can nest 3 times!", true);
            // Debug.Log("ループは3重までです！");
    }

    ///////////////////////////////////////////
    // 条件部
    public IEnumerator IfContent (int index, int code){
        /// <summary> 条件を司る関数
        /// <para> int index, int code
        /// </summary>
        bool done = false; //条件分岐用変数
        // 前に進めないなら
        if (code == 1){
            ConsoleOut("If do not go straight", false);
            if (playerscript.FrontSide == true){
                done = true;
                // びっくりアニメーション待機
                yield return new WaitForSeconds(0.6f);
                StartCoroutine(Exe(index+1));
            }
        }
        // 右に進めないなら
        else if (code == 2){
            ConsoleOut("If do not go right", false);
            if (playerscript.RightSide == true){
                done = true;
                StartCoroutine(Exe(index+1));
            }
        }
        // 左に進めないなら
        else if (code == 3){
            ConsoleOut("If do not go left", false);
            if (playerscript.LeftSide == true){
                done = true;
                StartCoroutine(Exe(index+1));
            }
        }
        // 後ろに進めないなら
        else if (code == 4){
            ConsoleOut("If do not go back", false);
            if (playerscript.BackSide == true){
                done = true;
                StartCoroutine(Exe(index+1));
            }
        }

        // 前に進めるなら
        else if (code == 5){
            ConsoleOut("If can go straight", false);
            if (playerscript.FrontSide == false){
                done = true;
                StartCoroutine(Exe(index+1));
            }
        }
        // 右に進めるなら
        else if (code == 6){
            ConsoleOut("If can go right", false);
            if (playerscript.RightSide == false){
                done = true;
                StartCoroutine(Exe(index+1));
            }
        }
        // 左に進めるなら
        else if (code == 7){
            ConsoleOut("If can go left", false);
            if (playerscript.LeftSide == false){
                done = true;
                StartCoroutine(Exe(index+1));
            }
        }
        // 後ろに進めるなら
        else if (code == 8){
            ConsoleOut("If can go back", false);
            if (playerscript.BackSide == false){
                done = true;
                StartCoroutine(Exe(index+1));
            }
        }

        // 条件が実行されなかったとき
        if (done == false){
            // 自分のelseかifendを探す
            int ifCount = 1;
            int elseCount = 0;
            int endCount = 0;
            int ifEndPoint;
            for (ifEndPoint = index + 1 ; ifEndPoint< datas_len ; ifEndPoint++){
                if (datas[ifEndPoint] >= 30000 && datas[ifEndPoint] <= 39700){
                    ifCount++;
                }
                else if (datas[ifEndPoint] == 39800){
                    elseCount++;
                    if (ifCount == elseCount) break;
                }
                else if (datas[ifEndPoint] == 39900){
                    endCount++;
                    if (ifCount == endCount) break;
                    if (elseCount != endCount) elseCount = endCount; // endに到達したら、elseをやっていなくてもやったことにしておく。
                }
            }
            if (ifEndPoint >= datas_len){
                playerscript.StopFlag = true;
                ConsoleOut("Cannot find end of if or else", true);
                yield break;
            }
            else
                StartCoroutine(Exe(ifEndPoint+1));
        }
    }

    void ConsoleOut(string value, bool error){
        if (ErrorFlag == false){
            if (error == true){
                Console.color = Color.red;
                ErrorFlag = true;
            }
            else {
                Console.color = Color.white;
            }
            Console.text = value;
        }
    }
}