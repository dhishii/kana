namespace mainspace
{
    using UnityEngine;

    public class SoundsManger : MonoBehaviour
    {
        public static SoundsManger instance;


        [SerializeField]
        private AudioSource audioSource;

        [SerializeField]
        private AudioClip start_web;
        [SerializeField]
        private AudioClip click_square;
        [SerializeField]
        private AudioClip start_btn;
        [SerializeField]
        private AudioClip collect_help;
        [SerializeField]
        private AudioClip collect_hint;
        [SerializeField]
        private AudioClip collect_coins;
        [SerializeField]
        private AudioClip true_effect;
        [SerializeField]
        private AudioClip false_effect;
        [SerializeField]
        private AudioClip click_btn;
        [SerializeField]
        private AudioClip old_effect;

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); return; }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void clickSquare()
        {
            audioSource.PlayOneShot(click_square);
            audioSource.pitch = Random.Range(0.8f, 1.2f);

        }
        public void clickStart_web()
        {
            audioSource.PlayOneShot(start_web);

        }
        public void clickBtn()
        {
            audioSource.PlayOneShot(click_btn);
            audioSource.pitch = Random.Range(0.8f, 1.2f);
        }
        public void collect_coins_sound()
        {
            audioSource.PlayOneShot(collect_coins);

        }
        public void collect_help_sound()
        {
            audioSource.PlayOneShot(collect_help);

        }
        public void collect_help_hint()
        {
            audioSource.PlayOneShot(collect_hint);

        }
        public void run_true_effect()
        {
            audioSource.PlayOneShot(true_effect);

        }
        public void run_false_effect()
        {
            audioSource.PlayOneShot(false_effect);

        }
        public void run_old_effect()
        {
            audioSource.PlayOneShot(old_effect);

        }
    }
}