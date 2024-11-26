using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyClass : MonoBehaviour
{
    public GameObject enemy;
    public float health;
    public float maxHealth;
    public float physicalDefence;
    public float magicalDefence;
    public float gaugeSpeed;
    public float gaugeSize;
    public float gauge;
    public float strength;
    public float intelligence;

    public enemyClass(float health, float maxHealth, float physicalDefence, float magicalDefence, 
        float gaugeSpeed, float gaugeSize, float gauge, float strength, float intelligence)
    {
        this.health = health;
        this.maxHealth = maxHealth;
        this.physicalDefence = physicalDefence;
        this.magicalDefence = magicalDefence;
        this.gaugeSpeed = gaugeSpeed;
        this.gaugeSize = gaugeSize;
        this.gauge = gauge;
        this.strength = strength;
        this.intelligence = intelligence;
    }


    public void enemyTurn()
    {
        if (gauge < gaugeSize * 100f)
        {
            gauge += gaugeSpeed;
        }
    }
}
