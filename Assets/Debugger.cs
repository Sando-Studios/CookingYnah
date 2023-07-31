using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Debugger : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    [SerializeField] private ArtifactProgress _progress;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.SetActive(!canvas.activeSelf);
        }
    }

    public void Max()
    {
        var pdata = UIManager.instance.player.GetPlayerData();
        pdata.Vitality = 10000;
        pdata.Strength = 10000;
        pdata.RawDamage = 10000;
        pdata.Resilience = 10000;
        pdata.CurrentHealth = pdata.MaxHealth;
    }

    public void UnlockArtifacts()
    {
        if (EnvironmentRef.Instance == null) return;
        
        _progress.UnlockAll();

        var door = EnvironmentRef.Instance.objects["artifactDoor"]?.GetComponent<ArtifactDoorHandler>();
        door.Start();
    }

    public void TeleportToChamber()
    {
        if (EnvironmentRef.Instance == null) return;

        var key = "bossArea";

        var inst = EnvironmentRef.Instance;

        if (!inst.objects.ContainsKey(key))
        {
            Debug.LogError("No Boss Area");
            return;
        }

        var trans = UIManager.instance.player.transform;

        var @ref = inst.objects[key].transform.position;

        trans.transform.position = new Vector3(@ref.x, trans.position.y, @ref.z);
    }
	
	public void ExitToMainMenu() {
		SceneManager.LoadSceneAsync("MainMenu").completed += _ =>
        {
            canvas.SetActive(false);
        };
	}
}
