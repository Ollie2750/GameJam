using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class alternativeGameManager : MonoBehaviour
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

    private Dictionary<string, attackClass> attackDictionary;



    void Start()
    {
        attackDictionary = new Dictionary<string, attackClass>
        {
            {"Uppercut", new attackClass("Uppercut", "strength", 10f, 1f, 1.2f, 0f, 0f, 0f, "Physical") },
            {"Rising Spirit", new attackClass("RisingSpirit", "strength", 30f, 2f, 1.3f, 0f, 0f, 0f, "Physical") },
            {"BodyBreaker", new attackClass("BodyBreaker", "strength", 50f, 4f, 3f, -(player.maxHealth), 0f, 0f, "Physical") },
            {"Embers", new attackClass("Embers", "intelligence", 10f, 1f, 1.2f, 0f, 0f, 0f, "Magical") },
            {"Thunderstrike", new attackClass("Thumderstrike", "intelligence", 20f, 1f, 1.4f, 0f, 0f, 0f, "Magical") },
            {"Frost Armor", new attackClass("Frost Armor", "intelligence", 35f, 0f, 0f, 0f, 0f, 1.4f, "Magical") },

        };
        foreach (var attack in attackDictionary.Values)
        {
            player.playerAttack.Add(attack);
        }
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
        if (attackDictionary.TryGetValue(attackName, out var attack))
        {
            // Update UI elements with attack information
            pickedActionIcon.GetComponent<Image>().sprite = GetSpriteForAttack(attackName); // Replace with your sprite logic
            pickedActionName.GetComponent<TMP_Text>().text = attack.attackName;
            pickedActionDescription.GetComponent<TMP_Text>().text = GetDescriptionForAttack(attackName); // Replace with your description logic
            pickedActionAction.GetComponent<TMP_Text>().text = $"{attack.damageType} damage: {player.strength * attack.attackDamage}";
            pickedActionRequirements.GetComponent<TMP_Text>().text = $"Needs {attack.unlockStatNumber} in {attack.unlockStatName}";
        }
        else
        {
            // Handle "None" or unknown attack case
            pickedActionIcon.GetComponent<Image>().sprite = noSprite;
            pickedActionName.GetComponent<TMP_Text>().text = "";
            pickedActionDescription.GetComponent<TMP_Text>().text = "";
            pickedActionAction.GetComponent<TMP_Text>().text = "";
            pickedActionRequirements.GetComponent<TMP_Text>().text = "";
        }
    }

    private Sprite GetSpriteForAttack(string attackName)
    {
        // Example method to get a sprite based on the attack name
        switch (attackName)
        {
            case "Uppercut": return uppercutSprite;
            default: return noSprite;
        }
    }

    private string GetDescriptionForAttack(string attackName)
    {
        // Example method to get a description based on the attack name
        switch (attackName)
        {
            case "Uppercut": return "Duck and strike your opponent from underneath.";
            default: return "Unknown attack.";
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
        playerGaugeBarLocation.position = new Vector2((-960 + (player.gauge / 2)) / 108, 0);
        playerGaugeBar.transform.localScale = new Vector2(player.gauge, 1);
    }
    public class AttackData
    {
        public Sprite Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float DamageMultiplier { get; set; }
        public string Requirements { get; set; }
    }
}
