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
    [SerializeField] private TowerAttackRange towerAttackRange;

    private Weapon currentTower;
    private void Awake()
    {
        OffPanel();
    }

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
        UpdateTowerData();
        towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.AttackRange);
    }

    public void OffPanel()
    {
        gameObject.SetActive(false);
        towerAttackRange.OffAttackRange();
    }

    private void UpdateTowerData()
    {
        textDamage.text = "Damage : " + currentTower.AttackDamage;
        textRate.text = "Rate       : " + currentTower.AttackRate;
        textRange.text = "Range    : " + currentTower.AttackRange;
        textLevel.text = "Lv. " + currentTower.Level;
    }
}
