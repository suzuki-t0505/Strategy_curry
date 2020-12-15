using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMinion : MonoBehaviour
{
    private bool check;
    public bool setCheck
    {
        set { check = value; }
    }

    private float attack;
    private GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        // pcキャラクター
        if (!check)
        {
            if (other.tag == "Unit")
            {
                Unit(other);
            }
            if (other.tag == "minion")
            {
                Minion(other);
            }
            if (other.tag == "MyTower")
            {
                Tower(other);
            }
            if (other.tag == "MyBase")
            {
                Base(other);
            }
        }
        // myキャラクター
        if (check)
        {
            if (other.tag == "UnitPC")
            {
                PcUnit(other);
            }
            if (other.tag == "minionPC")
            {
                Minion(other);
            }
            if (other.tag == "PcTower")
            {
                Tower(other);
            }
            if (other.tag == "PcBase")
            {
                Base(other);
            }
        }
    }
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine("Destruction");
    }

    private float AttackPower()
    {
        var randomAttackPower = Random.Range(0.8f, 1.5f);
        return randomAttackPower;
    }
    public float SetAttackPower
    {
        set { attack = value; }
    }

    // ベース
    private void Base(Collider other = null)
    {
        if (gameManager.MyGoBase == true)
        {
            if (other.GetComponent<BaseManager>().State == BaseManager.BaseState.Normal)
            {
                other.GetComponent<BaseManager>().BaseHP = AttackPower();
                Destroy(this.gameObject);
            }
            else if (other.GetComponent<BaseManager>().State == BaseManager.BaseState.Destroy)
            {
                return;
            }
        }
    }
    // タワー
    private void Tower(Collider other = null)
    {
        if (other.GetComponent<TowerManager>().State == TowerManager.TowerState.Normal)
        {
            other.GetComponent<TowerManager>().TowerHP = AttackPower();
            Destroy(this.gameObject);
        }
        else if (other.GetComponent<TowerManager>().State == TowerManager.TowerState.Destroy)
        {
            return;
        }
    }
    // ミニオン
    private void Minion(Collider other = null)
    {
        if (other.GetComponent<MinionController>().GetMinionState != MinionController.MinionState.Dead)
        {
            other.GetComponent<MinionController>().SetState(MinionController.MinionState.Damage);
            other.GetComponent<StatusController>().TakeDamage(AttackPower());
            Destroy(this.gameObject);
        }
    }
    // ユニット
    private void Unit(Collider other = null)
    {
        if (other.GetComponent<UnitController>().GetPlayerStatus != UnitController.PlayerStatus.Dead)
        {
            other.GetComponent<StatusController>().TakeDamage(AttackPower());
            other.GetComponent<UnitController>().TakeDamage();
            Destroy(this.gameObject);
        }
    }

    private void PcUnit(Collider other = null)
    {
        if (other.GetComponent<MinionController>().GetMinionState != MinionController.MinionState.Dead)
        {
            other.GetComponent<MinionController>().SetState(MinionController.MinionState.Damage);
            other.GetComponent<StatusController>().TakeDamage(AttackPower());
            Destroy(this.gameObject);
        }
    }

    private IEnumerator Destruction()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
