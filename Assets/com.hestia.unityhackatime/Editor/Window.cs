using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;


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

    public string textToDisplayForToday;
    public string textToDisplayForProjectToday;
    public string textToDisplayForProjectTotal;
    public string textToDisplayForTotal;
    public string _projectDisplayName;
    public string todaysDateyyyyMd;
    public bool projectTimeTodayAtTop; 
    

    [MenuItem("Window/HackaTime")]
    static void Init()
    {
      Window window = (Window)GetWindow(typeof(Window), false, "HackaTime");
      window.Show();
    }

    void OnGUI()
    {
      if (projectTimeTodayAtTop)
        EditorGUILayout.LabelField("⏱ Project Time Today", textToDisplayForProjectToday);

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
              "<Hackatime Clock> offline");
            return;
          }

          if (_debug)
            Debug.Log("<Hackatime Clock> Got response from (" + request.uri + ") \n" + request.downloadHandler.text);



          var json = request.downloadHandler.text;

          string jsonString = json.ToString();
          int found = jsonString.IndexOf("text");
          jsonString = jsonString.Substring(found + 4 + 3);
          found = jsonString.IndexOf("total_seconds");
          textToDisplayForToday = jsonString.Substring(0, found - 3);

          if (_debug)
            Debug.Log("textToDisplayForToday: " + textToDisplayForToday);

        };


      todaysDateyyyyMd = DateTime.Now.ToString("yyyy-M-d");

      Debug.Log(todaysDateyyyyMd);

      //https://hackatime.hackclub.com/api/v1/users/my/stats?start_date=2025-6-21&features=projects&limit=100

      var request2 = UnityWebRequest.Get("https://hackatime.hackclub.com/api/v1/" + "users/my/stats?start_date=" + todaysDateyyyyMd + "&features=projects&limit=100");
      request2.SetRequestHeader("Authorization", "Bearer " + _apiKey);
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
            Debug.Log("HERE <Hackatime Clock> Got response from (" + request2.uri + ") \n" + request2.downloadHandler.text);

          var jsonString2 = request2.downloadHandler.text;
          int found2 = jsonString2.IndexOf(_projectName);
          jsonString2 = jsonString2.Substring(found2);
          found2 = jsonString2.IndexOf("text");
          jsonString2 = jsonString2.Substring(found2 + 4 + 3);
          found2 = jsonString2.IndexOf("hours");
          jsonString2 = jsonString2.Substring(0, found2 - 3);

          textToDisplayForProjectToday = jsonString2;
          Debug.Log("textToDisplayForProjectToday: " + textToDisplayForProjectToday);

        };

      //https://hackatime.hackclub.com/api/v1/users/my/stats?features=projects&limit=100

      var request3 = UnityWebRequest.Get("https://hackatime.hackclub.com/api/v1/" + "users/my/stats?features=projects&limit=100");
      request3.SetRequestHeader("Authorization", "Bearer " + _apiKey);
      request3.downloadHandler = new DownloadHandlerBuffer();

      request3.SendWebRequest().completed +=
        operation =>
        {
          if (request3.downloadHandler.text == string.Empty)
          {
            Debug.LogWarning(
              "<Hackatime Clock> offline");
            return;
          }

          if (_debug)
            Debug.Log("HERE <Hackatime Clock> Got response from (" + request3.uri + ") \n" + request3.downloadHandler.text);

          var jsonString3 = request3.downloadHandler.text;
          int found3 = jsonString3.IndexOf(_projectName);
          jsonString3 = jsonString3.Substring(found3);
          found3 = jsonString3.IndexOf("text");
          jsonString3 = jsonString3.Substring(found3 + 4 + 3);
          found3 = jsonString3.IndexOf("hours");
          jsonString3 = jsonString3.Substring(0, found3 - 3);

          textToDisplayForProjectTotal = jsonString3;
          Debug.Log("textToDisplayForProjectTotal: " + textToDisplayForProjectTotal);

          var jsonString4 = request3.downloadHandler.text;
          var found4 = jsonString4.IndexOf("human_readable_total");
          jsonString4 = jsonString4.Substring(found4 + 20 + 3);
          found4 = jsonString4.IndexOf("human_readable_daily_average");
          jsonString4 = jsonString4.Substring(0, found4 - 3);

          textToDisplayForTotal = jsonString4;


        };

      EditorGUILayout.Space();

      EditorGUILayout.LabelField("⏱ Project Time Today", textToDisplayForProjectToday);

      projectTimeTodayAtTop = EditorGUILayout.Toggle("Add to Top", projectTimeTodayAtTop);

      EditorGUILayout.LabelField("⏱ Coding Time Today", textToDisplayForToday);

      EditorGUILayout.LabelField("⏱ Project Time Total", textToDisplayForProjectTotal);

      EditorGUILayout.LabelField("⏱ Coding Time Total", textToDisplayForTotal);
      EditorGUILayout.Space();

      GUILayout.ExpandHeight(true);
      GUIContent content = new GUIContent();
      content.text = "[Project Time Today] is the amount of time you spent coding <" + _projectName + "> today.\n[Total Time Today] is your amount of time spent coding across all IDE's and projects today.\n[Project Time Total] is the amount of total amount of time you spent coding <" + _projectName + "> since you started it.\n[Coding Time Total] is your total amount of time spent coding across all IDE's and projects since you began.";
      EditorGUILayout.HelpBox(content.text, MessageType.Info);
      GUILayout.ExpandHeight(false);
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