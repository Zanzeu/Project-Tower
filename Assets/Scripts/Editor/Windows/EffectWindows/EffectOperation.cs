using System.IO;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Tower.Editor
{
    public class EffectOperation
    {
        [SerializeField, LabelText("效果器名称")]
        private string scriptsName;

        private const string outputPath = "Assets/Scripts/Runtime/Gameplay/Effect";
        private const string dicPath = "Assets/Scripts/Runtime/Gameplay/Effect/Base/EffectConfigDic.cs";

        [Button("生成效果器脚本", ButtonSizes.Medium)]
        private void CreateEffectScript()
        {
            if (string.IsNullOrEmpty(scriptsName))
            {
                EditorUtility.DisplayDialog("错误", "请先输入效果器名称", "确定");
                return;
            }

            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);

            string fileName = $"Effect_{scriptsName}.cs";
            string filePath = Path.Combine(outputPath, fileName);

            if (File.Exists(filePath))
            {
                Debug.LogError($"已存在相同名称的效果器脚本：{filePath}");
                return;
            }

            string content = GenerateScript(scriptsName);
            File.WriteAllText(filePath, content);

            UpdateEffectConfigDic();

            AssetDatabase.Refresh();
            scriptsName = null;
        }

        private string GenerateScript(string name)
        {
            return
$@"using UnityEngine;
using Sirenix.OdinInspector;

namespace Tower.Runtime.Gameplay
{{
    /// <summary>
    /// 效果器：{name}
    /// 自动生成于 {System.DateTime.Now:yyyy-MM-dd HH:mm:ss}
    /// </summary>
    public class Effect_{name} : EffectBase
    {{
        private readonly EffectParam_{name} _p;

        public Effect_{name}(IEffectParam param) : base(param)
        {{
            _p = (EffectParam_{name})param;
        }}

        public override void OnTrigger(object caster, object target)
        {{

        }}
    }}

    [System.Serializable]
    public class EffectParam_{name} : IEffectParam
    {{

    }}
}}";
        }

        private void UpdateEffectConfigDic()
        {
            var paramTypes = new List<string>();

            foreach (string file in Directory.GetFiles(outputPath, "Effect_*.cs", SearchOption.TopDirectoryOnly))
            {
                string content = File.ReadAllText(file);
                var match = Regex.Match(content, @"class\s+EffectParam_(\w+)");
                if (match.Success)
                {
                    paramTypes.Add(match.Groups[1].Value);
                }
            }

            var sb = new StringBuilder();
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine();
            sb.AppendLine("namespace Tower.Runtime.Gameplay");
            sb.AppendLine("{");
            sb.AppendLine("    public static class EffectConfigDic");
            sb.AppendLine("    {");
            sb.AppendLine("        public static readonly Dictionary<string, IEffectParam> Dic = new()");
            sb.AppendLine("        {");

            foreach (string name in paramTypes)
            {
                sb.AppendLine($"            {{ \"{name}\", new EffectParam_{name}() }},");
            }

            sb.AppendLine("        };");
            sb.AppendLine("    }");
            sb.AppendLine("}");

            File.WriteAllText(dicPath, sb.ToString(), Encoding.UTF8);
        }
    }
}
