using System.IO;
using UnityEditor;
using UnityEngine;

namespace Surfer
{
    [InitializeOnLoadAttribute]
    public static class SUHierarchyMonitor
    {

        static SUHierarchyMonitor()
        {
            EditorApplication.hierarchyChanged += OnHierarchyChanged;
            EditorApplication.projectChanged += OnProjectChanged;
        }
        static void OnHierarchyChanged()
        {

            SurferHelper.SO.UpdateSceneList();
            SurferHelper.SO.UpdateLayersList();
            SurferHelper.SO.UpdateTagsList();
            SurferHelper.SO.UpdateEventsList();

            SurferManager[] sms = GameObject.FindObjectsOfType<SurferManager>();
            SurferManager mainCp = null;

            if (sms.Length <= 0)
            {
                mainCp = new GameObject("Surfer").AddComponent<SurferManager>();
            }
            else
            {
                mainCp = sms[0];

                if (sms.Length > 1)
                {
                    GameObject.DestroyImmediate(sms[1].gameObject);
                }
            }

            if (mainCp.gameObject.GetComponent<SUSafeAreaManager>() == null)
                mainCp.gameObject.AddComponent<SUSafeAreaManager>();
            if (mainCp.gameObject.GetComponent<SUInputIconsManager>() == null)
                mainCp.gameObject.AddComponent<SUInputIconsManager>();

        }

        static void OnProjectChanged()
        {

            SurferHelper.SO.UpdateSceneList();
            SurferHelper.SO.UpdateLayersList();
            SurferHelper.SO.UpdateTagsList();
            SurferHelper.SO.UpdateEventsList();

        }



    }
}



