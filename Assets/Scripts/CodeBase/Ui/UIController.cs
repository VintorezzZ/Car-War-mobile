using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Ui
{
    public class UIController : MonoBehaviour
    {
        public static UIController Instance;
    
        public GameObject deathUI;
        public GameObject optionsPanel;
        public GameObject whilePlayPanel;
        public GameObject optionsButton;
        public Text scoreText;
        public Text deathScoreText;
        public Text deadCountText;
        public Text timeText;
        public Text ammoText;
        public Slider sensSlider;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
    
        public void SetDeathUI()
        {
            deathUI.SetActive(true);
            whilePlayPanel.SetActive(false);
            optionsPanel.SetActive(false);
            //Cursor.lockState = CursorLockMode.None;
        }

        public void SetScoreText(int points, int deadCount)
        {
            scoreText.text = "Score  " + points;
            deathScoreText.text = "Score   " + points.ToString();
            deadCountText.text = "Cars destroyed   " + deadCount;
        }
    }
}
