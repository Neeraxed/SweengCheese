using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timeOfVulnerability;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject restartScreen;
    [SerializeField] private GameObject renderedParts;
    [SerializeField] private CharacterController cc;
    
    private const string YandexLeaderBoardName = "ScoreSweengCheese";

    public void ContinueScene()
    {
        cc.detectCollisions = false;
        restartScreen.SetActive(false);
        PlayerManager.gameOver = false;
        Time.timeScale = 0.6f;
        StartCoroutine(BecomeVulnerable());
        StartCoroutine(Blink());
        StartCoroutine(SetTimeScaleToNormal());
        Debug.Log("ContinueScene");
    }
    private IEnumerator BecomeVulnerable()
    {
        yield return new WaitForSeconds(timeOfVulnerability);
        cc.detectCollisions = true;
    }
    private IEnumerator Blink()
    {
        for (int i = 0; i < timeOfVulnerability * 5f; i++)
        {
            renderedParts.SetActive((!renderedParts.activeSelf));
            yield return new WaitForSeconds(0.2f);
        }
        renderedParts.SetActive(true);
    }
    private IEnumerator SetTimeScaleToNormal()
    {
        while (Time.timeScale < 1f)
        {
            yield return new WaitForSeconds(timeOfVulnerability);
            Time.timeScale += 0.2f;
        }
    }   
}
