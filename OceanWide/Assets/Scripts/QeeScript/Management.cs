using DG.Tweening;
using UnityEngine;

public class Management : MonoBehaviour
{
    public RectTransform left;
    public RectTransform right;

    private float duration = 0.2f;

    private Tween left_tween;
    private Tween right_tween;

    private Vector3 left_origin;
    private Vector3 right_origin;
    // Start is called before the first frame update
    void Awake(){
        DOTween.Init(true, true, null);
        DOTween.defaultAutoPlay = AutoPlay.None;
        DOTween.defaultAutoKill = false;

        left_origin = left.anchoredPosition;

        left_tween = left.DOAnchorPos3DX(10, duration);
        right_tween = right.DOAnchorPos3DX(0, duration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (left_tween.IsPlaying()) {
            left_tween.Pause();
        }

        if (right_tween.IsPlaying())
        {
            right_tween.Pause();
        }

        left_tween.PlayForward();
        right_tween.PlayForward();
    }

    private void OnDisable()
    {
        if (left_tween.IsPlaying())
        {
            left_tween.Pause();
        }

        if (right_tween.IsPlaying())
        {
            right_tween.Pause();
        }

        left_tween.PlayBackwards();
        right_tween.PlayBackwards();
    }
}
