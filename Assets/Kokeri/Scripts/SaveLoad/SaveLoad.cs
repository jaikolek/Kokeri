using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


}
