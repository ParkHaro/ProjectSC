using UnityEngine;
using Sc.Common.UI;
using TMPro;

namespace Sc.Tests
{
    /// <summary>
    /// 테스트용 Screen 변형 A.
    /// 중복 타입 제거 테스트를 위해 별도 타입 필요.
    /// </summary>
    public class SimpleTestScreenA : ScreenWidget<SimpleTestScreenA, SimpleTestScreenState>
    {
        private TextMeshProUGUI _nameText;
        private SimpleTestScreenState _state;

        protected override void OnInitialize()
        {
            _nameText = GetComponentInChildren<TextMeshProUGUI>();
        }

        protected override void OnBind(SimpleTestScreenState state)
        {
            _state = state ?? new SimpleTestScreenState { ScreenName = "ScreenA", Index = 0 };
            UpdateUI();
        }

        protected override void OnShow()
        {
            Debug.Log($"[SimpleTestScreenA] Show: {_state?.ScreenName}");
        }

        protected override void OnHide()
        {
            Debug.Log($"[SimpleTestScreenA] Hide: {_state?.ScreenName}");
        }

        public override SimpleTestScreenState GetState() => _state;

        private void UpdateUI()
        {
            if (_nameText != null)
                _nameText.text = $"{_state.ScreenName} (#{_state.Index})";
        }

        public static SimpleTestScreenA CreateInstance(Transform parent, string name = "TestScreenA")
        {
            var go = CreateScreenGameObject(parent, name, new Color(0.3f, 0.2f, 0.2f, 1f));
            return go.AddComponent<SimpleTestScreenA>();
        }

        private static GameObject CreateScreenGameObject(Transform parent, string name, Color bgColor)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent);

            var rect = go.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            go.AddComponent<CanvasGroup>();

            // Background
            var bgGO = new GameObject("Background");
            bgGO.transform.SetParent(go.transform);
            var bgRect = bgGO.AddComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.offsetMin = Vector2.zero;
            bgRect.offsetMax = Vector2.zero;
            var bgImage = bgGO.AddComponent<UnityEngine.UI.Image>();
            bgImage.color = bgColor;

            // Text
            var textGO = new GameObject("NameText");
            textGO.transform.SetParent(go.transform);
            var textRect = textGO.AddComponent<RectTransform>();
            textRect.anchorMin = new Vector2(0.5f, 0.5f);
            textRect.anchorMax = new Vector2(0.5f, 0.5f);
            textRect.sizeDelta = new Vector2(400, 100);
            var text = textGO.AddComponent<TextMeshProUGUI>();
            text.text = name;
            text.fontSize = 36;
            text.alignment = TextAlignmentOptions.Center;
            text.color = Color.white;

            return go;
        }
    }

    /// <summary>
    /// 테스트용 Screen 변형 B.
    /// </summary>
    public class SimpleTestScreenB : ScreenWidget<SimpleTestScreenB, SimpleTestScreenState>
    {
        private TextMeshProUGUI _nameText;
        private SimpleTestScreenState _state;

        protected override void OnInitialize()
        {
            _nameText = GetComponentInChildren<TextMeshProUGUI>();
        }

        protected override void OnBind(SimpleTestScreenState state)
        {
            _state = state ?? new SimpleTestScreenState { ScreenName = "ScreenB", Index = 0 };
            UpdateUI();
        }

        protected override void OnShow()
        {
            Debug.Log($"[SimpleTestScreenB] Show: {_state?.ScreenName}");
        }

        protected override void OnHide()
        {
            Debug.Log($"[SimpleTestScreenB] Hide: {_state?.ScreenName}");
        }

        public override SimpleTestScreenState GetState() => _state;

        private void UpdateUI()
        {
            if (_nameText != null)
                _nameText.text = $"{_state.ScreenName} (#{_state.Index})";
        }

        public static SimpleTestScreenB CreateInstance(Transform parent, string name = "TestScreenB")
        {
            var go = CreateScreenGameObject(parent, name, new Color(0.2f, 0.3f, 0.2f, 1f));
            return go.AddComponent<SimpleTestScreenB>();
        }

        private static GameObject CreateScreenGameObject(Transform parent, string name, Color bgColor)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent);

            var rect = go.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            go.AddComponent<CanvasGroup>();

            // Background
            var bgGO = new GameObject("Background");
            bgGO.transform.SetParent(go.transform);
            var bgRect = bgGO.AddComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.offsetMin = Vector2.zero;
            bgRect.offsetMax = Vector2.zero;
            var bgImage = bgGO.AddComponent<UnityEngine.UI.Image>();
            bgImage.color = bgColor;

            // Text
            var textGO = new GameObject("NameText");
            textGO.transform.SetParent(go.transform);
            var textRect = textGO.AddComponent<RectTransform>();
            textRect.anchorMin = new Vector2(0.5f, 0.5f);
            textRect.anchorMax = new Vector2(0.5f, 0.5f);
            textRect.sizeDelta = new Vector2(400, 100);
            var text = textGO.AddComponent<TextMeshProUGUI>();
            text.text = name;
            text.fontSize = 36;
            text.alignment = TextAlignmentOptions.Center;
            text.color = Color.white;

            return go;
        }
    }

    /// <summary>
    /// 테스트용 Screen 변형 C.
    /// </summary>
    public class SimpleTestScreenC : ScreenWidget<SimpleTestScreenC, SimpleTestScreenState>
    {
        private TextMeshProUGUI _nameText;
        private SimpleTestScreenState _state;

        protected override void OnInitialize()
        {
            _nameText = GetComponentInChildren<TextMeshProUGUI>();
        }

        protected override void OnBind(SimpleTestScreenState state)
        {
            _state = state ?? new SimpleTestScreenState { ScreenName = "ScreenC", Index = 0 };
            UpdateUI();
        }

        protected override void OnShow()
        {
            Debug.Log($"[SimpleTestScreenC] Show: {_state?.ScreenName}");
        }

        protected override void OnHide()
        {
            Debug.Log($"[SimpleTestScreenC] Hide: {_state?.ScreenName}");
        }

        public override SimpleTestScreenState GetState() => _state;

        private void UpdateUI()
        {
            if (_nameText != null)
                _nameText.text = $"{_state.ScreenName} (#{_state.Index})";
        }

        public static SimpleTestScreenC CreateInstance(Transform parent, string name = "TestScreenC")
        {
            var go = CreateScreenGameObject(parent, name, new Color(0.2f, 0.2f, 0.3f, 1f));
            return go.AddComponent<SimpleTestScreenC>();
        }

        private static GameObject CreateScreenGameObject(Transform parent, string name, Color bgColor)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent);

            var rect = go.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            go.AddComponent<CanvasGroup>();

            // Background
            var bgGO = new GameObject("Background");
            bgGO.transform.SetParent(go.transform);
            var bgRect = bgGO.AddComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.offsetMin = Vector2.zero;
            bgRect.offsetMax = Vector2.zero;
            var bgImage = bgGO.AddComponent<UnityEngine.UI.Image>();
            bgImage.color = bgColor;

            // Text
            var textGO = new GameObject("NameText");
            textGO.transform.SetParent(go.transform);
            var textRect = textGO.AddComponent<RectTransform>();
            textRect.anchorMin = new Vector2(0.5f, 0.5f);
            textRect.anchorMax = new Vector2(0.5f, 0.5f);
            textRect.sizeDelta = new Vector2(400, 100);
            var text = textGO.AddComponent<TextMeshProUGUI>();
            text.text = name;
            text.fontSize = 36;
            text.alignment = TextAlignmentOptions.Center;
            text.color = Color.white;

            return go;
        }
    }
}
