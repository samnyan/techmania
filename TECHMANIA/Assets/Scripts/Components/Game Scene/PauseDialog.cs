﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseDialog : MonoBehaviour
{
    private UnityAction closeCallback;

    public void Show(UnityAction closeCallback)
    {
        this.closeCallback = closeCallback;
        GetComponent<Dialog>().FadeIn();
    }

    private void OnDisable()
    {
        closeCallback?.Invoke();
    }

    public void OnRestartButtonClick()
    {
        Curtain.DrawCurtainThenGoToScene("Game");
    }

    public void OnQuitButtonClick()
    {
        closeCallback = null;
        WelcomeMat.skipToTrackSelect = true;
        Curtain.DrawCurtainThenGoToScene("Main Menu");
    }
}
