using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject inCombatButton;
    public GameObject attackMenuButton;
    public GameObject attackMenu;
    public GameObject attackScrollBarContent;
    public GameObject attackButton;

    public playerClass player;
    public enemyClass enemy;

    public List<attackClass> playerAttacks = new List<attackClass>();
    public List<enemyClass> enemys = new List<enemyClass>();

    public bool inCombat = false;

    public attackClass attack_Punch;


    void Start()
    {
        attack_Punch = new attackClass("Punch", "strength", 1f, 10f, 0f, 0f, 0f, 0f, "Physical");
        player.playerAttacks.Add(attack_Punch);
    }

    
    void FixedUpdate()
    {
        if (inCombat)
        {
            player.playerTurn();
            enemy.enemyTurn();
        } 
        
        
    }

    public void StartCombat()
    {
        inCombatButton.SetActive(false);
        attackMenuButton.SetActive(true);
        inCombat = true;

        foreach (attackClass attack in player.playerAttacks)
        {
            
            GameObject tempButton = Instantiate(attackButton);
            tempButton.transform.SetParent(attackScrollBarContent.transform);
            tempButton.transform.localScale = Vector3.one;
            GameObject tempButtonChild = tempButton.transform.GetChild(0).gameObject;
            tempButtonChild.GetComponent<TMP_Text>().text = attack.attackName;
        }
    }

    public void ShowAttackMenu()
    {
        attackMenu.SetActive(true);
    }
}
