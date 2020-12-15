using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseManager : MonoBehaviour
{
    public enum BaseState
    {
        Normal,
        // 破壊された状態
        Destroy
    }
    [SerializeField]
    private BaseState baseState;
    public BaseState State
    {
        get { return baseState; }
        set { baseState = value; }
    }

    // ベースーのhp
    private float hp;
    // ベースのmaxhp
    [SerializeField]
    private float maxhp;

    public float BaseHP
    {
        get { return hp; }
        set
        {
            hp -= value;
            HpUpdate();
        }
    }
    public float BaseMaxHP
    {
        get { return maxhp; }
    }

    [SerializeField]
    private Slider slider;

    [SerializeField, Header("ture==My,false==Pc")]
    private bool myORpc;
    public bool Check
    {
        get { return myORpc; }
    }

    private GameManager gameManager;

    void Start()
    {
        baseState = BaseState.Normal;
        hp = maxhp;
        slider.maxValue = maxhp;
        slider.value = hp;
        gameManager = FindObjectOfType<GameManager>();
    }

    void HpUpdate()
    {
        slider.value = hp;
        if (hp <= 0)
        {
            baseState = BaseState.Destroy;
            // コンピューターのベースが破壊されたら
            if (!myORpc)
            {
                gameManager.State = GameManager.GameState.PlayerWin;
            }
            // プレイヤーのベースが破壊されたら
            else if (myORpc)
            {
                gameManager.State = GameManager.GameState.PCWin;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
