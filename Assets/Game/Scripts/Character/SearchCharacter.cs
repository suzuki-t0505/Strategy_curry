using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchCharacter : MonoBehaviour
{
    private MinionController minionController;

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
            if (!minionController.View_EnemyCheck)
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
            // 自分側のキャラクター
            if (minionController.View_EnemyCheck)
            {
                if (other.tag == "UnitPC")
                {
                    Unit(other);
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
        else
        {
            return;
        }
    }

    // ベース
    private void Base(Collider other)
    {
        if (minionController.GetMinionState != MinionController.MinionState.Attack || minionController.GetMinionState != MinionController.MinionState.TowerAttack)
        {
            if (other.GetComponent<BaseManager>().State == BaseManager.BaseState.Normal)
            {
                minionController.SetState(MinionController.MinionState.Chase, other.transform);
            }
            else if (other.GetComponent<BaseManager>().State == BaseManager.BaseState.Destroy)
            {
                return;
            }
        }
        else if (minionController.GetMinionState == MinionController.MinionState.Attack || minionController.GetMinionState == MinionController.MinionState.TowerAttack)
        {
            return;
        }
    }

    // タワー
    private void Tower(Collider other)
    {
        if (minionController.GetMinionState != MinionController.MinionState.Attack || minionController.GetMinionState != MinionController.MinionState.TowerAttack)
        {
            TowerManager towerManager = other.gameObject.GetComponent<TowerManager>();
            if (towerManager.State == TowerManager.TowerState.Normal)
            {
                minionController.SetState(MinionController.MinionState.Chase, other.transform);
            }
            else if (towerManager.State == TowerManager.TowerState.Destroy)
            {
                minionController.SetState(MinionController.MinionState.Walk);
            }
        }
        else if (minionController.GetMinionState == MinionController.MinionState.Attack || minionController.GetMinionState == MinionController.MinionState.TowerAttack || minionController.GetMinionState == MinionController.MinionState.Freeze)
        {
            return;
        }
    }

    // ミニオン
    private void Minion(Collider other)
    {
        // ミニオンが追いかける状態でなければ追いかける設定に変更
        if (minionController.GetMinionState == MinionController.MinionState.Wait || minionController.GetMinionState == MinionController.MinionState.Walk)
        {
            minionController.SetState(MinionController.MinionState.Chase, other.transform);
        }
    }

    // ユニット
    private void Unit(Collider other)
    {
        // ミニオンが追いかける状態でなければ追いかける設定に変更
        if (minionController.GetMinionState == MinionController.MinionState.Wait || minionController.GetMinionState == MinionController.MinionState.Walk)
        {
            minionController.SetState(MinionController.MinionState.Chase, other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // pcキャラクター
        if (!minionController.View_EnemyCheck)
        {
            if (other.tag == "Unit")
            {
                minionController.SetState(MinionController.MinionState.Wait);
            }
            /*
            if (other.tag == "minion")
            {
                Debug.Log("見失う");
                minionController.SetState(MinionController.MinionState.Wait);
            }
            */
        }
    }
}
