using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="MyScriptable/Status")]
public class Status : ScriptableObject
{

    // 基礎ステータス
    [SerializeField, Header("基礎ステータス")]
    private float foundation_Hp;
    [SerializeField]
    private float foundation_AttackPower;
    [SerializeField]
    private float foundation_Mp;


    // 経験値
    [SerializeField, Header("経験値")]
    private float experience_point;


    public float Get_Experience_Point
    {
        get { return experience_point; }
    }

    public float Get_Foundation_HP
    {
        get { return foundation_Hp; }
    }

    public float Get_Foundation_AttackPower
    {
        get { return foundation_AttackPower; }
    }

    public float Get_Foundation_MP
    {
        get { return foundation_Mp; }
    }

}

