using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class Validation
{
    static public bool EmailCheck(string email)
    {
        bool bChecking = true;

        if (email.Length > 1)  //길이가 2이상
        {
            if (!email.Contains(" ")) // 띄어쓰기있는지 체크
            {

                if (!isKorean(email))
                {

                    if (email.Contains("."))
                    {
                        string[] split = email.Split('.');
                        for (int i = 0; i < split.Length; i++)
                        {
                            if (split[i].Length <= 1)
                            {
                                bChecking = false;
                            }

                        }

                    }
                    else
                    {
                        bChecking = false;

                    }
                }
                else
                {
                    bChecking = false;
                }
            }
            else
            {
                bChecking = false; // 띄어쓰기있는지 체크

            }
        }
        else
        {
            bChecking = false;//길이가 2이상
        }


        return bChecking;
    }
    static public bool ID_Check(string id)
    {
      
            bool bChecking = true;
        if (id.Length > 2)
        {
            if (!id.Contains(" "))
            {

               
                if (isKorean(id) || CheckingSpecialText(id) )
                {
                    bChecking = false;
                }
            }
            else
            {
                bChecking = false;
            }
        }
        else
        {
            bChecking = false;
        }

        return bChecking;


    }
    static public bool Password_Check(string pass)
    {

        bool bChecking = true;
        if (pass.Length >= 8)
        {
            if (!pass.Contains(" "))
            {

                if (isKorean(pass) || SequenceCharCheck(pass) || !Password_combineCheck(pass))
                {
                    bChecking = false;
                }
              
            }
            else
            {
                bChecking = false;
            }
        }
        else
        {
            bChecking = false;
        }

        return bChecking;


    }
    /// <summary>
    /// true : 포함됨
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    static public bool isKorean(string str)
    {

        char[] charArr = str.ToCharArray();
        foreach (char c in charArr)
        {
            if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
            {
                return true;
            }
        }
        return false;

    }
    /// <summary>
    /// true: 포함됨
    /// </summary>
    /// <param name="txt"></param>
    /// <returns></returns>
    static public bool CheckingSpecialText(string txt)
    {
        string str = @"[~!@\#$%^&*\()\=+|\\/:;?""<>']";
        System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(str);
        return rex.IsMatch(txt);
    }

    /// <summary>
    /// true: 포함됨
    /// </summary>
    /// <param name="txt"></param>
    /// <returns></returns>
    static public bool CheckingKoreanText(string txt)
    {

        string str = @"[ㄱ-ㅎㅏ-ㅣ]";
        System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(str);
        return rex.IsMatch(txt);
    }

    static public bool SequenceCharCheck(string txt)
    {
        
        char[] charArr = txt.ToCharArray();
        char tempChar = new char();
        int count = 0;
        bool bCheck = false;
        foreach (char c in charArr)
        {
         
            if (tempChar.Equals(c))
            {
                count += 1;
                if (count == 2)
                {
                    bCheck= true;
                }
            }
            else
            {
                tempChar = c;
                count = 0;
            }
        }

        return bCheck;


    }
    /// <summary>
    /// true: 정상값 
    ///
    /// </summary>
    /// <param name="txt"></param>
    /// <returns></returns>
    static public bool Password_combineCheck(string txt)
    {

       
        System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex("(?=.*?[a-zA-Z])(?=.*?[0-9])");
      
        return rex.IsMatch(txt);

      

    }

}
