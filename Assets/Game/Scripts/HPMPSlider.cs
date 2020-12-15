using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPMPSlider : MonoBehaviour
{

    [SerializeField]
    private Slider hpSlider = null;
    [SerializeField]
    private Slider mpSlider = null;
    [SerializeField]
    private Image lvImage = null;
    [SerializeField]
    private Sprite[] lvText = null;

    // スタート時のhpとレベルアップ時のhp更新
    public void SetHP(float maxhp)
    {
        hpSlider.maxValue = maxhp;
        hpSlider.value = maxhp;
    }

    // hp増減時のhp更新
    public void UpdateHP(float hp)
    {
        hpSlider.value = hp;
    }

    public void SetMP(float maxmp)
    {
        if (mpSlider != null)
        {
            mpSlider.maxValue = maxmp;
            mpSlider.value = maxmp;
        }
        else if (mpSlider == null)
        {
            return;
        }
    }

    public void UpdateMP(float mp)
    {
        if (mpSlider != null)
        {
            Debug.Log("mp回復");
            mpSlider.value = mp;
        }
        if (mpSlider == null)
        {
            return;
        }
    }

    public void UpdateLv(int lv)
    {
        if (lv == 2)
        {
            lvImage.sprite = lvText[0];
        }
        else if (lv == 3)
        {
            lvImage.sprite = lvText[1];
        }
        else if (lv == 4)
        {
            lvImage.sprite = lvText[2];
        }
        else if (lv == 5)
        {
            lvImage.sprite = lvText[3];
        }
    }
    
}
