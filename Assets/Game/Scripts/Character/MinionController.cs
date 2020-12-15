using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class MinionController : MonoBehaviour
{
    public enum MinionState
    {
        // 移動
        Walk,
        // 待ち時間
        Wait,
        // 追いかける
        Chase,
        // プレイヤーを攻撃
        Attack,
        // タワーを攻撃
        TowerAttack,
        // 攻撃した後の硬直
        Freeze,
        // ダメージを受けた
        Damage,
        // hpが0になった状態
        Dead
    }
    // ミニオンの状態
    private MinionState minionstate;
    public MinionState GetMinionState
    {
        get { return minionstate; }
        set { minionstate = value; }
    }

    // ユニットの位置
    private Transform unitTransform;
    // 目的地
    [SerializeField]
    private Transform target;

    private NavMeshAgent meshAgent;
    private StatusController statusController;
    private MinionAnimator minionAnimator;

    // 待ち時間
    private float waitTime;
    private float minWaitTime = 3f; // 待ち時間の最小
    private float maxWaitTime = 5f; // 待ち時間の最長
    // 経過時間
    private float elapseTime;
    // 攻撃した後のフリーズ時間
    private float freezeTime;
    private float minFreezeTime = 1f; // フリーズ時間の最小
    private float maxFreezeTime = 5f; // フリーズ時間の最長

    [SerializeField, Header("true=My,false=Pc")]
    private bool view_enemyCheck;
    [SerializeField, Header("true=Unit,false=Minion")]
    private bool characterCheck;
    private bool unitExperienceCheck;
    [SerializeField]
    private Transform respawnPos;

    private GameManager gameManager;

    public bool View_EnemyCheck
    {
        get { return view_enemyCheck; }
    }
    public float GetAttackPower
    {
        get { return statusController.Get_AttackPower; }
    }


    // ミニオンの状態変更メソッド
    public void SetState(MinionState tempState, Transform targetObj = null)
    {
        GetMinionState = tempState;
        if (tempState == MinionState.Walk)
        {
            elapseTime = 0f;
            meshAgent.SetDestination(new Vector3(target.position.x, target.position.y, target.position.z));
            meshAgent.isStopped = false;
        }
        else if (tempState == MinionState.Chase)
        {
            meshAgent.ResetPath();
            // 追いかける対象をセット
            unitTransform = targetObj;
            meshAgent.SetDestination(unitTransform.position);
            meshAgent.isStopped = false;
        }
        else if (tempState == MinionState.Wait)
        {
            Stop();
        }
        else if (tempState == MinionState.Attack)
        {
            SetAttack();
        }
        else if (tempState == MinionState.TowerAttack)
        {
            SetAttack();
        }
        else if (tempState == MinionState.Freeze)
        {
            Stop();

        }
        else if (tempState == MinionState.Damage)
        {
            minionAnimator.AnimeRest("Attack");
            minionAnimator.AnimeTrigger("Damage");
            meshAgent.isStopped = true;
            if (statusController.Get_HP <= 0)
            {
                SetState(MinionState.Dead);
            }
        }
        else if (tempState == MinionState.Dead)
        {
            GetMinionState = MinionState.Dead;
            PassExperience();
            meshAgent.ResetPath();
            minionAnimator.AnimeTrigger("Deth");
            // ミニオンの場合
            if (!characterCheck)
            {
                StartCoroutine("Deth");
            }
        }
    }

    private void SetAttack()
    {
        meshAgent.velocity = Vector3.zero;
        minionAnimator.MoveAnime("Speed", 0.0f);
        minionAnimator.AnimeTrigger("Attack");
    }
    private void Stop()
    {
        elapseTime = 0f;
        minionAnimator.MoveAnime("Speed", 0.0f);
    }

    private void PassExperience()
    {
        if (unitExperienceCheck)
        {
            GameObject unit = GameObject.FindGameObjectWithTag("Unit");
            StatusController unitStatus = unit.GetComponent<StatusController>();
            unitStatus.Set_Experience(statusController.Get_Experience);
        }
    }

    private IEnumerator Deth()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "UnitAttack")
        {
            unitExperienceCheck = true;
        }
    }

    private void GetComponents()
    {
        meshAgent = GetComponent<NavMeshAgent>();
        statusController = GetComponent<StatusController>();
        gameManager = FindObjectOfType<GameManager>();
        minionAnimator = GetComponent<MinionAnimator>();
    }

    void Start()
    {
        GetComponents();
        elapseTime = 0f;
        waitTime = Random.Range(minWaitTime, maxWaitTime);
        freezeTime = Random.Range(minFreezeTime, maxFreezeTime);
        SetState(MinionState.Walk);
        unitExperienceCheck = false;
    }

    void Update()
    {
        if (gameManager.State == GameManager.GameState.Play)
        {
            Update_StateTransition();
        }
        else
        {
            meshAgent.ResetPath();
            return;
        }
    }

    private void Update_StateTransition()
    {
        if (GetMinionState == MinionState.Walk || GetMinionState == MinionState.Chase)
        {
            Update_WalkAndChase();
        }
        else if (GetMinionState == MinionState.Wait) // 到着したら一定時間待つ
        {
            Update_Wait();
        }
        else if (GetMinionState == MinionState.Freeze) // 攻撃した後のフリーズ時間
        {
            Update_Freeze();
        }
        else if (GetMinionState == MinionState.Attack)
        {
            Update_Attacks(new Vector3(unitTransform.position.x, transform.position.y, transform.position.z));
        }
        else if (GetMinionState == MinionState.TowerAttack)
        {
            Update_Attacks(unitTransform);
        }
        else if (GetMinionState == MinionState.Dead && characterCheck)
        {
            Update_Dead();
        }

    }

    private void Update_WalkAndChase()
    {
        minionAnimator.MoveAnime("Speed", meshAgent.desiredVelocity.magnitude);

        if (GetMinionState == MinionState.Walk)
        {
            // 目的地に到着したかどうかの判定
            if (meshAgent.remainingDistance < 0.3f)
            {
                SetState(MinionState.Wait);
                minionAnimator.MoveAnime("Speed", 0.0f);
            }
        }
    }
    private void Update_Wait()
    {
        elapseTime += Time.deltaTime;
        // 待ち時間を超えていたら次の目的地を設定
        if (elapseTime > waitTime)
        {
            SetState(MinionState.Walk);
        }
    }
    private void Update_Freeze()
    {
        elapseTime += Time.deltaTime;
        if (elapseTime > freezeTime)
        {
            SetState(MinionState.Walk);
        }
    }
    private void Update_Attacks(Transform target)
    {
        transform.LookAt(target);
    }
    private void Update_Attacks(Vector3 target)
    {
        transform.LookAt(target);
    }
    private void Update_Dead()
    {
        StartCoroutine("Revival");
    }

    private IEnumerator Revival()
    {
        yield return new WaitForSeconds(5f);
        statusController.SetHP();
        // ここにベースのポジションに戻す処理
        this.gameObject.transform.position = respawnPos.position;
        yield return new WaitForSeconds(1f);
        minionAnimator.AnimeTrigger("Revival");
        SetState(MinionState.Wait);
    }

}
