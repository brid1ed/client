using System.Collections;
using Network;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Manager
{
    public class GameSceneManager : BaseManager
    {

        private string next_scene;

        private const string load_scene_name = "LoadScene";

        [SerializeField] private Image bar;
        
        public override bool Init()
        {
            next_scene = "";
            bar = null;
            Client client = new Client();
            
            return false;
        }


        public void LoadScene(string name) {
            this.next_scene = name;
            SceneManager.LoadScene(load_scene_name);
            StartCoroutine(LoadSceneWithLoading());
        }
        
        

        private bool FindBar()
        {
            GameObject obj = GameObject.Find("Progress");
            if(obj == null){ Debug.LogError("[Loading] Not Found ProgressBar");
                return false;
            }
            else
            {
                bar = obj.GetComponent<Image>();
                if(bar == null){ 
                    Debug.LogError("[Loading] Not Found ProgressBar");
                }
                return true;

            }
        }
        
        





        IEnumerator LoadSceneWithLoading()
        {

            while (!FindBar()) yield return new WaitForSeconds(0.1f);

            if (bar != null)
            {

                AsyncOperation async = SceneManager.LoadSceneAsync(next_scene);
                async.allowSceneActivation = false;
                float std = 0.9f;
                float progress = 0f;
                while (!async.isDone)
                {
                    yield return null; // 걍 넘겨줬다가 다시 바로 들어가버리기
                    if (async.progress < std) bar.fillAmount = async.progress;
                    else
                    {
                        progress += Time.unscaledDeltaTime;
                        bar.fillAmount = Mathf.Lerp(std, 1f, progress);
                        if (bar.fillAmount >= 1f)
                        {
                            yield return new WaitForSeconds(0.1f);
                            async.allowSceneActivation = true;
                            GameManager.Instance.GetSoundManager().SetStop(true);
                            yield break;
                        }
                    }

                }
            }
        }


    }
}