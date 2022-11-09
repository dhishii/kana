namespace toolspace
{
    using UnityEngine;

    public class GameData : MonoBehaviour
    {

        //internal int total_step_count;
        [Header("Colors")]
        [Tooltip("Arbitary text message")]
        // Material Colors
        public Color ColorA;
        public Color ColorB;
        public Color ColorC;

        public Color help_panel;
        public Color top_panel;
        public Color select1;
        public Color select2;

        public Color text_qus_color;

        [Header("Social Media Links")]
        [Tooltip("Arbitary text message")]
        public string face_book_group_id;
        public string instagram_id;
        [Header("packages")]
        [Tooltip("Arbitary text message")]
        public string android_packege = "com.mohanad.cross";
        public string ios_id = "id1607084016";
        [Header("Admob Network Identifiers")]
        [Tooltip("Arbitary text message")]
        public string admob_android_banner = "";
        public string admob_android_Interstitial = "";
        public string admob_android_rewarded = "";

        public string admob_ios_banner = "";
        public string admob_ios_Interstitial = "";
        public string admob_ios_rewarded = "";
        [Header("In-app purchase identifiers")]
        [Tooltip("Arbitary text message")]
        public string coin_100 = "coin_100";
        public string coin_250 = "coin_250";
        public string coin_1000 = "coin_1000";

    }
}
