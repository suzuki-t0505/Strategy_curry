using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPosition : MonoBehaviour
{
    private MinionController minionController;
    [SerializeField, Header("true==my,false==pc")]
    private bool check;

    private GameManager gameManager;

    void Start()
    {
        minionController = GetComponentInParent<MinionController>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (gameManager.State == GameManager.GameState.Play)
        {
            // pcキャラクター
            if (!check)
            {
                if (other.tag == "Unit")
                {
                    if (other.GetComponent<UnitController>().GetPlayerStatus != UnitController.PlayerStatus.Dead)
                    {
                        Attack();
                    }
                    else if (other.GetComponent<UnitController>().GetPlayerStatus == UnitController.PlayerStatus.Dead)
                    {
                        return;
                    }
                }
                if (other.tag == "minion")
                {
                    Attack();
                }
                if (other.tag == "MyTower")
                {
                    if (other.GetComponent<TowerManager>().State == TowerManager.TowerState.Normal)
                    {
                        FieldAttack();
                    }
                    else if (other.GetComponent<TowerManager>().State == TowerManager.TowerState.Destroy)
                    {
                        return;
                    }
                }
                if (other.tag == "MyBase")
                {
                    if (other.GetComponent<BaseManager>().State == BaseManager.BaseState.Normal)
                    {
                        FieldAttack();
                    }
                }
            }
            // myキャラクター
            if (check)
            {
                if (other.tag == "UnitPC")
                {
                    if (other.GetComponent<MinionController>().GetMinionState != MinionController.MinionState.Dead)
                    {
                        Attack();
                    }
                    else if (other.GetComponent<MinionController>().GetMinionState == MinionController.MinionState.Dead)
                    {
                        return;
                    }
                }
                if (other.tag == "minionPC")
                {
                    Attack();
                }
                if (other.tag == "PcTower")
                {
                    if (other.GetComponent<TowerManager>().State == TowerManager.TowerState.Normal)
                    {
                        FieldAttack();
                    }
                    else if (other.GetComponent<TowerManager>().State == TowerManager.TowerState.Destroy)
                    {
                        return;
                    }
                }
                if (other.tag == "PcBase")
                {
                    if (other.GetComponent<BaseManager>().State == BaseManager.BaseState.Normal)
                    {
                        FieldAttack();
                    }
                }
            }
        }
        else
        {
            return;
        }
    }

    private void FieldAttack()
    {
        if (minionController.GetMinionState == MinionController.MinionState.Chase)
        {
            minionController.SetState(MinionController.MinionState.TowerAttack);
        }
        else if (minionController.GetMinionState == MinionController.MinionState.Freeze)
        {
            return;
        }
    }

    private void Attack()
    {
        if (minionController.GetMinionState == MinionController.MinionState.Chase)
        {
            minionController.SetState(MinionController.MinionState.Attack);
        }
        else if (minionController.GetMinionState != MinionController.MinionState.Chase)
        {
            return;
        }
    }

}
