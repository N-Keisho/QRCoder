using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveDataScript : MonoBehaviour
{
    public SaveData savedata;
    string filepath;
    string fileName = "SaveData.json";

    void Awake()
    {
        // パス名取得
        filepath = Application.persistentDataPath + "/" + fileName;       

        // ファイルがないとき、ファイル作成
        if (!File.Exists(filepath)) {
            savedata.ServerUrl = "https://10-9sai.kogcoder.com";
            savedata.StepSize = 6.0f;
            savedata.Interval = 0.5f;
            savedata.RotateSpeed = 600f;
            savedata.SelectedStage = 0;
            Save();
        }

        savedata.ServerUrl = "https://10-9sai.kogcoder.com";
        // ファイルを読み込んでdataに格納
        savedata = Load(filepath);          
    }

    //-------------------------------------------------------------------
    // jsonとしてデータを保存
    public void Save()
    {
        string json = JsonUtility.ToJson(savedata);                 // jsonとして変換
        StreamWriter wr = new StreamWriter(filepath, false);    // ファイル書き込み指定
        wr.WriteLine(json);                                     // json変換した情報を書き込み
        wr.Close();                                             // ファイル閉じる
    }

    // jsonファイル読み込み
    SaveData Load(string path)
    {
        StreamReader rd = new StreamReader(path);               // ファイル読み込み指定
        string json = rd.ReadToEnd();                           // ファイル内容全て読み込む
        rd.Close();                                             // ファイル閉じる
                                                                
        return JsonUtility.FromJson<SaveData>(json);            // jsonファイルを型に戻して返す
    }

    //-------------------------------------------------------------------
    // ゲーム終了時に保存
    void OnDestroy()
    {
        Save();
    }
}
