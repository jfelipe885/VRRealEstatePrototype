using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class VRDeviceManager : MonoBehaviour {
    [SerializeField]
    GameObject m_oculus_device = null;
    [SerializeField]
    GameObject m_vive_device = null;
    [SerializeField]
    GameObject m_gearvr_device = null;
    [SerializeField]
    GameObject m_other_device = null;
    [SerializeField]
    GameObject CameraRig = null;

    public static String OCULUS_DEVICE = "Oculus Rift CV1";
    public static String VIVE_DEVICE = "Vive MV";
    public static String OTHER_DEVICE = "OpenVR";

    // Use this for initialization
    void Start () {

        foreach (string s in VRSettings.supportedDevices) {
            Debug.Log("Device: " + s);
        }
        Debug.Log("***Device: " + UnityEngine.VR.VRDevice.model);
        if (String.Compare(UnityEngine.VR.VRDevice.model, OCULUS_DEVICE) == 0)
        {
            CameraRig = m_oculus_device;
            Debug.Log("Oculus is Active: " + m_oculus_device.activeSelf);
        }
        else if (String.Compare(UnityEngine.VR.VRDevice.model, VIVE_DEVICE) == 0)
        {
            CameraRig = m_vive_device;
            Debug.Log("Vive is Active: " + m_vive_device.activeSelf);
        }
        else if (String.Compare(UnityEngine.VR.VRDevice.model, OTHER_DEVICE) == 0)
        {
            CameraRig = m_other_device;
            Debug.Log("Other VR is Active: " + m_other_device.activeSelf);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
