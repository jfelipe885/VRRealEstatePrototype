/************************************************************************************

Copyright   :   Copyright 2014 Oculus VR, LLC. All Rights reserved.

Licensed under the Oculus VR Rift SDK License Version 3.3 (the "License");
you may not use the Oculus VR Rift SDK except in compliance with the License,
which is provided at the time of installation or download, or which
otherwise accompanies this software in either electronic or hard copy form.

You may obtain a copy of the License at

http://www.oculus.com/licenses/LICENSE-3.3

Unless required by applicable law or agreed to in writing, the Oculus VR SDK
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

************************************************************************************/

using UnityEngine;
using UnityEditor;

/// <summary>
/// Allows Oculus to build apps from the command line.
/// </summary>
partial class OculusBuildApp
{
	static void SetPCTarget()
	{
		if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.StandaloneWindows)
		{
      //JFR: 6/27/2017 updating obsolete call
			//EditorUserBuildSettings.SwitchActiveBuildTarget (BuildTarget.StandaloneWindows);
      EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows);
      //...JFR
    }
#if UNITY_5_4
		UnityEditorInternal.VR.VREditor.SetVREnabled(BuildTargetGroup.Standalone, true);
#endif
		PlayerSettings.virtualRealitySupported = true;
		AssetDatabase.SaveAssets();
	}

	static void SetAndroidTarget()
	{
		EditorUserBuildSettings.androidBuildSubtarget = MobileTextureSubtarget.ASTC;

		if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
		{
      //JFR: 6/27/2017 updating obsolete call
      EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
      //EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);
      //...JFR
    }
#if UNITY_5_4
		UnityEditorInternal.VR.VREditor.SetVREnabled(BuildTargetGroup.Android, true);
#endif
    PlayerSettings.virtualRealitySupported = true;
		AssetDatabase.SaveAssets();
	}
}
