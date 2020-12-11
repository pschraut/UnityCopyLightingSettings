# Copy Lighting Settings for Unity

The "Copy Lighting Settings" package allows you to copy&paste lighting settings from one scene to another.

The need for this functionality is based on an Unity [forum thread](https://forum.unity.com/threads/copy-lighting-settings-from-scene-to-scene.308634/), where several community members wonder how to transfer the lighting settings from one scene to another.

I found this to be an interesting problem to solve and thus looked into it. In the video below, I show how the tool can be used.

[![](http://img.youtube.com/vi/-TQzrVn1kWM/0.jpg)](https://youtu.be/-TQzrVn1kWM "")

# How it works

Copying the "Sun Source" setting is a special case that is explained below, 
all other settings are just copied as you would expect.

"Sun Source" is a reference to a Light Component in the scene, 
but Unity does not support cross scene references. 
Therefore the tool does not copy&paste the reference, 
but it looks for a Light with the same name (upper/lower-case ignored) in the target scene.
It looks first for active lights, then inactive lights.

If a Light with the same name exists in the target scene and there is 
currently no "Sun Source" assigned, then the tool sets it to the Light found in that scene.

# Installation in Unity 2019.3 and newer

In Unity's Package Manager, choose "Add package from git URL" and insert one of the Package URL's you can find below.

## Package URL's

| Version  |     Link      |
|----------|---------------|
| 1.5.0 | https://github.com/pschraut/UnityCopyLightingSettings.git#1.5.0 |
| 1.4.0 | https://github.com/pschraut/UnityCopyLightingSettings.git#1.4.0 |
| 1.3.0 | https://github.com/pschraut/UnityCopyLightingSettings.git#1.3.0 |


# Installation in older Unity versions

If you use an older Unity version than 2019.3, the Package Manager might not exist or might not have git support yet.

In this case, I believe the easiest way is to download the `Editor/CopyLightingSettings.cs` file from this repository and save it in your project under `Assets/Editor/CopyLightingSettings.cs`.
