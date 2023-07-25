using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    public static Minimap Instance;

    [Tooltip("Top Left")]
    [SerializeField] private Transform topLeft;
    [Tooltip("Bottom Right")]
    [SerializeField] private Transform bottomRight;

    private Vector3 topRight;
    private Vector3 bottomLeft;

    private Dictionary<Transform, RectTransform> toTrack = new();
    [SerializeField] GameObject iconPrefab;

    [SerializeField] private Image minimapImg;

    public void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        topRight = new Vector3(bottomRight.position.x, 0, topLeft.position.z);
        bottomLeft = new Vector3(topLeft.position.x, 0, bottomRight.position.z);
        
        Track(UIManager.instance.player.gameObject);
    }

    // error when the player dies
    void Update()
    {
        foreach (var pair in toTrack)
        {
            if (!pair.Value.gameObject) toTrack.Remove(pair.Key);
            pair.Value.anchoredPosition = GetEquivalentLocation(pair.Key.transform);
        }
    }

    #if UNITY_EDITOR
    [UnityEditor.MenuItem("Manager Tools/Minimap/Clear All")]
    public static void ClearAll()
    {
        foreach (var keyValuePair in Instance.toTrack)
        {
            Instance.UnTrack(keyValuePair.Key.gameObject);
        }
    }
    #endif

    public void OnDrawGizmos()
    {
        topRight = new Vector3(bottomRight.position.x, 0, topLeft.position.z);
        bottomLeft = new Vector3(topLeft.position.x, 0, bottomRight.position.z);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(topRight, 5);
        Gizmos.DrawSphere(bottomLeft, 5);
        Gizmos.DrawSphere(topLeft.position, 5);
        Gizmos.DrawSphere(bottomRight.position, 5);

        Gizmos.DrawLine(topLeft.position, topRight);
        Gizmos.DrawLine(topRight, bottomRight.position);
        Gizmos.DrawLine(bottomRight.position, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft.position);

    }

    public void Track(GameObject untracked)
    {
        var icon = Instantiate(iconPrefab, Vector2.zero, Quaternion.identity, minimapImg.transform);
        // TODO: Replace the icon on the prefab from `VisibleInMinimap` to `icon`
        toTrack.Add(untracked.transform, icon.GetComponent<RectTransform>());
    }

    public void UnTrack(GameObject tracked)
    {
        if (!toTrack.ContainsKey(tracked.transform))
        {
            Debug.LogWarning($"Seems like {tracked.name} was not tracked in the first place");
            return;
        }
        Destroy(toTrack[tracked.transform].gameObject);
        toTrack.Remove(tracked.transform);
    }

    private Vector2 GetEquivalentLocation(Transform thing)
    {
        var miniMap = minimapImg.GetComponent<RectTransform>();

        var x = Util.Map(thing.position.x, topLeft.position.x, bottomRight.position.x, miniMap.rect.width / -2, miniMap.rect.width / 2);
        var y = Util.Map(thing.position.z, bottomRight.position.z, topLeft.position.z, miniMap.rect.height / -2, miniMap.rect.height / 2);

        return new Vector2(x, y);
    }
}

public static class Util
{
    public static float Map(float val, float min1, float max1, float min2, float max2)
    {
        return min2+(max2-min2)*((val-min1)/(max1-min1));
    }
}