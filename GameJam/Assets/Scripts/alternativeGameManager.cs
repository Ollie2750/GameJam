using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class alternativeGameManager : MonoBehaviour
{
    public Sprite noSprite;

    //Level up screen Objects
    public GameObject levelUpCanvas;


    //Menu UI GameObjects
    public GameObject menuCanvas;

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
    public int amountOfGaugeBars = 20;
    
    //player
    public TMP_Text playerHealthText;
    public playerClass player;
    public GameObject playerBody;

    //enemy
    public TMP_Text enemyhealthText;
    public enemyClass enemy;
    public GameObject enemyPrefab;
    private GameObject currentEnemy;

    public List<attackClass> playerAttacks = new List<attackClass>();


    public bool inCombat = false;

    private Dictionary<string, attackClass> attackDictionary;
    private List<string> enemyNameList = new List<string>();

    //Stat cards
    private List<string> statList = new List<string>() {"Max Health","Strength","Intelligence","Physical Defence","Magical Defence", "Gauge Speed", "Gauge Size", "Luck" };
    private List<int> levelCard1 = new List<int>();
    private List<int> levelCard2 = new List<int>();
    private List<int> levelCard3 = new List<int>();

    private List<string> levelCard1StatType = new List<string>();
    private List<string> levelCard2StatType = new List<string>();
    private List<string> levelCard3StatType = new List<string>();


    //Attack sprites
    public Sprite uppercutSprite;
    public Sprite punchSprite;

    //Enemy sprite
    public Sprite slimeSprite;
    public Sprite goblinSprite;

    void Start()
    {
        menuCanvas.SetActive(true);
        levelUpCanvas.SetActive(false);
        attackDictionary = new Dictionary<string, attackClass>
        {
            {"Punch", new attackClass("Punch", "", 0f, 1f, 1f, 0f, 0f, 0f, "Physical", punchSprite, "Lets intrduce them to our fist") },
            {"Uppercut", new attackClass("Uppercut", "strength", 10f, 1f, 1.2f*player.strength, 0f, 0f, 0f, "Physical", uppercutSprite, "Duck and strike your opponent from underneath") },
            {"Rising Spirit", new attackClass("RisingSpirit", "strength", 30f, 2f, 1.3f, 0f, 0f, 0f, "Physical", noSprite, "Bolster your booty and spirit to strike your opponent and raise your strength") },
            {"BodyBreaker", new attackClass("BodyBreaker", "strength", 50f, 4f, 3f, -(player.maxHealth), 0f, 0f, "Physical", noSprite, "Break your opponents body with a force so great that it damages your own.") },
            {"Embers", new attackClass("Embers", "intelligence", 10f, 1f, 1.2f, 0f, 0f, 0f, "Magical", noSprite, "Produce a small, yet deadly spark of embers from your fingers, and set it towards") },
            {"Thunderstrike", new attackClass("Thumderstrike", "intelligence", 20f, 1f, 1.4f, 0f, 0f, 0f, "Magical", noSprite, "Overpower your enemies with a chaotic force of thunder that strikes the opponent and his ally.") },
            {"Frost Armor", new attackClass("Frost Armor", "intelligence", 35f, 0f, 0f, 0f, 0f, 1.4f, "Magical", noSprite, "Reinforce your body with magical ice to absorb incoming magical damage.") },

        };
        foreach (var attack in attackDictionary.Values)
        {
            player.playerAttacks.Add(attack);
        }

     

        enemyNameList = new List<string>() {"Slime","Goblin"};

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
            button.onClick.AddListener(() => PlayerAttack(attack.attackName));


            GameObject tempButtonChild = tempButton.transform.GetChild(0).gameObject;
            tempButtonChild.GetComponent<TMP_Text>().text = attack.attackName;
        }

    }


    void FixedUpdate()
    {
        if (inCombat)
        {
            player.playerTurn();
            string anAttack = enemy.enemyTurn(); //Pick enemy attack
            if (anAttack != "")
            {
                EnemyAttack(anAttack);
            }
            
            ChangeGaugeBar();
            playerHealthText.text = player.health.ToString();
            enemyhealthText.text = enemy.health.ToString();

            if (enemy.health <= 0)
            {
                inCombat = false;
                attackMenuButton.SetActive(false);
                attackMenu.SetActive(false);
                menu.SetActive(false);
                menuCanvas.SetActive(false);
                currentEnemy.SetActive(false);

                playerWon(enemy.exp);
            } 
            else if (player.health <= 0)
            {
                inCombat = false;
                attackMenuButton.SetActive(false);
                attackMenu.SetActive(false);
                menu.SetActive(false);
                menuCanvas.SetActive(false);
                playerBody.SetActive(false);

            }
        }


    }

    public void StartCombat()
    {
        inCombatButton.SetActive(false);
        attackMenuButton.SetActive(true);
        menu.SetActive(true);
        attackMenu.SetActive(true);

        NewEnemy(enemyNameList[UnityEngine.Random.Range(0, enemyNameList.Count)]);

        currentEnemy.SetActive(true);
        inCombat = true;

    }

    public void ShowAttackMenu()
    {
        attackMenu.SetActive(true);
    }

    public void ChangeMenu(string name)
    {
        if (attackDictionary.TryGetValue(name, out var attack))
        {
            pickedActionIcon.GetComponent<Image>().sprite = attack.sprite; // Replace with your sprite logic
            pickedActionName.GetComponent<TMP_Text>().text = attack.attackName;
            pickedActionDescription.GetComponent<TMP_Text>().text = attack.description; // Replace with your description logic
            pickedActionAction.GetComponent<TMP_Text>().text = $"{attack.damageType} damage: {attackDictionary[name].attackDamage}";
            pickedActionRequirements.GetComponent<TMP_Text>().text = $"Needs {attack.unlockStatNumber} in {attack.unlockStatName}";
        }
        else
        {
            pickedActionIcon.GetComponent<Image>().sprite = noSprite;
            pickedActionName.GetComponent<TMP_Text>().text = "";
            pickedActionDescription.GetComponent<TMP_Text>().text = "";
            pickedActionAction.GetComponent<TMP_Text>().text = "";
            pickedActionRequirements.GetComponent<TMP_Text>().text = "";
        }
    }

    /// <summary>
    /// Adds a new enemy
    /// </summary>
    /// <param Enemy name="name"></param>
    public void NewEnemy(string name)
    {
        
        switch (name)
        {
            case "Slime":
                enemy = new enemyClass(slimeSprite, 5f, 5f, 0f, 1f, .5f, 3f, 0f, 1f, 1f, 1, new List<attackClass>());
                enemy.attackList.Clear();
                enemy.attackList.Add(attackDictionary["Punch"]);
                break;
            case "Goblin":
                enemy = new enemyClass(goblinSprite, 8f, 8f, 2f, 1f, .8f, 3f, 0f, 2f, 0f, 2, new List<attackClass>());
                enemy.attackList.Clear();
                enemy.attackList.Add(attackDictionary["Punch"]);
                break;
        }
        
        
        if (enemy != null) { Destroy(currentEnemy); }
        currentEnemy = Instantiate(enemyPrefab);
        currentEnemy.transform.position = new Vector2(5, -1);
        currentEnemy.GetComponent<SpriteRenderer>().sprite = enemy.sprite;

        
    }

    public void PlayerAttack(string name)
    {
        if (player.gauge > attackDictionary[name].gaugeCost)
        {
            player.gauge -= attackDictionary[name].gaugeCost;
            switch (name)
            {
                case "Punch":
                    //play animation
                    if (player.strength >= player.intelligence)
                    {
                        enemy.getHit(player.strength, attackDictionary[name].damageType);
                    }
                    else
                    {
                        enemy.getHit(player.intelligence, attackDictionary[name].damageType);
                    }
                    
                    break;

                case "Uppercut":
                    enemy.getHit(attackDictionary[name].attackDamage, attackDictionary[name].damageType);
                    break;
            }
        }
        
    }

    public void EnemyAttack(string name)
    {
        if (enemy.gauge > attackDictionary[name].gaugeCost)
        {
            enemy.gauge -= attackDictionary[name].gaugeCost;
            switch (name)
            {
                case "Punch":
                    //play animation
                    player.getHit(attackDictionary[name].attackDamage, attackDictionary[name].damageType);
                    break;
            }
        }
    }

    public void ChangeGaugeBar()
    {
        playerGaugeBar.transform.localScale = new Vector2((player.gauge / player.gaugeBarSize) / amountOfGaugeBars, 1);
    }

    public void playerWon(int exp)
    {
        player.exp += exp;
        if (player.exp >= player.level * 10)
        {
            player.exp -= player.level * 10;
            playerLevelUp();
        }
        else
        {
            menuCanvas.SetActive(true);
            inCombatButton.SetActive(true);
            enemyPrefab.SetActive(true);
        }
    }

    public void playerLevelUp()
    {
        playerBody.SetActive(false);
        player.level++;

        levelCard1 = setlevelCardAmounts();
        levelCard2 = setlevelCardAmounts();
        levelCard3 = setlevelCardAmounts();

        levelCard1StatType = setlevelCardStatTypes();
        levelCard2StatType = setlevelCardStatTypes();
        levelCard3StatType = setlevelCardStatTypes();

        //Show on cards in game


        levelUpCanvas.SetActive(true);
    }

    public List<int> setlevelCardAmounts()
    {
        List<int> aLevelCard = new List<int>() { 0, 0, 0 };
        List<int> anotherLevelCard = new List<int> { 0, 0, 0 };
        
        int statAmout = UnityEngine.Random.Range(3 * player.level, 6 * player.level);
        Debug.Log(statAmout);
        foreach (int i in aLevelCard)
        {
            if (i == 2)
            {
                anotherLevelCard[i] = statAmout;
            }
            else
            {
                int amount = UnityEngine.Random.Range(0, statAmout);
                statAmout -= amount;
                anotherLevelCard[i] = amount;
            }
        }

        return anotherLevelCard;
    }

    public List<string> setlevelCardStatTypes()
    {
        List<string> aLevelCard = new List<string>();
        aLevelCard.Add(statList[UnityEngine.Random.Range(0, statList.Count)]);
        aLevelCard.Add(statList[UnityEngine.Random.Range(0, statList.Count)]);

        return aLevelCard;
    }

    public void SelectStats(int cardNumber)
    {
        switch (cardNumber)
        {
            case 0:
                Debug.Log(levelCard1StatType[0] + " : " + levelCard1[0]);
                Debug.Log(levelCard1StatType[1] + " : " + levelCard1[1]);
                Debug.Log(levelCard1[2]);
                player.stats[levelCard1StatType[0]] += levelCard1[0];
                player.stats[levelCard1StatType[1]] += levelCard1[1];
                player.health += levelCard1[2];
                break;
            case 1:
                player.stats[levelCard2StatType[0]] += levelCard2[0];
                player.stats[levelCard2StatType[1]] += levelCard2[1];
                player.health += levelCard2[2];
                break;
            case 2:
                player.stats[levelCard3StatType[0]] += levelCard3[0];
                player.stats[levelCard3StatType[1]] += levelCard3[1];
                player.health += levelCard3[2];
                break;
        }
        player.updateStats();
        Debug.Log("Stats selected");
        levelUpCanvas.SetActive(false);
        menuCanvas.SetActive(true);
        playerBody.SetActive(true);
        inCombatButton.SetActive(true);

    }

    //Skal måske ikke bruges
    public class AttackData
    {
        public Sprite Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float DamageMultiplier { get; set; }
        public string Requirements { get; set; }
    }
}
