using UnityEngine;

public class Property : MonoBehaviour
{
    public int hpMax = 100, speedMin = 3;
    public PropertyPairs hp, speed, criticalRate, criticalDamage, attack;

    protected virtual void Start()
    {
        hp.propertyType = PropertyType.HP;
        attack.propertyType = PropertyType.Attack;
        speed.propertyType  = PropertyType.Speed;
        criticalRate.propertyType = PropertyType.CriticalRate;
        criticalDamage.propertyType = PropertyType.CriticalDamage;
    }
}
