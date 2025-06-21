using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


namespace WakaTime
{
  public class Window : EditorWindow
  {
    private string _apiKey = "";
    private string _projectName = "";
    private bool _enabled = true;
    private bool _debug = true;

    private bool _needToReload;

    const string DASHBOARD_URL = "https://hackatime.hackclub.com/";

    private const string URL_PREFIX = "https://hackatime.hackclub.com/api/hackatime/v1/";

    public string textToDisplayForTotal;
    public string textToDisplayForProjectTotal;
    public string _projectDisplayName;
    

    [MenuItem("Window/HackaTime")]
    static void Init()
    {
      Window window = (Window)GetWindow(typeof(Window), false, "HackaTime");
      window.Show();
    }

    void OnGUI()
    {
      var request = UnityWebRequest.Get(URL_PREFIX + "users/current/statusbar/today?api_key=" + _apiKey);
      request.downloadHandler = new DownloadHandlerBuffer();

      request.SendWebRequest().completed +=
        operation =>
        {
          if (request.downloadHandler.text == string.Empty)
          {
            Debug.LogWarning(
              "<Hackatime Clock> offline");
            return;
          }

          if (_debug)
            Debug.Log("<Hackatime Clock> Got response from (" + request.uri + ") \n" + request.downloadHandler.text);


      
      var json = request.downloadHandler.text;

          string jsonString = json.ToString();
          int found = jsonString.IndexOf("m");
          textToDisplayForTotal = (jsonString.Substring(32, found +1 -32));
          
          if (_debug)
          Debug.Log("textToDisplayForTotal: " + textToDisplayForTotal);

        };

      var request2 = UnityWebRequest.Get(URL_PREFIX + "users/stats?api_key=" + _apiKey);
      request2.downloadHandler = new DownloadHandlerBuffer();

      request2.SendWebRequest().completed +=
        operation =>
        {
          if (request2.downloadHandler.text == string.Empty)
          {
            Debug.LogWarning(
              "<Hackatime Clock> offline");
            return;
          }

          if (_debug)
            Debug.Log("HERE <Hackatime Clock> Got response from (" + request2.uri + ") \n" + request.downloadHandler.text);
        };

      if (_projectName.Length > 20)
      {
        _projectDisplayName = _projectName.Substring(0, 20);
        _projectDisplayName += "...";
      }

      else
        _projectDisplayName = _projectName;

      //textToDisplayForProjectTotal = ?

      EditorGUILayout.Space();
      EditorGUILayout.LabelField("⏱ " + _projectDisplayName, "textToDisplayForProjectTotal");
    
      EditorGUILayout.LabelField("⏱ Total Time Today", textToDisplayForTotal);
      EditorGUILayout.Space();

      GUIContent content = new GUIContent();
      content.text = "Your [Total Time] is your time spent coding across all IDE's and projects today.";
      EditorGUILayout.HelpBox(content.text, MessageType.Info);

      _enabled = EditorGUILayout.Toggle("Enable HackaTime", _enabled);
      _apiKey = EditorGUILayout.TextField("API key", _apiKey);

      

      EditorGUILayout.LabelField("Project name", _projectName);

      if (GUILayout.Button("Change project name"))
      {
        ProjectEditWindow.Display();
        _needToReload = true;
      }

      _debug = EditorGUILayout.Toggle("Debug", _debug);

      EditorGUILayout.BeginHorizontal();

      if (GUILayout.Button("Save Preferences"))
      {
        EditorPrefs.SetString(Plugin.API_KEY_PREF, _apiKey);
        EditorPrefs.SetBool(Plugin.ENABLED_PREF, _enabled);
        EditorPrefs.SetBool(Plugin.DEBUG_PREF, _debug);
        Plugin.Initialize();
      }

      if (GUILayout.Button("Open Dashboard"))
        Application.OpenURL(DASHBOARD_URL);

      EditorGUILayout.EndHorizontal();
    }


    void OnFocus()
    {
      if (_needToReload)
      {
        Plugin.Initialize();
        _needToReload = false;
      }

      if (EditorPrefs.HasKey(Plugin.API_KEY_PREF))
        _apiKey = EditorPrefs.GetString(Plugin.API_KEY_PREF);
      if (EditorPrefs.HasKey(Plugin.ENABLED_PREF))
        _enabled = EditorPrefs.GetBool(Plugin.ENABLED_PREF);
      if (EditorPrefs.HasKey(Plugin.DEBUG_PREF))
        _debug = EditorPrefs.GetBool(Plugin.DEBUG_PREF);

      _projectName = Plugin.ProjectName;
    }
  }
}