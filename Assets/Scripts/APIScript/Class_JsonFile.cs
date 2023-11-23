using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; //JsonUtilityを扱うために必要

// 扱うjsonファイルの形式を定義
[Serializable] 
public class SourceData{

    public int id;
    public string image;
    public int[] source;
    public string created_at;

    public void Show(){
        for (int i = 0 ; i < source.Length ; i++)
            Debug.Log(source[i]);
    }
}