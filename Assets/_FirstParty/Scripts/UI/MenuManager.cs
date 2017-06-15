using UnityEngine;

public class MenuManager : Singleton<MenuManager>
{
  [SerializeField]
  private GameObject _inGameMenu = null;

  [SerializeField]
  private GameObject _camera = null;

  [SerializeField]
  private float _distanceToMenu = 3.0f; //meters?

  [SerializeField]
  private float _yOffset = 0.1f;

  public void ShowInGameMenu()
  {
    if (_camera == null)
    {
      return;
    }
    if (_inGameMenu == null)
    {
      return;
    }

    //get the camera forward vector
    _inGameMenu.transform.position = _camera.transform.position + (_camera.transform.forward * _distanceToMenu);
    _inGameMenu.transform.position += (-_inGameMenu.transform.up * _yOffset);    
    _inGameMenu.transform.LookAt(_inGameMenu.transform.position + (_camera.transform.forward * _distanceToMenu));
    if (_inGameMenu != null)
    {
      _inGameMenu.SetActive(true);
    }
  }

  public void HideInGameMenu()
  {
    if (_inGameMenu != null)
    {
      _inGameMenu.SetActive(false);
    }
  }
}