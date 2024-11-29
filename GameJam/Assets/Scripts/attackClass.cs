using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackClass
{
    public string attackName;
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
    public Sprite buttonSprite;
    public string description;
    public AudioClip SFX;

    public attackClass(string attackName, float unlockStatNumber1, float unlockStatNumber2, 
        float gaugeCost ,float attackDamage1, float attackDamage2, float healing, float physicalDefenceIncrease, 
        float magicalDefenceIncrease, float duration, string damageType1, string damageType2, Sprite sprite, Sprite buttonSprite, string description, AudioClip SFX)
    {
        this.attackName = attackName;
        this.unlockStatNumber1 = unlockStatNumber1;
        this.unlockStatNumber2 = unlockStatNumber2;
        this.gaugeCost = gaugeCost * 40f;
        this.attackDamage1 = attackDamage1;
        this.attackDamage2 = attackDamage2;
        this.healing = healing;
        this.physicalDefenceIncrease = physicalDefenceIncrease;
        this.magicalDefenceIecrease = magicalDefenceIncrease;
        this.damageType1 = damageType1;
        this.damageType2 = damageType2;
        this.duration = duration;
        this.sprite = sprite;
        this.buttonSprite = buttonSprite;
        this.description = description;
        this.SFX = SFX;

    }
}
