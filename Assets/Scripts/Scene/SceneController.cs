using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public delegate void SceneLoadDelegate(SceneData scene);

    public static SceneController Instance { get; private set; }

    private AsyncOperation _loadingOperation;
    private MenuController _menuController;
    private SceneData _targetScene;
    private MenuType _loadingMenu;
    private SceneLoadDelegate _sceneLoadDelegate;
    private bool _isLoading;

    private MenuController MenuController
    {
        get
        {
            if (_menuController == null)
            {
                _menuController = MenuController.Instance;
            }
            if (_menuController == null)
            {
                Debug.LogWarning("You are trying to access the MenuController, but no instance was found.");
            }
            return _menuController;
        }
    }

    private string CurrentSceneName => SceneManager.GetActiveScene().name;
  
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Load(SceneData scene, SceneLoadDelegate sceneLoadDelegate = null, MenuType loadingMenu = MenuType.None, bool reload = false)
    {
        if ((loadingMenu != MenuType.None && !MenuController) || !SceneCanBeLoaded(scene, reload))
        {
            return;
        }

        _isLoading = true;
        _targetScene = scene;
        _loadingMenu = loadingMenu;
        _sceneLoadDelegate = sceneLoadDelegate;

        if (loadingMenu != MenuType.None)
        {
            MenuController.EnableMenu(loadingMenu);
        }

        StartCoroutine(LoadSceneWithDelay());
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (_targetScene == null || _targetScene.SceneName != scene.name)
        {
            return;
        }

        if (_sceneLoadDelegate != null)
        {
            _sceneLoadDelegate(_targetScene);
        }

        if (_loadingMenu != MenuType.None)
        {
            MenuController.DisableMenu(_loadingMenu);
        }

        _isLoading = false;
    }

    private IEnumerator LoadSceneWithDelay()
    {
        _loadingOperation = _targetScene.LoadAsync(LoadSceneMode.Single);
        _loadingOperation.allowSceneActivation = false;

        yield return new WaitForSeconds(_targetScene.LoadDelay);

        _loadingOperation.allowSceneActivation = true;
    } 

    private bool SceneCanBeLoaded(SceneData scene, bool reload)
    {
        return (CurrentSceneName != scene.SceneName || reload) && !_isLoading;
    }
}