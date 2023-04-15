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

    public void LoadScene<T>(eSceneType sceneType) where T : SceneMain
    {
        var idx = (int)sceneType;
        SceneManager.LoadSceneAsync(idx).completed += (obj) =>
        {
            var main = GameObject.FindObjectOfType<T>();

            main.onDestroy.AddListener(() =>
            {
                uiApp.FadeOut();
            });

            switch (sceneType)
            {
                case eSceneType.Logo:
                    {
                        this.uiApp.FadeOutImmediately();
                        var logoMain = main as LogoMain;
                        logoMain.AddListener("onShowLogoComplete", (param) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<LoadingMain>(eSceneType.Loading);
                            });

                        });

                        this.uiApp.FadeIn(2f, () =>
                        {
                            logoMain.Init();
                        });
                        break;
                    }
                case eSceneType.Loading:
                    {
                        this.uiApp.FadeIn(0.5f, () =>
                        {
                            main.AddListener("onLoadComplete", (data) =>
                            {
                                this.uiApp.FadeOut(0.5f, () =>
                                {
                                    this.LoadScene<TitleMain>(eSceneType.Title);
                                });
                            });
                            main.Init();
                        });

                        break;
                    }
                case eSceneType.Title:
                    {
                        this.uiApp.FadeIn();

                        main.AddListener("onClick", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<StoryMain>(eSceneType.Story);
                            });
                        });
                        main.Init();
                        break;
                    }
                case eSceneType.Story:
                    {
                        this.uiApp.FadeIn();

                        main.AddListener("onClickWorkBtn", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<CookMain>(eSceneType.Cook);
                            });
                        });
                        main.Init();
                        break;
                    }
                case eSceneType.Cook:
                    {
                        this.uiApp.FadeIn();

                        main.AddListener("onClickWorkBtn", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                //this.LoadScene<StoryMain>(eSceneType.Cook);
                            });
                        });
                        main.Init();
                        break;
                    }


            }
        };
    }

}