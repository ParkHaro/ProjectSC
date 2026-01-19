using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

namespace Sc.Editor.AI
{
    /// <summary>
    /// 에디터 UI 생성 공용 헬퍼.
    /// 모든 Setup 도구에서 공통으로 사용하는 UI 생성 유틸리티.
    /// </summary>
    public static class EditorUIHelpers
    {
        private const string FONT_PATH = "Fonts & Materials/PretendardVariable SDF";
        private static TMP_FontAsset _cachedFont;

        #region Font

        /// <summary>
        /// 프로젝트 기본 폰트 로드 (캐싱).
        /// </summary>
        public static TMP_FontAsset GetProjectFont()
        {
            if (_cachedFont == null)
            {
                _cachedFont = Resources.Load<TMP_FontAsset>(FONT_PATH);
                if (_cachedFont == null)
                {
                    Debug.LogWarning($"[EditorUIHelpers] 프로젝트 폰트를 찾을 수 없습니다: {FONT_PATH}");
                }
            }
            return _cachedFont;
        }

        #endregion

        #region Canvas

        /// <summary>
        /// Canvas 찾거나 생성.
        /// </summary>
        public static Canvas FindOrCreateCanvas(string name, int sortingOrder = 100)
        {
            var existing = GameObject.Find(name);
            if (existing != null) return existing.GetComponent<Canvas>();

            var canvasGo = new GameObject(name);
            var canvas = canvasGo.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = sortingOrder;

            var scaler = canvasGo.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);

            canvasGo.AddComponent<GraphicRaycaster>();

            return canvas;
        }

        #endregion

        #region Panels

        /// <summary>
        /// 전체화면 패널 생성.
        /// </summary>
        public static GameObject CreateFullscreenPanel(string name, Color bgColor, Transform parent = null)
        {
            var panel = new GameObject(name);
            if (parent != null) panel.transform.SetParent(parent, false);

            var rect = panel.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta = Vector2.zero;
            rect.anchoredPosition = Vector2.zero;

            var image = panel.AddComponent<Image>();
            image.color = bgColor;

            return panel;
        }

        /// <summary>
        /// 중앙 정렬 패널 생성.
        /// </summary>
        public static GameObject CreateCenteredPanel(string name, Vector2 size, Color bgColor, Transform parent = null)
        {
            var panel = CreateUIObject(name, parent);

            var rect = panel.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = size;

            var image = panel.AddComponent<Image>();
            image.color = bgColor;

            return panel;
        }

        #endregion

        #region Text

        /// <summary>
        /// 중앙 정렬 TMP 텍스트 생성.
        /// </summary>
        public static TextMeshProUGUI CreateCenteredText(
            Transform parent,
            string name,
            string content,
            Vector2 offset,
            Vector2 size,
            int fontSize)
        {
            var go = CreateUIObject(name, parent);

            var rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = offset;
            rect.sizeDelta = size;

            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = content;
            tmp.fontSize = fontSize;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.color = Color.white;

            var font = GetProjectFont();
            if (font != null) tmp.font = font;

            return tmp;
        }

        /// <summary>
        /// 스트레치 TMP 텍스트 생성 (부모에 맞춤).
        /// </summary>
        public static TextMeshProUGUI CreateStretchedText(
            Transform parent,
            string name,
            string content,
            int fontSize,
            TextAlignmentOptions alignment = TextAlignmentOptions.Center)
        {
            var go = CreateUIObject(name, parent);

            var rect = go.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta = Vector2.zero;
            rect.anchoredPosition = Vector2.zero;

            var tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = content;
            tmp.fontSize = fontSize;
            tmp.alignment = alignment;
            tmp.color = Color.white;

            var font = GetProjectFont();
            if (font != null) tmp.font = font;

            return tmp;
        }

        /// <summary>
        /// 기본 TMP 텍스트 컴포넌트 추가.
        /// </summary>
        public static TextMeshProUGUI AddText(GameObject obj, string text, float fontSize)
        {
            var tmp = obj.AddComponent<TextMeshProUGUI>();
            tmp.text = text;
            tmp.fontSize = fontSize;

            var font = GetProjectFont();
            if (font != null) tmp.font = font;

            return tmp;
        }

        #endregion

        #region Buttons

        /// <summary>
        /// 중앙 정렬 버튼 생성.
        /// </summary>
        public static Button CreateCenteredButton(
            Transform parent,
            string name,
            string label,
            Vector2 offset,
            Vector2 size,
            Color? bgColor = null)
        {
            var go = CreateUIObject(name, parent);

            var rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = offset;
            rect.sizeDelta = size;

            var color = bgColor ?? new Color(0.3f, 0.3f, 0.45f, 1f);
            var image = go.AddComponent<Image>();
            image.color = color;

            var button = go.AddComponent<Button>();
            var colors = button.colors;
            colors.highlightedColor = color * 1.2f;
            colors.pressedColor = color * 0.8f;
            button.colors = colors;

            // Label
            var labelGo = CreateUIObject("Label", go.transform);
            var labelRect = labelGo.GetComponent<RectTransform>();
            labelRect.anchorMin = Vector2.zero;
            labelRect.anchorMax = Vector2.one;
            labelRect.sizeDelta = Vector2.zero;

            var labelTmp = labelGo.AddComponent<TextMeshProUGUI>();
            labelTmp.text = label;
            labelTmp.fontSize = 18;
            labelTmp.alignment = TextAlignmentOptions.Center;
            labelTmp.color = Color.white;

            var font = GetProjectFont();
            if (font != null) labelTmp.font = font;

            return button;
        }

        /// <summary>
        /// 레이아웃 그룹용 버튼 생성 (LayoutElement 포함).
        /// </summary>
        public static Button CreateLayoutButton(Transform parent, string name, string label, Color bgColor)
        {
            var go = CreateUIObject(name, parent);

            var layoutElement = go.AddComponent<LayoutElement>();
            layoutElement.minHeight = 50;
            layoutElement.preferredHeight = 50;

            var image = go.AddComponent<Image>();
            image.color = bgColor;

            var button = go.AddComponent<Button>();
            var colors = button.colors;
            colors.highlightedColor = bgColor * 1.2f;
            colors.pressedColor = bgColor * 0.8f;
            button.colors = colors;

            // Label
            var labelGo = CreateUIObject("Label", go.transform);
            var labelRect = labelGo.GetComponent<RectTransform>();
            labelRect.anchorMin = Vector2.zero;
            labelRect.anchorMax = Vector2.one;
            labelRect.sizeDelta = Vector2.zero;

            var labelTmp = labelGo.AddComponent<TextMeshProUGUI>();
            labelTmp.text = label;
            labelTmp.fontSize = 20;
            labelTmp.alignment = TextAlignmentOptions.Center;
            labelTmp.color = Color.white;

            var font = GetProjectFont();
            if (font != null) labelTmp.font = font;

            return button;
        }

        #endregion

        #region Basic UI Objects

        /// <summary>
        /// RectTransform이 포함된 기본 UI 오브젝트 생성.
        /// </summary>
        public static GameObject CreateUIObject(string name, Transform parent = null)
        {
            var go = new GameObject(name);
            if (parent != null) go.transform.SetParent(parent, false);
            go.AddComponent<RectTransform>();
            return go;
        }

        /// <summary>
        /// 딤 배경 생성 (반투명 검정).
        /// </summary>
        public static Image CreateDimBackground(Transform parent, string name = "Background", float alpha = 0.5f)
        {
            var bgObj = CreateUIObject(name, parent);

            var rect = bgObj.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta = Vector2.zero;

            var image = bgObj.AddComponent<Image>();
            image.color = new Color(0, 0, 0, alpha);

            return image;
        }

        /// <summary>
        /// EventSystem 찾거나 생성.
        /// </summary>
        public static UnityEngine.EventSystems.EventSystem EnsureEventSystem()
        {
            var es = Object.FindFirstObjectByType<UnityEngine.EventSystems.EventSystem>();
            if (es != null) return es;

            var go = new GameObject("EventSystem");
            es = go.AddComponent<UnityEngine.EventSystems.EventSystem>();
            go.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();

            return es;
        }

        #endregion

        #region Prefab Utilities

        /// <summary>
        /// 프리팹 저장 (폴더 자동 생성).
        /// </summary>
        public static GameObject SaveAsPrefab(GameObject instance, string folderPath, string prefabName)
        {
            EnsureFolder(folderPath);

            var prefabPath = $"{folderPath}/{prefabName}.prefab";
            var prefab = PrefabUtility.SaveAsPrefabAsset(instance, prefabPath);
            Object.DestroyImmediate(instance);

            Debug.Log($"[EditorUIHelpers] 프리팹 저장: {prefabPath}");
            return prefab;
        }

        /// <summary>
        /// 폴더 경로 존재 확인 및 생성.
        /// </summary>
        public static void EnsureFolder(string path)
        {
            var parts = path.Split('/');
            var current = parts[0];

            for (int i = 1; i < parts.Length; i++)
            {
                var next = $"{current}/{parts[i]}";
                if (!AssetDatabase.IsValidFolder(next))
                {
                    AssetDatabase.CreateFolder(current, parts[i]);
                }
                current = next;
            }
        }

        #endregion
    }
}
