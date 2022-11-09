namespace mainspace
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class Manger_base : MonoBehaviour
    {
        public static Manger_base instance;
        internal int total_step_count;
        internal string android_packege;
        internal string ios_id;
        internal string face_book_group_id;
        internal string instagram_id;

        // Material Colors
        internal Color ColorA;
        internal Color ColorB;
        internal Color ColorC;

        public Material material;
        internal Color help_panel;
        internal Color top_panel;
        internal Color select1;
        internal Color select2;

        public Color text_qus_color;

        public Color green;

        public Color red;
        public Color black;

        public Color[] ArrayColors = new Color[6];

        public Sprite simple_square;
        public Sprite d3_square;

        public Canvas canvas;

        public Text text_step_type;

        internal int now_step;
        public GameObject won_panel;
        public GameObject panel_prize;
        public Text text_won_coins;

        public GameObject panel_Review;
        public GameObject panel_Qut;
        public GameObject panel_no_video;
        public GameObject panel_loading;

        public GameObject panel_price_follow;


        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); return; }
            instance = this;
            DontDestroyOnLoad(gameObject);

            Application.targetFrameRate = 30;
            Input.multiTouchEnabled = false;
            Screen.sleepTimeout = 30;


        }
        private void Start()
        {
            toolspace.GameData gameData = FindObjectOfType<toolspace.GameData>();
            total_step_count = PlayerPrefs.GetInt("total_step_count", 100);
            android_packege = gameData.android_packege;
            ios_id = gameData.ios_id;
            face_book_group_id = gameData.face_book_group_id;
            instagram_id = gameData.instagram_id;

            material.SetColor("_ColorA", gameData.ColorA);
            material.SetColor("_ColorB", gameData.ColorB);
            material.SetColor("_ColorC", gameData.ColorC);

            ColorA = gameData.ColorA;
            ColorB = gameData.ColorB;
            ColorC = gameData.ColorC;

            help_panel = gameData.help_panel;
            top_panel = gameData.top_panel;
            select1 = gameData.select1;
            select2 = gameData.select2;

            text_qus_color = gameData.text_qus_color;


            if (PlayerPrefs.GetInt("follow", 0) == 1)
            {
                panel_price_follow.SetActive(false);
            }

            if (Application.isEditor)
            {
                if (PlayerPrefs.HasKey("id_test_step"))
                {
                    //FindObjectOfType<WebsManger>().startStep(PlayerPrefs.GetInt("id_test_step"));
                    SceneManager.LoadScene("Map");

                    //PlayerPrefs.DeleteKey("id_test_step");
                }
            }
        }
        void Update()
        {
            // Make sure user is on Android platform
            if (Application.platform == RuntimePlatform.Android)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Qut_btn(false);
                }
            }
        }
        public void won()
        {
            int step = PlayerPrefs.GetInt("step", 1);
            if (now_step == step && now_step < total_step_count)
            {
                int review = PlayerPrefs.GetInt("review", 0);
                int coins = PlayerPrefs.GetInt("coins", 100);
                int follow = PlayerPrefs.GetInt("follow", 0);

                PlayerPrefs.DeleteAll();
                PlayerPrefs.SetInt("step", step + 1);
                PlayerPrefs.SetInt("review", review);
                PlayerPrefs.SetInt("coins", coins);
                PlayerPrefs.SetInt("follow", follow);

                // prize won
                panel_prize.SetActive(true);
                int rr = new System.Random().Next(3, 8);
                text_won_coins.text = new System.Random().Next(3, 8) + "";
                mainspace.CoinsManger.instance.addCoins(rr);
            }
            else if (now_step == total_step_count)
            {
                int review = PlayerPrefs.GetInt("review", 0);
                int coins = PlayerPrefs.GetInt("coins", 100);
                int follow = PlayerPrefs.GetInt("follow", 0);

                PlayerPrefs.DeleteAll();
                PlayerPrefs.SetInt("step", step);
                PlayerPrefs.SetInt("review", review);
                PlayerPrefs.SetInt("coins", coins);
                PlayerPrefs.SetInt("follow", follow);
            }
            if (now_step == total_step_count)
            {
                SceneManager.LoadScene("finsh");
                return;
            }
            if (now_step != step)
            {
                panel_prize.SetActive(false);
            }
            setPanelWonY();
            won_panel.SetActive(true);

            if (step > 10 && PlayerPrefs.GetInt("review", 0) != 1)
            {
                panel_Review.SetActive(true);
                PlayerPrefs.SetInt("review", 1);
            }


        }

        void setPanelWonY()
        {
            won_panel.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -350, 0);

        }

        public void Qut_btn(bool isGoToMap)
        {
            if (isGoToMap)
            {
                //SceneManager.LoadScene("Map");

                won_panel.SetActive(false);
                SoundsManger.instance.clickSquare();
                panel_Qut.SetActive(false);
                AdsManger.instance.showInterstitial();

                return;
            }

            string scene_name = SceneManager.GetActiveScene().name;
            if (scene_name == "Main")
            {
                Application.Quit();
            }
            else if (scene_name == "Map")
            {
                SceneManager.LoadScene("Main");

            }
            else
            {

                panel_Qut.SetActive(true);

            }
        }

        public void follow()
        {
            if (PlayerPrefs.GetInt("follow", 0) != 1)
            {
                InvokeRepeating("follow_aaddCoins", 5f, 5f);  //1s delay, repeat every 1s

            }

            SoundsManger.instance.clickSquare();
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                Application.OpenURL("https://www.instagram.com/" + instagram_id);
            }
            else
            {
                Application.OpenURL("instagram://user?username=" + instagram_id);
            }
        }
        void follow_aaddCoins()
        {
            if (Application.isPlaying)
            {
                mainspace.CoinsManger.instance.addCoins(50);
                PlayerPrefs.SetInt("follow", 1);
                panel_price_follow.SetActive(false);

                CancelInvoke("follow_aaddCoins");
            }
        }

        public void goToStore()
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                Application.OpenURL("itms-apps://itunes.apple.com/us/app/" + ios_id);
            }
            else
            {
                Application.OpenURL("market://details?id=" + android_packege);
            }
            panel_Review.SetActive(false);

        }




    }
}
