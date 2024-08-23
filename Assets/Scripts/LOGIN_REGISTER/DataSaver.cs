using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

[Serializable]
public class DataToSave
{
    public int totalCoin;
    public List<bool> playerBought = new List<bool>(12);
}
public class DataSaver : MonoBehaviour
{
    // Start is called before the first frame update
    public static DataSaver Instance;
    public FirebaseUser User;
    public string userId;
    public DatabaseReference dbRef;
    public DataToSave dts;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            dbRef = FirebaseDatabase.DefaultInstance.RootReference;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void SaveDateFn(DataToSave _dts)
    {
        string json = JsonUtility.ToJson(_dts);
        dbRef.Child("users").Child(userId).SetRawJsonValueAsync(json);
    }
    public void LoadDataFn()
    {
        StartCoroutine(LoadDataEnum());
    }
    IEnumerator LoadDataEnum()
    {
        
        var serverData = dbRef.Child("users").Child(userId).GetValueAsync();
        yield return new WaitUntil(predicate: () => serverData.IsCompleted);
        //print("proce")
        DataSnapshot snapshot = serverData.Result;
        string jsonData = snapshot.GetRawJsonValue();
        if (jsonData == null)
        {
            Debug.Log("no data found");
            CreateDataForNewAccount();
        }
        else
        {
            dts = JsonUtility.FromJson<DataToSave>(jsonData);
            Debug.Log(dts.playerBought[0]);
        }
    }
    public void CreateDataForNewAccount()
    {
        DataToSave dataToSave = new DataToSave();
        List<bool> playerHasBought = new List<bool>();
        playerHasBought.Add(true);
        dataToSave.totalCoin = 1000;
        for (int i = 0; i < 11; i++)
        {
            playerHasBought.Add(false);
        }
        dataToSave.playerBought = playerHasBought;
        dts = dataToSave;
        SaveDateFn(dataToSave);       
    }

    public void IncreaseCoin(bool _isWin)
    {
        if (_isWin)
        {
            dts.totalCoin += 200;
        }
        else
        {
            dts.totalCoin += 100;
        }
        SaveDateFn(dts);
    }
}
