using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //iniciamos la operacion asincrona
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation(){
        //creamos la operacion asincrona
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(2);
        //cuando finalice cargamos la escena
        yield return new WaitForEndOfFrame();
    }
}
