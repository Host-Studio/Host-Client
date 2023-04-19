using UnityEngine;
using UnityEngine.SceneManagement;

public class App : MonoBehaviour
{
    public enum eSceneType
    {
        App, Logo, Loading, Title, Story, Cook, Hospitality
    }

    public static App instance;

    private UIApp uiApp;

    private void Awake()
    {
        App.instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        this.uiApp = GameObject.FindObjectOfType<UIApp>();
        this.uiApp.Init();

        this.LoadScene<LogoMain>(eSceneType.Logo);
    }


    public void LoadScene<T>(eSceneType sceneType, float fadeOutTime = 0.5f, 
        float fadeInTime = 2f, SceneParams param = null) where T : SceneMain
    {
        var idx = (int)sceneType;

        this.uiApp.FadeOut(fadeOutTime, () =>
        {
            SceneManager.LoadSceneAsync(idx).completed += (obj) =>
            {
                var main = GameObject.FindObjectOfType<T>();

                this.uiApp.FadeIn(fadeInTime, () =>
                {
                    main.Init(param);
                });
            };
        });
    }
}