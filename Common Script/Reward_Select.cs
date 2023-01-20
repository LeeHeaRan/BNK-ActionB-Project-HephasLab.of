using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reward_Select : MonoBehaviour
{
    private string DateKey;
    private string Reward = "";
    private string Count;
    public MainRewardPageControl RewardPageControl;

    public void SetDateKey(string date) {

        DateKey = date;
    }
  /*  public void SetDateKey(string date)
    {
        DateKey = date;
    }*/

    public void RewardSetting(MainRewardPageControl _RewardPageControl, string value)
    {
        Count = value;
        RewardPageControl =  _RewardPageControl;
        gameObject.GetComponentInChildren<Text>().text = value;
    }
    public void RewardClick()
    {
        RewardPageControl.RewardClick(Count);
    }
}
