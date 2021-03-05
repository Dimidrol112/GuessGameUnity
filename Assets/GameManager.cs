using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FileProcessor))]
public class GameManager : MonoBehaviour
{
    public WordPanel wordPanel;
    public GameObject resultPanel;
    public Text resultText;
    public Text attemptsText;
    public Text scoreText;
    [SerializeField]
    private int minWordLenght = 4;
    private string currentWord;
    private string remainedChars;

    private int score = 0;
    private int Score { get { return score; } set { score = value; scoreText.text = "Score: "+value.ToString(); } }
    [SerializeField]
    private int attempts = 3;
    private int Attempts { get { return attempts; } set { attempts = value; attemptsText.text = "Attempts: "+value.ToString(); } }

    public string minLenghtString { set { minWordLenght = int.Parse(value); fileProcessor.minWordLenght = minWordLenght; } }

    private List<string> words = new List<string>();

    private FileProcessor fileProcessor;
    private List<Image> closedButtons = new List<Image>();
    private void Start()
    {
        fileProcessor = GetComponent<FileProcessor>();
    }

    public void StartGame()
    {
        Score = 0;
        Attempts = 3;
        ClearKeyboard();

        words = fileProcessor.GetWordList(minWordLenght);
        if (words.Count <= 0)
            fileProcessor.noWords.Invoke();
        InitNewWord();
    }

    private void InitNewWord()
    {
        currentWord = words[Random.Range(0, words.Count - 1)];
        remainedChars = currentWord;
        words.Remove(currentWord);
        wordPanel.GenNewWord(currentWord);
    }

    private void ClearKeyboard()
    {
        foreach (var button in closedButtons)
        {
            button.color = new Color(1, 1, 1, 0);
            button.GetComponent<Button>().interactable = true;
        }

        closedButtons = new List<Image>();
    }

    public void ProcessCharButtonPress(GameObject charButton)
    {
        if (currentWord.Contains(charButton.name))
        {
            wordPanel.OpenChars(charButton.name);
            RightChar(charButton.name);
        }
        else
        {
            WrongChar();
        }
        closedButtons.Add(charButton.GetComponent<Image>());
        charButton.GetComponent<Image>().color = Color.black;
        charButton.GetComponent<Button>().interactable = false;
    }

    private void RightChar(string rightChar)
    {
        remainedChars = remainedChars.Replace(rightChar, "");
        if(remainedChars.Length <= 0)
        {
            StartCoroutine(NextWord());
        }
    }

    private void WrongChar()
    {
        Attempts--;
        if (Attempts < 0)
        {
            GameOver();
        }
    }

    private IEnumerator NextWord()
    {
        yield return new WaitForEndOfFrame();
        Score += Attempts;
        if (words.Count <= 0)
        {
            Win();
            yield break;
        }
        
        Attempts = 3;
        ClearKeyboard();
        InitNewWord();
    }

    private void Win()
    {
        resultPanel.SetActive(true);
        resultText.text = "U won";
    }

    private void GameOver()
    {
        resultPanel.SetActive(true);
        resultText.text = "Game over";
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
