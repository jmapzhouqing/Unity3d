using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BuilderControl : MonoBehaviour
{
    public RectTransform container;

    public Text title;

    private Vector2 size;

    private bool is_expand = false;

    private Tween tween;

    private float duration = 0.2f;

    private LayoutElement element;

    private RectTransform level_prefab;

    private Dictionary<int, LevelItemControl> levels;

    // Start is called before the first frame update
    void Awake() {
        DOTween.Init(true, true, null);
        DOTween.defaultAutoPlay = AutoPlay.None;
        DOTween.defaultAutoKill = true;

        levels = new Dictionary<int, LevelItemControl>();

        level_prefab = Resources.Load<RectTransform>("UIPrefab/LevelItem");

        element = this.GetComponent<LayoutElement>();
        this.size = new Vector2(element.preferredWidth, element.preferredHeight);
        this.Expand(false);
    }

    // Update is called once per frame
    void Update() {

    }

    public void SetBuilderName(string name) {
        title.text = name;
    }

    public void CreateLevelItem() {
        for (int i = 0; i < 10; i++) {
            RectTransform child = GameObject.Instantiate<RectTransform>(level_prefab, container);
            LevelItemControl control = child.GetComponentInChildren<LevelItemControl>();
            levels.Add(i, control);
        }
        StartCoroutine(UpdateElementHeight());
    }

    public void SetLevel(int index) {
        LevelItemControl control;
        if (levels.TryGetValue(index, out control)) {
            control.Selected();
        }
    }

    private IEnumerator UpdateElementHeight() {
        if (is_expand) {
            while (container.sizeDelta.y < Mathf.Pow(10, -2)) {
                yield return new WaitForEndOfFrame();
            }
            float height = (title.rectTransform.sizeDelta.y + container.sizeDelta.y);

            while (Mathf.Abs(element.preferredHeight - height) > Mathf.Pow(10, -2)) {
                element.preferredHeight = height;
                yield return new WaitForEndOfFrame();
            }
        }
        //this.size = new Vector2(element.preferredWidth,element.preferredHeight);
    }

    public void Expand(bool is_expand) {
        if (tween != null && tween.IsPlaying())
        {
            return;
        }

        if (is_expand) {
            foreach (BuilderControl control in this.transform.parent.GetComponentsInChildren<BuilderControl>(true)) {
                control.Expand(false);
            }
            size = new Vector2(this.size.x, title.rectTransform.sizeDelta.y + container.sizeDelta.y);

            tween = element.DOPreferredSize(size, duration).Play();
        } else {
            tween = element.DOPreferredSize(new Vector2(this.size.x, title.rectTransform.sizeDelta.y), duration).Play();
        }

        this.is_expand = is_expand;
    }

    public void Expand() {
        this.Expand(!is_expand);
    }

    public void Clear(){
        for (int i = 0, number = container.childCount; i < number; i++) {
            GameObject.DestroyImmediate(container.GetChild(0).gameObject);
        }
    }
}
