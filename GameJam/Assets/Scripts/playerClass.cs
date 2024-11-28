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
    public float gaugeBarSize = 40f;
    public float strength;
    public float intelligence;
    public float luck;
    public int exp;
    public int level;

    public Dictionary<string , float> stats;
    public List<attackClass> playerAttacks = new List<attackClass>();
    public List<List<float>> strengthBuffs = new List<List<float>>();
    public List<List<float>> intelligenceBuffs = new List<List<float>>();
    public List<List<float>> physicalDefenceBuffs = new List<List<float>>();
    public List<List<float>> magicalDefenceBuffs = new List<List<float>>();



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
        if (this.health > this.maxHealth)
        {
            this.health = this.maxHealth;
        }
    }

    public void getHit(float damage, string type)
    {
        if (type == "Physical")
        {
            if (this.physicalDefence != 0)
            {
                this.health -= damage / this.physicalDefence;
            }
            else
            {
                this.health -= damage;
            }
            
        }
        else if (type == "Magical")
        {
            if (this.physicalDefence != 0)
            {
                this.health -= damage / this.magicalDefence;
            }
            else
            {
                this.health -= damage;
            }
        }

    }

    public void boostStat(float statAmount, string statType, float duration)
    {
        float fps = 50f;
        switch (statType)
        {
            case "Strength":
                this.strength += statAmount;
                this.strengthBuffs.Add(new List<float> { statAmount, duration * fps});
                break;
            case "Intelligence":
                this.intelligence += statAmount;
                this.intelligenceBuffs.Add(new List<float> { statAmount, duration * fps });
                break;
            case "PhysicalDefence":
                this.physicalDefence += statAmount;
                this.physicalDefenceBuffs.Add(new List<float> { statAmount, duration * fps });
                break;
            case "MagicalDefence":
                this.magicalDefence += statAmount;
                this.magicalDefenceBuffs.Add(new List<float> { statAmount, duration * fps });
                break;
        }

    }

    public void updateBufs()
    {
        foreach (List<float> buff in strengthBuffs)
        {
            buff[1] -= 1f;
        }
        for (int i = 0; i < strengthBuffs.Count; i++)
        {
            if (strengthBuffs[i][1] <= 0f)
            {
                this.strength -= strengthBuffs[i][0];
                strengthBuffs.RemoveAt(i);
            }
        }
        foreach (List<float> buff in intelligenceBuffs)
        {
            buff[1] -= 1f;
        }
        for (int i = 0; i < intelligenceBuffs.Count; i++)
        {
            if (intelligenceBuffs[i][1] <= 0f)
            {
                this.intelligence -= intelligenceBuffs[i][0];
                intelligenceBuffs.RemoveAt(i);
            }
        }
        foreach (List<float> buff in physicalDefenceBuffs)
        {
            buff[1] -= 1f;
        }
        for (int i = 0; i < physicalDefenceBuffs.Count; i++)
        {
            if (physicalDefenceBuffs[i][1] <= 0f)
            {
                this.physicalDefence -= physicalDefenceBuffs[i][0];
                physicalDefenceBuffs.RemoveAt(i);
            }
        }
        foreach (List<float> buff in magicalDefenceBuffs)
        {
            buff[1] -= 1f;
        }
        for (int i = 0; i < magicalDefenceBuffs.Count; i++)
        {
            if (magicalDefenceBuffs[i][1] <= 0f)
            {
                this.magicalDefence -= magicalDefenceBuffs[i][0];
                magicalDefenceBuffs.RemoveAt(i);
            }
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
