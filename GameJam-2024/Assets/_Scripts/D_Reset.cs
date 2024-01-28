using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class D_Reset : MonoBehaviour
{
    [Button]
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
