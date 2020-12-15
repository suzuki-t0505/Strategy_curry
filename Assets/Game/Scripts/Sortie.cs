using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sortie : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField,Header("true=MyMinion,false=PcMinion")]
    private bool check;

    [SerializeField]
    private GameObject minion;
    [SerializeField]
    private Transform[] apperpos = new Transform[3];

    private GameObject[] minionCount;
    private float timer = 0.0f;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine("StartApper");

    }

    // ゲームスタート時のミニオン生成
    private IEnumerator StartApper()
    {
        yield return new WaitForSeconds(5f);
        ApperMinion();
        yield return new WaitForSeconds(1f);
        ApperMinion();
        yield return new WaitForSeconds(1f);
        ApperMinion();
        if (check)
        {
            Check("minion");
        }
        else if (!check)
        {
            Check("minionPC");
        }
    }

    // 2回目以降のミニオン生成
    private IEnumerator Apper()
    {
        ApperMinion();
        yield return new WaitForSeconds(1f);
        ApperMinion();
        yield return new WaitForSeconds(1f);
        ApperMinion();
    }

    // ミニオン生成
    private void ApperMinion()
    {
        Instantiate(minion, apperpos[0].transform.position, Quaternion.identity);
        Instantiate(minion, apperpos[1].transform.position, Quaternion.identity);
        Instantiate(minion, apperpos[2].transform.position, Quaternion.identity);
    }

    // フィールドにミニオンが何体いるか確認
    private void Check(string tagname)
    {
        minionCount = GameObject.FindGameObjectsWithTag(tagname);
        if (minionCount.Length <= 7)
        {
            StartCoroutine("Apper");
        }
    }

    void Update()
    {
        if (gameManager.State == GameManager.GameState.Play)
        {
            timer += Time.deltaTime;
            if (timer > 5f)
            {
                if (check)
                {
                    Check("minion");
                    timer = 0.0f;
                }
                else if (!check)
                {
                    Check("minionPC");
                    timer = 0.0f;
                }
            }
        }
        else
        {
            return;
        }
    }
}
