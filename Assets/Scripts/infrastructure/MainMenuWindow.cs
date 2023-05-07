using UnityEngine;
using UnityEngine.UI;

namespace infrastructure
{
    public class MainMenuWindow : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;
        private SceneLoader _sceneLoader;

        public void Show(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;

            _playButton.onClick.AddListener(() =>
            {
                _sceneLoader.Load("Main");
            });

            _exitButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }
    }
}