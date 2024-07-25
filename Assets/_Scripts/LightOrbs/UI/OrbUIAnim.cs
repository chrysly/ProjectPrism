using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OrbUIAnim : MonoBehaviour
{
    [SerializeField] private float _fadeTime = 1.5f;
    [SerializeField] private CanvasGroup _mainCanvas;   // for the entire canvas *cries*
    [SerializeField] private CanvasGroup _sideBitches;
    [SerializeField] private RectTransform _rectTransform;
    
    // ---
    private List<UIOrb> _orbs = new List<UIOrb>();
    [SerializeField] private Image[] _orbImgs;
    [SerializeField] private Image[] _bckgImgs;

    private Image MainHolder => _bckgImgs[0];

    void Start() {
        OrbHandler.OnOrbsSwapped += StartAnim;
        OrbHandler.OnOrbThrown += OnOrbThrown;
        Player.OnSpawn += OnSpawn;
        //_canvasGroup.alpha = 0f;
    }

    /// <summary>
    /// When the player spawns in play this
    /// </summary>
    private void OnSpawn(List<EColor> c) {
        _mainCanvas.alpha = 0f;
        _sideBitches.alpha = 0f;
        _mainCanvas.DOFade(1, _fadeTime);

        // create the orb objects and set their initial color + position
        for (int i = 0; i < _orbImgs.Length; ++i) {
            UIOrb obj = new UIOrb(_orbImgs[i], _bckgImgs[i].transform);
            _orbs.Add(obj);
            obj.SetOrbColor(c[i]);
            obj.FadeOrb(1, _fadeTime);
        }

        StartCoroutine(SpawnWait());
    }

    // jank bc dreamhack kms
    private IEnumerator SpawnWait() {
        yield return new WaitForSeconds(1f);
        //FadeOutBckg();
        for (int i = 1; i < _orbs.Count; ++i) {
            _orbs[i].FadeOrb(0, _fadeTime);
        }
    }

    private void OnOrbThrown(List<EColor> cls) {
        _orbs[0].FadeOrb(0, _fadeTime);

        for (int i = 1; i < _orbs.Count; ++i) {
            _orbs[i].FadeOrb(1, _fadeTime);
            _orbs[i].MoveOrb(_bckgImgs[i].transform.localPosition, _fadeTime);
        }

        //FadeOutOrb(_orbImgs[0]);
        //FadeInBckg();
        //FadeInOrb(_orbImgs[1]);
        //FadeInOrb(_orbImgs[2]);

        //RotateOrbImages();
    }

    private void StartAnim(List<EColor> cls) {
        FadeInBckg();
        SpawnOrbs(cls);
    }

    // Utility methods ---

    private void SetInitialOrbColor(List<EColor> cls) {
        for (int i = 0; i < cls.Count; i++) {
            _orbImgs[i].color = cls[i].GetColor();
        }
    }

    private void FadeInBckg() {
        for(int i = 1; i < _bckgImgs.Length; ++i) {
            _bckgImgs[i].transform.localPosition = new Vector3(0f, -0.5f, 0f);
            _bckgImgs[i].rectTransform.DOAnchorPos(new Vector2(0f, 0f), _fadeTime, false).SetEase(Ease.OutQuint);
            _bckgImgs[i].DOFade(1, _fadeTime);
        }
        
        //_canvasGroup.alpha = 0f;
        //_rectTransform.transform.localPosition = new Vector3(0f, -0.5f, 0f);
        //_rectTransform.DOAnchorPos(new Vector2(0f, 0f), _fadeTime, false).SetEase(Ease.OutQuint);
        //_canvasGroup.DOFade(1, _fadeTime);
    }
    private void FadeOutBckg() {
        for (int i = 1; i < _bckgImgs.Length; ++i) {
            _bckgImgs[i].DOFade(0, _fadeTime);
        }
        //_rectTransform.transform.localPosition = new Vector3(0f, -0.5f, 0f);
        //_rectTransform.DOAnchorPos(new Vector2(0f, 0f), _fadeTime, false).SetEase(Ease.OutQuint);

        //_canvasGroup.DOFade(0, _fadeTime);
    }

    private void SpawnOrbs(List<EColor> cls) {
        //for (int i = 0; i < _orbs.Count; i++) {
        //    FadeInOrb(_orbImgs[i]);
        //}
    }

    private void RotateOrbImages() {
        //if (_orbImgs == null || _orbImgs.Count == 0) return;
        //StartCoroutine(RotatePositionsCoroutine());
    }

    //// might refactor this but if it works for the mvp eh
    //private IEnumerator RotatePositionsCoroutine() {
    //    yield return new WaitForSeconds(1f);

    //    int numImg = _orbImgs.Count;
    //    Vector3[] startPos = new Vector3[numImg];
    //    Vector3[] endPos = new Vector3[numImg];

    //    // store the starting positions
    //    for (int i = 0; i < numImg; i++) { startPos[i] = _orbImgs[i].rectTransform.position; }

    //    // determine end pos
    //    for (int i = 0; i < numImg; i++) {
    //        int nextIndex = (i + 1) % numImg;
    //        endPos[nextIndex] = startPos[i];
    //    }

    //    float elapsedTime = 0f;

    //    while (elapsedTime < _animationDuration) {
    //        elapsedTime += Time.deltaTime;
    //        float t = Mathf.Clamp01(elapsedTime / _animationDuration);

    //        // slerp positions
    //        for (int i = 0; i < numImg; i++) {
    //            _orbImgs[i].rectTransform.position = Vector3.Slerp(startPos[i], endPos[i], t);
    //        }
    //        yield return null;
    //    }

    //    // ensure final positions are set
    //    for (int i = 0; i < numImg; i++) { _orbImgs[i].rectTransform.position = endPos[i];  }

    //    // rotate img data in list
    //    //Image lastImg = _orbImgs[_orbImgs.Count - 1];
    //    //for (int i = numImg - 1; i > 0; i--) {
    //    //    _orbImgs[i] = _orbImgs[i - 1];
    //    //}
    //    //_orbImgs[0] = lastImg;

    //    //Image[] tempImages = new Image[numImg];
    //    //for (int i = 0; i < numImg; i++) {
    //    //    tempImages[(i + 1) % numImg] = _orbImgs[i];
    //    //}

    //    //for (int i = 0; i < numImg; i++) {
    //    //    _orbImgs[i] = tempImages[i];
    //    //}

    //    // hard coded shit
    //    Image temp = _orbImgs[0];
    //    _orbImgs.Remove(temp);
    //    _orbImgs.Add(temp);


    //    yield return new WaitForSeconds(0.5f);
    //    FadeOutBckg();
    //    FadeOutOrb(_orbImgs[1]);
    //    FadeOutOrb(_orbImgs[2]);
    //}
}
