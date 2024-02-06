using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public void ReloadSceneAfterDelay(float delay)
{
    StartCoroutine(ReloadAfterDelay(delay));
}

private IEnumerator ReloadAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay);
    UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name); // Restart the game
}
}