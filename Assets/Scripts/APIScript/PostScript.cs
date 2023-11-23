using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System; //JsonUtilityを扱うために必要
using System.IO; //ファイル読み書き用
using System.Text;
using UnityEngine.UI;
using TMPro;

public class PostScript : MonoBehaviour {
    SourceData sourcedata = new SourceData();
    public string url = "http://localhost:8080/";
    
    string reqJson;
    byte[] postData;
    
    void Start(){
    }


    public void InputText(){

    }

    public void OnClick() {
        //Print();
        StartCoroutine(PostJson());
    }


    IEnumerator PostJson() {

        //sourcedata.Show();

        // リクエストオブジェクトを JSON に変換（byte配列）
        reqJson = JsonUtility.ToJson(sourcedata);
        postData = System.Text.Encoding.UTF8.GetBytes(reqJson);

        // UnityWebRequestでPOST用のやつを実体化
        UnityWebRequest request = new UnityWebRequest(url, "POST");

        // アップロードの準備
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(postData);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // API 通信（完了待ち）
        yield return request.SendWebRequest();
    }
}