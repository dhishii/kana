namespace editorspace
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using UnityEditor;
    using UnityEditor.SceneManagement;
    using UnityEngine;

    public class StepsAdder : EditorWindow
    {
        Transform steps_parent;
        int hight;
        int width;

        int count_chars;

        string[] chars_grid;
        string[] btns_grid;
        string text_add;
        bool isLtoR = false;

        List<int[]> listssh;
        List<int[]> listssw;

        gamespace.web_item3 web;
        bool[] menu = new bool[3];

        [MenuItem("Window/Steps Adder")]
        public static void ShowWindow()
        {
            GetWindow<StepsAdder>("Steps Adder");

        }

        public void AddScene(string path, string path_folder)
        {
            if (!AssetDatabase.IsValidFolder(path_folder)) return;

            EditorBuildSettingsScene[] editorBuildSettingsScenes = EditorBuildSettings.scenes;
            foreach (EditorBuildSettingsScene s in editorBuildSettingsScenes)
            {
                if (path == s.path) return;
            }
            List<EditorBuildSettingsScene> list = new List<EditorBuildSettingsScene>(editorBuildSettingsScenes);
            list.Add(new EditorBuildSettingsScene(path, true));
            EditorBuildSettings.scenes = list.ToArray();

        }

        public void switchMenu(int id)
        {
            for (int i = 0; i < menu.Length; i++)
            {
                if (id == i)
                    menu[i] = true;
                else
                    menu[i] = false;

            }
        }


        private void OnEnable()
        {

            web = new gamespace.web_item3();
            web.qus = new List<gamespace.Qus3>();
            web.adds = new List<string>();
            switchMenu(0);

            AddScene("Assets/Mohanad developer/Mix Letters (Word Games)/Scenes/Main.unity", "Assets/Mohanad developer/Mix Letters (Word Games)/Scenes");
            AddScene("Assets/Mohanad developer/Mix Letters (Word Games)/Scenes/Map.unity", "Assets/Mohanad developer/Mix Letters (Word Games)/Scenes");
            AddScene("Assets/Mohanad developer/Mix Letters (Word Games)/Scenes/finsh.unity", "Assets/Mohanad developer/Mix Letters (Word Games)/Scenes");
            AddScene("Assets/Mohanad developer/Mix Letters (Word Games)/Scenes/Game3.unity", "Assets/Mohanad developer/Mix Letters (Word Games)/Scenes");

            EditorSceneManager.OpenScene("Assets/Mohanad developer/Mix Letters (Word Games)/Scenes/Map.unity");

            if (GameObject.Find("Steps_Parent"))
                steps_parent = GameObject.Find("Steps_Parent").transform;

            if (PlayerPrefs.GetInt("isLtoR", 1) == 1)
            {
                isLtoR = true;
            }
            else
            {
                isLtoR = false;

            }
            reName();
        }
        private void OnGUI()
        {

            if (menu[0])
            {
                MenuGUI0();
            }
            if (menu[1])
            {
                MenuGUI1();
            }
            if (menu[2])
            {
                MenuGUI2();
            }

        }


        private void MenuGUI1()
        {
            GUILayout.BeginArea(new Rect(position.width - 60, 0, 60, 30));
            if (GUILayout.Button("back"))
            {
                switchMenu(0);
            }
            GUILayout.EndArea();

            GUILayout.Space(10);
            isLtoR = GUILayout.Toggle(isLtoR, "is L to R language");
            if (PlayerPrefs.GetInt("isLtoR", 1) == 1)
            {
                isLtoR = true;
            }
            else
            {
                isLtoR = false;

            }
            GUILayout.Space(5);

            if (GUILayout.Button("Save Step"))
            {
                foreach (string s in btns_grid)
                {
                    if (s.Length != 1)
                    {
                        EditorUtility.DisplayDialog("error", "set chars in (chars for mix:)", "ok");
                        return;
                    }
                }
                web.btns = btns_grid;
                getChars();

                GameObject step = new GameObject(steps_parent.childCount + "");
                step.AddComponent<mapspace.Step>();
                step.GetComponent<mapspace.Step>().json = JsonUtility.ToJson(web);

                step.transform.parent = steps_parent;

                if (isLtoR)
                    PlayerPrefs.SetInt("isLtoR", 1);
                else
                    PlayerPrefs.SetInt("isLtoR", 0);

                switchMenu(0);
                reName();
                EditorUtility.DisplayDialog("done", "done save step", "ok");
                PlayerPrefs.SetInt("total_step_count", steps_parent.childCount);
                EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());

            }


            listssh = new List<int[]>();
            listssw = new List<int[]>();

            GUILayout.Label("chars Map:", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            int i = 0;
            for (int h = 0; h < hight; h++)
            {
                int[] arrw = new int[width];
                //GUILayout.BeginVertical("hi");
                GUILayout.BeginArea(new Rect(42 * h, 73, 40, 42 * width));
                for (int w = 0; w < width; w++)
                {

                    GUIStyle myStyle = new GUIStyle(GUI.skin.textField);
                    myStyle.alignment = TextAnchor.MiddleCenter;
                    chars_grid[i] = GUILayout.TextField(chars_grid[i], myStyle, GUILayout.Width(40), GUILayout.Height(40));
                    if (chars_grid[i].Length > 1)
                    {
                        chars_grid[i] = chars_grid[i].Substring(0, 1);
                    }
                    arrw[w] = i;
                    i++;

                }

                GUILayout.EndArea();

                listssw.Add(arrw);

            }
            GUILayout.EndHorizontal();
            for (int h = 0; h < width; h++)
            {
                int[] arrh = new int[hight];
                int ii = 0;
                for (int x = h; x < hight * width; x = x + width)
                {
                    arrh[ii] = x;
                    ii++;
                }
                listssh.Add(arrh);

            }
            GUILayout.Space(width * 45);
            GUILayout.Label("chars for mix:", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            for (int c = 0; c < count_chars; c++)
            {
                GUIStyle myStyle = new GUIStyle(GUI.skin.textField);
                myStyle.alignment = TextAnchor.MiddleCenter;
                btns_grid[c] = GUILayout.TextField(btns_grid[c], myStyle, GUILayout.Width(50), GUILayout.Height(50));
                if (btns_grid[c].Length > 1)
                {
                    btns_grid[c] = btns_grid[c].Substring(0, 1);
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(20);


            text_add = EditorGUILayout.TextField("extra word", text_add);
            if (GUILayout.Button("Add"))
            {
                if (text_add != null && text_add.Length > 2)
                {
                    web.adds.Add(ReplaceAllWhiteSpaces(text_add));
                    text_add = "";
                }
                else
                {
                    EditorUtility.DisplayDialog("error", "extra word value is wrong", "ok");

                }
            }



            if (web.adds.Count == 0) return;

            //GUILayout.BeginArea(new Rect(0, 100, 235f, 400f));

            ScrollPos = EditorGUILayout.BeginScrollView(ScrollPos, GUILayout.ExpandHeight(true));

            int n = 0;
            if (web.adds.Count == 0) return;
            for (int h = 0; h < 500; h++)
            {

                GUILayout.BeginHorizontal();
                for (int w = 0; w < 8; w++)
                {
                    if (n == web.adds.Count)
                    {
                        EditorGUILayout.EndScrollView();
                        //GUILayout.EndArea();
                        return;
                    }
                    GUIStyle myStyle = new GUIStyle(GUI.skin.textField);
                    myStyle.alignment = TextAnchor.MiddleCenter;
                    if (GUILayout.Button(web.adds[n], GUILayout.Width(web.adds[n].Length * 10), GUILayout.Height(30)))
                    {
                        web.adds.RemoveAt(n);
                        return;
                    }

                    n++;

                }
                GUILayout.EndHorizontal();
            }
        }

        // for edit step
        public void MenuGUI2()
        {
            GUILayout.BeginArea(new Rect(position.width - 60, 0, 60, 30));
            if (GUILayout.Button("back"))
            {
                switchMenu(0);
            }
            GUILayout.EndArea();

            GUILayout.Space(10);
            isLtoR = GUILayout.Toggle(isLtoR, "is L to R language");
            GUILayout.Space(5);

            if (GUILayout.Button("Save Step"))
            {
                web.btns = btns_grid;
                web.qus = new List<gamespace.Qus3>();

                getChars();

                steps_parent.GetChild(id_step_edit).GetComponent<mapspace.Step>().json = JsonUtility.ToJson(web);

                EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());

                if (isLtoR)
                    PlayerPrefs.SetInt("isLtoR", 1);
                else
                    PlayerPrefs.SetInt("isLtoR", 0);

                EditorUtility.DisplayDialog("done", "done save step", "ok");
            }

            if (GUILayout.Button("remove this step"))
            {
                if (EditorUtility.DisplayDialog("remove step", "are you sure ??", "ok", "cancel"))
                {
                    DestroyImmediate(steps_parent.GetChild(id_step_edit).gameObject);
                    switchMenu(0);
                    EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
                    PlayerPrefs.SetInt("total_step_count", steps_parent.childCount);
                    return;
                }

            }
            if (GUILayout.Button("try step"))
            {
                PlayerPrefs.SetInt("id_test_step", id_step_edit + 1);
                EditorSceneManager.OpenScene("Assets/Mohanad developer/Mix Letters (Word Games)/Scenes/Main.unity");

                this.Close();

                EditorApplication.EnterPlaymode();
            }

            listssh = new List<int[]>();
            listssw = new List<int[]>();

            GUILayout.Label("chars Map:", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            int i = 0;
            for (int h = 0; h < hight; h++)
            {
                int[] arrw = new int[width];
                //GUILayout.BeginVertical("hi");
                GUILayout.BeginArea(new Rect(42 * h, 120, 40, 42 * width));
                for (int w = 0; w < width; w++)
                {

                    GUIStyle myStyle = new GUIStyle(GUI.skin.textField);
                    myStyle.alignment = TextAnchor.MiddleCenter;
                    chars_grid[i] = GUILayout.TextField(chars_grid[i], myStyle, GUILayout.Width(40), GUILayout.Height(40));
                    if (chars_grid[i].Length > 1)
                    {
                        chars_grid[i] = chars_grid[i].Substring(0, 1);
                    }
                    arrw[w] = i;
                    i++;

                }

                GUILayout.EndArea();

                listssw.Add(arrw);

            }
            GUILayout.EndHorizontal();
            for (int h = 0; h < width; h++)
            {
                int[] arrh = new int[hight];
                int ii = 0;
                for (int x = h; x < hight * width; x = x + width)
                {
                    arrh[ii] = x;
                    ii++;
                }
                listssh.Add(arrh);

            }
            GUILayout.Space(width * 45);
            GUILayout.Label("chars for mix:", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            for (int c = 0; c < count_chars; c++)
            {
                GUIStyle myStyle = new GUIStyle(GUI.skin.textField);
                myStyle.alignment = TextAnchor.MiddleCenter;
                btns_grid[c] = GUILayout.TextField(btns_grid[c], myStyle, GUILayout.Width(50), GUILayout.Height(50));
                if (btns_grid[c].Length > 1)
                {
                    btns_grid[c] = btns_grid[c].Substring(0, 1);
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(20);


            text_add = EditorGUILayout.TextField("extra word", text_add);
            if (GUILayout.Button("Add"))
            {
                if (text_add != null && text_add.Length > 2)
                {
                    web.adds.Add(ReplaceAllWhiteSpaces(text_add));
                    text_add = "";
                }
                else
                {
                    EditorUtility.DisplayDialog("error", "extra word value is wrong", "ok");

                }
            }



            ScrollPos = EditorGUILayout.BeginScrollView(ScrollPos, GUILayout.ExpandHeight(true));

            int n = 0;
            //if (web.adds.Count == 0) return;
            for (int h = 0; h < 500; h++)
            {

                GUILayout.BeginHorizontal();
                for (int w = 0; w < 8; w++)
                {
                    if (n == web.adds.Count)
                    {
                        EditorGUILayout.EndScrollView();
                        setEditData();
                        return;
                    }
                    GUIStyle myStyle = new GUIStyle(GUI.skin.textField);
                    myStyle.alignment = TextAnchor.MiddleCenter;
                    if (GUILayout.Button(web.adds[n], GUILayout.Width(web.adds[n].Length * 10), GUILayout.Height(30)))
                    {
                        web.adds.RemoveAt(n);
                        return;
                    }

                    n++;

                }
                GUILayout.EndHorizontal();
            }


        }
        int id_step_edit = 0;
        bool isDoneSetEditData = false;
        public void setEditData()
        {

            if (isDoneSetEditData) return;

            for (int i = 0; i < web.chars.Length; i++)
            {
                chars_grid[i] = web.chars[i];
            }
            for (int i = 0; i < web.btns.Length; i++)
            {
                btns_grid[i] = web.btns[i];
            }

            isDoneSetEditData = true;
        }



        Vector2 ScrollPos;

        public void MenuGUI0()
        {
            hight = EditorGUILayout.IntField("width", hight);
            width = EditorGUILayout.IntField("height", width);
            count_chars = EditorGUILayout.IntField("count chars", count_chars);

            if (GUILayout.Button("start"))
            {
                if ((width + "").Length == 0 || width <= 0)
                {
                    EditorUtility.DisplayDialog("error", "Height value is wrong", "ok");
                    return;
                }
                if ((hight + "").Length == 0 || hight <= 0)
                {
                    EditorUtility.DisplayDialog("error", "Width value is wrong", "ok");
                    return;
                }
                if ((count_chars + "").Length == 0 || count_chars <= 0)
                {
                    EditorUtility.DisplayDialog("error", "(count chars) value is wrong", "ok");
                    return;
                }
                if (width > 15)
                {
                    EditorUtility.DisplayDialog("error", "Height value is big", "ok");
                    return;
                }
                if (hight > 15)
                {
                    EditorUtility.DisplayDialog("error", "Width value is big", "ok");
                    return;
                }
                if (count_chars > 8)
                {
                    EditorUtility.DisplayDialog("error", "(count chars) value is big", "ok");
                    return;
                }
                if (width < 3)
                {
                    EditorUtility.DisplayDialog("error", "Height value is low", "ok");
                    return;
                }
                if (hight < 3)
                {
                    EditorUtility.DisplayDialog("error", "Width value is low", "ok");
                    return;
                }
                if (count_chars < 3)
                {
                    EditorUtility.DisplayDialog("error", "(count chars) value is low", "ok");
                    return;
                }

                web.hh = hight;
                web.vv = width;
                web.chars = new string[hight * width];
                chars_grid = new string[hight * width];
                web.btns = new string[count_chars];
                btns_grid = new string[count_chars];

                for (int i = 0; i < chars_grid.Length; i++)
                {
                    chars_grid[i] = "";
                    web.chars[i] = "";
                }
                for (int i = 0; i < count_chars; i++)
                {
                    btns_grid[i] = "";
                }
                switchMenu(1);

            }
            if (steps_parent.childCount == 0) return;

            GUILayout.BeginArea(new Rect(0, 100, 235f, 400f));

            ScrollPos = EditorGUILayout.BeginScrollView(ScrollPos, GUILayout.ExpandHeight(true));

            int n = 1;

            for (int h = 0; h < 500; h++)
            {

                GUILayout.BeginHorizontal();
                for (int w = 0; w < 5; w++)
                {

                    GUIStyle myStyle = new GUIStyle(GUI.skin.textField);
                    myStyle.alignment = TextAnchor.MiddleCenter;
                    if (GUILayout.Button("" + n, GUILayout.Width(40), GUILayout.Height(40)))
                    {

                        id_step_edit = n - 1;
                        web = JsonUtility
                            .FromJson<gamespace.web_item3>(steps_parent.GetChild(id_step_edit).GetComponent<mapspace.Step>().json);
                        hight = web.hh;
                        width = web.vv;
                        count_chars = web.btns.Length;
                        chars_grid = new string[hight * width];
                        btns_grid = new string[count_chars];

                        for (int i = 0; i < chars_grid.Length; i++)
                        {
                            chars_grid[i] = "";
                        }
                        for (int i = 0; i < count_chars; i++)
                        {
                            btns_grid[i] = "";
                        }
                        isDoneSetEditData = false;
                        switchMenu(2);
                    }
                    if (n == steps_parent.childCount)
                    {
                        EditorGUILayout.EndScrollView();
                        GUILayout.EndArea();

                        return;
                    }
                    n++;

                }
                GUILayout.EndHorizontal();
            }
        }


        public void getChars()
        {
            for (int i = 0; i < chars_grid.Length; i++)
            {
                web.chars[i] = chars_grid[i];
            }
            foreach (int[] arr in listssh)
            {
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    if (chars_grid[arr[i]].Length > 0)
                    {
                        if (i == 0 && chars_grid[arr[1]].Length > 0)
                        {
                            selecter(arr, i, true);
                        }
                        else if (i != 0 && chars_grid[arr[i + 1]].Length > 0 && chars_grid[arr[i - 1]].Length == 0)
                        {
                            selecter(arr, i, true);
                        }
                    }
                }
            }
            foreach (int[] arr in listssw)
            {
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    if (chars_grid[arr[i]].Length > 0)
                    {
                        if (i == 0 && chars_grid[arr[1]].Length > 0)
                        {
                            selecter(arr, i, false);
                        }
                        else if (i != 0 && chars_grid[arr[i + 1]].Length > 0 && chars_grid[arr[i - 1]].Length == 0)
                        {
                            selecter(arr, i, false);
                        }
                    }
                }
            }

        }

        public void selecter(int[] arr, int start_id, bool ish)
        {
            gamespace.Qus3 qus = new gamespace.Qus3();
            List<int> quslist = new List<int>();

            for (int i = start_id; i < arr.Length; i++)
            {
                if (chars_grid[arr[i]].Length > 0)
                {
                    quslist.Add(arr[i]);
                }
                else
                {
                    break;
                }
            }
            if (ish && !isLtoR) quslist.Reverse();
            qus.ans = quslist.ToArray();
            web.qus.Add(qus);
        }

        public string ReplaceAllWhiteSpaces(string str)
        {
            return Regex.Replace(str, @"\s+", string.Empty);
        }

        public void reName()
        {
            for (int i = 0; i < steps_parent.childCount; i++)
            {
                steps_parent.GetChild(i).gameObject.name = i + 1 + "";
            }
        }

    }

}