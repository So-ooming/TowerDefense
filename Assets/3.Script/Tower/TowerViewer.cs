using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerViewer : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text textDamage;
    [SerializeField] private Text textRate;
    [SerializeField] private Text textRange;
    [SerializeField] private Text textLevel;
    [SerializeField] private Text textUpgradeCost;
    [SerializeField] private Text textSellCost;
    [SerializeField] private Text textImprovedDamage;
    [SerializeField] private Text textImprovedRate;
    [SerializeField] private Text textImprovedRange;
    [SerializeField] private TowerAttackRange towerAttackRange;
    [SerializeField] private TowerSpawner towerSpawner;
    [SerializeField] private PlayerGold playerGold;
    [SerializeField] private ObjectDetector objectDetector;
    private Tile tile;

    private int upgradeCost;
    private int sellCost;

    private Weapon currentTower;
    /*private void Awake()
    {
        OffPanel();
    }*/

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OffPanel();
        }
    }

    public void OnPanel(Transform tower)
    {
        currentTower = tower.GetComponent<Weapon>();
        gameObject.SetActive(true);
        SetCost();
        UpdateTowerData();
        towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.AttackRange);
    }

    public void UpgradeTower()
    {
        if(playerGold.CurrentGold > currentTower.Level * upgradeCost)
        {
            playerGold.CurrentGold -= currentTower.Level * upgradeCost;
            currentTower.UpgradeTower(currentTower);
        }
    }

    public void SellTower()
    {
        playerGold.CurrentGold += currentTower.Level * sellCost;
        currentTower.IsBuildSetFalse();
        Destroy(currentTower.gameObject);
        OffPanel();
    }

    public void OffPanel()
    {
        gameObject.SetActive(false);
        towerAttackRange.OffAttackRange();
    }

    public void UpdateTowerData()
    {
        image.sprite = currentTower.TowerSprite;
        textDamage.text = "Damage : " + (currentTower.AttackDamage);
        textRate.text = "Rate       : " + (currentTower.AttackRate).ToString("F2");
        textRange.text = "Range    : " + (currentTower.AttackRange).ToString("F2");
        textLevel.text = "Lv. " + currentTower.Level;
        textUpgradeCost.text = (currentTower.Level * (int)upgradeCost).ToString() + " $";
        textSellCost.text = (currentTower.Level * (int)sellCost).ToString() + " $";
        textImprovedDamage.text = "(+" + currentTower.ImprovedDamage.ToString() + ")";
        textImprovedRate.text = "(-" + (-currentTower.ImprovedRate).ToString("F2") + ")";
        textImprovedRange.text = "(+" + currentTower.ImprovedRange.ToString("F2") + ")";
        towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.AttackRange);
    }

    private void SetCost()
    {
        upgradeCost = (int)((float)towerSpawner.TowerBuildGold * 1.2f);
        sellCost = (int)((float)towerSpawner.TowerBuildGold * 0.6f);
    }
}
