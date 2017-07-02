using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class VRDeviceManager : MonoBehaviour
{
  [SerializeField]
  private GameObject _oculusDevice = null;

  [SerializeField]
  private GameObject _viveDevice = null;

  [SerializeField]
  private GameObject _gearVrDevice = null;

  [SerializeField]
  private GameObject _otherDevice = null;

  //[SerializeField]
  private GameObject _cameraRig = null;

  private List<GameObject> _devices = new List<GameObject>(4);

  public static String OCULUS_DEVICE = "Oculus Rift CV1";
  public static String VIVE_DEVICE = "Vive MV";
  public static String OTHER_DEVICE = "OpenVR";

  // Use this for initialization
  private void Start()
  {
    _devices.Add(_oculusDevice);
    _devices.Add(_viveDevice);
    _devices.Add(_gearVrDevice);
    _devices.Add(_otherDevice);
    foreach (GameObject gameObject in _devices)
    {
      if (gameObject != null)
      {
        gameObject.SetActive(false);
      }
    }

    foreach (string s in VRSettings.supportedDevices)
    {
      Debug.Log("Device: " + s);
    }

    Debug.Log("***Device: " + UnityEngine.VR.VRDevice.model);
    if (String.Compare(UnityEngine.VR.VRDevice.model, OCULUS_DEVICE) == 0)
    {
      _cameraRig = _oculusDevice;
      Debug.Log("Oculus is Active: " + _oculusDevice.activeSelf);
    }
    else if (String.Compare(UnityEngine.VR.VRDevice.model, VIVE_DEVICE) == 0)
    {
      _cameraRig = _viveDevice;
      Debug.Log("Vive is Active: " + _viveDevice.activeSelf);
    }
    else if (String.Compare(UnityEngine.VR.VRDevice.model, OTHER_DEVICE) == 0)
    {
      _cameraRig = _otherDevice;
      Debug.Log("Other VR is Active: " + _otherDevice.activeSelf);
    }

    if (_cameraRig != null)
    {
      _cameraRig.SetActive(true);
    }
  }

  // Update is called once per frame
  //private void Update()
  //{
  //}
}