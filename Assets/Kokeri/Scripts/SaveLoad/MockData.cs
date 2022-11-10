using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockData : Data
{
    private string m_Name;
    private int m_Score;

    public MockData() { m_Name = "Player"; m_Score = 0; dataType = Datatype.DEFAULT; }

    public string Name
    {
        get { return m_Name; }
        set { m_Name = value; }
    }

    public int Score
    {
        get { return m_Score; }
        set { m_Score = value; }
    }
}
