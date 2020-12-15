using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessCharaAnimEvent : MonoBehaviour
{
    private UnitController unitController;
    //[SerializeField]
    //private BoxCollider boxCollider;
    // 斬撃を飛ばす場所
    [SerializeField]
    private GameObject missilePos;
    // 斬撃を飛ばすオブジェクト
    [SerializeField]
    private GameObject shell;
    // 斬撃の速さ
    [SerializeField]
    private float speed;

    void Start()
    {
        unitController = GetComponent<UnitController>();
    }

    void AttackStart()
    {
        //boxCollider.enabled = true;
        GameObject missiles = Instantiate(shell, missilePos.transform.position, Quaternion.identity);
        Rigidbody shellRb = missiles.GetComponent<Rigidbody>();
        AttackUnit unit = missiles.GetComponent<AttackUnit>();
        unit.SetAttackPower = unitController.GetAttackPower;
        shellRb.AddForce(transform.forward * speed);
    }

    void AttackEnd()
    {
        //boxCollider.enabled = false;
    }

    void StateEnd()
    {
        unitController.GetPlayerStatus = UnitController.PlayerStatus.Walk;
    }

    public void EndDamage()
    {
        if (unitController.GetPlayerStatus != UnitController.PlayerStatus.Dead)
        {
            Debug.Log("ダメージ");
            unitController.GetPlayerStatus = UnitController.PlayerStatus.Walk;
        }
    }

}
