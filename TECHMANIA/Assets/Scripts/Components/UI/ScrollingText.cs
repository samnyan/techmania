﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingText : MonoBehaviour
{
    public enum Direction
    {
        Horizontal,
        Vertical
    }
    public Direction direction;

    private RectTransform rect;
    private RectTransform innerRect;
    private float maskSize;
    private float contentSize;

    void OnEnable()
    {
        rect = GetComponent<RectTransform>();
        innerRect = rect.GetChild(0).GetComponent<RectTransform>();
        TextMeshProUGUI[] allTexts = 
            GetComponentsInChildren<TextMeshProUGUI>();

        contentSize = 0f;
        switch (direction)
        {
            case Direction.Horizontal:
                maskSize = rect.sizeDelta.x;
                foreach (TextMeshProUGUI t in allTexts)
                {
                    contentSize += t.preferredWidth;
                }
                break;
            case Direction.Vertical:
                maskSize = rect.sizeDelta.y;
                foreach (TextMeshProUGUI t in allTexts)
                {
                    contentSize += t.preferredHeight;
                }
                break;
        }

        if (contentSize > maskSize)
        {
            StartCoroutine(Scroll());
        }
        else
        {
            ScrollTo(direction == Direction.Horizontal ? 0.5f : 0f);
        }
    }

    private void ScrollTo(float t)
    {
        switch (direction)
        {
            case Direction.Horizontal:
                innerRect.anchorMin = new Vector2(t, 0f);
                innerRect.anchorMax = new Vector2(t, 1f);
                innerRect.pivot = new Vector2(t, 0.5f);
                break;
            case Direction.Vertical:
                innerRect.anchorMin = new Vector2(0f, 1f - t);
                innerRect.anchorMax = new Vector2(1f, 1f - t);
                innerRect.pivot = new Vector2(0.5f, 1f - t);
                break;
        }
    }

    private IEnumerator Scroll()
    {
        const float kScrollTime = 2f;
        const float kWaitTime = 2f;

        while (true)
        {
            ScrollTo(0f);
            yield return new WaitForSeconds(kWaitTime);
            for (float time = 0;
                time < kScrollTime; time += Time.deltaTime)
            {
                float progress = time / kScrollTime;
                ScrollTo(progress);
                yield return null;
            }
            ScrollTo(1f);
            yield return new WaitForSeconds(kWaitTime);
            for (float time = 0;
                time < kScrollTime; time += Time.deltaTime)
            {
                float progress = 1f - time / kScrollTime;
                ScrollTo(progress);
                yield return null;
            }
        }
    }
}
