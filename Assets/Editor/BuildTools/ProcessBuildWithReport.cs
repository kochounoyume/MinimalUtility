using System.IO;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

/// <summary>
/// ビルド前後に実行される処理の実装クラス.
/// </summary>
public sealed class ProcessBuildWithReport : IPreprocessBuildWithReport, IPostprocessBuildWithReport
{
    private const string StyleCopAnalyzersPath = "Assets/StyleCop.Analyzers.dll";
    private const string MinimalUtilityPath = "Packages/MinimalUtility/StyleCop.Analyzers.dll";
    private const string StyleCopAnalyzersMetaPath = "Assets/StyleCop.Analyzers.dll.meta";
    private const string MinimalUtilityMetaPath = "Packages/MinimalUtility/StyleCop.Analyzers.dll.meta";
    private const string RuntimeRulesetPath = "Packages/MinimalUtility/Runtime/MinimalUtility.Runtime.ruleset";
    private const string RuntimeRulesetMetaPath = "Packages/MinimalUtility/Runtime/MinimalUtility.Runtime.ruleset.meta";
    private const string EditorRulesetPath = "Packages/MinimalUtility/Editor/MinimalUtility.Editor.ruleset";
    private const string EditorRulesetMetaPath = "Packages/MinimalUtility/Editor/MinimalUtility.Editor.ruleset.meta";
    private const string VContainerRulesetPath = "Packages/MinimalUtility/Runtime/VContainer/MinimalUtility.VContainer.ruleset";
    private const string VContainerRulesetMetaPath = "Packages/MinimalUtility/Runtime/VContainer/MinimalUtility.VContainer.ruleset.meta";

    /// <inheritdoc/>
    int IOrderedCallback.callbackOrder => 0; // 処理中で最高優先順位

    /// <inheritdoc/>
    void IPreprocessBuildWithReport.OnPreprocessBuild(BuildReport report) => UniTask.Void(static async () =>
    {
        await (MoveFileAsync(StyleCopAnalyzersPath, MinimalUtilityPath),
            MoveFileAsync(StyleCopAnalyzersMetaPath, MinimalUtilityMetaPath),
            UniTask.RunOnThreadPool(static async () =>
            {
                const string defaultRulesetPath = "Assets/Default.ruleset";

                // Default.rulesetの記述を取得する
                var defaultRuleset = await File.ReadAllTextAsync(defaultRulesetPath);

                await (CreateRulesetFileAsync(RuntimeRulesetPath, defaultRuleset),
                    CreateRulesetFileAsync(EditorRulesetPath, defaultRuleset),
                    CreateRulesetFileAsync(VContainerRulesetPath, defaultRuleset));
            }));

        // 最後にRefreshしてmetaファイルを生成させる
        AssetDatabase.Refresh();
    });

    /// <inheritdoc/>
    void IPostprocessBuildWithReport.OnPostprocessBuild(BuildReport report) => UniTask.Void(static async () =>
    {
        await (MoveFileAsync(MinimalUtilityPath, StyleCopAnalyzersPath),
            MoveFileAsync(MinimalUtilityMetaPath, StyleCopAnalyzersMetaPath),
            DeleteFileAsync(RuntimeRulesetPath),
            DeleteFileAsync(RuntimeRulesetMetaPath),
            DeleteFileAsync(EditorRulesetPath),
            DeleteFileAsync(EditorRulesetMetaPath),
            DeleteFileAsync(VContainerRulesetPath),
            DeleteFileAsync(VContainerRulesetMetaPath));
        AssetDatabase.Refresh();
    });

    private static async UniTask MoveFileAsync(string sourcePath, string destinationPath)
    {
        try
        {
            await UniTask.SwitchToThreadPool();
            if (File.Exists(sourcePath) && !File.Exists(destinationPath))
            {
                File.Move(sourcePath, destinationPath);
            }
        }
        finally
        {
            await UniTask.SwitchToMainThread();
        }
    }

    private static async UniTask CreateRulesetFileAsync(string sourcePath, string writeText)
    {
        try
        {
            await UniTask.SwitchToThreadPool();
            if (!File.Exists(sourcePath))
            {
                await File.WriteAllTextAsync(sourcePath, writeText);
            }
        }
        finally
        {
            await UniTask.SwitchToMainThread();
        }
    }

    private static async UniTask DeleteFileAsync(string path)
    {
        try
        {
            await UniTask.SwitchToThreadPool();
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        finally
        {
            await UniTask.SwitchToMainThread();
        }
    }
}
