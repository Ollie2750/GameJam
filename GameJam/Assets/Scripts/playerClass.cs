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
    public float strength;
    public float intelligence;
    public float luck;

    public List<attackClass> playerAttacks = new List<attackClass>();

    void Start()
    {
        
    }


    public void playerTurn()
    {
        if (gauge < gaugeSize * 100f)
        {
            gauge += gaugeSpeed;
        }

    }
}
