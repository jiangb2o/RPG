using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperty : Property
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        hpMax = 100;
        hp.value = hpMax;
        speed.value = 5f;
        criticalRate.value = 0;
        criticalDamage.value = 0;
        attack.value = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
