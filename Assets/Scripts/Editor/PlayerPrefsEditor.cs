// http://smartgames.hatenablog.com/entry/2015/01/17/001456

using UnityEngine;
using UnityEditor;

namespace MosquitoGame.Editor
{
    public class PlayerPrefsEditor
    {
        [MenuItem("Tools/PlayerPrefs/DeleteAll")]
        static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Delete All Data Of PlayerPrefs!!");
        }
    }
}
