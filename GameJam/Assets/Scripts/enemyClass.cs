using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemyClass
{
    public Sprite sprite;
    public float health;
    public float maxHealth;
    public float physicalDefence;
    public float magicalDefence;
    public float gaugeSpeed;
    public float gaugeSize;
    public float gauge;
    public float strength;
    public float intelligence;
    public int exp;
    public List<attackClass> attackList = new List<attackClass>();

    public enemyClass(Sprite sprite,float health, float maxHealth, float physicalDefence, float magicalDefence, 
        float gaugeSpeed, float gaugeSize, float gauge, float strength, float intelligence,int exp, List<attackClass> attackList)
    {
        this.sprite = sprite;
        this.health = health;
        this.maxHealth = maxHealth;
        this.physicalDefence = physicalDefence;
        this.magicalDefence = magicalDefence;
        this.gaugeSpeed = gaugeSpeed;
        this.gaugeSize = gaugeSize;
        this.gauge = gauge;
        this.strength = strength;
        this.intelligence = intelligence;
        this.exp = exp;
        this.attackList = attackList;
    }


    public string enemyTurn()
    {
        if (this.gauge < this.gaugeSize * 100f)
        {
            this.gauge += this.gaugeSpeed;
        }

        foreach(attackClass attack in attackList)
        {
            if (attack.gaugeCost < this.gauge)
            {
                return attack.attackName;
            }
        }
        return "";
    }

    public void getHit(float damage, string type)
    {
        if (type == "Physical")
        {
            this.health -= damage - this.physicalDefence;
        }
        else if (type == "Magical")
        {
            this.health -= damage - this.magicalDefence;
        }
        
    }
}


