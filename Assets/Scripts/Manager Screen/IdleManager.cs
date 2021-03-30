using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class IdleManager : MonoBehaviour
{
    [HideInInspector]
    public int length;

    [HideInInspector]
    public int strength;

    [HideInInspector]
    public int offlineEarnings;

    [HideInInspector]
    public int lengthCost;

    [HideInInspector]
    public int strengthCost;

    [HideInInspector]
    public int offlineEarningsCost;

    [HideInInspector]
    public int wallet;

    [HideInInspector]
    public int totalGain;

    private int[] costs = new int[]
    {
        100,
        200,
        400,
        600,
        800,
        1000,
        1300,
        1500,
        1900,
        2200,
        2800,
        3200,
        4200,
        5400,
        6700,
        7500,
        7501,
        7502,
        7503,
        7504,
        7505,
        7506,
        7507,
        7508

    };

    public static IdleManager instance;
    void Awake()
    {
        if (IdleManager.instance)
        {
            UnityEngine.Object.Destroy(gameObject);
        }
        else
        {
            IdleManager.instance = this;
        }

        length = -PlayerPrefs.GetInt("Length", 30);
        strength = PlayerPrefs.GetInt("Strength", 3);
        offlineEarnings = PlayerPrefs.GetInt("Offline", 3);
        lengthCost = costs[-length / 100 - 3];
        strengthCost = costs[strength-3];
        offlineEarningsCost = costs[offlineEarnings-3];
        wallet = PlayerPrefs.GetInt("Wallet", 0);
    }

    private void OnApplicationPause(bool paused)
    {
        if (paused)
        {
            DateTime now = DateTime.Now;
            PlayerPrefs.SetString("Date", now.ToString());
            MonoBehaviour.print(now.ToString());
        }
        else
        {
            string @string = PlayerPrefs.GetString("Date", string.Empty);
            if (@string != string.Empty)
            {
                DateTime date = DateTime.Parse(@string);
                totalGain = (int)((DateTime.Now - date).TotalMinutes * offlineEarnings + 1.0);
                ScreenManager.instance.CheckScreen(Screens.RETURN);
            }
        }
    }

    private void OnApplicationQuit()
    {
        OnApplicationPause(true);
    }

    public void BuyLength()
    {
        length -= 10;
        wallet -= lengthCost;
        lengthCost = costs[strength - 3];
        PlayerPrefs.SetInt("Length", -length);
        PlayerPrefs.SetInt("Wallet", wallet);
        ScreenManager.instance.CheckScreen(Screens.MAIN);
    }
    public void BuyStrength()
    {
        strength++;
        wallet -= strengthCost;
        strengthCost = costs[strength - 3];
        PlayerPrefs.SetInt("Strength", strength);
        PlayerPrefs.SetInt("Wallet", wallet);
        ScreenManager.instance.CheckScreen(Screens.MAIN);
    }
    public void BuyOfflineEarnings()
    {
        offlineEarnings++;
        wallet -= offlineEarningsCost;
        offlineEarningsCost = costs[offlineEarnings - 3];
        PlayerPrefs.SetInt("Offline", offlineEarnings);
        PlayerPrefs.SetInt("Wallet", wallet);
        ScreenManager.instance.CheckScreen(Screens.MAIN);
    }

    public void CollectMoney()
    {
        wallet += totalGain;
        PlayerPrefs.SetInt("Wallet", wallet);
        ScreenManager.instance.CheckScreen(Screens.MAIN);
    }

    public void CollectMoney2x()
    {
        wallet += totalGain*2;
        PlayerPrefs.SetInt("Wallet", wallet);
        ScreenManager.instance.CheckScreen(Screens.MAIN);
    }
}
