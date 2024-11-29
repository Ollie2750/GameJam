using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class alternativeGameManager : MonoBehaviour
{

    //GameOver screen objects
    public GameObject gameOverCanvas;


    //Level up screen Objects
    public GameObject levelUpCanvas;

    [Header("Cards")]
    //Card1
    public Image card1Image1;
    public Image card1Image2;
    public TMP_Text card1Stat1;
    public TMP_Text card1Stat2;
    public TMP_Text card1Stat3;

    //card2
    public Image card2Image1;
    public Image card2Image2;
    public TMP_Text card2Stat1;
    public TMP_Text card2Stat2;
    public TMP_Text card2Stat3;

    //card3
    public Image card3Image1;
    public Image card3Image2;
    public TMP_Text card3Stat1;
    public TMP_Text card3Stat2;
    public TMP_Text card3Stat3;

    //Menu UI GameObjects
    [Header("Menu UI GameObjects")]
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
    public int amountOfGaugeBars = 5;

    private List<GameObject> attackAndAbilitieButtens = new List<GameObject>();

    [Header("Player")]
    //player
    public TMP_Text playerHealthText;
    public playerClass player;
    public GameObject playerBody;
    public GameObject playerHealthBar;
    public List<attackClass> playerAttacks = new List<attackClass>();


    [Header("Enemy")]
    //enemy
    private float enemyScaling = 1f;
    public TMP_Text enemyhealthText;
    public enemyClass enemy;
    public GameObject enemyPrefab;
    private GameObject currentEnemy;
    public GameObject enemyHealthBar;



    public bool inCombat = false;

    private Dictionary<string, attackClass> attackDictionary;
    private List<string> enemyNameList = new List<string>();


    [Header("Stat cards")]
    //Stat cards
    private List<string> statList = new List<string>() {"Max Health","Strength","Intelligence","Physical Defence","Magical Defence", "Gauge Speed", "Gauge Size", "Luck" };
    private List<int> levelCard1 = new List<int>();
    private List<int> levelCard2 = new List<int>();
    private List<int> levelCard3 = new List<int>();

    private List<string> levelCard1StatType = new List<string>();
    private List<string> levelCard2StatType = new List<string>();
    private List<string> levelCard3StatType = new List<string>();


    [Header("Sprites")]
    //Placeholder sprite
    public Sprite noSprite;

    //Attack sprites
    public Sprite punchSprite;
    public Sprite punchButtonSprite;
    public Sprite uppercutSprite;
    public Sprite uppercutButtonSprite;


    //Enemy sprite
    public Sprite slimeSprite;
    public Sprite goblinSprite;

    [Header("-----SFX-----")]
    public AudioClip punchSFX;
    public AudioClip uppercutSFX;
    public AudioClip risingSpiritSFX;
    public AudioClip bodyBreakerSFX;
    public AudioClip runicImpactSFX;
    public AudioClip embersSFX;
    public AudioClip thunderstrikeSFX;
    public AudioClip frostArmorSFX;

    void Start()
    {
        menuCanvas.SetActive(true);
        levelUpCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        attackDictionary = new Dictionary<string, attackClass>
        {
            {"Punch", new attackClass("Punch", 0f, 0f, 1f, 1f, 1f, 0f, 0f, 0f, 0f, "Physical","", punchSprite, punchButtonSprite, "Lets intrduce them to our fist", punchSFX) },
            {"Uppercut", new attackClass("Uppercut", 2f, 0f, 1f, 1.2f, 0f, 0f, 0f, 0f, 0f, "Physical", "", uppercutSprite, uppercutButtonSprite, "Duck and strike your opponent from underneath", uppercutSFX) },
            {"Rising Spirit", new attackClass("Rising Spirit", 30f, 0f, 2f, 1.3f, 0f, 0f, 0f, 0f, 0f, "Physical", "", noSprite, noSprite, "Bolster your booty and spirit to strike your opponent and raise your strength", risingSpiritSFX) },
            {"BodyBreaker", new attackClass("BodyBreaker", 50f, 0f, 4f, 3f, 0f, 0, 0f, 0f, 0f, "Physical", "", noSprite, noSprite, "Break your opponents body with a force so great that it damages your own.", bodyBreakerSFX) },
            {"Runic Impact", new attackClass("Runic Impact", 40f, 15f, 2f, 1.4f, 1.1f, 0f, 0f, 0f, 0f, "Physical", "Magical", noSprite, noSprite, "Channel your inner magic into your fist, to stike your opponent with a devastating runic blast", runicImpactSFX) },
            {"Embers", new attackClass("Embers", 10f, 0f, 1f, 1.2f, 0f, 0f, 0f, 0f, 0f, "Magical", "", noSprite, noSprite, "Produce a small, yet deadly spark of embers from your fingers, and set it towards", embersSFX) },
            {"Thunderstrike", new attackClass("Thunderstrike", 20f, 0f, 1f, 1.4f, 0f, 0f, 0f, 0f, 0f, "Magical", "", noSprite, noSprite, "Overpower your enemies with a chaotic force of thunder that strikes the opponent and his ally.", thunderstrikeSFX) },
            {"Frost Armor", new attackClass("Frost Armor", 35f, 0f, 1f, 0f, 0f, 0f, 0f, 1.4f, 15f, "", "", noSprite, noSprite, "Reinforce your body with magical ice to absorb incoming magical damage.",frostArmorSFX) },

        };

     

        enemyNameList = new List<string>() {"Slime","Goblin"};

        

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
            player.updateBufs();
            
            ChangeGaugeBar();
            ChangeHealthBarPlayer();
            ChangeHealthBarEnemy();
            int hp = (int)player.health;
            playerHealthText.text = hp.ToString();
            hp = (int)enemy.health;
            enemyhealthText.text = hp.ToString();
            

            if (enemy.health <= 0)
            {
                inCombat = false;
                attackMenuButton.SetActive(false);
                attackMenu.SetActive(false);
                menu.SetActive(false);
                menuCanvas.SetActive(false);
                currentEnemy.SetActive(false);

                playerWon((int)enemy.exp);
            } 
            else if (player.health <= 0)
            {
                inCombat = false;
                attackMenuButton.SetActive(false);
                attackMenu.SetActive(false);
                menu.SetActive(false);
                menuCanvas.SetActive(false);
                playerBody.SetActive(false);

                gameOverCanvas.SetActive(true);
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

        player.playerAttacks.Clear();

        foreach (var attack in attackDictionary)
        {
            bool type1 = false;
            bool type2 = false;
            
            if (player.strength >= attackDictionary[attack.Key].unlockStatNumber1)
            {
                type1 = true;
            }
            if (player.intelligence >= attackDictionary[attack.Key].unlockStatNumber2)
            {
                type2 = true;
            }

            if (type1 && type2)
            {
                player.playerAttacks.Add(attackDictionary[attack.Key]);
            }
        }

        foreach (GameObject aButten in attackAndAbilitieButtens)
        {
            Destroy(aButten);
        }
        attackAndAbilitieButtens.Clear();

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
        
            tempButton.GetComponent<Image>().sprite = attack.buttonSprite;

            attackAndAbilitieButtens.Add(tempButton);
        }


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
            pickedActionAction.GetComponent<TMP_Text>().text = $"{attack.damageType1} damage: {attackDictionary[name].attackDamage1}";
            pickedActionRequirements.GetComponent<TMP_Text>().text = $"Needs {attack.unlockStatNumber1} in strength";
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
                enemy = new enemyClass(slimeSprite, 5f * enemyScaling, 5f * enemyScaling, 0f * enemyScaling, 1f * enemyScaling, .5f * enemyScaling, 3f * enemyScaling, 0f * enemyScaling, 1f * enemyScaling, 1f * enemyScaling, 1f * enemyScaling, new List<attackClass>());
                enemy.attackList.Clear();
                enemy.attackList.Add(attackDictionary["Punch"]);
                break;
            case "Goblin":
                enemy = new enemyClass(goblinSprite, 8f * enemyScaling, 8f * enemyScaling, 2f * enemyScaling, 1f * enemyScaling, .8f * enemyScaling, 3f * enemyScaling, 0f * enemyScaling, 2f * enemyScaling, 0f * enemyScaling, 2f * enemyScaling, new List<attackClass>());
                enemy.attackList.Clear();
                enemy.attackList.Add(attackDictionary["Punch"]);
                break;
        }
        
        
        if (enemy != null) { Destroy(currentEnemy); }
        currentEnemy = Instantiate(enemyPrefab);
        currentEnemy.transform.position = new Vector2(5, -.4f);
        currentEnemy.GetComponent<SpriteRenderer>().sprite = enemy.sprite;

        enemyScaling += .1f;
    }

    public void PlayerAttack(string name)
    {
        if (player.gauge >= attackDictionary[name].gaugeCost)
        {
            player.gauge -= attackDictionary[name].gaugeCost;
            switch (name)
            {
                case "Punch":
                    //play animation
                    if (player.strength >= player.intelligence)
                    {
                        enemy.getHit(player.strength, attackDictionary[name].damageType1);
                    }
                    else
                    {
                        enemy.getHit(player.intelligence, attackDictionary[name].damageType1);
                    }
                    soundEffects.PlaySFX(attackDictionary[name].SFX);
                    break;

                case "Uppercut":
                    enemy.getHit(attackDictionary[name].attackDamage1 * player.strength, attackDictionary[name].damageType1);
                    soundEffects.PlaySFX(attackDictionary[name].SFX);
                    break;
                case "Rising Spirit":
                    enemy.getHit(attackDictionary[name].attackDamage1 * player.strength, attackDictionary[name].damageType1);
                    soundEffects.PlaySFX(attackDictionary[name].SFX);
                    break;
                case "BodyBreaker":
                    enemy.getHit(attackDictionary[name].attackDamage1 * player.strength, attackDictionary[name].damageType1);
                    player.getHit(player.health * .2f, attackDictionary[name].damageType1);
                    soundEffects.PlaySFX(attackDictionary[name].SFX);
                    break;
                case "Runic Impact":
                    enemy.getHit(attackDictionary[name].attackDamage1 * player.strength, attackDictionary[name].damageType1);
                    enemy.getHit(attackDictionary[name].attackDamage2 * player.intelligence, attackDictionary[name].damageType2);
                    soundEffects.PlaySFX(attackDictionary[name].SFX);
                    break;
                case "Embers":
                    enemy.getHit(attackDictionary[name].attackDamage1 * player.strength, attackDictionary[name].damageType1);
                    soundEffects.PlaySFX(attackDictionary[name].SFX);
                    break;
                case "Thunderstrike":
                    enemy.getHit(attackDictionary[name].attackDamage1 * player.strength, attackDictionary[name].damageType1);
                    soundEffects.PlaySFX(attackDictionary[name].SFX);
                    break;
                case "Frost Armor":
                    player.boostStat(attackDictionary[name].magicalDefenceIecrease, "MagicalDefence", attackDictionary[name].duration);
                    soundEffects.PlaySFX(attackDictionary[name].SFX);
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
                    player.getHit(attackDictionary[name].attackDamage1, attackDictionary[name].damageType1);
                    
                    break;
            }
        }
    }

    public void ChangeGaugeBar()
    {
        playerGaugeBar.transform.localScale = new Vector2((player.gauge / player.gaugeBarSize), 1);
    }

    public void ChangeHealthBarPlayer()
    {
        playerHealthBar.transform.localScale = new Vector2(player.health / player.maxHealth, 1);
    }

    public void ChangeHealthBarEnemy()
    {
        enemyHealthBar.transform.localScale = new Vector2(enemy.health / enemy.maxHealth, 1);
    }

    public void playerWon(int exp)
    {
        player.exp += exp;
        if (player.exp >= player.level * 3)
        {
            player.exp -= player.level * 3;
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
        card1Image1.sprite = noSprite;
        card1Image2.sprite = noSprite;
        card1Stat1.text = (levelCard1StatType[0] + " : " + levelCard1[0]);
        card1Stat2.text = (levelCard1StatType[1] + " : " + levelCard1[1]);
        card1Stat3.text = ("Heal : " + levelCard1[2]);

        card2Image1.sprite = noSprite;
        card2Image2.sprite = noSprite;
        card2Stat1.text = (levelCard2StatType[0] + " : " + levelCard2[0]);
        card2Stat2.text = (levelCard2StatType[1] + " : " + levelCard2[1]);
        card2Stat3.text = ("Heal : " + levelCard2[2]);

        card3Image1.sprite = noSprite;
        card3Image2.sprite = noSprite;
        card3Stat1.text = (levelCard3StatType[0] + " : " + levelCard3[0]);
        card3Stat2.text = (levelCard3StatType[1] + " : " + levelCard3[1]);
        card3Stat3.text = ("Heal : " + levelCard3[2]);


        levelUpCanvas.SetActive(true);
    }

    public List<int> setlevelCardAmounts()
    {
        List<int> aLevelCard = new List<int>() { 0, 0, 0 };
        
        int statAmount = UnityEngine.Random.Range(3 * player.level, 6 * player.level);
        
        
        for (int i = 0; i < aLevelCard.Count; i++)
        {

            if (i == aLevelCard.Count - 1)
            {
                aLevelCard[i] = statAmount;
            }
            else
            {
                int amount = UnityEngine.Random.Range(0, statAmount);

                aLevelCard[i] = amount;
                statAmount -= amount;

            }
        }

        return aLevelCard;
    }

    public List<string> setlevelCardStatTypes()
    {
        List<string> aLevelCard = new List<string>();
        aLevelCard.Add(statList[UnityEngine.Random.Range(0, statList.Count)]);
        aLevelCard.Add(statList[UnityEngine.Random.Range(0, statList.Count)]);
        if(player.gaugeSize >= 5f && statList[statList.Count - 2] == "Gauge Size")
        {
            statList.RemoveAt(statList.Count - 2);
        }
        return aLevelCard;
    }

    public void SelectStats(int cardNumber)
    {
        switch (cardNumber)
        {
            case 0:
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
