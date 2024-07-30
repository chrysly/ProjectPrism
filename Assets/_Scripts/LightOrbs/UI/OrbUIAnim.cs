using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Rendering.UI;

public class OrbUIAnim : MonoBehaviour
{
    [SerializeField] private float _fadeTime = 1.5f;
    [SerializeField] private float _animationDuration = 1f;
    [SerializeField] private CanvasGroup _mainCanvas;   // for the entire canvas *cries*
    [SerializeField] private CanvasGroup _sideBitches;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private OrbHandler _orbHandler;
    
    // ---
    private List<UIOrb> _orbs = new List<UIOrb>();
    [SerializeField] private Image[] _orbImgs;
    [SerializeField] private Image[] _bckgImgs;

    private Image MainHolder => _bckgImgs[0];

    private int _numOrbs = 0;

    void Start() {
        //OrbHandler.OnOrbsSwapped += StartAnim;
        _orbHandler.OnThrowWindUp += OnOrbThrown;
        Player.OnSpawn += OnSpawn;
    }

    /// <summary>
    /// When the player spawns in play this
    /// </summary>
    private void OnSpawn(List<EColor> cls) {
        SetNumOrbs(cls.Count);
        _mainCanvas.alpha = 0f;
        _sideBitches.alpha = 0f;
        _mainCanvas.DOFade(1, _fadeTime);

        // create the orb objects and set their initial color + position
        for (int i = 0; i < _orbImgs.Length; ++i) {
            UIOrb obj = new UIOrb(_orbImgs[i], _bckgImgs[i].transform);
            _orbs.Add(obj);
            obj.SetOrbColor(cls[i]);
            obj.FadeOrb(1, _fadeTime);
        }

        StartCoroutine(SpawnWait());
    }

    // jank bc dreamhack kms
    private IEnumerator SpawnWait() {
        yield return new WaitForSeconds(0.4f);
        FadeBackgroundOrbs(0, _numOrbs);
    }

    private void SetNumOrbs(int num) {
        _numOrbs = num;
    }

    private void OnOrbThrown(List<EColor> cls) {
        
        _orbs[0].SpecialFade(0, 1f);

       FadeBackgroundOrbs(1, _numOrbs);

       RotateOrbImages();
    }

    // Utility methods ---

    private void FadeBackgroundOrbs(int alpha, int numOrbs) {
        if (alpha > 0) {
            for (int i = 1; i < _orbs.Count; ++i) {
                _orbs[i].FadeOrb(1, _fadeTime);
                _orbs[i].MoveOrb(_bckgImgs[i].transform.localPosition, _fadeTime);
            }
        } else {
            for (int i = 1; i < _orbs.Count; ++i) {
                _orbs[i].FadeOrb(0, _fadeTime);
            }
        }
        
    }

    private void RotateOrbImages() {
        if (_orbs == null || _orbs.Count == 0) return;
        StartCoroutine(RotatePositionsCoroutine());
    }

    // might refactor this but if it works for the mvp eh
    private IEnumerator RotatePositionsCoroutine() {
        yield return new WaitForSeconds(1f);

        int numImg = _orbs.Count;
        Vector2[] endPos = new Vector2[numImg];

        // determine end pos
        for (int i = 0; i < numImg; i++) {
            int nextIndex = (i + 1) % numImg;
            endPos[nextIndex] = _bckgImgs[i].rectTransform.localPosition;
        }

        float elapsedTime = 0f;

        while (elapsedTime < _animationDuration) {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / _animationDuration);

            // slerp positions
            for (int i = 0; i < numImg; ++i) {
                _orbs[i].MoveToPos(endPos[i], t);
            }
            yield return null;
        }

        // ensure final positions are set
        //for (int i = 0; i < numImg; i++) { _orbs[i].SetUIOrbPos(endPos[i]); }

        // hard coded shit
        UIOrb temp = _orbs[0];
        _orbs.Remove(temp);
        _orbs.Add(temp);


        yield return new WaitForSeconds(0.1f);
        FadeBackgroundOrbs(0, _numOrbs);
    }
}
