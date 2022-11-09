namespace mapspace
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;
    public class WebsManger : MonoBehaviour
    {
        public Transform Steps_parent;

        private void Start()
        {
            PlayerPrefs.SetInt("total_step_count", Steps_parent.childCount);
            if (Application.isEditor)
            {
                if (PlayerPrefs.HasKey("id_test_step"))
                {
                    startStep(PlayerPrefs.GetInt("id_test_step"));

                    PlayerPrefs.DeleteKey("id_test_step");
                }
            }
        }

        public void startStep(int id)
        {
            Step step = Steps_parent.GetChild(id - 1).GetComponent<Step>();
            mainspace.Manger_base.instance.now_step = id;
            mainspace.Manger_base.instance.text_step_type.text = id + " - " + "Mix Letter";

            gamespace.Manger3.web = JsonUtility.FromJson<gamespace.web_item3>(step.json);
            SceneManager.LoadScene("Game3");
        }

    }
}