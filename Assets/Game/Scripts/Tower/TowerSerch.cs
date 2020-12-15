using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSerch : MonoBehaviour
{
    private TowerManager towerManager;


    void Start()
    {
        towerManager = GetComponentInParent<TowerManager>();

    }

    private void OnTriggerExit(Collider other)
    {
        // MyTower
        if (towerManager.Check)
        {
            if (towerManager.State == TowerManager.TowerState.Normal)
            {
                if(other.tag== "minionPC")
                {
                    towerManager.RestTowerHP();
                }
                if (other.tag == "UnitPC")
                {
                    towerManager.RestTowerHP();
                }
            }
            else
            {
                return;
            }
        }
        // PCTower
        if (!towerManager.Check)
        {
            if (towerManager.State == TowerManager.TowerState.Normal)
            {
                if (other.tag == "minion")
                {
                    towerManager.RestTowerHP();
                }
                if (other.tag == "Unit")
                {
                    towerManager.RestTowerHP();
                }
            }
            else
            {
                return;
            }
        }
    }

    // タワーからの攻撃 
    private void OnTriggerStay(Collider other)
    {
        // Myタワー
        if (towerManager.Check)
        {
            if (towerManager.State == TowerManager.TowerState.Normal)
            {
                if (other.tag == "minionPC")
                {
                    var pos = other.GetComponent<Transform>();
                    towerManager.AttackTower(pos);
                }
                if (other.tag == "UnitPC")
                {
                    var pos = other.GetComponent<Transform>();
                    towerManager.AttackTower(pos);
                }
            }
            else
            {
                return;
            }
        }

        // Pcタワー
        if (!towerManager.Check)
        {
            if (towerManager.State == TowerManager.TowerState.Normal)
            {
                if (other.tag == "minion")
                {
                    var pos = other.GetComponent<Transform>();
                    towerManager.AttackTower(pos);
                }
                if (other.tag == "Unit")
                {
                    var pos = other.GetComponent<Transform>();
                    towerManager.AttackTower(pos);
                }
            }
            else
            {
                return;
            }
        }
    }
}
