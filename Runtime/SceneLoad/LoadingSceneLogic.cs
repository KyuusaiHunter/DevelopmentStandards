using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Rendering;
namespace StandardizedProcess
{
    public class LoadingSceneLogic : MonoBehaviour
    {
        public Transform barfill;
        public Vector3 startPos;
        public Vector3 endPos = Vector3.zero;
        public void Start()
        {
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
            if (!MainRoot.Instance.��Ŀ����.�������Ƿ�����)
            {
                Transform player = GameObject.FindWithTag("Player").transform;
                player.localPosition = Vector3.zero;
                player.localRotation = Quaternion.Euler(Vector3.zero);
            }
            startPos = barfill.localPosition;
            StartCoroutine(LoadTargetScene());
        }

        private IEnumerator LoadTargetScene()
        {
            int sceneToLoad = SceneTransferData.TargetSceneNumber;

            if (sceneToLoad! < 0)
            {
                Debug.LogError("δ����Ŀ�곡������");
                yield break;
            }

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
            asyncLoad.allowSceneActivation = false;

            while (asyncLoad.progress < 0.9f)
            {
                float clampe = Mathf.Lerp(startPos.x, endPos.x, asyncLoad.progress);
                Debug.Log(clampe);
                barfill.localPosition = new Vector3(clampe, startPos.y, startPos.z);
                // ���ڴ����ý������� asyncLoad.progress
                yield return null;
            }
            if (sceneToLoad == 0)
            {
                try
                {
                    
                }
                catch
                {

                }
            }
            else
            {
                
            }

            // �ɲ��붯��/�ӳ�
            yield return new WaitForSeconds(1f);

            asyncLoad.allowSceneActivation = true;
            SceneTransferData.TargetSceneNumber = -1;
        }
    }
}

