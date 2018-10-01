// http://baba-s.hatenablog.com/entry/2016/01/01/100000
using UnityEngine;

namespace MosquitoGame.Utils
{
    public static class PlayerPrefsUtils
    {
        /// <summary>
        /// 指定されたオブジェクトの情報を保存します
        /// </summary>
        public static void SetObject<T>(string key, T obj)
        {
            var json = JsonUtility.ToJson(obj);
            PlayerPrefs.SetString(key, json);
        }

        /// <summary>
        /// 指定されたオブジェクトの情報を読み込みます
        /// </summary>
        public static T GetObject<T>(string key)
        {
            var json = PlayerPrefs.GetString(key);
            var obj = JsonUtility.FromJson<T>(json);
            return obj;
        }

        public static bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }
    }
}
