using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ratioData
{
    public int result;
    public int number;
}

class FindRatio
{
    private static void CariRatio(float screenHeight, float screenWidth)
    {
        int num = 1;
        int ratioHeight = new int();
        int ratioWidth = new int();
        List<ratioData> dataHeight = new List<ratioData>();
        List<ratioData> dataWidth = new List<ratioData>();

        bool loop = true;

        while (loop)
        {
            ratioData value;

            value.result = (int)screenHeight / num;
            value.number = num;
            dataHeight.Add(value);

            value.result = (int)screenWidth / num;
            value.number = num;
            dataWidth.Add(value);

            foreach (ratioData dataOne in dataHeight)
            {
                foreach (ratioData dataTwo in dataWidth)
                {
                    if (dataOne.result == dataTwo.result)
                    {
                        ratioHeight = dataOne.number;
                        ratioWidth = dataTwo.number;
                        loop = false;
                    }
                }
            }

            num++;
        }

        Debug.Log("Width : " + ratioWidth);
        Debug.Log("Height : " + ratioHeight);
    }
}