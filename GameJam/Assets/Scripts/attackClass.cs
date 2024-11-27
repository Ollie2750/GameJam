using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackClass
{
    public string attackName;
    public string unlockStatName;
    public float unlockStatNumber;
    public float gaugeCost;
    public float attackDamage;
    public float healing;
    public float physicalDefenceIncrease;
    public float magicalDefenceIecrease;
    public string damageType;
    public Sprite sprite;
    public string description;

    public attackClass(string attackName, string unlockStatName, float unlockStatNumber, 
        float gaugeCost ,float attackDamage, float healing, float physicalDefenceIncrease, 
        float magicalDefenceIncrease, string damageType, Sprite sprite, string description)
    {
        this.attackName = attackName;
        this.unlockStatName = unlockStatName;
        this.unlockStatNumber = unlockStatNumber;
        this.gaugeCost = gaugeCost * 100f;
        this.attackDamage = attackDamage;
        this.healing = healing;
        this.physicalDefenceIncrease = physicalDefenceIncrease;
        this.magicalDefenceIecrease = magicalDefenceIncrease;
        this.damageType = damageType;
        this.sprite = sprite;
        this.description = description;

    }
}
