using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Linq;
using System.IO;

#if UNITY_EDITOR
public class SceneLoaderWindow : EditorWindow
{
    private const string SEARCH_PATH = "Assets/_Project/Scenes";
    private const string HEADER_MENU_ITEM = "Tools/Scene Loader";
    private const string HEADER_WINDOW = "Scene Loader";
    private const string HEADER_AVAILABLE_SCENE = "Available Scenes";
    private const string HEADER_SEARCH = "Search:";
    private const string SEARCH_PATTERN = "*.unity";
    private string _searchQuery = string.Empty;

    [MenuItem(HEADER_MENU_ITEM)]
    public static void ShowWindow() => 
        GetWindow<SceneLoaderWindow>(HEADER_WINDOW);

    private void OnGUI()
    {
        GUILayout.Label(HEADER_AVAILABLE_SCENE, EditorStyles.boldLabel);

        _searchQuery = EditorGUILayout.TextField(HEADER_SEARCH, _searchQuery);

        string[] scenePaths = Directory.GetFiles(SEARCH_PATH, SEARCH_PATTERN, SearchOption.AllDirectories)
            .Where(path => string.IsNullOrEmpty(_searchQuery) ||
                Path.GetFileNameWithoutExtension(path).ToLower().Contains(_searchQuery.ToLower()))
            .ToArray();

        foreach (string scenePath in scenePaths)
        {
            string sceneName = Path.GetFileNameWithoutExtension(scenePath);

            if (GUILayout.Button(sceneName))
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    EditorSceneManager.OpenScene(scenePath);
            }
        }
    }
}
#endif