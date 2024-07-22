using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OrbUIAnim : MonoBehaviour
{
    [SerializeField] private float _fadeTime = 1.5f;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private List<Image> _orbImages;

    void Start() {
        OrbHandler.OnOrbsSwapped += StartAnim;
        _canvasGroup.alpha = 0f;
    }
    private void StartAnim(List<EColor> colors) {
        FadeInBckg();
        SpawnOrbs(colors);
    }

    private void FadeInBckg() {
        _canvasGroup.alpha = 0f;
        _rectTransform.transform.localPosition = new Vector3(0f, -0.5f, 0f);
        _rectTransform.DOAnchorPos(new Vector2(0f, 0f), _fadeTime, false).SetEase(Ease.OutQuint);
        _canvasGroup.DOFade(1, _fadeTime);
    }

    private void FadeInOrb(Image img) {
        img.transform.localPosition = new Vector3(0f, -0.5f, 0f);
        img.rectTransform.DOAnchorPos(new Vector2(0f, 0f), _fadeTime, false).SetEase(Ease.OutQuint);
        img.DOFade(1, _fadeTime);
    }

    private void SpawnOrbs(List<EColor> colors) {
        for (int i = 0; i < colors.Count; i++) {
            FadeInOrb(_orbImages[i]);
        }
    }
}
