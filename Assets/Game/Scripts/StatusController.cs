using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusController : MonoBehaviour
{
    [SerializeField]
    private Status myStatus = null;
    private HPMPSlider slider;
    private GameManager gameManager;
    private float elapseTime;

    // ステータス
    private float hp;
    private float maxHp;
    private float attackPower;
    private float mp;
    private float maxMp;

    [SerializeField]
    private int lv;
    private float experience;     // 経験値
    private float maxExperience;  // 最大経験値
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<HPMPSlider>();
        gameManager = FindObjectOfType<GameManager>();
        StateUp();
        UpdateStaeMaxHPMP();
        slider.SetHP(maxHp);
        slider.SetMP(maxMp);
        elapseTime = 0f;
        maxExperience = 100.0f;

    }

    public void Set_Experience(float getExperience)
    {
        if (lv < 5)
        {
            experience += getExperience;
            if (maxExperience <= experience)
            {
                LevlUP();
            }
            else
            {
                return;
            }
        }
    }

    public void LevlUP()
    {
        lv += 1;
        experience -= maxExperience;
        maxExperience *= 1.5f;
        StateUp();
        UpdateStaeMaxHPMP();
        slider.SetHP(maxHp);
        slider.SetMP(maxMp);
        slider.UpdateLv(lv);
    }

    private void StateUp()
    {
        hp = myStatus.Get_Foundation_HP * lv;
        attackPower = myStatus.Get_Foundation_AttackPower * lv;
        mp = myStatus.Get_Foundation_MP * lv;
    }
    private void UpdateStaeMaxHPMP()
    {
        maxHp = hp;
        maxMp = mp;
    }
    public void SetHP()
    {
        hp = maxHp;
        slider.UpdateHP(hp);
    }

    // ダメージ時
    public void TakeDamage(float damage)
    {
        hp -= damage;
        slider.UpdateHP(hp);
    }

    // MP使用時
    public void SetMP(float useMp)
    {
        mp -= useMp;
        slider.UpdateMP(mp);
    }

    /* プロパティ公開*/
    public float Get_HP
    {
        get { return hp; }
    }

    public float Get_MaxHP
    {
        get { return maxHp; }
    }

    public float Get_AttackPower
    {
        get { return attackPower; }
    }

    public float Get_MP
    {
        get { return mp; }
    }

    public float Get_MaxMP
    {
        get { return maxMp; }
    }

    public float Get_Experience
    {
        get { return myStatus.Get_Experience_Point; }
    }
    /*ここまで*/

    void Update()
    {
        if (gameManager.State == GameManager.GameState.Play)
        {
            // MP回復
            elapseTime += Time.deltaTime;
            if (elapseTime > 1.0f)
            {
                if (mp <= maxMp)
                {
                    mp += 1.0f;
                    slider.UpdateMP(mp);
                    elapseTime = 0f;
                }
                else
                {
                    elapseTime = 0f;
                }
            }
        }
    }
}
