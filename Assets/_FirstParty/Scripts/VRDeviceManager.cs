using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class VRDeviceManager : Singleton<VRDeviceManager>
{
  [Serializable]
  public class VRDevice
  {
    public GameObject _device;
    public Camera _camera;
  };

  public static String OCULUS_DEVICE = "Oculus Rift CV1";
  public static String VIVE_DEVICE = "Vive MV";
  public static String OTHER_DEVICE = "OpenVR";

  //===========================================================================
  public Camera GetCurrentDeviceCamera ()
  {
    return _currentDevice._camera;
  }

  //===========================================================================
  private void Awake ()
  {
    _devices.Add(_oculusDevice);
    _devices.Add(_viveDevice);
    _devices.Add(_gearVrDevice);
    _devices.Add(_otherDevice);
    _devices.Add(_simulator);

    foreach (VRDevice vrDevice in _devices)
    {
      if (vrDevice != null && vrDevice._device != null)
      {
        vrDevice._device.SetActive(false);
      }
    }

    foreach (string s in VRSettings.supportedDevices)
    {
      Debug.Log("Device: " + s);
    }

    if (_useSimulator == true && _simulator != null)
    {
      _currentDevice = _simulator;
    }
    else
    {
      Debug.Log("***Device: " + UnityEngine.VR.VRDevice.model);
      if (String.Compare(UnityEngine.VR.VRDevice.model, OCULUS_DEVICE) == 0)
      {
        _currentDevice = _oculusDevice;
        Debug.Log("Oculus is Active: " + _oculusDevice._device.activeSelf);
      }
      else if (String.Compare(UnityEngine.VR.VRDevice.model, VIVE_DEVICE) == 0)
      {
        _currentDevice = _viveDevice;
        Debug.Log("Vive is Active: " + _viveDevice._device.activeSelf);
      }
      else if (String.Compare(UnityEngine.VR.VRDevice.model, OTHER_DEVICE) == 0)
      {
        _currentDevice = _otherDevice;
        Debug.Log("Other VR is Active: " + _otherDevice._device.activeSelf);
      }
    }

    //if we get here and current device still null (We don't have anything hooked up) then let's use the simulator
    if (_currentDevice == null && _simulator != null)
    {
      _currentDevice = _simulator;
    }

    if (_currentDevice != null)
    {
      _currentDevice._device.SetActive(true);
    }
  }

  [SerializeField]
  private VRDevice _oculusDevice = null;

  [SerializeField]
  private VRDevice _viveDevice = null;

  [SerializeField]
  private VRDevice _gearVrDevice = null;

  [SerializeField]
  private VRDevice _simulator = null;

  [SerializeField]
  private VRDevice _otherDevice = null;

  [SerializeField]
  private bool _useSimulator = false;

  private VRDevice _currentDevice = null;
  private List<VRDevice> _devices = new List<VRDevice>(5);
}