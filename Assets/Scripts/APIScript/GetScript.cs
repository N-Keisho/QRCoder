using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System; //JsonUtilityを扱うために必要
using System.IO; //ファイル読み書き用
using System.Text;
using UnityEngine.UI;
using TMPro;


public class GetScript : MonoBehaviour
{
    //// URL変数
    string url_server;
    string url_id;

    // SouseData
    public SourceData sourcedata;    

    // 正常に取得できたか
    public int GetSuccess = 0;

    // コンソール
    TMP_Text Console;

    // savedata
    SaveData savedata;
    
    void Start() {
        GameObject obj = GameObject.Find("Console");
        Console = obj.GetComponent<TMP_Text>();

        obj = GameObject.Find("SaveDataManager");
        savedata = obj.GetComponent<SaveDataScript>().savedata;
    }
 
    public IEnumerator GetCode() {

        url_id = savedata.Id;
        url_server = savedata.ServerUrl;
        string url =  url_server + "/code/" + url_id + "/?format=dim1";

        // urlにGetリクエストを送信
        // Debug.Log(url);
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("Authorization", "146E254A-27C1-1305-9E74-35C2CB0AB0E5+7662562C-7549-4E3D-21C3-1BD10A896611+5B26C9E9-A11A-89ED-62BE-A43E7E11C0DF");
        yield return request.SendWebRequest();
         
        if (request.result != UnityWebRequest.Result.Success) {
            GetSuccess = -1;
            Console.color = Color.red;
            Console.text = request.error;
        }
        else {
            GetSuccess = 1;
            sourcedata = JsonUtility.FromJson<SourceData>(request.downloadHandler.text);
            // sourcedata.Show();
        }
    }
}
