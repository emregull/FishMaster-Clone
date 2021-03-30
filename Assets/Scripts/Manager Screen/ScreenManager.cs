﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance;

    private GameObject currentScreen;

    public GameObject endScreen;
    public GameObject gameScreen;
    public GameObject mainScreen;
    public GameObject returnScreen;

    public Button lengthButton;
    public Button strengthButton;
    public Button offlineButton;

    public Text gameScreenMoney;
    public Text lengthCostText;
    public Text lengthValueText;
    public Text strengthCostText;
    public Text strengthValueText;
    public Text offlineCostText;
    public Text offlineValueText;
    public Text endScreenMoneyText;
    public Text returnScreenMoneyText;

    private int gameCount;

    void Awake()
    {
        if (ScreenManager.instance)
        {
            Destroy(base.gameObject);
        }
        else
        {
            ScreenManager.instance = this;
        }

        currentScreen = mainScreen;
    }


    void Start()
    {
        CheckIdles();
        UpdateTexts();
    }

    public void CheckScreen(Screens screen)
    {
        currentScreen.SetActive(false);

        switch (screen)
        {
            case Screens.MAIN:
                currentScreen = mainScreen;
                UpdateTexts();
                CheckIdles();
                break;
            
            case Screens.END:
                currentScreen = endScreen;
                SetEndScreenMoney();
                break;
            
            case Screens.GAME:
                currentScreen = gameScreen;
                gameCount++;
                break;
            
            case Screens.RETURN:
                currentScreen = returnScreen;
                SetReturnScreenMoney();
                break;
        }

        currentScreen.SetActive(true);
    }

    public void SetEndScreenMoney()
    {
        endScreenMoneyText.text = "$" + IdleManager.instance.totalGain;
    }

    public void SetReturnScreenMoney()
    {
        returnScreenMoneyText.text = "$" + IdleManager.instance.totalGain + "gained while waiting!";
    }

    public void CheckIdles()
    {
        gameScreenMoney.text = "$" + IdleManager.instance.wallet;
        lengthCostText.text = "$" + IdleManager.instance.lengthCost;
        lengthValueText.text = -IdleManager.instance.length + "m";
        strengthCostText.text = "$" + IdleManager.instance.strengthCost;
        strengthValueText.text = IdleManager.instance.strength + " fishes.";
        offlineCostText.text = "$" + IdleManager.instance.offlineEarningsCost;
        offlineValueText.text = "$" + IdleManager.instance.offlineEarnings + " /min";
    }

    public void UpdateTexts()
    {
        int lengthCost = IdleManager.instance.lengthCost;
        int strengthCost = IdleManager.instance.strengthCost;
        int offlineCost = IdleManager.instance.offlineEarningsCost;
        int wallet = IdleManager.instance.wallet;

        if (wallet < lengthCost)
            lengthButton.interactable = false;
        else
            lengthButton.interactable = true;

        if (wallet < strengthCost)
            strengthButton.interactable = false;
        else
            strengthButton.interactable = true;

        if (wallet < offlineCost)
            offlineButton.interactable = false;
        else
            offlineButton.interactable = true;
    }
}
