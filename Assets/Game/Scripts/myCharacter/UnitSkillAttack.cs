using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSkillAttack : MonoBehaviour
{
    private UnitController unitController;
    private UnitAnimator unitAnimator;
    private UnitMove unitMove;
    private StatusController statusController;

    // 経過時間
    private float[] elapseTime = new float[2];
    // クールタイム
    [SerializeField]
    private float waitTime;

    private bool skillCollTime1;

    [SerializeField]
    private Image skillaicon;



    void Start()
    {
        unitController = GetComponent<UnitController>();
        unitAnimator = GetComponent<UnitAnimator>();
        unitMove = GetComponent<UnitMove>();
        statusController = GetComponent<StatusController>();
    }

    void Update()
    {
        if (skillCollTime1)
        {
            elapseTime[0] += Time.deltaTime;
            if (waitTime < elapseTime[0])
            {
                skillCollTime1 = false;
                EndOfCoolDown();
            }
        }
    }


    // ユニットの攻撃
    public void Attack1()
    {
        if (!skillCollTime1)
        {
            if(unitController.GetPlayerStatus != UnitController.PlayerStatus.Attack)
            {
                if(statusController.Get_MP >= 10.0f)
                {
                    unitController.GetPlayerStatus = UnitController.PlayerStatus.Attack;
                    unitMove.Stop();
                    unitAnimator.Attak1_Skill();
                    statusController.SetMP(5f);
                    skillaicon.color = Color.black;
                }
            }
            elapseTime[0] = 0f;
            skillCollTime1 = true;
        }
        else
        {
            return;
        }
    }

    private void EndOfCoolDown()
    {
        skillaicon.color = Color.white;
    }
}
