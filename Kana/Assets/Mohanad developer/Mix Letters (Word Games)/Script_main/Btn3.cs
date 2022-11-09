namespace gamespace
{
    using UnityEngine;
    using UnityEngine.UI;

    public class Btn3 : MonoBehaviour
    {
        public Text text;
        public Image image;
        internal bool isClicked = false;
        internal string charNum;
        Manger3 manger;
        private void Start()
        {
            manger = FindObjectOfType<Manger3>();
        }

        public void Touch()
        {
            if (isClicked) return;
            manger.Touch_btn(this);
        }


    }
}
