namespace gamespace
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class Manger3 : MonoBehaviour
    {
        internal static web_item3 web;

        public Transform panel_chars_base;
        public GameObject pref_panel_chars, pref_char;

        public GameObject Item_splite_circle;
        public RectTransform Circle;
        public GameObject Btn3;

        internal List<Char3> chars;
        internal List<Btn3> btns;

        public LineRenderer lineLink;


        public Text text_word;
        public RectTransform background_word;

        List<int> solved_ans = new List<int>();
        List<int> solved_adds = new List<int>();
        public Text text_adds;
        List<string> select_array;


        public Animation btn_extra;

        public Image panel_help;
        public Image btn_random, btn_extra_img;
        private void Awake()
        {
            panel_help.color = mainspace.Manger_base.instance.help_panel;
            btn_random.color = mainspace.Manger_base.instance.ColorA;
            btn_extra_img.color = mainspace.Manger_base.instance.ColorA;

        }

        void Start()
        {
            //Quss
            chars = new List<Char3>();
            int id = 0;
            for (int p = 0; p < web.hh; p++)
            {
                RectTransform parent_btns = Instantiate(pref_panel_chars, panel_chars_base).GetComponent<RectTransform>();
                int width_parent_btns = 0;
                if (web.hh > web.vv)
                {
                    width_parent_btns = (int)(350 / web.hh);
                }
                else
                {
                    width_parent_btns = (int)(280 / web.vv);
                }


                parent_btns.sizeDelta = new Vector2(width_parent_btns, parent_btns.sizeDelta.y);
                for (int b = 0; b < web.vv; b++)
                {
                    Char3 char3 = Instantiate(pref_char, parent_btns).GetComponent<Char3>();
                    char3.GetComponent<RectTransform>().sizeDelta = new Vector2(width_parent_btns, width_parent_btns);
                    if (web.chars[id].Length > 0)
                    {
                        char3.id = id;
                        char3.text.text = web.chars[id];
                        char3.text.enabled = false;

                        char3.charNum = web.chars[id];
                    }
                    else
                    {
                        char3.isHide = true;
                        char3.background.enabled = false;
                        char3.text.enabled = false;
                    }
                    chars.Add(char3);

                    id++;

                }
            }

            //btns
            btns = new List<Btn3>();
            float part_circle = 360 / web.btns.Length;
            for (int i = 0; i < web.btns.Length; i++)
            {
                RectTransform item_splite_circle = Instantiate(Item_splite_circle, Circle).GetComponent<RectTransform>();
                item_splite_circle.eulerAngles = Vector3.forward * (part_circle * (i + 1));
                Btn3 btn = Instantiate(Btn3, item_splite_circle.GetChild(0).position, Quaternion.identity, Circle).GetComponent<Btn3>();
                Destroy(item_splite_circle.gameObject);
                btn.charNum = web.btns[i];
                btn.text.text = web.btns[i];
                btns.Add(btn);

            }

            select_array = new List<string>();
            mainspace.Manger_base.instance.canvas.worldCamera = Camera.main;
            reStoreStepData();

        }

        void Update()
        {

            if (Input.GetMouseButtonUp(0))
            {
                lineLink.positionCount = 0;
                returnBtns();
                StartCoroutine(returnTextWord());
                if (select_array.Count != 0)
                    checkSelectArray();
                select_array = new List<string>();
            }

            if (lineLink.positionCount > 0)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                lineLink.SetPosition(lineLink.positionCount - 1, pos);
            }

        }

        public void Touch_btn(Btn3 btn3)
        {

            if (btn3.isClicked) return;
            btn3.isClicked = true;

            btn3.image.enabled = true;
            btn3.image.color = mainspace.Manger_base.instance.select2;
            if (lineLink.positionCount == 0)
            {
                lineLink.positionCount = lineLink.positionCount + 2;
                lineLink.SetPosition(0, btn3.transform.position);
                select_array.Add(btn3.charNum);

                text_word.text = "";
                background_word.sizeDelta = new Vector2(0, 0);
                background_word.GetComponent<Image>().color = mainspace.Manger_base.instance.black;
            }
            else
            {
                lineLink.positionCount = lineLink.positionCount + 1;
                lineLink.SetPosition(lineLink.positionCount - 2, btn3.transform.position);
                select_array.Add(btn3.charNum);

            }
            mainspace.SoundsManger.instance.clickSquare();
            text_word.text = text_word.text + btn3.text.text;
            background_word.sizeDelta = new Vector2(30 * text_word.text.Length, 60);
        }
        public void checkSelectArray()
        {
            //select_array.Reverse();
            int selectArrayCount = select_array.Count;
            if (selectArrayCount < 3) return;

            // ans
            for (int i = 0; i < web.qus.Count; i++)
            {
                //Debug.Log(selectArrayCount);
                if (selectArrayCount == web.qus[i].ans.Length)
                {
                    string[] ans_array_nums = new string[web.qus[i].ans.Length];
                    for (int ii = 0; ii < web.qus[i].ans.Length; ii++)
                    {
                        ans_array_nums[ii] = web.chars[web.qus[i].ans[ii]];
                    }
                    //select_array.Reverse();
                    if (checkTowArray(select_array, ans_array_nums))
                    {
                        foreach (int solved in solved_ans)
                        {
                            if (solved == i)
                            {
                                // the qus is solved
                                background_word.GetComponent<Image>().color = mainspace.Manger_base.instance.select1;
                                mainspace.SoundsManger.instance.run_old_effect();
                                StartCoroutine(runAnimationChars(web.qus[i].ans));
                                return;
                            }
                        }
                        foreach (int s in web.qus[i].ans)
                        {
                            StartCoroutine(runAnimationChars(web.qus[i].ans));
                            chars[s].background.color = mainspace.Manger_base.instance.green;
                            chars[s].text.enabled = true;
                            chars[s].isSolved = true;
                        }
                        background_word.GetComponent<Image>().color = mainspace.Manger_base.instance.green;
                        solved_ans.Add(i);
                        mainspace.SoundsManger.instance.run_true_effect();
                        checkWon();

                        return;
                    }

                }
            }
            // adds
            for (int i = 0; i < web.adds.Count; i++)
            {
                //Debug.Log(selectArrayCount);
                if (selectArrayCount == web.adds[i].Length)
                {
                    if (string.Join("", select_array) == web.adds[i])
                    {
                        foreach (int solved in solved_adds)
                        {
                            if (i == solved)
                            {
                                mainspace.SoundsManger.instance.run_old_effect();
                                background_word.GetComponent<Image>().color = mainspace.Manger_base.instance.select1;
                                btn_extra.Play("square");

                                return;
                            }
                        }
                        solved_adds.Add(i);
                        mainspace.SoundsManger.instance.run_true_effect();
                        background_word.GetComponent<Image>().color = mainspace.Manger_base.instance.green;
                        btn_extra.Play("Extra");

                        string word = "";

                        foreach (string x in select_array)
                        {
                            word += x;
                        }
                        if (solved_adds.Count > 1)
                        {
                            text_adds.text += " - ";

                        }
                        text_adds.text += word;

                        return;
                    }

                }
            }

            background_word.GetComponent<Image>().color = mainspace.Manger_base.instance.red;

        }

        IEnumerator runAnimationChars(int[] ans)
        {
            foreach (int c in ans)
            {
                chars[c].GetComponent<Animation>().Play();
                yield return new WaitForSeconds(0.1f);

            }
        }

        public bool checkTowArray(List<string> l1, string[] l2)
        {

            if (System.Linq.Enumerable.SequenceEqual(l1, l2)) return true;
            return false;
        }

        public void returnBtns()
        {
            for (int i = 0; i < btns.Count; i++)
            {
                btns[i].isClicked = false;
                btns[i].image.color = Color.white;
                btns[i].image.enabled = false;
            }
        }
        IEnumerator returnTextWord()
        {
            yield return new WaitForSeconds(0.1f);
            text_word.text = "";
            background_word.sizeDelta = new Vector2(0, 0);
            background_word.GetComponent<Image>().color = mainspace.Manger_base.instance.black;
        }

        // دوال لتبديل مكان الازرار
        bool isRandomingBtns = false;
        public void RandomBtns()
        {
            if (isRandomingBtns) return;
            isRandomingBtns = true;
            List<Vector3> ListPostionBtns = new List<Vector3>();

            for (int i = 0; i < btns.Count; i++)
            {
                ListPostionBtns.Add(btns[i].transform.position);
            }
            Shuffle(ListPostionBtns);

            for (int i = 0; i < btns.Count; i++)
            {
                StartCoroutine(MoveOverSeconds(btns[i].gameObject, ListPostionBtns[i], 0.3f));
            }

        }
        public void Shuffle(List<Vector3> alpha)
        {
            for (int i = 0; i < alpha.Count; i++)
            {
                Vector3 temp = alpha[i];
                int randomIndex = Random.Range(i, alpha.Count);
                alpha[i] = alpha[randomIndex];
                alpha[randomIndex] = temp;
            }
        }
        public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
        {
            float elapsedTime = 0;
            Vector3 startingPos = objectToMove.transform.position;
            while (elapsedTime < seconds)
            {
                objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            objectToMove.transform.position = end;
            isRandomingBtns = false;
        }

        public void showHint()
        {
            foreach (Char3 cc in chars)
            {
                if (!cc.isSolved && !cc.isHide)
                {
                    cc.background.color = mainspace.Manger_base.instance.green;
                    cc.text.enabled = true;
                    cc.isSolved = true;
                    mainspace.SoundsManger.instance.collect_help_sound();
                    checkIfHintSolveQus(cc);
                    checkWon();
                    return;
                }
            }
        }

        void checkIfHintSolveQus(Char3 cc)
        {
            int qus_id = 0;
            foreach (Qus3 qus in web.qus)
            {
                foreach (int a in qus.ans)
                {
                    if (a == cc.id)
                    {
                        foreach (int a2 in qus.ans)
                        {
                            if (!chars[a2].isSolved) return;
                        }
                        solved_ans.Add(qus_id);
                    }
                }
                qus_id++;
            }
        }

        void checkWon()
        {
            saveStepData();
            foreach (Char3 cc in chars)
            {
                if (!cc.isSolved && !cc.isHide) return;
            }
            mainspace.Manger_base.instance.won();

        }

        public void useCoins()
        {
            mainspace.CoinsManger.instance.removeCoins(10);
        }
        public void useVideo()
        {
            mainspace.AdsManger.instance.showVideo();
        }

        public void saveStepData()
        {
            if (!PlayerPrefs.HasKey("id_test_step")) return;

            int step = PlayerPrefs.GetInt("step", 1);
            if (mainspace.Manger_base.instance.now_step != step) return;

            PlayerPrefs.SetInt("solved_ans_count", solved_ans.Count);
            for (int i = 0; i < solved_ans.Count; i++)
            {
                PlayerPrefs.SetInt("solved_ans" + i, solved_ans[i]);

            }

        }

        public void reStoreStepData()
        {
            if (!PlayerPrefs.HasKey("id_test_step")) return;

            int step = PlayerPrefs.GetInt("step", 1);
            if (mainspace.Manger_base.instance.now_step != step) return;

            for (int i = 0; i < PlayerPrefs.GetInt("solved_ans_count"); i++)
            {
                solved_ans.Add(PlayerPrefs.GetInt("solved_ans" + i));
                foreach (int n in web.qus[PlayerPrefs.GetInt("solved_ans" + i)].ans)
                {
                    chars[n].background.color = mainspace.Manger_base.instance.green;
                    chars[n].text.enabled = true;
                    chars[n].isSolved = true;
                }
            }


        }


    }
}