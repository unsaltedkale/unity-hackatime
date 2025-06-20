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

    public string textToDisplay;

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Data
    {
      public GrandTotal grand_total { get; set; }
    }

    public class GrandTotal
    {
      public string text { get; set; }
      public int total_seconds { get; set; }
    }

    public class Root
    {
      public Data data { get; set; }
    }

    [MenuItem("Window/HackaTime")]
    static void Init()
    {
      Window window = (Window)GetWindow(typeof(Window), false, "HackaTime");
      window.Show();
    }

    void OnGUI()
    {
      Debug.Log("hello!!");
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

      var request = UnityWebRequest.Get(URL_PREFIX + "users/current/statusbar/today?api_key=" + _apiKey);
      request.downloadHandler = new DownloadHandlerBuffer();

      request.SendWebRequest().completed +=
        operation =>
        {
          if (request.downloadHandler.text == string.Empty)
          {
            Debug.LogWarning(
              "<Hestia> offline");
            return;
          }

          if (_debug)
            Debug.Log("<Hestia> Got response from (" + request.uri + ") \n" + request.downloadHandler.text);

          if (1 == 2)
          {
            Debug.LogWarning("<Hestia> what the hell");
          }
          else
          {
            if (_debug) Debug.Log("<Hestia> Sent heartbeat!");
          }


      if (request == null)
      {
        Debug.Log("Yell");
      }
      var json = request.downloadHandler.text;

      Debug.Log(json.ToString());

      Root myDeserializedClass = JsonUtility.FromJson<Root>(json);

      Debug.Log(myDeserializedClass.data.grand_total.text);

      Debug.Log("textToDisplay: " + textToDisplay);

      //Debug.Log(response);
        };


      EditorGUILayout.LabelField("⏱ Total Time", "ugh");
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