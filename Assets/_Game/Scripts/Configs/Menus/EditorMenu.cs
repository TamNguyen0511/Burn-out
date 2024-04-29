using _Game.Scripts.Configs.Editors;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace _Game.Scripts.Configs.Menus
{
    public class EditorMenu : OdinMenuEditorWindow
    {
        private MaterialConfigs _materialConfigs;

        /// <summary>
        /// Create editor menu and open it
        /// </summary>
        [MenuItem("Gadgame/Editor Menu")]
        private static void OpenWindow()
        {
            GetWindow<EditorMenu>().Show();
        }

        #region Implement from OdinMenuEditorWindow

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree()
            {
                Selection =
                {
                    SupportsMultiSelect = false
                }
            };

            _materialConfigs =
                AssetDatabase.LoadAssetAtPath<MaterialConfigs>(
                    "Assets/_Game/Databases/SO Database/Configs/Test Kitchen Config.asset");
            tree.Add("Test Material config", _materialConfigs);

            return tree;
        }

        #endregion
    }
}