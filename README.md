# UnityPackage-Scene-Reference
Custom Package to save a reference to the name of a scene and update it easily when scene name changes.

This tool will help you reference scenes without worrying about changing the name of the scene you want to reference if the name of the scene changes.

Just right-click the scene you want to reference then go to Create -> Scriptable Objects -> Scene Reference. This will automatically create a ReferenceToScene Scriptable Object named after your scene. It contains the name of the scene and it's GUID, which is used to check when the scene name changes and update this Scriptable Object.

You can also drag and drop another scene to this Scriptable Object to make it reference another scene so you don't have to replace it wherever it's used.

In case the name was deleted or modified, you can recover it by clicking the "Get Scene Name" button.

Any suggestions to improve the tool or the code is welcome!

