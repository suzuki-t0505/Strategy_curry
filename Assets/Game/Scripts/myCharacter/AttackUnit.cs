using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUnit : MonoBehaviour
{
    private float attack;
    private GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "UnitPC" || other.tag == "minionPC")
        {
            other.GetComponent<MinionController>().SetState(MinionController.MinionState.Damage);
            other.GetComponent<StatusController>().TakeDamage(AttackPower());
            Destroy(this.gameObject);
        }
        if (other.tag == "PcTower")
        {
            if (other.GetComponent<TowerManager>().State == TowerManager.TowerState.Normal)
            {
                other.GetComponent<TowerManager>().TowerHP = AttackPower();
                Destroy(this.gameObject);
            }
            else
            {
                return;
            }
        }
        if (other.tag == "PcBase")
        {
            if (gameManager.PcGoBase == true)
            {
                if (other.GetComponent<BaseManager>().State == BaseManager.BaseState.Normal)
                {
                    other.GetComponent<BaseManager>().BaseHP = AttackPower();
                    Destroy(this.gameObject);
                }
                else
                {
                    return;
                }
            }
        }
    }

    private float AttackPower()
    {
        var randomAttackPower = attack * Random.Range(0.8f, 1.5f);
        return randomAttackPower;
    }

    public float SetAttackPower
    {
        set { attack = value; }
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine("Destruction");
    }
    private IEnumerator Destruction()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
