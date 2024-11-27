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


    //Base attack
    public attackClass attack_Punch;
    //public Sprite ;


    //Physical attacks
    public attackClass attack_Uppercut;
    public Sprite uppercutSprite;
    //public attackClass attack_PunchRush;
    //public Sprite;
    public attackClass attack_RisingSpirit;
    //public Sprite;
    public attackClass attack_BodyBreaker;
    //public Sprite;
    public attackClass attack_RunicImpact;
    //public Sprite;
    public attackClass attack_StrikingLightning;
    //public Sprite;


    public attackClass attack_Embers;
    //public Sprite;
    public attackClass attack_ThunderStrike;
    //public Sprite;
    public attackClass attack_MagicMirror;
    //public Sprite;
    public attackClass attack_FrostArmor;
    //public Sprite;
    public attackClass attack_Enfeeble;
    //public Sprite;
    public attackClass attack_DomainExpansion;
    //public Sprite;


    void Start()
    {
        attack_Uppercut = new attackClass("Uppercut", "strength", 10f, 1f, 1.2f, 0f, 0f, 0f, "Physical");
        player.playerAttacks.Add(attack_Uppercut);
        //attack_PunchRush = new attackClass("PunchRush", "strength", 20f, 2f, 0.65f, 0f, 0f, 0f, "Physical");
        //player.playerAttacks.Add(attack_PunchRush);
        attack_RisingSpirit = new attackClass("RisingSpirit", "strength", 30f, 2f, 1.3f, 0f, 0f, 0f, "Physical");
        player.playerAttacks.Add(attack_RisingSpirit);
        attack_BodyBreaker = new attackClass("BodyBreaker", "strength", 50f, 4f, 3f, -(player.maxHealth), 0f, 0f, "Physical");
        player.playerAttacks.Add(attack_BodyBreaker);
        /*
        attack_RunicImpact = new attackClass("RunicImpact", "strength", f, f, f, f, f, f, "Physical");
        player.playerAttacks.Add(attack_RunicImpact);
        attack_StrikingLightning = new attackClass("StrikingLightning", "strength", f, f, f, f, f, f, "Physical");
        player.playerAttacks.Add(attack_StrikingLightning);
        */
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
                pickedActionAction.GetComponent<TMP_Text>().text = ("Physical damage: " + player.strength * attack_Uppercut.attackDamage);
                pickedActionRequirements.GetComponent<TMP_Text>().text = $"Needs {attack_Uppercut.unlockStatNumber} in {attack_Uppercut.unlockStatName}";
                break;
            
        case "Punch Rush":
            pickedActionIcon.GetComponent<Image>().sprite = noSprite;
            pickedActionName.GetComponent<TMP_Text>().text = name;
            pickedActionDescription.GetComponent<TMP_Text>().text = "Rush forward and pummel your opponent with a barrage of strikes";
            pickedActionAction.GetComponent<TMP_Text>().text = ("Physical damage: " + player.strength * attack_PunchRush.attackDamage);
            pickedActionRequirements.GetComponent<TMP_Text>().text = ("20 Needs  in strength");
            break;
         /*
            case "":
                pickedActionIcon.GetComponent<Image>().sprite = noSprite;
                pickedActionName.GetComponent<TMP_Text>().text = name;
                pickedActionDescription.GetComponent<TMP_Text>().text = "";
                pickedActionAction.GetComponent<TMP_Text>().text = ("Physical damage: " + player.strength * 1.2f);
                pickedActionRequirements.GetComponent<TMP_Text>().text = ("Needs " + " in strength");
                break;

            case "":
                pickedActionIcon.GetComponent<Image>().sprite = noSprite;
                pickedActionName.GetComponent<TMP_Text>().text = name;
                pickedActionDescription.GetComponent<TMP_Text>().text = "";
                pickedActionAction.GetComponent<TMP_Text>().text = ("Physical damage: " + player.strength * 1.2f);
                pickedActionRequirements.GetComponent<TMP_Text>().text = ("Needs  in strength");
                break;

            case "":
                pickedActionIcon.GetComponent<Image>().sprite = noSprite;
                pickedActionName.GetComponent<TMP_Text>().text = name;
                pickedActionDescription.GetComponent<TMP_Text>().text = "";
                pickedActionAction.GetComponent<TMP_Text>().text = ("Physical damage: " + player.strength * 1.2f);
                pickedActionRequirements.GetComponent<TMP_Text>().text = ("Needs  in strength");
                break;
                */
            
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
