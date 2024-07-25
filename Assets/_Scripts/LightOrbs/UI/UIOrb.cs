using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Class for the OrbUI Objects
/// </summary>
public class UIOrb
{
    private Transform _currPos;
    private Image _img;

    public UIOrb(Image i, Transform t) {
        _currPos = t;
        _img = i;
        SetUIOrbPos(t);
    }

    public void SetUIOrbPos(Transform t) { _img.transform.localPosition = t.localPosition; }

    public void FadeOrb(int alpha, float fadeTime) { 
        if (alpha > 0) {
            Vector2 currPos = _img.rectTransform.localPosition;
            currPos.y -= 0.5f;
            _img.rectTransform.localPosition = currPos;

            currPos.y += 0.5f;
            _img.rectTransform.DOAnchorPos(currPos, fadeTime, false).SetEase(Ease.OutQuint);
        }
        else {
            _img.rectTransform.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.OutQuint);
        }
        _img.DOFade(alpha, fadeTime);
    }

    public void MoveOrb(Vector2 pos, float time) {
        _img.rectTransform.DOAnchorPos(pos, time, false).SetEase(Ease.OutQuint);
    }

    public void SetOrbColor(EColor color) { _img.color = color.GetColor(); }
}
