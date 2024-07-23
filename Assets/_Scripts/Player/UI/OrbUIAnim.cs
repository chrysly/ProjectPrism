using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OrbUIAnim : MonoBehaviour
{
    [SerializeField] private float _fadeTime = 1.5f;
    [SerializeField] private CanvasGroup _mainHolder;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private List<Image> _orbImages;

    [SerializeField] private float _animationDuration = 1.0f; // Duration of the Slerp animation
    [SerializeField] private GameObject _bigDaddyCanvas;

    void Start() {
        OrbHandler.OnOrbsSwapped += StartAnim;
        OrbHandler.OnOrbThrown += OnOrbThrown;
        Player.OnSpawn += OnSpawn;
        _canvasGroup.alpha = 0f;
    }

    /// <summary>
    /// When the player spawns in play this
    /// </summary>
    private void OnSpawn(EColor c) {
        _mainHolder.alpha = 0f;
        _mainHolder.transform.localPosition = new Vector3(0f, -0.5f, 0f);
        _mainHolder.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, 0f), _fadeTime, false).SetEase(Ease.OutQuint);
        _mainHolder.DOFade(1, _fadeTime);

        FadeInOrb(_orbImages[0], c);
    }

    private void OnOrbThrown(List<EColor> cls) {
        SetInitialOrbColor(cls);
        FadeOutOrb(_orbImages[0]);
        FadeInBckg();
        FadeInOrb(_orbImages[1], cls[1]);
        FadeInOrb(_orbImages[2], cls[2]);

        RotateOrbImages();
    }

    private void StartAnim(List<EColor> cls) {
        FadeInBckg();
        SpawnOrbs(cls);
    }

    // Utility methods ---

    private void SetInitialOrbColor(List<EColor> cls) {
        for (int i = 0; i < cls.Count; i++) {
            _orbImages[i].color = ColorUtils.GetColor(cls[i]);
        }
    }

    private void FadeInBckg() {
        _canvasGroup.alpha = 0f;
        _rectTransform.transform.localPosition = new Vector3(0f, -0.5f, 0f);
        _rectTransform.DOAnchorPos(new Vector2(0f, 0f), _fadeTime, false).SetEase(Ease.OutQuint);
        _canvasGroup.DOFade(1, _fadeTime);
    }

    private void FadeInOrb(Image img, EColor c) {
        img.color = ColorUtils.GetColor(c);
        img.transform.localPosition = new Vector3(0f, -0.5f, 0f);
        img.rectTransform.DOAnchorPos(new Vector2(0f, 0f), _fadeTime, false).SetEase(Ease.OutQuint);
        img.DOFade(1, _fadeTime);
    }

    private void FadeOutOrb(Image img) {
        img.DOFade(0, 0.7f);
    }

    private void SpawnOrbs(List<EColor> cls) {
        for (int i = 0; i < cls.Count; i++) {
            FadeInOrb(_orbImages[i], cls[i]);
        }
    }

    private void RotateOrbImages() {
        if (_orbImages == null || _orbImages.Count == 0) return;
        StartCoroutine(RotatePositionsCoroutine());
    }

    // might refactor this but if it works for the mvp eh
    private IEnumerator RotatePositionsCoroutine() {
        yield return new WaitForSeconds(1f);

        int numImg = _orbImages.Count;
        Vector3[] startPos = new Vector3[numImg];
        Vector3[] endPos = new Vector3[numImg];

        // store the starting positions
        for (int i = 0; i < numImg; i++) { startPos[i] = _orbImages[i].rectTransform.position; }

        // determine end pos
        for (int i = 0; i < numImg; i++) {
            int nextIndex = (i + 1) % numImg;
            endPos[nextIndex] = startPos[i];
        }

        float elapsedTime = 0f;

        while (elapsedTime < _animationDuration) {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / _animationDuration);

            // slerp positions
            for (int i = 0; i < numImg; i++) {
                _orbImages[i].rectTransform.position = Vector3.Slerp(startPos[i], endPos[i], t);
            }
            yield return null;
        }

        // ensure final positions are set
        for (int i = 0; i < numImg; i++) { _orbImages[i].rectTransform.position = endPos[i];  }

        // rotate img data in list
        Image lastImg = _orbImages[_orbImages.Count - 1];
        for (int i = numImg - 1; i > 0; i--) {
            _orbImages[i] = _orbImages[i - 1];
        }
        _orbImages[0] = lastImg;
    }
}
