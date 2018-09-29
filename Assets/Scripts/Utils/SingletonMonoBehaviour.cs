// 妹蚊プロジェクトより、Thanks to @0V

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MosquitoGame.Utils
{

    /// <summary>
    /// シングルトンなMonoBehaviour.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));
                    if (instance == null)
                    {
                        Debug.LogError(typeof(T) + "をアタッチしているGameObjectはありません.");
                    }
                }
                return instance;
            }
        }

        protected virtual void Awake()
        {
            CheckInstance();
        }

        protected bool CheckInstance()
        {
            if (instance == null)
            {
                instance = (T)this;
                return true;
            }
            else if (Instance == this)
            {
                return true;
            }

            Destroy(this);
            Debug.LogError(
                typeof(T) +
                " は既に他のGameObjectにアタッチされているため、コンポーネントを破棄しました." +
                " アタッチされているGameObjectは " + Instance.gameObject.name + " です.");

            return false;
        }

        void OnDestroy()
        {
            instance = null;
        }
    }

}
