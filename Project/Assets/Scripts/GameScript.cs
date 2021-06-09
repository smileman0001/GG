using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{

    public QuestionList[] questions;
    public Text[] answersText;
    public Text questionText;
    QuestionList currentQuestion;
    List<object> questionList;
    int randomQuestion;
    public GameObject Head;
    public GameObject GameOver;
    public int Gold;
    public int AllGold;
    public Text GoldText;
   

    public void OnClickPlay()
    {
        questionList = new List<object>(questions);
        questionGenerate();
        if (!Head.GetComponent<Animator>().enabled) 
            Head.GetComponent<Animator>().enabled = true;
        else 
            Head.GetComponent<Animator>().SetTrigger("In");
    }
    void questionGenerate()
    {
        if (questionList.Count > 0)
        {
            randomQuestion = Random.Range(0, questionList.Count);
            currentQuestion = questionList[randomQuestion] as QuestionList;
            questionText.text = currentQuestion.question;
            List<string> answers = new List<string>(currentQuestion.answers);
            for (int i = 0; i < currentQuestion.answers.Length; i++)
            {
                int random = Random.Range(0, answers.Count);
                answersText[i].text = answers[random];
                answers.RemoveAt(random);
            }
        }
        else
            print("Вы прошли игру");
    }

    IEnumerator trueOrFalse(bool check)
    {
        if (check)
        {
            Gold += 1;

            print("Всего заработано" + Gold.ToString());
            print("Всего имеется" + AllGold.ToString());
            print("Правильный ответ");
            yield return new WaitForSeconds(0);
            questionList.RemoveAt(randomQuestion);
            questionGenerate();
            yield break;
        }
        else
        {
            if (!GameOver.GetComponent<Animator>().enabled)
                GameOver.GetComponent<Animator>().enabled = true;
            else
                GameOver.GetComponent<Animator>().SetTrigger("In2");

            AllGold += Gold;

            GoldText.text = Gold.ToString();
            print("Неправильный ответ");
            print(" ");
            print("Всего заработано за игру" + Gold.ToString());
            print("");
            print("Всего заработано" + AllGold.ToString());
            Gold = 0;
            yield return new WaitForSeconds(0);
        }
    }

    public void AnswerBttns(int index)
    {
        if (answersText[index].text.ToString() == currentQuestion.answers[0]) StartCoroutine(trueOrFalse(true));
        else StartCoroutine(trueOrFalse(false));
    }
}
[System.Serializable]
public class QuestionList
{
    public string question;
    public string[] answers = new string[4];
}