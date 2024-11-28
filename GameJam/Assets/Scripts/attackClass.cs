using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackClass
{
    public string attackName;
    public string unlockStatName1;
    public string unlockStatName2;
    public float unlockStatNumber1;
    public float unlockStatNumber2;
    public float gaugeCost;
    public float attackDamage1;
    public float attackDamage2;
    public float healing;
    public float physicalDefenceIncrease;
    public float magicalDefenceIecrease;
    public float duration;
    public string damageType1;
    public string damageType2;
    public Sprite sprite;
    public string description;

    public attackClass(string attackName, string unlockStatName1, string unlockStatName2, float unlockStatNumber1, float unlockStatNumber2, 
        float gaugeCost ,float attackDamage1, float attackDamage2, float healing, float physicalDefenceIncrease, 
        float magicalDefenceIncrease, float duration, string damageType1, string damageType2, Sprite sprite, string description)
    {
        this.attackName = attackName;
        this.unlockStatName1 = unlockStatName1;
        this.unlockStatName2 = unlockStatName2;
        this.unlockStatNumber1 = unlockStatNumber1;
        this.unlockStatNumber2 = unlockStatNumber2;
        this.gaugeCost = gaugeCost * 100f;
        this.attackDamage1 = attackDamage1;
        this.attackDamage2 = attackDamage2;
        this.healing = healing;
        this.physicalDefenceIncrease = physicalDefenceIncrease;
        this.magicalDefenceIecrease = magicalDefenceIncrease;
        this.damageType1 = damageType1;
        this.damageType2 = damageType2;
        this.duration = duration;
        this.sprite = sprite;
        this.description = description;

    }
}
