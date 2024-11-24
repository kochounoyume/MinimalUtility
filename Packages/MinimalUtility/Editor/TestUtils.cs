﻿using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.Build.Player;

namespace MinimalUtility.Editor
{
    /// <summary>
    /// テスト関連のユーティリティ.
    /// </summary>
    public static class TestUtils
    {
        /// <summary>
        /// 指定したビルドターゲットでスクリプトをコンパイルし、成功したかどうかを返す.
        /// </summary>
        /// <param name="buildTarget">テスト対象環境.</param>
        /// <param name="outputPath">出力先パス.</param>
        /// <param name="scriptingDefines">スクリプト条件コンパイル定義があれば登録.</param>
        /// <returns>成功したかどうか.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SuccessCompile(BuildTarget buildTarget, string outputPath = "Temp/TestOutput", params string[] scriptingDefines)
        {
            ScriptCompilationSettings settings = new ()
            {
                extraScriptingDefines = scriptingDefines,
                group = BuildPipeline.GetBuildTargetGroup(buildTarget),
                target = buildTarget,
            };
            try
            {
                ScriptCompilationResult result = PlayerBuildInterface.CompilePlayerScripts(settings, outputPath);
                return result is { assemblies: { Count: > 0 }, typeDB: not null };
            }
            catch
            {
                throw;
            }
            finally
            {
                if (Directory.Exists(outputPath))
                {
                    Directory.Delete(outputPath, true);
                }
            }
        }
    }
}