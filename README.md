# ObjectGUI
In-game object value editor for Unity

Unity offers a fantastic editor. When working on projects that target tablets and mobile devices we often have the need to change and tweak parameters at run time whithout having to re-deploy the whole project.

These scripts offer this functionality. Using the legacy GUI system they recreate a simplified version of the unity editor for a set of Monobehaviours accessible via the in-game UI. The UI is toggled by a hidden button.

The current implementation offers a subset of serializable fields: int, float, bool, string, Vector2, Vector3 and Color.

## Usage

* ObjectGUI
   Contains the link to the monobehaviour to be drawn.

* ObjectGUIManager  
   Defines the UI area and the placement of the hidden toggle button. This script will gather the ObjectGUIs attached to the same object and draw their values in the defined UI area.



Please refer to the example scene contained in the repository for more information

![alt text](https://raw.githubusercontent.com/weezah/ObjectGUI/master/screenshot.png "Screenshot")
