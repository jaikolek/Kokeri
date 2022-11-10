using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveLoad : MonoBehaviour
{
    #region singleton
    private static SaveLoad instance;
    public static SaveLoad Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SaveLoad>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(SaveLoad).Name;
                    instance = obj.AddComponent<SaveLoad>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion singleton

    //  public variable
    public int MaxLeaderBoard;

    //  Saved Data
    private int m_Coin;
    private List<GameData> m_GameData = new List<GameData>();

    public int Coin
    { 
        get { return m_Coin; }
        set { m_Coin = value; }
    }

    public List<GameData> gameData
    {
        get { return m_GameData; }
        set { m_GameData = value; }
    }

    //  Contructor
    public SaveLoad() { }

    private void Start()
    {
        InitializedData();
    }

    public void InitializedData()
    {
        m_GameData = null;
        m_Coin = 0;
    }

    public void LoadData()
    {
        instance =  SaveSystem.LoadData();
    }

    public void SaveData()
    {
        SaveSystem.SaveData(instance);
    }

    public void SetGameData(MockData _data)
    {
        if (m_GameData == null)
        {
            GameData dataOne = new GameData();
            dataOne.AddGameData(_data, MaxLeaderBoard);
            m_GameData.Add(dataOne);
        }
        else
        {
            foreach (GameData data in m_GameData)
            {
                if (data.GetDataType() == _data.dataType)
                {
                    data.AddGameData(_data, MaxLeaderBoard);
                    SaveData();
                }
                else
                {
                    GameData dataTwo = new GameData();
                    dataTwo.AddGameData(_data, MaxLeaderBoard);
                    m_GameData.Add(dataTwo);
                }
            }
        }
    }

    public GameData GetGameData(Datatype _data)
    {
        LoadData();
        GameData returnData = null;

        foreach (GameData data in m_GameData)
        {
            if (data.GetDataType() == _data)
            {
                returnData = data;
            }
        }
        return returnData;
    }

    //  SHOP NOT IMPLEMENTED YET
}
