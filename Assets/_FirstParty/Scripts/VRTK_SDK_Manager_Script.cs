using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using VRTK;

public class VRTK_SDK_Manager_Script : MonoBehaviour {
    [Serializable]
    public class VRDevice
    {
        public GameObject _device;
        public Camera _camera;

        [Tooltip("A reference to the GameObject that is the user's boundary or play area, most likely provided by the SDK's Camera Rig.")]
        public GameObject actualBoundaries;
        [Tooltip("A reference to the GameObject that contains the VR camera, most likely provided by the SDK's Camera Rig Headset.")]
        public GameObject actualHeadset;
        [Tooltip("A reference to the GameObject that contains the SDK Left Hand Controller.")]
        public GameObject actualLeftController;
        [Tooltip("A reference to the GameObject that contains the SDK Right Hand Controller.")]
        public GameObject actualRightController;

        [Header("Controller Aliases")]

        [Tooltip("A reference to the GameObject that models for the Left Hand Controller.")]
        public GameObject modelAliasLeftController;
        [Tooltip("A reference to the GameObject that models for the Right Hand Controller")]
        public GameObject modelAliasRightController;
        [Tooltip("A reference to the GameObject that contains any scripts that apply to the Left Hand Controller.")]
        public GameObject scriptAliasLeftController;
        [Tooltip("A reference to the GameObject that contains any scripts that apply to the Right Hand Controller.")]
        public GameObject scriptAliasRightController;               
    };

    private bool isHand = false;
    private Transform rightHand;
    private Transform leftHand;
    private Transform currentHand;
    private Vector3 oldPos;
    private Transform myCamera;
    private SDK_ControllerSim rightController;
    private SDK_ControllerSim leftController;
    private static GameObject cachedCameraRig;
    private static bool destroyed = false;

    public static String OCULUS_DEVICE = "Oculus Rift CV1";
    public static String VIVE_DEVICE = "Vive MV";
    public static String OTHER_DEVICE = "OpenVR";

    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Awake()
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
            Debug.Log("*Device: " + UnityEngine.VR.VRDevice.model);
            if (String.Compare(UnityEngine.VR.VRDevice.model, OCULUS_DEVICE) == 0)
            {
                _currentDevice = _oculusDevice;
                Debug.Log("Oculus is Active: " + _oculusDevice._device.activeSelf);
            }
            else if (String.Compare(UnityEngine.VR.VRDevice.model, VIVE_DEVICE) == 0)
            {
                _currentDevice = _viveDevice;

                rightHand = transform.Find("RightHand");
                rightHand.gameObject.SetActive(false);
                leftHand = transform.Find("LeftHand");
                leftHand.gameObject.SetActive(false);
                currentHand = rightHand;
                oldPos = Input.mousePosition;
                myCamera = transform.Find("Camera");
                leftHand.Find("Hand").GetComponent<Renderer>().material.color = Color.red;
                rightHand.Find("Hand").GetComponent<Renderer>().material.color = Color.green;
                rightController = rightHand.GetComponent<SDK_ControllerSim>();
                leftController = leftHand.GetComponent<SDK_ControllerSim>();
                rightController.Selected = true;
                leftController.Selected = false;
                destroyed = false;

                Debug.Log("VRTK_SDKManager.instance: " + VRTK_SDKManager.instance);               
                VRTK_SDKManager.instance.actualHeadset = _viveDevice.actualHeadset;               
                VRTK_SDKManager.instance.actualBoundaries = _viveDevice.actualBoundaries;

                Debug.Log("*****leftController before: " + VRTK_SDKManager.instance.actualLeftController);
                VRTK_SDKManager.instance.actualLeftController = leftController.gameObject;
                Debug.Log("*****leftController after: " + VRTK_SDKManager.instance.actualLeftController);

                VRTK_SDKManager.instance.actualRightController = rightController.gameObject;

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
