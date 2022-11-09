namespace mapspace
{
    using UnityEngine;
    using UnityEngine.UI;

    public class Step_btn : MonoBehaviour
    {
        public Image background;
        public GameObject lock_img;

        public Text id;

        public void click()
        {
            FindObjectOfType<MapManger>().clickStep(int.Parse(id.text));
        }
    }
}