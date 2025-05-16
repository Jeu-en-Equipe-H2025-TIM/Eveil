using UnityEngine;
using TMPro;
using System.Collections;

public class scriptTextTest : MonoBehaviour
{
    // Pris d'un tutoriel pour faire apparaitre du texte lettre par lettre par Pixelbug Studio, "Type Writing effect in Unity using TextMesh Pro" 

    [SerializeField] TextMeshProUGUI _textMeshPro;

    public string[] stringArray;

    [SerializeField] float timeBtwnChars;

    [SerializeField] float timeBtwnWords;

    int i = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
            EndCheck();
        
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EndCheck();
        }
    }*/

    void EndCheck()
    {
        if (i <= stringArray.Length - 1)
        {
            _textMeshPro.text = stringArray[i];
            StartCoroutine(TextVisible());
        }
    }

    private IEnumerator TextVisible()
    {
        _textMeshPro.ForceMeshUpdate();
        int totalVisibleCharacters = _textMeshPro.textInfo.characterCount;
        int counter = 0;

        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 3);
            _textMeshPro.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters)
            {
                i += 1;
               Invoke("EndCheck", timeBtwnWords);
                break;
            }

            counter += 3;
            yield return new WaitForSeconds(timeBtwnChars);
        }
    }
}
