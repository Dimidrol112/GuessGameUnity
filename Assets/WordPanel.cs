using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordPanel : MonoBehaviour
{
    public GameObject charPrefab;
    public bool showNextWord = true;
    private List<Character> characters = new List<Character>();

    public void ClearWord()
    {
        if (characters.Count > 0)
        {
            for (int i = 0; i < characters.Count; i++)
            {
                Destroy(characters[i].go.gameObject);
            }
        }

        characters = new List<Character>();
    }

    public void GenNewWord(string word)
    {
        if(showNextWord)
            Debug.Log(word);

        ClearWord();
        foreach (var character in word)
        {
            var characterObject = Instantiate(charPrefab, transform);
            var charComp = characterObject.GetComponent<CharInWord>();
            charComp.charText.text = character.ToString();
            characters.Add(new Character { character = character.ToString(), cover = charComp.charCover, go = characterObject });
        }
    }

    public void OpenChars(string charToOpen)
    {
        foreach (var item in characters)
        {
            if(item.character == charToOpen)
            {
                item.cover.SetActive(false);
            }
        }
    }
}

public class Character
{
    public string character;
    public GameObject cover;
    public GameObject go;
}
