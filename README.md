# unity-hackatime

A [HackaTime](https://hackatime.hackclub.com/) plugin for [Unity](https://unity.com).

![Screenshot](https://github.com/user-attachments/assets/421a59c9-8375-4ffa-96ef-270ef149b4ae)

## About

Existing solutions didn't work for me (https://github.com/vladfaust/unity-wakatime, and other solutions lack an option for sending heartbeats to different URLs, such as to Hackatime), so I decided to fork vladfaust's solution to support Hackatime instead of Wakatime.

## Installation using the Unity Package Manager (Unity 2018.1+)

The [Unity Package Manager](https://docs.unity3d.com/Packages/com.unity.package-manager-ui@1.8/manual/index.html) (UPM) keeps package contents separate from your main project files.

1. Modify your project's `Packages/manifest.json` file by adding this line:

    ```json
    "com.daniel-geo.unityhackatime": "https://github.com/daniel-geo/unity-hackatime.git#package"
    ```

    Make sure it's a valid JSON file. For example:

    ```json
    {
        "dependencies": {
            "com.unity.ads": "2.0.8",
            "com.daniel-geo.unityhackatime": "https://github.com/daniel-geo/unity-hackatime.git#package"
        }
    }
    ```

## Installation (all other Unity versions)

If you don't use the Unity Package Manager, you may copy the `Editor` folder from inside `Assets/com.daniel-geo.unityhackatime` into your project's `Assets` folder.

## Setup

1. Run the Unity editor, go to `Window/HackaTime`, and insert your API key (grab one from https://hackatime.hackclub.com/my/wakatime_setup)
2. Press `Save Preferences`
3. Enjoy!

## Usage

The plugin will automatically send heartbeats to HackaTime after the following events:

* DidReloadScripts
* EditorApplication.playModeStateChanged
* EditorApplication.contextualPropertyMenu
* EditorApplication.hierarchyWindowChanged
* EditorSceneManager.sceneSaved
* EditorSceneManager.sceneOpened
* EditorSceneManager.sceneClosing
* EditorSceneManager.newSceneCreated
