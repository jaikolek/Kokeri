using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    private int m_Count;
    private List<MockData> m_MockData;

    public GameData()
    {
        m_Count = 0;
        m_MockData = null;
    }


    public int Count
    {
        get { return m_Count; }
        set { m_Count = value; }
    }

    public List<MockData> mockData
    {
        get { SortData(); return m_MockData; }
        set { m_MockData = value; }
    }

    public void AddGameData(MockData _data, int _max)
    {
        if (m_Count <= _max)
        {
            m_MockData.Add(_data);
            SortData();
            m_Count++;
        }
        else
        {
            foreach (MockData data in m_MockData)
            {
                if (data.Score < _data.Score)
                {
                    m_MockData.Add(_data);
                    SortData();
                    m_Count++;
                }
            }
        }
    }

    public Datatype GetDataType()
    {
        Datatype returnValue = Datatype.DEFAULT;
        int laut, hutan, desa;
        laut = hutan = desa = 0;

        foreach(MockData mockData in m_MockData)
        {
            switch (mockData.dataType)
            {
                case Datatype.DESA:
                    desa++;
                    break;

                case Datatype.LAUT:
                    laut++;
                    break;

                case Datatype.HUTAN:
                    hutan++;
                    break;

                default:
                    Debug.Log("Err::Undifined Data Type");
                    break;
            }
        }

        if (desa >= laut && desa >= hutan) returnValue = Datatype.DESA;
        if (laut >= desa && laut >= hutan) returnValue = Datatype.LAUT;
        if (hutan >= laut && hutan >= desa) returnValue = Datatype.HUTAN;

        return returnValue;
    }

    public void SortData()
    {
        m_MockData.Sort();
    }
}
