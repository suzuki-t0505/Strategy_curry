using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ProcessCharaAnimEvent))]
public class UnitController : MonoBehaviour
{
    // ユニットの状態
    public enum PlayerStatus
    {
        // 移動状態
        Walk,
        // ダメージを受けている状態
        Damage,
        // 攻撃している状態
        Attack,
        // hpが0になった状態
        Dead
    }
    private PlayerStatus unitStatus;
    public PlayerStatus GetPlayerStatus
    {
        get { return unitStatus; }
        set { unitStatus = value; }
    }


    private GameManager gameManager;
    private UnitAnimator unitAnimator;
    private UnitMove unitMove;
    private StatusController statusController;

    // 復活時間
    [SerializeField]
    private float revivalTime;
    // 経過時間
    //private float[] elapseTime = new float[1];
    // 復活場所
    [SerializeField]
    private Transform respawnPos;

    private void GetComponent()
    {
        gameManager = FindObjectOfType<GameManager>();
        unitAnimator = GetComponent<UnitAnimator>();
        unitMove = GetComponent<UnitMove>();
        statusController = GetComponent<StatusController>();
    }
    void Start()
    {
        GetComponent();
        //elapseTime[0] = 0f;
    }
    public float GetAttackPower
    {
        get { return statusController.Get_AttackPower; }
    }

    void Update()
    {
        if (gameManager.State == GameManager.GameState.Play)
        {
            Update_Play();
        }
        else if (gameManager.State == GameManager.GameState.Start)
        {
            unitMove.Stop();
        }
        else
        {
            unitMove.Stop();
        }
    }
    private void Update_Play()
    {
        // 歩く
        if (GetPlayerStatus == PlayerStatus.Walk)
        {
            unitMove.Move();
        }
        // HP==0の状態
        else if (GetPlayerStatus == PlayerStatus.Dead)
        {
            Update_Dead();
        }
    }

    private void Update_Dead()
    {
        StartCoroutine("Revival");

    }
    private IEnumerator Revival()
    {
        yield return new WaitForSeconds(revivalTime);
        statusController.SetHP();
        this.gameObject.transform.position = respawnPos.position;
        //animator.SetTrigger("Revival");
        unitAnimator.RevivalAnima();
        GetPlayerStatus = PlayerStatus.Walk;
    }

    // ユニットがダメージを受けたとき
    public void TakeDamage()
    {
        Debug.Log("ダメージ");
        GetPlayerStatus = PlayerStatus.Damage;
        unitMove.Stop();
        unitAnimator.DamageAnima();
        //Hpが0のとき
        Deth();
    }
    private void Deth()
    {
        if (statusController.Get_HP <= 0)
        {
            GetPlayerStatus = PlayerStatus.Dead;
            Debug.Log(GetPlayerStatus);
            unitAnimator.DethAnima();
            statusController.SetHP();
            //elapseTime[0] = 0f;
        }
    }
}

