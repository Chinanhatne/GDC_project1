using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private TextMeshProUGUI goldText;
    private int gold;
    void Start()
    {
        gold = 0;
        goldText.text = "Gold : 0";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addGold(int goldAdd)
    {
        gold = gold + goldAdd;
        ChangeGoldText();
    }

    public int getGold()
    {
        return gold;
    }

    private void ChangeGoldText()
    {
        goldText.text = "Gold: " + getGold().ToString();
    }
    
    public void Continue()
    {
        Time.timeScale = 0f;
    }
}
