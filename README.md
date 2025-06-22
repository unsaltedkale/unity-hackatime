> [!IMPORTANT]
> This plugin (& README) is _should_ be in a state where you can install it. I intend to add a little more before it's completely done.

# Unity Hackatime

A [Hackatime](https://hackatime.hackclub.com/) plugin for [Unity](https://unity.com). (Forked from [@Daniel-Geo](https://github.com/Daniel-Geo/unity-hackatime))

![A screen shot showing the Hackatime window in Unity.](https://github.com/user-attachments/assets/50eebaec-0248-40f2-90c9-a70cb15c5e88)


## About

This is a fork of [@Daniel-Geo's Hackatime Unity Plugin](https://github.com/Daniel-Geo/unity-hackatime) because I was inspired to add some extra features! 

Existing solutions didn't work for me (https://github.com/vladfaust/unity-wakatime, and other solutions lack an option for sending heartbeats to different URLs, such as to Hackatime), so I decided to fork vladfaust's solution to support Hackatime, Wakatime and others using the API URL.

The only feature that isn't here from Daniel-Geo's version is the API link field, because the API for the clocks would break if I allowed that.

Bonus features are:
- A total of four (4) updating clocks for:
   - Time coded on the currently open project today
   - Time coded across every IDE and project today
   - Total time coded on the currently open project
   - Total time coded across every IDE and project 
- A toggle to add any combination of clocks to the top (and stars to show which ones you've toggled on or off)
- You can then drag the window to the bottom of the Unity board and resize it to show the clocks you selected!
  
![A screen shot of an example Unity project with all four clocks displayed at the bottom.](https://github.com/user-attachments/assets/f640ab60-5190-40eb-97dc-e04125bd0ecc)



## Installation using the Unity Package Manager (Unity 2019.1+)

The [Unity Package Manager](https://docs.unity3d.com/Packages/com.unity.package-manager-ui@1.8/manual/index.html) (UPM) keeps package contents separate from your main project files.

1. In the Unity Package Manager, click on the plus sign in the top left corner
2. Select "Install package from git URL..."
3. Enter the following line:
   ```
    https://github.com/unsaltedkale/unity-hackatime.git?path=/Assets/com.hestia.hackatime
    ```
![A screenshot showing the "Install package from git URL..." window.](https://github.com/user-attachments/assets/6ff94df3-c11b-4cf3-8667-f49cc0740225)

4. Click on the install button

![A screenshot showing the Unity Hackatime package manager page.](https://github.com/user-attachments/assets/3ff57cbf-30ab-4a01-9747-87a0c669f7c0)





## Installation using the Unity Package Manager manifest.json (Unity 2018.1+)

1. Modify your project's `Packages/manifest.json` file by adding this line:

    ```json
    "com.hestia.hackatime": "https://github.com/unsaltedkale/unity-hackatime.git?path=/Assets/com.hestia.hackatime"
    ```

    Make sure it's a valid JSON file. You might need to add a comma somewhere. For example:

    ```json
    {
        "dependencies": {
            "com.unity.ads": "2.0.8",
            "com.hestia.hackatime": "https://github.com/unsaltedkale/unity-hackatime.git?path=/Assets/com.hestia.hackatime"
        }
    }
    ```

## Installation (all other Unity versions)

If you don't use the Unity Package Manager, you may copy the `Editor` folder from inside `Assets/com.hestia.hackatime` into your project's `Assets` folder.

## Setup

1. Run the Unity editor, go to `Window/HackaTime`, and insert your API key (grab one from [the hackatime set up page](https://hackatime.hackclub.com/my/wakatime_setup) or [your hackatime settings page](https://hackatime.hackclub.com/my/settings))
2. Press `Save Preferences`
3. If desired, drag the Hackatime window to the bottom of your Unity layout and resize it so that it only shows the clocks at the top.
4. Enjoy!

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

## Credits

__A huge thanks to [@Daniel-Geo](https://github.com/Daniel-Geo) for making the original Hackatime fork and helping me out__
__This plugin wouldn’t exist without [@taciturnaxolotl](https://github.com/taciturnaxolotl)__  
__A huge thanks to [@ImShyMike](https://github.com/ImShyMike) for helping with Hackatime API fetching__
