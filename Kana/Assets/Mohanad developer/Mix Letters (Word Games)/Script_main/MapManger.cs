namespace mapspace
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class MapManger : MonoBehaviour
    {
        public Transform parent_btns;
        WebsManger websManger;
        int step;
        public GameObject panel_lock_step;
        Vector3 last_pos;
        int now_step_in_map = 0;

        private void Start()
        {
            step = PlayerPrefs.GetInt("step", 1);

            //PlayerPrefs.SetInt("coins", 500);
            //step = 126;
            websManger = FindObjectOfType<WebsManger>();

            int start_count = 1; if (step > 100) start_count = 101;
            for (int i = 1; i <= 100; i++)
            {
                if (start_count > step + 5 || start_count > mainspace.Manger_base.instance.total_step_count) break;
                Step_btn step_btn = parent_btns.GetChild(i - 1).GetComponent<Step_btn>();
                step_btn.gameObject.SetActive(true);
                step_btn.id.text = start_count + "";
                if (start_count < step)
                {
                    step_btn.lock_img.SetActive(false);
                    step_btn.background.color = mainspace.Manger_base.instance.green;
                    step_btn.id.gameObject.SetActive(true);
                    step_btn.id.color = Color.white;
                }


                if (start_count == step)
                {
                    step_btn.lock_img.SetActive(false);
                    step_btn.background.color = Color.white;
                    step_btn.id.gameObject.SetActive(true);

                }
                start_count++;
            }



            if (step <= 100)
            {
                now_step_in_map = step;
            }
            else if (step <= 200)
            {
                now_step_in_map = step - 100;
            }
            else if (step <= 300)
            {
                now_step_in_map = step - 200;

            }
            else if (step <= 400)
            {
                now_step_in_map = step - 300;

            }
            else if (step <= 500)
            {
                now_step_in_map = step - 400;

            }
            else if (step <= 600)
            {
                now_step_in_map = step - 500;

            }
            else if (step <= 700)
            {
                now_step_in_map = step - 600;

            }
            else if (step <= 800)
            {
                now_step_in_map = step - 700;

            }
            else if (step <= 900)
            {
                now_step_in_map = step - 800;

            }
            else if (step <= 1000)
            {
                now_step_in_map = step - 900;

            }


            if (now_step_in_map > 10)
            {
                if (now_step_in_map > 35)
                    parent_btns.position = parent_btns.position - (new Vector3(0, 52, 0) * now_step_in_map);
                else
                    parent_btns.position = parent_btns.position - (new Vector3(0, 40, 0) * now_step_in_map);

            }
            last_pos = parent_btns.position;


        }
        public void clickStep(int id)
        {
            if (id > step)
            {
                mainspace.SoundsManger.instance.run_false_effect();
                panel_lock_step.SetActive(true);
                return;
            }
            parent_btns.gameObject.SetActive(false);
            //StartCoroutine(starStep(id))
            starStep(id);
        }

        void starStep(int id)
        {
            mainspace.SoundsManger.instance.clickBtn();
            //yield return new WaitForSeconds(1);
            mainspace.SoundsManger.instance.clickStart_web();
            websManger.startStep(id);
            mainspace.Manger_base.instance.now_step = id;
        }
        public void onValueChanged(Vector2 vector)
        {
            //Debug.Log(vector.y);
            if (vector.y > (0.01 * now_step_in_map))
            {
                parent_btns.position = last_pos;
                return;
            }
            last_pos = parent_btns.position;
        }


    }
}