
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeSceneOnDeath : OnDeathBehavior
{
    [SerializeField] private float delay;
    public string sceneName;

    protected override void OnDeath()
    {
        StartCoroutine(LoadStartScene());
        Damageable.OnDeath -= OnDeath;
    }

    private IEnumerator LoadStartScene()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}