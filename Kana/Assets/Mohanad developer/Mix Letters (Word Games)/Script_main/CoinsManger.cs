namespace mainspace
{
    using UnityEngine.SceneManagement;
    using UnityEngine;
    using UnityEngine.UI;

    public class CoinsManger : MonoBehaviour
    {
        public static CoinsManger instance;

        public int coins = 0;
        public ParticleSystem effect_coins;

        public Text text;

        public GameObject panel_buy_coins;

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); return; }
            instance = this;
            DontDestroyOnLoad(gameObject);



        }
        private void Start()
        {
            coins = PlayerPrefs.GetInt("coins", 100);
            text.text = coins + "";
        }

        public void addCoins(int amount)
        {
            panel_buy_coins.SetActive(false);
            SoundsManger.instance.collect_coins_sound();
            coins += amount;
            PlayerPrefs.SetInt("coins", coins);
            effect_coins.Play();
            text.text = coins + "";

        }

        public void removeCoins(int amount)
        {
            if (coins - amount >= 0)
            {
                coins -= amount;
                PlayerPrefs.SetInt("coins", coins);
                text.text = coins + "";
                effect_coins.Play();
                FindObjectOfType<gamespace.Manger3>().showHint();

            }
            else
            {
                panel_buy_coins.SetActive(true);
            }

        }

        public void show_panel_coins()
        {
            panel_buy_coins.SetActive(true);
            SoundsManger.instance.clickSquare();
        }

        public bool checkCoins(int num)
        {
            if (coins - num >= 0)
            {
                return true;
            }
            else
            {
                return false;

            }
        }
    }
}