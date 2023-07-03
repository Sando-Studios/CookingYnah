using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asyncoroutine;

public class Bocchi : MonoBehaviour
{
    [SerializeField] private float lifeSpan;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeSpan);
        Grow(gameObject);
    }

    public async void Grow(GameObject obj)
    {
        while (obj)
        {
            Vector3 newScale = gameObject.transform.localScale;
            newScale.y += 0.5f;
            gameObject.transform.localScale = newScale;
            await new WaitForSeconds(0.2f);
        }
    }
}
