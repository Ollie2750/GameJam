using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    private Sprite noSprite;

    public GameObject inCombatButton;
    public GameObject menu;
    public GameObject pickedActionIcon;
    public GameObject pickedActionName;
    public GameObject pickedActionDescription;
    public GameObject pickedActionAction;
    public GameObject pickedActionRequirements;

    public GameObject attackMenuButton;
    public GameObject attackMenu;
    public GameObject attackScrollBarContent;
    public GameObject attackButton;
 
    public Transform playerGaugeBarLocation;
    public GameObject playerGaugeBar;
    public playerClass player;
    public enemyClass enemy;

    public List<attackClass> playerAttacks = new List<attackClass>();
    public List<enemyClass> enemys = new List<enemyClass>();

    public bool inCombat = false;

    public attackClass attack_Uppercut;
    public Sprite uppercutSprite;


    void Start()
    {
        attack_Uppercut = new attackClass("Uppercut", "strength", 1f, 1f, 0f, 0f, 0f, 0f, "Physical");
        player.playerAttacks.Add(attack_Uppercut);
    }

    
    void FixedUpdate()
    {
        if (inCombat)
        {
            player.playerTurn();
            enemy.enemyTurn();
            ChangeGaugeBar();
        } 
        
        
    }

    public void StartCombat()
    {
        inCombatButton.SetActive(false);
        attackMenuButton.SetActive(true);
        menu.SetActive(true);
        inCombat = true;

        foreach (attackClass attack in player.playerAttacks)
        {
            
            GameObject tempButton = Instantiate(attackButton);
            tempButton.transform.SetParent(attackScrollBarContent.transform);
            tempButton.transform.localScale = Vector2.one;


            EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry();
            pointerEnterEntry.eventID = EventTriggerType.PointerEnter;
            pointerEnterEntry.callback.AddListener((data) => ChangeMenu(attack.attackName));
            tempButton.GetComponent<EventTrigger>().triggers.Add(pointerEnterEntry);

            EventTrigger.Entry pointerExitEntry = new EventTrigger.Entry();
            pointerExitEntry.eventID = EventTriggerType.PointerExit;
            pointerExitEntry.callback.AddListener((data) => ChangeMenu("None"));
            tempButton.GetComponent<EventTrigger>().triggers.Add(pointerExitEntry);

            Button button = tempButton.GetComponent<Button>();
            button.onClick.AddListener(() => action(attack.attackName));


            GameObject tempButtonChild = tempButton.transform.GetChild(0).gameObject;
            tempButtonChild.GetComponent<TMP_Text>().text = attack.attackName;
        }
    }

    public void ShowAttackMenu()
    {
        attackMenu.SetActive(true);
    }

    public void ChangeMenu(string name)
    {
        switch (name)
        {
            case "None":
                pickedActionIcon.GetComponent<Image>().sprite = noSprite;
                pickedActionName.GetComponent<TMP_Text>().text = "";
                pickedActionDescription.GetComponent<TMP_Text>().text = "";
                pickedActionAction.GetComponent<TMP_Text>().text = "";
                pickedActionRequirements.GetComponent<TMP_Text>().text = "";
                break;

            case "Uppercut":
                pickedActionIcon.GetComponent<Image>().sprite = uppercutSprite;
                pickedActionName.GetComponent<TMP_Text>().text = name;
                pickedActionDescription.GetComponent<TMP_Text>().text = "Duck and strike your opponent from underneath";
                pickedActionAction.GetComponent<TMP_Text>().text = ("Physical damage: " + player.strength * 1.2f);
                pickedActionRequirements.GetComponent<TMP_Text>().text = ("Needs 10 in strength");
                break;
        }
    }

    public void action(string name)
    {
        switch (name)
        {
            case "Uppercut":
                player.gauge -= attack_Uppercut.gaugeCost;
                break;
        }
    }

    public void ChangeGaugeBar()
    {
        playerGaugeBarLocation.position = new Vector2((-960 + (player.gauge/2))/108,0);
        playerGaugeBar.transform.localScale = new Vector2(player.gauge,1);   
    }
}
