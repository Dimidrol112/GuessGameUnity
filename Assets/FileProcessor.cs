using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

public class FileProcessor : MonoBehaviour
{
    
    public UnityEvent noWords;
    public int minWordLenght = 4;
    public UnityEvent noFile;

    [ContextMenu("TestGen")]
    public void TestGen()
    {
        GetWordList(4);
    }

    public void CheckIsThereWords()
    {
        if(GetWordList(minWordLenght).Count <=0 )
        {
            noWords.Invoke();
        }
    }

    public List<string> GetWordList(int minLenght)
    {
        if(!File.Exists(Application.dataPath + "/source.txt"))
        {
            noFile.Invoke();
            return new List<string>();
        }
        string rawData = File.ReadAllText(Application.dataPath + "/source.txt").ToLower();
        var rawWords = Regex.Split(rawData, @"\W+").Where(word => word != "");
        List<string> cleanWords = new List<string>();
        foreach (var rawWord in rawWords)
        {
            string onlyAlfabet = Regex.Replace(rawWord, "[0-9]", "", RegexOptions.IgnoreCase);
            bool isLongEnoght = onlyAlfabet.Length >= minLenght;
            if (!string.IsNullOrEmpty(onlyAlfabet) && isLongEnoght)
            {
                cleanWords.Add(onlyAlfabet);
            }
        }

        var uniqWords = cleanWords.Distinct();
        return uniqWords.ToList(); ;
    }
}
