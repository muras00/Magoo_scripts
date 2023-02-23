using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneList : MonoBehaviour
{
    [Header("Scene List")]
    [SerializeField] private string[] _list;

    public void SetScene(string scene)
    {
        if (scene == string.Empty || scene == null)
        {
            Debug.LogError("The Parameter is Empty");
            return;
        }

        for (int i = 0; i < _list.Length; i++)
        {
            if (scene == _list[i]) {

                SceneManager.LoadScene(scene);
            }
        }
    }
}
