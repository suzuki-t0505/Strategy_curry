using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessMinionAnimeEvent : MonoBehaviour
{
    private MinionController minionController;
    //[SerializeField]
    //private SphereCollider sphereCollider;
    // 斬撃を飛ばす場所
    [SerializeField]
    private GameObject missilePos;
    // 斬撃を飛ばすオブジェクト
    [SerializeField]
    private GameObject shell;
    // 斬撃の速さ
    [SerializeField]
    private float speed;




    // Start is called before the first frame update
    void Start()
    {
        minionController = GetComponent<MinionController>();
    }

    void AttackStart()
    {
        //sphereCollider.enabled = true;
        GameObject missiles = Instantiate(shell, missilePos.transform.position, Quaternion.identity);
        Rigidbody shellRb = missiles.GetComponent<Rigidbody>();
        AttackMinion minion = missiles.GetComponent<AttackMinion>();
        minion.SetAttackPower = minionController.GetAttackPower;
        if (minionController.View_EnemyCheck)
        {
            minion.setCheck = true;
        }
        else if (!minionController.View_EnemyCheck)
        {
            minion.setCheck = false;
        }
        shellRb.AddForce(transform.forward * speed);
    }

    void AttackEnd()
    {
        //sphereCollider.enabled = false;
    }

    void StateEnd()
    {
        if (minionController.GetMinionState != MinionController.MinionState.Freeze && minionController.GetMinionState != MinionController.MinionState.Dead)
        {
            minionController.SetState(MinionController.MinionState.Freeze);
        }
        else if (minionController.GetMinionState == MinionController.MinionState.Dead)
        {
            return;
        }
    }

    void EndDamage()
    {
        if (minionController.GetMinionState != MinionController.MinionState.Dead)
        {
            minionController.SetState(MinionController.MinionState.Walk);
        }
        else if (minionController.GetMinionState == MinionController.MinionState.Dead)
        {
            return;
        }
    }
}
