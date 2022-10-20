using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Spawner spawn;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        spawn.StartSpawning();
    }
}
