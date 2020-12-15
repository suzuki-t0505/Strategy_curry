using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        // ゲームスタートする状態
        Start,
        // ゲームプレイの状態（キャラクターの操作ができる）
        Play,
        // 一時停止
        Pause,
        // プレイヤーの勝ち
        PlayerWin,
        // コンピューターの勝ち
        PCWin,
        // 引き分け
        Draw
    }
    [SerializeField]
    private GameState gameState;
    public GameState State
    {
        get { return gameState; }
        set
        {
            gameState = value;
            if (gameState == GameState.PlayerWin)
            {
                Debug.Log("プレイヤーの勝ち");
            }
            else if (gameState == GameState.PCWin)
            {
                Debug.Log("コンピューターの勝ち");
            }
            else if (gameState == GameState.Pause)
            {

            }
            else if (gameState == GameState.Draw)
            {
                Debug.Log("引き分け");
            }
        }
    }

    // 経過時間 0=StartTimer 1=PlayTimer
    private float[] elapsedTime = new float[2];
    // スタート時間
    [SerializeField]
    private float startTimer;

    // プレイ時間・・・分
    [SerializeField]
    private int playTimerMinute;
    // プレイ時間・・・秒
    [SerializeField]
    private float playTimerSeconds;
    public int PlayTimerMinute
    {
        get { return playTimerMinute; }
    }
    public float PlayTimerSeconds
    {
        get { return playTimerSeconds; }
    }

    // プレイヤー側の破壊されたタワーの数
    private int myTowerCount;
    public int MyTowerCount
    {
        get { return myTowerCount; }
        set 
        { 
            myTowerCount += value;
            var myBase = GameObject.FindGameObjectsWithTag("MyTower");
            if (myBase.Length == 0)
            {
                myGoBase = true;
            }
            // コンピューター側のテキストを更新
            uiManager.UpdatePcTowerCountText();
        }
    }
    // ベースを攻撃できるフラグ
    private bool myGoBase = false;
    public bool MyGoBase
    {
        get { return myGoBase; }
    }
    // コンピューター側の破壊されたタワーの数
    private int pcTowerCount;
    public int PcTowerCount
    {
        get { return pcTowerCount; }
        set 
        {
            pcTowerCount += value;
            var pcBase = GameObject.FindGameObjectsWithTag("PcTower");
            if (pcBase.Length == 0)
            {
                pcGoBase = true;
            }
            // プレイヤー側のテキストを更新
            uiManager.UpdateMyTowerCountText();
        }
    }
    private bool pcGoBase = false;
    public bool PcGoBase
    {
        get { return pcGoBase; }
    }

    private UIManager uiManager;


    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        gameState = GameState.Start;
        elapsedTime[0] += startTimer;
        elapsedTime[1] = playTimerMinute * 60 + playTimerSeconds;
        uiManager.UpdateStartTimerText((int)elapsedTime[0]);
        uiManager.UpdatePlayTimerText(playTimerMinute, playTimerSeconds);
    }

    void Update()
    {
        if (gameState == GameState.Start)
        {
            elapsedTime[0] -= Time.deltaTime;
            // テキスト更新
            uiManager.UpdateStartTimerText((int)elapsedTime[0]);
            if (elapsedTime[0] <= 0)
            {
                gameState = GameState.Play;
            }

        }
        else if (gameState == GameState.Play)
        {
            if (elapsedTime[1] <= 0f)
            {
                if (myTowerCount < pcTowerCount)
                {
                    State = GameState.PlayerWin;
                    uiManager.Result(0);
                }
                else if (pcTowerCount < myTowerCount)
                {
                    State = GameState.PCWin;
                    uiManager.Result(1);
                }
                else if (myTowerCount == pcTowerCount)
                {
                    State = GameState.Draw;
                    uiManager.Result(2);
                }
            }
            elapsedTime[1] = playTimerMinute * 60 + playTimerSeconds;
            elapsedTime[1] -= Time.deltaTime;
            playTimerMinute = (int)elapsedTime[1] / 60;
            playTimerSeconds = elapsedTime[1] - playTimerMinute * 60;
            uiManager.UpdatePlayTimerText(playTimerMinute, playTimerSeconds);
        }
        else
        {
            return;
        }
    }
}
