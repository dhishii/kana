namespace mainspace
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class MainBtns : MonoBehaviour
    {
        public GameObject panel_share_game;


        private void Start()
        {
            int step = PlayerPrefs.GetInt("step", 1);
            if (step > 5 && SceneManager.GetActiveScene().name == "Main")
            {
                panel_share_game.SetActive(true);

            }


        }
        public void btn_star_play()
        {
            SceneManager.LoadScene("Map");
            //FindObjectOfType<WebsManger>().startStep(PlayerPrefs.GetInt("step", 1));
            //SoundsManger.instance.clickSquare();
        }

        public void goToFacebookGroup()
        {
            SoundsManger.instance.clickSquare();
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                Application.OpenURL("https://facebook.com/groups/" + Manger_base.instance.face_book_group_id);

            }
            else
            {
                Application.OpenURL("fb://group/" + Manger_base.instance.face_book_group_id);

            }
        }
        public void goToInstagram()
        {
            SoundsManger.instance.clickSquare();
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                Application.OpenURL("https://www.instagram.com/" + Manger_base.instance.instagram_id);

            }
            else
            {
                Application.OpenURL("instagram://user?username=" + Manger_base.instance.instagram_id);

            }
        }


        public void goToStore()
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                Application.OpenURL("itms-apps://itunes.apple.com/us/app/" + Manger_base.instance.ios_id);
            }
            else
            {
                Application.OpenURL("market://details?id=" + Manger_base.instance.android_packege);
            }

        }


    }
}