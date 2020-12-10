# Copy Lighting Settings for Unity

The "Copy Lighting Settings" package allows you to copy&paste lighting settings from one scene to another.

The need for this functionality is based on an Unity [forum thread](https://forum.unity.com/threads/copy-lighting-settings-from-scene-to-scene.308634/), where several community members wonder how to transfer the lighting settings from one scene to another.

I found this to be an interesting problem to solve and thus looked into it. In the video below, I show how the tool can be used.

[![](http://img.youtube.com/vi/-TQzrVn1kWM/0.jpg)](https://youtu.be/-TQzrVn1kWM "")

As of Unity 2020.1, Unity Technologies improved the lighting workflow, here is a quote from their release notes:
> GI: Added all lightmap settings as an asset. This will allow the user to share them between scenes or switch them out in an easy way.
https://unity3d.com/unity/alpha/2020.1.0a14

While they moved all lightmap settings to that asset, there are still some settings stored in the scene. The "Environment" settings for example. In 2020.1 and newer, this package copies the reference to the lighting settings asset and all the other settings that remained in the scene (except for the 'Sun' setting, because Unity doesn't allow references to objects from one scene in another).

# Installation in Unity 2019.3 and newer

In Unity's Package Manager, choose "Add package from git URL" and insert one of the Package URL's you can find below.

## Package URL's

| Version  |     Link      |
|----------|---------------|
| 1.4.0 | https://github.com/pschraut/UnityCopyLightingSettings.git#1.4.0 |
| 1.3.0 | https://github.com/pschraut/UnityCopyLightingSettings.git#1.3.0 |


# Installation in older Unity versions

If you use an older Unity version than 2019.3, the Package Manager might not exist or might not have git support yet.

In this case, I believe the easiest way is to download the `Editor/CopyLightingSettings.cs` file from this repository and save it in your project under `Assets/Editor/CopyLightingSettings.cs`.
