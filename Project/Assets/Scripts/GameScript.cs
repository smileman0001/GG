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
    public GameObject Win;
    public GameObject Menu1st;
    public GameObject Theory;
    public GameObject Intro;

    public Image QuizBack;
    public Sprite Right;
    public Sprite Wrong;

    public int Gold;
    public int AllGold;

    public Text GoldText;
    public Text GoldText1;
    public Text GoldText2;
    public Text moneyText;
    public Text inventory;

    public int cost;
    public string itemName;
    
    public Image BobHead;
    public Sprite zero;
    public Sprite first;
    public Sprite second;
    public Sprite third;

    public GameObject firstHead;
    public GameObject secondHead;
    public GameObject thirdHead;

    public Image BobHeadFall;
    public Sprite zeroFall;
    public Sprite firstFall;
    public Sprite secondFall;
    public Sprite thirdFall;

    public GameObject PointerZero;
    public GameObject PointerOne;
    public GameObject PointerTwo;
    public GameObject PointerThree;

    public bool boughtFirst = false;
    public bool boughtSecond = false;
    public bool boughtThird = false;

    public int Lives = 3;

    public GameObject threeHP;
    public GameObject twoHP;
    public GameObject oneHP;


    public void OnClickPlay()
    {
        questionList = new List<object>(questions);
        questionGenerate();
        Animate(Head, "In");
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
        {
            Animate(Win, "In3");

            AllGold += Gold;
            moneyText.text = AllGold.ToString();
            GoldText1.text = Gold.ToString();
            GoldText2.text = Gold.ToString();

            StartCoroutine(Delay2());
        }
    }

    IEnumerator Delay2()
    {
        yield return new WaitForSeconds(0.8f);
        Animate(Head, "Out");

        Gold = 0;

        Lives = 3;

        threeHP.SetActive(true);
        twoHP.SetActive(true);
        oneHP.SetActive(true);
    }

    IEnumerator trueOrFalse(bool check)
    {
        if (check)
        {
            QuizBack.sprite = Right;
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
            QuizBack.sprite = Wrong;
            Lives -= 1;

            if(Lives == 2)
                threeHP.SetActive(false);
            else
            {
                if (Lives == 1)
                    twoHP.SetActive(false);
                else
                {
                    Animate(GameOver, "In2");
                    StartCoroutine(Delay1());
                    AllGold += Gold;
                    moneyText.text = AllGold.ToString();
                    GoldText.text = Gold.ToString();
                }

            }

            yield return new WaitForSeconds(0);
        }
    }

    IEnumerator Delay1()
    {
        yield return new WaitForSeconds(0.8f);
        Animate(Head, "Out");

        oneHP.SetActive(false);
        Gold = 0;
        Lives = 3;
        QuizBack.sprite = Right;
        threeHP.SetActive(true);
        twoHP.SetActive(true);
        oneHP.SetActive(true);
    }

    public void AnswerBttns(int index)
    {
        if (answersText[index].text.ToString() == currentQuestion.answers[0]) StartCoroutine(trueOrFalse(true));
        else StartCoroutine(trueOrFalse(false));
    }

    public void IntroOut()
    {
        Animate(Intro, "OutIntro");
    }

    public void TheoryIn()
    {
        Animate(Theory, "In4");
    }

    public void TheoryOut()
    {
        Animate(Theory, "Out4");
    }

    public void In(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void Out(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void BuyHat(int id)
    {
        if(id==1)
        {
            if ((AllGold >= 10) && boughtFirst == false)
            {
                AllGold -= 10;
                moneyText.text = AllGold.ToString();
                firstHead.SetActive(true);
                boughtFirst = true;
            }
        }
        else if(id == 2)
        {
            if ((AllGold >= 20) && boughtSecond == false)
            {
                AllGold -= 20;
                moneyText.text = AllGold.ToString();
                secondHead.SetActive(true);
                boughtSecond = true;
            }
        }
        else
        {
            if ((AllGold >= 45) && boughtThird == false)
            {
                AllGold -= 45;
                moneyText.text = AllGold.ToString();
                thirdHead.SetActive(true);
                boughtThird = true;
            }
        }
    }

    public void SwapZeroHat()
    {
        SwapHat(zero, zeroFall, true, false, false, false);
    }

    public void Swap1stHat()
    {
        SwapHat(first, firstFall, false, true, false, false);
    }

    public void Swap2ndHat()
    {
        SwapHat(second, secondFall, false, false, true, false);
    }

    public void Swap3rdHat()
    {
        SwapHat(third, thirdFall, false, false, false, true);
    }

    public void SwapHat(Sprite spr, Sprite spr2, bool tf1, bool tf2, bool tf3, bool tf4)
    {
        BobHead.sprite = spr;
        BobHeadFall.sprite = spr2;
        PointerZero.SetActive(tf1);
        PointerOne.SetActive(tf2);
        PointerTwo.SetActive(tf3);
        PointerThree.SetActive(tf4);
    }

    public void ReturnToMenu1stFromWin()
    {
        Animate(Win, "Out3");
    }

    public void ReturnToMenu1stFromGameOver()
    {
        Animate(GameOver, "Out2");
    }

    public void Animate(GameObject obj, string trigger)
    {
        if (!obj.GetComponent<Animator>().enabled)
            obj.GetComponent<Animator>().enabled = true;
        else
            obj.GetComponent<Animator>().SetTrigger(trigger);
    }
}
[System.Serializable]
public class QuestionList
{
    public string question;
    public string[] answers = new string[4];
}