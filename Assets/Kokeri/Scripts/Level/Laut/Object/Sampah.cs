using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sampah : Objek
{
    public override void UbahSpeed()
    {
        kecepatan = GameManager.tempSpeed;
    }
}
