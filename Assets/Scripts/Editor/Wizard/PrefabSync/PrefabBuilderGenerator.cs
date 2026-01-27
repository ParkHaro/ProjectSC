using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Sc.Editor.Wizard.PrefabSync
{
    /// <summary>
    /// JSON Spec을 기반으로 PrefabBuilder C# 코드를 생성하는 Generator.
    /// </summary>
    public static class PrefabBuilderGenerator
    {
        private const string OUTPUT_FOLDER = "Assets/Scripts/Editor/Wizard/Generators";
        private const string SPEC_FOLDER = "Assets/Scripts/Editor/Wizard/PrefabSync/Specs";

        // .Widgets 서브 네임스페이스가 존재하는 모듈 목록
        private static readonly HashSet<string> NamespacesWithWidgets = new HashSet<string>
        {
            "Sc.Contents.Character",
            "Sc.Contents.Event",
            "Sc.Contents.Inventory",
            "Sc.Contents.Lobby",
            "Sc.Contents.Stage",
            "Sc.Contents.Shop"
        };

        #region Public API

        /// <summary>
        /// JSON Spec 파일로부터 Builder 코드 생성.
        /// </summary>
        public static string Generate(string specPath) => Generate(specPath, forceOverwrite: false);

        /// <summary>
        /// JSON Spec 파일로부터 Builder 코드 생성.
        /// </summary>
        /// <param name="specPath">JSON Spec 파일 경로</param>
        /// <param name="forceOverwrite">true이면 수동 빌더가 있어도 Generated 파일 생성</param>
        public static string Generate(string specPath, bool forceOverwrite)
        {
            if (!File.Exists(specPath))
            {
                Debug.LogError($"Spec file not found: {specPath}");
                return null;
            }

            var json = File.ReadAllText(specPath);
            var spec = JsonConvert.DeserializeObject<PrefabStructureSpec>(json);

            if (spec == null)
            {
                Debug.LogError($"Failed to parse spec: {specPath}");
                return null;
            }

            // 기존 수동 빌더가 있는지 확인 (Generated가 아닌 파일)
            if (!forceOverwrite)
            {
                var manualBuilderPath = $"{OUTPUT_FOLDER}/{spec.metadata.prefabName}PrefabBuilder.cs";
                if (File.Exists(manualBuilderPath))
                {
                    Debug.LogWarning($"[PrefabBuilderGenerator] Skipped: Manual builder exists - {manualBuilderPath}");
                    return null;
                }
            }

            var code = GenerateBuilderCode(spec);
            var outputPath = SaveBuilderCode(spec.metadata.prefabName, code);

            Debug.Log($"Builder code generated: {outputPath}");
            return outputPath;
        }

        /// <summary>
        /// LobbyScreen Builder 코드 생성 (테스트용 메뉴).
        /// </summary>
        [MenuItem("Tools/ProjectSC/PrefabSync/Generate LobbyScreen Builder")]
        public static void GenerateLobbyScreenBuilder()
        {
            Generate($"{SPEC_FOLDER}/LobbyScreen.structure.json");
        }

        /// <summary>
        /// 선택된 Spec 파일로부터 Builder 생성.
        /// </summary>
        [MenuItem("Tools/ProjectSC/PrefabSync/Generate Builder from Selected Spec")]
        public static void GenerateFromSelectedSpec()
        {
            var selected = Selection.activeObject;
            if (selected == null)
            {
                Debug.LogError("No spec file selected");
                return;
            }

            var path = AssetDatabase.GetAssetPath(selected);
            if (!path.EndsWith(".structure.json"))
            {
                Debug.LogError("Selected file is not a .structure.json spec file");
                return;
            }

            Generate(path);
        }

        #endregion

        #region Code Generation

        private static string GenerateBuilderCode(PrefabStructureSpec spec)
        {
            var sb = new StringBuilder();
            var prefabName = spec.metadata.prefabName;
            var componentNs = spec.metadata.componentNamespace;
            var componentType = spec.metadata.componentType;

            // Usings
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using Sc.Common.UI.Widgets;");
            sb.AppendLine("using Sc.Editor.AI;");
            sb.AppendLine("using TMPro;");
            sb.AppendLine("using UnityEditor;");
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine("using UnityEngine.UI;");

            if (!string.IsNullOrEmpty(componentNs))
            {
                sb.AppendLine($"using {componentNs};");

                // .Widgets 서브 네임스페이스가 존재하는 경우만 추가
                if (NamespacesWithWidgets.Contains(componentNs))
                {
                    sb.AppendLine($"using {componentNs}.Widgets;");
                }
            }

            sb.AppendLine();

            // Namespace & Class
            sb.AppendLine("namespace Sc.Editor.Wizard.Generators");
            sb.AppendLine("{");
            sb.AppendLine($"    /// <summary>");
            sb.AppendLine($"    /// {prefabName} 프리팹 빌더 (자동 생성됨).");
            sb.AppendLine($"    /// Generated from: {spec.metadata.prefabPath}");
            sb.AppendLine($"    /// Generated at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"    /// </summary>");
            sb.AppendLine($"    public static class {prefabName}PrefabBuilder_Generated");
            sb.AppendLine("    {");

            // Theme Colors
            GenerateThemeColors(sb, spec.theme);

            // Constants
            GenerateConstants(sb, spec);

            // Font Helper
            GenerateFontHelper(sb);

            // 중복 이름 미리 수집 (Build 메서드와 Create 메서드에서 공유)
            var duplicateNames = CollectDuplicateNames(spec.hierarchy);
            var nameCounter = new Dictionary<string, int>();

            // Build Method - duplicateNames 전달
            GenerateBuildMethod(sb, spec, duplicateNames);

            // Create Methods for each hierarchy node
            GenerateCreateMethods(sb, spec.hierarchy, 0, duplicateNames, nameCounter);

            // SerializedField Connection
            if (spec.serializedFields?.Count > 0)
            {
                GenerateConnectSerializedFieldsMethod(sb, spec);
            }

            // Helper Methods
            GenerateHelperMethods(sb);

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private static void GenerateThemeColors(StringBuilder sb, ThemeInfo theme)
        {
            sb.AppendLine("        #region Theme Colors");
            sb.AppendLine();

            // 기본 테마 색상 (항상 포함)
            var defaultColors = new Dictionary<string, string>
            {
                { "BgDeep", "new Color32(10, 10, 18, 255)" },
                { "BgCard", "new Color32(25, 25, 45, 217)" },
                { "BgOverlay", "new Color32(0, 0, 0, 200)" },
                { "TextPrimary", "Color.white" },
                { "TextSecondary", "new Color(1f, 1f, 1f, 0.7f)" },
                { "TextMuted", "new Color(1f, 1f, 1f, 0.5f)" },
                { "AccentPrimary", "new Color32(100, 200, 255, 255)" },
                { "AccentGold", "new Color32(255, 215, 100, 255)" },
                { "Transparent", "Color.clear" }
            };

            // 기본 색상 출력
            foreach (var kvp in defaultColors)
            {
                sb.AppendLine($"        private static readonly Color {kvp.Key} = {kvp.Value};");
            }

            // JSON에서 추출된 추가 색상이 있으면 추가
            if (theme?.colors != null && theme.colors.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("        // Extracted from prefab");

                var uniqueColors = new Dictionary<string, string>();
                foreach (var kvp in theme.colors)
                {
                    var simpleName = SimplifyColorName(kvp.Key, kvp.Value);
                    // 기본 색상과 중복되지 않는 것만 추가
                    if (!defaultColors.ContainsKey(simpleName) && !uniqueColors.ContainsKey(simpleName))
                        uniqueColors[simpleName] = kvp.Value;
                }

                foreach (var kvp in uniqueColors.OrderBy(k => k.Key))
                {
                    var colorValue = ParseHexColor(kvp.Value);
                    sb.AppendLine($"        private static readonly Color {kvp.Key} = {colorValue};");
                }
            }

            sb.AppendLine();
            sb.AppendLine("        #endregion");
            sb.AppendLine();
        }

        private static void GenerateConstants(StringBuilder sb, PrefabStructureSpec spec)
        {
            sb.AppendLine("        #region Constants");
            sb.AppendLine();

            // 계층 구조에서 상수 추출
            var constants = ExtractLayoutConstants(spec.hierarchy);

            foreach (var kvp in constants.OrderBy(k => k.Key))
            {
                sb.AppendLine($"        private const float {kvp.Key} = {kvp.Value}f;");
            }

            sb.AppendLine();
            sb.AppendLine("        #endregion");
            sb.AppendLine();
        }

        private static void GenerateFontHelper(StringBuilder sb)
        {
            sb.AppendLine("        #region Font Helper");
            sb.AppendLine();
            sb.AppendLine("        private static void ApplyFont(TextMeshProUGUI tmp)");
            sb.AppendLine("        {");
            sb.AppendLine("            var font = EditorUIHelpers.GetProjectFont();");
            sb.AppendLine("            if (font != null) tmp.font = font;");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        #endregion");
            sb.AppendLine();
        }

        private static void GenerateBuildMethod(StringBuilder sb, PrefabStructureSpec spec,
            HashSet<string> duplicateNames)
        {
            var prefabName = spec.metadata.prefabName;
            var componentType = spec.metadata.componentType;

            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        /// {prefabName} 프리팹용 GameObject 생성.");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        public static GameObject Build()");
            sb.AppendLine("        {");
            sb.AppendLine($"            var root = CreateRoot(\"{prefabName}\");");
            sb.AppendLine();

            // 자식 노드들 생성 호출 - 중복 이름 인덱스 추적
            var nameCounter = new Dictionary<string, int>();
            foreach (var child in spec.hierarchy.children ?? new List<HierarchyNode>())
            {
                var cleanName = SanitizeNodeName(child.name);
                var isDuplicate = duplicateNames.Contains(cleanName);

                int index = 0;
                if (isDuplicate)
                {
                    if (nameCounter.ContainsKey(cleanName))
                    {
                        nameCounter[cleanName]++;
                    }
                    else
                    {
                        nameCounter[cleanName] = 1;
                    }

                    index = nameCounter[cleanName];
                }

                var methodName = GetCreateMethodName(child.name, isDuplicate ? index : 0);
                var varName = ToCamelCase(child.name);
                sb.AppendLine($"            var {varName} = {methodName}(root);");
            }

            sb.AppendLine();

            // 메인 컴포넌트 추가
            if (!string.IsNullOrEmpty(componentType))
            {
                sb.AppendLine($"            // Add main component");
                sb.AppendLine($"            root.AddComponent<{componentType}>();");
                sb.AppendLine();
            }

            // SerializedField 연결
            if (spec.serializedFields?.Count > 0)
            {
                sb.AppendLine("            // Connect serialized fields");
                sb.AppendLine("            ConnectSerializedFields(root);");
                sb.AppendLine();
            }

            sb.AppendLine("            return root;");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private static void GenerateCreateMethods(StringBuilder sb, HierarchyNode node, int depth,
            HashSet<string> duplicateNames, Dictionary<string, int> nameCounter)
        {
            // 루트는 별도 처리
            if (depth == 0)
            {
                foreach (var child in node.children ?? new List<HierarchyNode>())
                {
                    GenerateCreateMethods(sb, child, 1, duplicateNames, nameCounter);
                }

                return;
            }

            var cleanName = SanitizeNodeName(node.name);
            var isDuplicate = duplicateNames.Contains(cleanName);

            // 인덱스 계산: 중복 이름인 경우에만 인덱스 사용
            int index = 0;
            if (isDuplicate)
            {
                if (nameCounter.ContainsKey(cleanName))
                {
                    nameCounter[cleanName]++;
                }
                else
                {
                    nameCounter[cleanName] = 1;
                }

                index = nameCounter[cleanName];
            }

            var methodName = GetCreateMethodName(node.name, isDuplicate ? index : 0);

            sb.AppendLine($"        #region {node.name}");
            sb.AppendLine();
            sb.AppendLine($"        private static GameObject {methodName}(GameObject parent)");
            sb.AppendLine("        {");
            sb.AppendLine($"            var go = CreateChild(parent, \"{node.name}\");");

            // RectTransform 설정
            GenerateRectSetup(sb, node.rect, node.name);

            // 컴포넌트 추가
            GenerateComponentSetup(sb, node.components);

            // 활성 상태
            if (!node.active)
            {
                sb.AppendLine("            go.SetActive(false);");
            }

            // 자식 노드 생성 호출을 위한 메서드 이름 미리 계산
            // 자식들의 인덱스를 알기 위해 임시로 카운터 복사본 사용
            var tempCounter = new Dictionary<string, int>(nameCounter);

            // 자식 노드 생성
            if (node.children?.Count > 0)
            {
                sb.AppendLine();
                foreach (var child in node.children)
                {
                    var childCleanName = SanitizeNodeName(child.name);
                    var childIsDuplicate = duplicateNames.Contains(childCleanName);

                    int childIndex = 0;
                    if (childIsDuplicate)
                    {
                        if (tempCounter.ContainsKey(childCleanName))
                        {
                            tempCounter[childCleanName]++;
                        }
                        else
                        {
                            tempCounter[childCleanName] = 1;
                        }

                        childIndex = tempCounter[childCleanName];
                    }

                    var childMethodName = GetCreateMethodName(child.name, childIsDuplicate ? childIndex : 0);
                    sb.AppendLine($"            {childMethodName}(go);");
                }
            }

            // 위젯 필드 연결
            if (node.widgetFields?.Count > 0)
            {
                GenerateWidgetFieldConnections(sb, node);
            }

            sb.AppendLine();
            sb.AppendLine("            return go;");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        #endregion");
            sb.AppendLine();

            // 자식들의 Create 메서드 재귀 생성
            foreach (var child in node.children ?? new List<HierarchyNode>())
            {
                GenerateCreateMethods(sb, child, depth + 1, duplicateNames, nameCounter);
            }
        }

        private static void GenerateRectSetup(StringBuilder sb, RectInfo rect, string nodeName)
        {
            if (rect == null) return;

            var anchor = rect.anchor ?? "Stretch";

            if (anchor == "Stretch")
            {
                sb.AppendLine("            SetStretch(go);");

                // offset이 있는 경우
                if (rect.offsetMin != null && rect.offsetMax != null)
                {
                    var hasOffset = Math.Abs(rect.offsetMin[0]) > 0.01f ||
                                    Math.Abs(rect.offsetMin[1]) > 0.01f ||
                                    Math.Abs(rect.offsetMax[0]) > 0.01f ||
                                    Math.Abs(rect.offsetMax[1]) > 0.01f;

                    if (hasOffset)
                    {
                        sb.AppendLine($"            var rect = go.GetComponent<RectTransform>();");
                        sb.AppendLine(
                            $"            rect.offsetMin = new Vector2({rect.offsetMin[0]}f, {rect.offsetMin[1]}f);");
                        sb.AppendLine(
                            $"            rect.offsetMax = new Vector2({rect.offsetMax[0]}f, {rect.offsetMax[1]}f);");
                    }
                }
            }
            else
            {
                sb.AppendLine("            var rect = go.GetComponent<RectTransform>();");
                sb.AppendLine("            if (rect == null) rect = go.AddComponent<RectTransform>();");

                // 앵커 설정
                sb.AppendLine($"            rect.anchorMin = new Vector2({rect.anchorMin[0]}f, {rect.anchorMin[1]}f);");
                sb.AppendLine($"            rect.anchorMax = new Vector2({rect.anchorMax[0]}f, {rect.anchorMax[1]}f);");

                if (rect.pivot != null)
                {
                    sb.AppendLine($"            rect.pivot = new Vector2({rect.pivot[0]}f, {rect.pivot[1]}f);");
                }

                if (rect.sizeDelta != null)
                {
                    sb.AppendLine(
                        $"            rect.sizeDelta = new Vector2({rect.sizeDelta[0]}f, {rect.sizeDelta[1]}f);");
                }

                if (rect.anchoredPosition != null)
                {
                    sb.AppendLine(
                        $"            rect.anchoredPosition = new Vector2({rect.anchoredPosition[0]}f, {rect.anchoredPosition[1]}f);");
                }
            }
        }

        private static void GenerateComponentSetup(StringBuilder sb, List<ComponentInfo> components)
        {
            if (components == null || components.Count == 0) return;

            foreach (var comp in components)
            {
                if (comp.isMainComponent) continue; // 메인 컴포넌트는 Build에서 처리

                sb.AppendLine();

                switch (comp.type)
                {
                    case "Image":
                        GenerateImageSetup(sb, comp);
                        break;

                    case "TextMeshProUGUI":
                        GenerateTextSetup(sb, comp);
                        break;

                    case "Button":
                        GenerateButtonSetup(sb, comp);
                        break;

                    case "VerticalLayoutGroup":
                        GenerateVerticalLayoutSetup(sb, comp);
                        break;

                    case "HorizontalLayoutGroup":
                        GenerateHorizontalLayoutSetup(sb, comp);
                        break;

                    case "GridLayoutGroup":
                        GenerateGridLayoutSetup(sb, comp);
                        break;

                    case "LayoutElement":
                        GenerateLayoutElementSetup(sb, comp);
                        break;

                    case "ScrollRect":
                        GenerateScrollRectSetup(sb, comp);
                        break;

                    case "Mask":
                        GenerateMaskSetup(sb, comp);
                        break;

                    case "CanvasGroup":
                        sb.AppendLine("            go.AddComponent<CanvasGroup>();");
                        break;

                    case "ContentSizeFitter":
                        GenerateContentSizeFitterSetup(sb, comp);
                        break;

                    default:
                        // 기타 커스텀 컴포넌트
                        if (!string.IsNullOrEmpty(comp.fullType) && comp.fullType.StartsWith("Sc."))
                        {
                            sb.AppendLine($"            go.AddComponent<{comp.type}>();");
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// 위젯 SerializeField 연결 코드 생성.
        /// </summary>
        private static void GenerateWidgetFieldConnections(StringBuilder sb, HierarchyNode node)
        {
            if (node.widgetFields == null || node.widgetFields.Count == 0) return;

            // 위젯 컴포넌트 타입 찾기
            var widgetComp = node.components?.FirstOrDefault(c =>
                !string.IsNullOrEmpty(c.fullType) && c.fullType.StartsWith("Sc."));

            if (widgetComp == null) return;

            sb.AppendLine();
            sb.AppendLine("            // Connect widget SerializeFields");
            sb.AppendLine($"            var widgetComp = go.GetComponent<{widgetComp.type}>();");
            sb.AppendLine("            if (widgetComp != null)");
            sb.AppendLine("            {");
            sb.AppendLine("                var widgetSo = new SerializedObject(widgetComp);");

            foreach (var field in node.widgetFields)
            {
                if (field.isArray && field.arrayPaths?.Count > 0)
                {
                    // 배열 필드 처리
                    var propName = ToCamelCase(field.fieldName.TrimStart('_'));
                    sb.AppendLine(
                        $"                var {propName}Prop = widgetSo.FindProperty(\"{field.fieldName}\");");
                    sb.AppendLine($"                {propName}Prop.arraySize = {field.arrayPaths.Count};");

                    for (int i = 0; i < field.arrayPaths.Count; i++)
                    {
                        var getter = GetWidgetFieldGetter(field.targetType, $"\"{field.arrayPaths[i]}\"");
                        sb.AppendLine(
                            $"                {propName}Prop.GetArrayElementAtIndex({i}).objectReferenceValue = {getter};");
                    }
                }
                else if (!string.IsNullOrEmpty(field.targetPath))
                {
                    // 단일 필드 처리
                    var getter = GetWidgetFieldGetter(field.targetType, $"\"{field.targetPath}\"");
                    sb.AppendLine(
                        $"                widgetSo.FindProperty(\"{field.fieldName}\").objectReferenceValue = {getter};");
                }
            }

            sb.AppendLine("                widgetSo.ApplyModifiedPropertiesWithoutUndo();");
            sb.AppendLine("            }");
        }

        /// <summary>
        /// 위젯 필드의 getter 표현식 생성.
        /// </summary>
        private static string GetWidgetFieldGetter(string targetType, string pathExpr)
        {
            if (string.IsNullOrEmpty(targetType) || targetType == "GameObject")
                return $"go.transform.Find({pathExpr})?.gameObject";
            if (targetType == "Transform")
                return $"go.transform.Find({pathExpr})";
            if (targetType == "RectTransform")
                return $"go.transform.Find({pathExpr}) as RectTransform";

            return $"go.transform.Find({pathExpr})?.GetComponent<{targetType}>()";
        }

        private static void GenerateImageSetup(StringBuilder sb, ComponentInfo comp)
        {
            sb.AppendLine("            var image = go.AddComponent<Image>();");

            if (comp.properties.TryGetValue("color", out var color))
            {
                var colorCode = HexToColorCode(color.ToString());
                sb.AppendLine($"            image.color = {colorCode};");
            }

            if (comp.properties.TryGetValue("raycastTarget", out var raycast))
            {
                sb.AppendLine($"            image.raycastTarget = {raycast.ToString().ToLower()};");
            }
        }

        private static void GenerateTextSetup(StringBuilder sb, ComponentInfo comp)
        {
            sb.AppendLine("            var tmp = go.AddComponent<TextMeshProUGUI>();");

            if (comp.properties.TryGetValue("text", out var text))
            {
                sb.AppendLine($"            tmp.text = \"{EscapeString(text.ToString())}\";");
            }

            if (comp.properties.TryGetValue("fontSize", out var fontSize))
            {
                sb.AppendLine($"            tmp.fontSize = {fontSize}f;");
            }

            if (comp.properties.TryGetValue("color", out var color))
            {
                var colorCode = HexToColorCode(color.ToString());
                sb.AppendLine($"            tmp.color = {colorCode};");
            }

            if (comp.properties.TryGetValue("alignment", out var alignment))
            {
                sb.AppendLine($"            tmp.alignment = TextAlignmentOptions.{alignment};");
            }

            if (comp.properties.TryGetValue("fontStyle", out var fontStyle) && fontStyle.ToString() != "Normal")
            {
                sb.AppendLine($"            tmp.fontStyle = FontStyles.{fontStyle};");
            }

            if (comp.properties.TryGetValue("raycastTarget", out var raycast))
            {
                sb.AppendLine($"            tmp.raycastTarget = {raycast.ToString().ToLower()};");
            }

            sb.AppendLine("            ApplyFont(tmp);");
        }

        private static void GenerateButtonSetup(StringBuilder sb, ComponentInfo comp)
        {
            sb.AppendLine("            var button = go.AddComponent<Button>();");

            // Image가 있으면 targetGraphic 연결
            sb.AppendLine("            var img = go.GetComponent<Image>();");
            sb.AppendLine("            if (img != null) button.targetGraphic = img;");
        }

        private static void GenerateVerticalLayoutSetup(StringBuilder sb, ComponentInfo comp)
        {
            sb.AppendLine("            var layout = go.AddComponent<VerticalLayoutGroup>();");

            if (comp.properties.TryGetValue("spacing", out var spacing))
            {
                sb.AppendLine($"            layout.spacing = {spacing}f;");
            }

            if (comp.properties.TryGetValue("padding", out var padding))
            {
                var p = JsonConvert.DeserializeObject<int[]>(padding.ToString());
                if (p != null && p.Length == 4)
                {
                    sb.AppendLine($"            layout.padding = new RectOffset({p[0]}, {p[1]}, {p[2]}, {p[3]});");
                }
            }

            if (comp.properties.TryGetValue("childAlignment", out var alignment))
            {
                sb.AppendLine($"            layout.childAlignment = TextAnchor.{alignment};");
            }

            GenerateLayoutGroupFlags(sb, comp, "layout");
        }

        private static void GenerateHorizontalLayoutSetup(StringBuilder sb, ComponentInfo comp)
        {
            sb.AppendLine("            var layout = go.AddComponent<HorizontalLayoutGroup>();");

            if (comp.properties.TryGetValue("spacing", out var spacing))
            {
                sb.AppendLine($"            layout.spacing = {spacing}f;");
            }

            if (comp.properties.TryGetValue("padding", out var padding))
            {
                var p = JsonConvert.DeserializeObject<int[]>(padding.ToString());
                if (p != null && p.Length == 4)
                {
                    sb.AppendLine($"            layout.padding = new RectOffset({p[0]}, {p[1]}, {p[2]}, {p[3]});");
                }
            }

            if (comp.properties.TryGetValue("childAlignment", out var alignment))
            {
                sb.AppendLine($"            layout.childAlignment = TextAnchor.{alignment};");
            }

            GenerateLayoutGroupFlags(sb, comp, "layout");
        }

        private static void GenerateGridLayoutSetup(StringBuilder sb, ComponentInfo comp)
        {
            sb.AppendLine("            var grid = go.AddComponent<GridLayoutGroup>();");

            if (comp.properties.TryGetValue("cellSize", out var cellSize))
            {
                var cs = JsonConvert.DeserializeObject<float[]>(cellSize.ToString());
                if (cs != null && cs.Length == 2)
                {
                    sb.AppendLine($"            grid.cellSize = new Vector2({cs[0]}f, {cs[1]}f);");
                }
            }

            if (comp.properties.TryGetValue("spacing", out var spacing))
            {
                var sp = JsonConvert.DeserializeObject<float[]>(spacing.ToString());
                if (sp != null && sp.Length == 2)
                {
                    sb.AppendLine($"            grid.spacing = new Vector2({sp[0]}f, {sp[1]}f);");
                }
            }

            if (comp.properties.TryGetValue("startCorner", out var startCorner))
            {
                sb.AppendLine($"            grid.startCorner = GridLayoutGroup.Corner.{startCorner};");
            }

            if (comp.properties.TryGetValue("startAxis", out var startAxis))
            {
                sb.AppendLine($"            grid.startAxis = GridLayoutGroup.Axis.{startAxis};");
            }

            if (comp.properties.TryGetValue("childAlignment", out var alignment))
            {
                sb.AppendLine($"            grid.childAlignment = TextAnchor.{alignment};");
            }

            if (comp.properties.TryGetValue("constraint", out var constraint))
            {
                sb.AppendLine($"            grid.constraint = GridLayoutGroup.Constraint.{constraint};");
            }

            if (comp.properties.TryGetValue("constraintCount", out var count))
            {
                sb.AppendLine($"            grid.constraintCount = {count};");
            }

            if (comp.properties.TryGetValue("padding", out var padding))
            {
                var p = JsonConvert.DeserializeObject<int[]>(padding.ToString());
                if (p != null && p.Length == 4)
                {
                    sb.AppendLine($"            grid.padding = new RectOffset({p[0]}, {p[1]}, {p[2]}, {p[3]});");
                }
            }
        }

        private static void GenerateLayoutElementSetup(StringBuilder sb, ComponentInfo comp)
        {
            sb.AppendLine("            var layoutElement = go.AddComponent<LayoutElement>();");

            if (comp.properties.TryGetValue("minWidth", out var minWidth))
                sb.AppendLine($"            layoutElement.minWidth = {minWidth}f;");

            if (comp.properties.TryGetValue("minHeight", out var minHeight))
                sb.AppendLine($"            layoutElement.minHeight = {minHeight}f;");

            if (comp.properties.TryGetValue("preferredWidth", out var prefWidth))
                sb.AppendLine($"            layoutElement.preferredWidth = {prefWidth}f;");

            if (comp.properties.TryGetValue("preferredHeight", out var prefHeight))
                sb.AppendLine($"            layoutElement.preferredHeight = {prefHeight}f;");

            if (comp.properties.TryGetValue("flexibleWidth", out var flexWidth))
                sb.AppendLine($"            layoutElement.flexibleWidth = {flexWidth}f;");

            if (comp.properties.TryGetValue("flexibleHeight", out var flexHeight))
                sb.AppendLine($"            layoutElement.flexibleHeight = {flexHeight}f;");
        }

        private static void GenerateScrollRectSetup(StringBuilder sb, ComponentInfo comp)
        {
            sb.AppendLine("            var scrollRect = go.AddComponent<ScrollRect>();");

            if (comp.properties.TryGetValue("horizontal", out var horizontal))
                sb.AppendLine($"            scrollRect.horizontal = {horizontal.ToString().ToLower()};");

            if (comp.properties.TryGetValue("vertical", out var vertical))
                sb.AppendLine($"            scrollRect.vertical = {vertical.ToString().ToLower()};");

            if (comp.properties.TryGetValue("movementType", out var movementType))
                sb.AppendLine($"            scrollRect.movementType = ScrollRect.MovementType.{movementType};");
        }

        private static void GenerateMaskSetup(StringBuilder sb, ComponentInfo comp)
        {
            sb.AppendLine("            var mask = go.AddComponent<Mask>();");

            if (comp.properties.TryGetValue("showMaskGraphic", out var show))
                sb.AppendLine($"            mask.showMaskGraphic = {show.ToString().ToLower()};");
        }

        private static void GenerateContentSizeFitterSetup(StringBuilder sb, ComponentInfo comp)
        {
            sb.AppendLine("            var fitter = go.AddComponent<ContentSizeFitter>();");

            if (comp.properties.TryGetValue("horizontalFit", out var hFit))
                sb.AppendLine($"            fitter.horizontalFit = ContentSizeFitter.FitMode.{hFit};");

            if (comp.properties.TryGetValue("verticalFit", out var vFit))
                sb.AppendLine($"            fitter.verticalFit = ContentSizeFitter.FitMode.{vFit};");
        }

        private static void GenerateLayoutGroupFlags(StringBuilder sb, ComponentInfo comp, string varName)
        {
            if (comp.properties.TryGetValue("childControlWidth", out var ccw))
                sb.AppendLine($"            {varName}.childControlWidth = {ccw.ToString().ToLower()};");

            if (comp.properties.TryGetValue("childControlHeight", out var cch))
                sb.AppendLine($"            {varName}.childControlHeight = {cch.ToString().ToLower()};");

            if (comp.properties.TryGetValue("childForceExpandWidth", out var cfew))
                sb.AppendLine($"            {varName}.childForceExpandWidth = {cfew.ToString().ToLower()};");

            if (comp.properties.TryGetValue("childForceExpandHeight", out var cfeh))
                sb.AppendLine($"            {varName}.childForceExpandHeight = {cfeh.ToString().ToLower()};");
        }

        private static void GenerateConnectSerializedFieldsMethod(StringBuilder sb, PrefabStructureSpec spec)
        {
            var componentType = spec.metadata.componentType;

            sb.AppendLine("        #region SerializedField Connection");
            sb.AppendLine();
            sb.AppendLine("        private static void ConnectSerializedFields(GameObject root)");
            sb.AppendLine("        {");
            sb.AppendLine($"            var component = root.GetComponent<{componentType}>();");
            sb.AppendLine("            if (component == null) return;");
            sb.AppendLine();
            sb.AppendLine("            var so = new SerializedObject(component);");
            sb.AppendLine();

            foreach (var field in spec.serializedFields)
            {
                if (field.isArray && field.arrayPaths?.Count > 0)
                {
                    // 배열 요소 타입 추출: PassButton[] -> PassButton, List`1 -> 첫 번째 요소에서 추출
                    var elementType = field.targetType;
                    if (string.IsNullOrEmpty(elementType))
                    {
                        // fieldType에서 추출 시도 (예: PassButton[], List<PassButton>)
                        var ft = field.fieldType;
                        if (ft.EndsWith("[]"))
                        {
                            elementType = ft.Substring(0, ft.Length - 2);
                        }
                        else if (ft.Contains("`"))
                        {
                            // List`1 등의 제네릭 타입은 지원하지 않음, GameObject로 폴백
                            elementType = null;
                        }
                    }

                    sb.AppendLine($"            // {field.fieldName} (array)");
                    sb.AppendLine(
                        $"            var {ToCamelCase(field.fieldName)}Prop = so.FindProperty(\"{field.fieldName}\");");
                    sb.AppendLine(
                        $"            {ToCamelCase(field.fieldName)}Prop.arraySize = {field.arrayPaths.Count};");

                    for (int i = 0; i < field.arrayPaths.Count; i++)
                    {
                        var path = field.arrayPaths[i];
                        if (!string.IsNullOrEmpty(elementType) && elementType != "GameObject")
                        {
                            sb.AppendLine(
                                $"            {ToCamelCase(field.fieldName)}Prop.GetArrayElementAtIndex({i}).objectReferenceValue = FindChild(root, \"{path}\")?.GetComponent<{elementType}>();");
                        }
                        else
                        {
                            // GameObject 타입이거나 타입을 알 수 없는 경우
                            sb.AppendLine(
                                $"            {ToCamelCase(field.fieldName)}Prop.GetArrayElementAtIndex({i}).objectReferenceValue = FindChild(root, \"{path}\");");
                        }
                    }

                    sb.AppendLine();
                }
                else if (!string.IsNullOrEmpty(field.targetPath))
                {
                    sb.AppendLine($"            // {field.fieldName}");

                    if (string.IsNullOrEmpty(field.targetType) || field.targetType == "GameObject")
                    {
                        sb.AppendLine(
                            $"            so.FindProperty(\"{field.fieldName}\").objectReferenceValue = FindChild(root, \"{field.targetPath}\");");
                    }
                    else
                    {
                        sb.AppendLine(
                            $"            so.FindProperty(\"{field.fieldName}\").objectReferenceValue = FindChild(root, \"{field.targetPath}\")?.GetComponent<{field.targetType}>();");
                    }

                    sb.AppendLine();
                }
            }

            sb.AppendLine("            so.ApplyModifiedPropertiesWithoutUndo();");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        private static GameObject FindChild(GameObject root, string path)");
            sb.AppendLine("        {");
            sb.AppendLine("            if (string.IsNullOrEmpty(path)) return root;");
            sb.AppendLine("            var t = root.transform.Find(path);");
            sb.AppendLine("            return t?.gameObject;");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        #endregion");
            sb.AppendLine();
        }

        private static void GenerateHelperMethods(StringBuilder sb)
        {
            sb.AppendLine("        #region Helpers");
            sb.AppendLine();
            sb.AppendLine("        private static GameObject CreateRoot(string name)");
            sb.AppendLine("        {");
            sb.AppendLine("            var root = new GameObject(name);");
            sb.AppendLine("            var rect = root.AddComponent<RectTransform>();");
            sb.AppendLine("            rect.anchorMin = Vector2.zero;");
            sb.AppendLine("            rect.anchorMax = Vector2.one;");
            sb.AppendLine("            rect.offsetMin = Vector2.zero;");
            sb.AppendLine("            rect.offsetMax = Vector2.zero;");
            sb.AppendLine("            root.AddComponent<CanvasGroup>();");
            sb.AppendLine("            return root;");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        private static GameObject CreateChild(GameObject parent, string name)");
            sb.AppendLine("        {");
            sb.AppendLine("            var child = new GameObject(name);");
            sb.AppendLine("            child.transform.SetParent(parent.transform, false);");
            sb.AppendLine("            return child;");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        private static RectTransform SetStretch(GameObject go)");
            sb.AppendLine("        {");
            sb.AppendLine("            var rect = go.GetComponent<RectTransform>();");
            sb.AppendLine("            if (rect == null) rect = go.AddComponent<RectTransform>();");
            sb.AppendLine("            rect.anchorMin = Vector2.zero;");
            sb.AppendLine("            rect.anchorMax = Vector2.one;");
            sb.AppendLine("            rect.offsetMin = Vector2.zero;");
            sb.AppendLine("            rect.offsetMax = Vector2.zero;");
            sb.AppendLine("            return rect;");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        #endregion");
        }

        #region Duplicate Name Detection

        /// <summary>
        /// 계층 구조에서 중복된 노드 이름을 수집합니다.
        /// </summary>
        private static HashSet<string> CollectDuplicateNames(HierarchyNode root)
        {
            var nameCount = new Dictionary<string, int>();
            CollectNodeNamesRecursive(root, nameCount);

            // 2번 이상 등장한 이름만 반환
            var duplicates = new HashSet<string>();
            foreach (var kvp in nameCount)
            {
                if (kvp.Value > 1)
                {
                    duplicates.Add(kvp.Key);
                }
            }

            return duplicates;
        }

        private static void CollectNodeNamesRecursive(HierarchyNode node, Dictionary<string, int> nameCount)
        {
            if (node == null) return;

            // 루트는 제외
            if (!string.IsNullOrEmpty(node.name))
            {
                var cleanName = SanitizeNodeName(node.name);
                if (nameCount.ContainsKey(cleanName))
                    nameCount[cleanName]++;
                else
                    nameCount[cleanName] = 1;
            }

            // 자식 노드 재귀 처리
            if (node.children != null)
            {
                foreach (var child in node.children)
                {
                    CollectNodeNamesRecursive(child, nameCount);
                }
            }
        }

        #endregion

        #endregion

        #region Utilities

        private static Dictionary<string, float> ExtractLayoutConstants(HierarchyNode node)
        {
            var constants = new Dictionary<string, float>();

            void Traverse(HierarchyNode n)
            {
                if (n.rect != null)
                {
                    var anchor = n.rect.anchor ?? "";

                    // 고정 높이/너비 추출
                    if (anchor.Contains("Stretch") && n.rect.sizeDelta != null)
                    {
                        if (Math.Abs(n.rect.sizeDelta[1]) > 1)
                        {
                            var constName = $"{ToConstantName(n.name)}_HEIGHT";
                            constants[constName] = Math.Abs(n.rect.sizeDelta[1]);
                        }
                    }
                    else if (n.rect.sizeDelta != null)
                    {
                        if (n.rect.sizeDelta[0] > 1)
                            constants[$"{ToConstantName(n.name)}_WIDTH"] = n.rect.sizeDelta[0];
                        if (n.rect.sizeDelta[1] > 1)
                            constants[$"{ToConstantName(n.name)}_HEIGHT"] = n.rect.sizeDelta[1];
                    }
                }

                foreach (var child in n.children ?? new List<HierarchyNode>())
                {
                    Traverse(child);
                }
            }

            Traverse(node);
            return constants;
        }

        private static string SimplifyColorName(string key, string hex)
        {
            // 해시 제거하고 의미있는 이름 생성
            if (key.StartsWith("Dark_")) return "BgDeep";
            if (key.StartsWith("Glass_")) return "BgGlass";
            if (key.StartsWith("Light_")) return "TextPrimary";
            if (key.StartsWith("Transparent_")) return "Transparent";

            // 색상별 이름
            if (hex.StartsWith("#00D4FF")) return "AccentPrimary";
            if (hex.StartsWith("#FF6B9D")) return "AccentSecondary";
            if (hex.StartsWith("#FFD700")) return "AccentGold";
            if (hex.StartsWith("#A855F7")) return "AccentPurple";

            return key.Split('_')[0];
        }

        private static string ParseHexColor(string hex)
        {
            if (ColorUtility.TryParseHtmlString(hex, out var color))
            {
                var r = (byte)(color.r * 255);
                var g = (byte)(color.g * 255);
                var b = (byte)(color.b * 255);
                var a = (byte)(color.a * 255);

                if (a == 255)
                    return $"new Color32({r}, {g}, {b}, 255)";
                else
                    return $"new Color32({r}, {g}, {b}, {a})";
            }

            return "Color.white";
        }

        private static string HexToColorCode(string hex)
        {
            // 알려진 색상 매칭
            if (hex == "#0A0A12FF") return "BgDeep";
            if (hex == "#1919D9D9" || hex == "#19192DD9") return "BgCard";
            if (hex == "#FFFFFFFF") return "TextPrimary";
            if (hex == "#FFFFFFB2") return "TextSecondary";
            if (hex == "#FFFFFF66") return "TextMuted";
            if (hex == "#00D4FFFF") return "AccentPrimary";
            if (hex == "#FF6B9DFF") return "AccentSecondary";
            if (hex == "#FFD700FF") return "AccentGold";
            if (hex == "#A855F7FF") return "AccentPurple";

            // 알 수 없는 색상은 직접 파싱
            return ParseHexColor(hex);
        }


        /// <summary>
        /// 노드 이름을 C# 식별자로 사용할 수 있도록 정제
        /// </summary>
        private static string SanitizeNodeName(string name)
        {
            if (string.IsNullOrEmpty(name)) return "Node";

            var sb = new StringBuilder();
            foreach (var c in name)
            {
                if (char.IsLetterOrDigit(c) || c == '_')
                    sb.Append(c);
            }

            var result = sb.ToString();
            return string.IsNullOrEmpty(result) ? "Node" : result;
        }

        private static string GetCreateMethodName(string nodeName, int index = 0)
        {
            var baseName = SanitizeNodeName(nodeName);

            // 인덱스가 0이면 고유한 이름, 1 이상이면 중복 이름
            if (index > 0)
            {
                return $"Create{baseName}_{index}";
            }

            return $"Create{baseName}";
        }

        private static string ToCamelCase(string name)
        {
            if (string.IsNullOrEmpty(name)) return name;

            // 공백 및 특수문자 제거
            name = name.Replace(" ", "");

            // 유효하지 않은 문자 제거
            var sb = new StringBuilder();
            foreach (var c in name)
            {
                if (char.IsLetterOrDigit(c) || c == '_')
                    sb.Append(c);
            }

            name = sb.ToString();

            if (string.IsNullOrEmpty(name)) return "item";

            // _prefix 제거
            if (name.StartsWith("_"))
                name = name.Substring(1);

            if (string.IsNullOrEmpty(name)) return "item";

            return char.ToLower(name[0]) + name.Substring(1);
        }

        private static string ToConstantName(string name)
        {
            var result = new StringBuilder();

            for (int i = 0; i < name.Length; i++)
            {
                var c = name[i];

                // 공백은 언더스코어로 변환
                if (char.IsWhiteSpace(c))
                {
                    if (result.Length > 0 && result[result.Length - 1] != '_')
                        result.Append('_');
                    continue;
                }

                // 유효하지 않은 문자는 건너뜀 (문자, 숫자, 언더스코어만 허용)
                if (!char.IsLetterOrDigit(c) && c != '_')
                {
                    continue;
                }

                // 대문자 앞에 언더스코어 추가 (연속 언더스코어 방지)
                if (char.IsUpper(c) && i > 0 && result.Length > 0 && result[result.Length - 1] != '_')
                    result.Append('_');

                result.Append(char.ToUpper(c));
            }

            // 연속 언더스코어 제거
            var final = result.ToString();
            while (final.Contains("__"))
            {
                final = final.Replace("__", "_");
            }

            return final.Trim('_');
        }

        private static string EscapeString(string str)
        {
            return str?.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n") ?? "";
        }

        private static string SaveBuilderCode(string prefabName, string code)
        {
            var outputPath = $"{OUTPUT_FOLDER}/{prefabName}PrefabBuilder.Generated.cs";

            // 기존 파일이 있으면 삭제 후 재생성 (meta 파일 문제 방지)
            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
                var metaPath = outputPath + ".meta";
                if (File.Exists(metaPath))
                    File.Delete(metaPath);
            }

            File.WriteAllText(outputPath, code);

            // 단일 파일만 Import (Refresh 대신)
            AssetDatabase.ImportAsset(outputPath, ImportAssetOptions.ForceUpdate);

            return outputPath;
        }

        #endregion
    }
}