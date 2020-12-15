using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{

    public enum TowerState
    {
        Normal,
        // 破壊された状態
        Destroy
    }
    [SerializeField]
    private TowerState towerState;
    // タワーの状態を公開、設定
    public TowerState State
    {
        get { return towerState; }
        set { towerState = value; }
    }

    // タワーのhp
    private float hp;
    // タワーのmaxhp
    [SerializeField]
    private float maxhp;

    // タワーのhpを公開設定
    public float TowerHP
    {
        get { return hp; }
        set { hp -= value;
            HpUpdate();
        }
    }
    // タワーのhpを元にもどす
    public void RestTowerHP()
    {
        hp = maxhp;
        HpUpdate();
    }
    // タワーのmaxhpを公開
    public float TowerMaxHP
    {
        get { return maxhp; }
    }

    [SerializeField]
    private Slider slider;

    [SerializeField,Header("ture==My,false==Pc")]
    private bool myORpc;
    public bool Check
    {
        get { return myORpc; }
    }

    private GameManager gameManager;

    [SerializeField]
    private Material destroyMaterial;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        towerState = TowerState.Normal;
        RestTowerHP();
        slider.maxValue = maxhp;
        slider.value = hp;
    }

    // hpのスライダーを更新
    void HpUpdate()
    {
        slider.value = hp;
        if (hp <= 0)
        {
            towerState = TowerState.Destroy;
            this.GetComponent<Renderer>().material = destroyMaterial;
            if (myORpc)
            {
                gameManager.MyTowerCount = 1;
            }
            else if (!myORpc)
            {
                gameManager.PcTowerCount = 1;
            }
        }
    }

    public void AttackTower(Transform targetpositon)
    {

    }


    void Update()
    {
        
    }
}
