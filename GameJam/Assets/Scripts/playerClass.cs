using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerClass : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float physicalDefence;
    public float magicalDefence;
    public float gaugeSpeed;
    public float gaugeSize;
    public float gauge;
    public float gaugeBarSize = 100f;
    public float strength;
    public float intelligence;
    public float luck;
    public int exp;
    public int level;

    public Dictionary<string , float> stats;
    public List<attackClass> playerAttacks = new List<attackClass>();

    void Start()
    {
        stats = new Dictionary<string, float>()
        {
            {"Max Health", this.health},
            {"Strength", this.strength},
            {"Intelligence", this.intelligence },
            {"Physical Defence", this.physicalDefence},
            {"Magical Defence", this.magicalDefence},
            {"Gauge Speed", this.gaugeSpeed},
            {"Gauge Size", this.gaugeSize},
            {"Luck", this.luck}
        };
    }


    public void playerTurn()
    {
        if (gauge < gaugeSize * gaugeBarSize)
        {
            gauge += gaugeSpeed;
        }

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

    public void updateStats()
    {
        this.maxHealth = this.stats["Max Health"];
        this.strength = this.stats["Strength"];
        this.intelligence = this.stats["Intelligence"];
        this.physicalDefence = this.stats["Physical Defence"];
        this.magicalDefence = this.stats["Magical Defence"];
        this.gaugeSpeed = this.stats["Gauge Speed"];
        this.gaugeSize = this.stats["Gauge Size"];
        this.luck = this.stats["Luck"];
    }
}
