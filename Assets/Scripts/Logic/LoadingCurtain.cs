using System.Collections;
using UnityEngine;

namespace Logic
{
    public class LoadingCurtain : MonoBehaviour
    {
        private CanvasGroup _curtain;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            _curtain = GetComponent<CanvasGroup>();
        }
        public void Show()
        {
            gameObject.SetActive(true);
            _curtain.alpha = 1;
        }
        public void Hide()
        {
            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            while(_curtain.alpha > 0)
            {
                _curtain.alpha -= 0.03f;
                yield return new WaitForSeconds(0.03f);
            }

            gameObject.SetActive(false);
        }
    }
}

