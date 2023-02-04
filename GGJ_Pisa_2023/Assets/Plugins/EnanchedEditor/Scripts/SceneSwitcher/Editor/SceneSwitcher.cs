using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WarcosSceneSwitcher
{
	public struct SceneWrapper
	{
		public int buildIndex;
		public bool isInBuild;
		public string sceneName;
		public string scenePath;
		public DateTime lastModified;

		public SceneWrapper(Scene scene)
		{
			buildIndex = scene.buildIndex;
			isInBuild = buildIndex != -1;
			sceneName = scene.name;
			scenePath = scene.path;
			lastModified = File.GetLastWriteTime(scenePath);
		}
	}
	public class SceneSwitcher : EditorWindow
	{

		private float separetorHeight = 3;
		private static List<SceneWrapper> projectScenes;
		private string settingsPath;
		private static Vector2 minSize = new(470f, 290f);
		private static Vector2 maxSize = new(470f, 4000f);

		private Vector2 scrollPos = Vector2.zero;
		private Color[] colors;
		private static EditorWindow window;

		[MenuItem("Warco's Tools/Scene Switcher")]
		private static void ShowWindow()
		{
			window = GetWindow<SceneSwitcher>();
			window.titleContent = new GUIContent("Warco's Scene Switcher");
			window.maxSize = maxSize;
			window.minSize = minSize;
			window.Show();
		}

		private void OnEnable()
		{
			projectScenes = new List<SceneWrapper>();
			GetScenes();
		}

		private void OnGUI()
		{
			scrollPos = GUILayout.BeginScrollView(scrollPos, false, false);
			if (projectScenes.Count > 0)
			{
				foreach (SceneWrapper scene in projectScenes)
				{
					DrawSlot(scene);
				}
			}
			if (GUILayout.Button("refresh"))
			{
				GetScenes();
			}
			GUILayout.EndScrollView();
		}

		private void GetScenes()
		{
			projectScenes = new List<SceneWrapper>();
			var sceneGuids = AssetDatabase.FindAssets("t:scene");
			foreach (string guid in sceneGuids)
			{
				var path = AssetDatabase.GUIDToAssetPath(guid);
				Scene data = EditorSceneManager.OpenScene(path, OpenSceneMode.AdditiveWithoutLoading);
				projectScenes.Add(new SceneWrapper(data));
				if (EditorSceneManager.GetActiveScene() != data)
				{
					EditorSceneManager.CloseScene(data, true);
				}
			}
			var temp = projectScenes.OrderByDescending(scene => scene.isInBuild).ThenBy(scene => scene.buildIndex).ThenBy(scene => scene.sceneName);
			projectScenes = new List<SceneWrapper>(temp);
		}

		private void DrawSlot(SceneWrapper sceneWrapper)
		{
			EditorGUILayout.BeginHorizontal();

			#region Slot Left Side

			EditorGUILayout.BeginVertical();
			EditorGUILayout.Space(10);
			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button
			    (
				    $"{sceneWrapper.sceneName}",
				    new GUIStyle(GUI.skin.button)
				    {
					    alignment = TextAnchor.MiddleCenter,
					    fontStyle = FontStyle.Bold,
					    fontSize = 20,
					    normal = new GUIStyleState
					    {
						    textColor = Color.white
					    }
				    },
				    GUILayout.Width(200),
				    GUILayout.Height(50),
				    GUILayout.ExpandWidth(true)
			    )
			   )
			{
				if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
				{
					EditorSceneManager.SaveOpenScenes();
				}
				EditorSceneManager.OpenScene(sceneWrapper.scenePath);
			}
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.EndVertical();

			#endregion

			#region Slot Right Side

			EditorGUILayout.BeginVertical();
			string textToDisplay = sceneWrapper.isInBuild ? $"Scene in build at index {sceneWrapper.buildIndex}" : "Scene out of build";
			Color colorOfText = sceneWrapper.isInBuild ? Color.green : Color.red;
			EditorGUILayout.LabelField(textToDisplay, new GUIStyle(GUI.skin.box) { alignment = TextAnchor.MiddleCenter, normal = new GUIStyleState { textColor = colorOfText } }, GUILayout.ExpandWidth(true));
			EditorGUILayout.LabelField(Directory.GetParent(sceneWrapper.scenePath).Name, new GUIStyle(GUI.skin.box) { alignment = TextAnchor.MiddleCenter, normal = new GUIStyleState{textColor = Color.white}}, GUILayout.ExpandWidth(true));
			EditorGUILayout.LabelField(sceneWrapper.lastModified.ToString(CultureInfo.InvariantCulture), new GUIStyle(GUI.skin.box) { alignment = TextAnchor.MiddleCenter, normal = new GUIStyleState{textColor = Color.white} }, GUILayout.ExpandWidth(true));
			EditorGUILayout.EndVertical();

			#endregion

			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space(separetorHeight);
			var separator = EditorGUILayout.GetControlRect(false, separetorHeight);
			EditorGUI.DrawRect(separator, Color.black);
		}
	}

}