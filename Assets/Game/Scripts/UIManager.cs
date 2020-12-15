using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField]
    private Sprite[] startTimerSprite = new Sprite[6];
    [SerializeField]
    private Image startImage;
    [SerializeField]
    private GameObject startPanel;
    // プレイタイマーカウントテキスト
    [SerializeField,Header("プレイタイマー")]
    private Text playTimerText;
    // 破壊したタワーの数
    [SerializeField, Header("タワーカウント")]
    private Text[] towerCountText = new Text[2];

    private bool gameStopCheck;
    [SerializeField]
    private GameObject pusePanel;

    [SerializeField]
    private GameObject resultPanle;
    [SerializeField]
    private GameObject[] winText = new GameObject[3];



    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameStopCheck = false;
    }

    // スタートタイマーのスプライトを更新
    public void UpdateStartTimerText(int countTimer)
    {
        //startTimerText.text = a.ToString();
        if(countTimer<=5 && countTimer > 4)
        {
            startImage.sprite = startTimerSprite[5];
        }
        else if(countTimer <=4 && countTimer > 3)
        {
            startImage.sprite = startTimerSprite[4];
        }
        else if (countTimer <= 3 && countTimer > 2)
        {
            startImage.sprite = startTimerSprite[3];
        }
        else if (countTimer <= 2 && countTimer > 1)
        {
            startImage.sprite = startTimerSprite[2];
        }
        else if (countTimer <= 1 && countTimer > 0)
        {
            startImage.sprite = startTimerSprite[1];
        }
        else if (countTimer <= 0)
        {
            startImage.sprite = startTimerSprite[0];
            Destroy(startPanel, 0.5f);
        }
    }

    // プレイタイマーのテキストを更新
    public void UpdatePlayTimerText(int minute,float seconds)
    {
        playTimerText.text = minute.ToString("00") + "：" + ((int)seconds).ToString("00");
    }

    // プレイヤー側のタワーカウントのテキストを更新
    public void UpdateMyTowerCountText()
    {
        towerCountText[0].text = gameManager.PcTowerCount.ToString();
    }

    // コンピューター側のタワーカウントのテキストを更新
    public void UpdatePcTowerCountText()
    {
        towerCountText[1].text = gameManager.MyTowerCount.ToString();
    }

    public void GameStop()
    {
        if (!gameStopCheck)
        {
            Time.timeScale = 0;
            pusePanel.SetActive(true);
            gameManager.State = GameManager.GameState.Pause;
            gameStopCheck = true;
        }
        else if (gameStopCheck){
            Time.timeScale = 1;
            pusePanel.SetActive(false);
            gameManager.State = GameManager.GameState.Play;
            gameStopCheck = false;
        }
    }

    public void Result(int winNumber)
    {
        resultPanle.SetActive(true);
        winText[winNumber].SetActive(true);
    }

}
