using System;
using System.Collections.Generic;
using System.Linq;

namespace ABNumGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // 產生答案
            Random ran = new Random();
            string answer = ran.Next(0, 9999 + 1).ToString("0000");
            // 輸出答案
            Console.WriteLine(string.Format("正確答案為：{0}\n", answer));

            // 無限猜測答案直到猜對退出
            while (true)
            {
                // 輸入猜測
                Console.Write(">");
                string guess = Console.ReadLine();

                // 暫存 TryParse 輸出
                int iGuess = 0;
                // 檢測猜測是否符合規範 (長度為 4 與是否皆為數字)
                if (guess.Length != 4 || !int.TryParse(guess, out iGuess))
                {
                    Console.WriteLine("輸入錯誤！大小為 0000 - 9999 的四碼數字。\n");
                }
                else
                {
                    // 建立集合
                    // // A => 數字正確且位置正確
                    // // B => 數字正確但位置錯誤
                    Dictionary<string, List<char>> dic = new Dictionary<string, List<char>>
                    {
                        { "A", new List<char>() },
                        { "B", new List<char>() }
                    };

                    // 解析 A 與 B
                    // // 解析說明
                    // // 先判斷 A
                    // //   正確 : A++, 並把該字元改為 -
                    // //   錯誤 : 檢測答案是否還有該字元
                    // //           => 包含 : 測試猜測字串其相對於該字元位於答案的索引值之字元是否相同
                    // //                      -> 相同 : A++, 並把該字元改為 -
                    // //                      -> 不同 : B++, 並把該字元改為 -
                    // //           => 不含 : 無操作
                    string tmpAnswer = answer;
                    for (int i = 0; i < guess.Length; i++)
                    {
                        if (tmpAnswer[i] == guess[i])
                        {
                            dic["A"].Add(tmpAnswer[i]);
                            tmpAnswer = tmpAnswer.Substring(0, i) + '-' + tmpAnswer.Substring(i + 1, tmpAnswer.Length - i - 1);
                        }
                        else if (tmpAnswer.Any(c => c == guess[i]))
                        {
                            // 先測試第一個找到的數字是否為 A
                            int cIndex = tmpAnswer.IndexOf(guess[i]);
                            if (tmpAnswer[cIndex] == guess[cIndex])
                            {
                                dic["A"].Add(tmpAnswer[i]);
                            }
                            else
                            {
                                dic["B"].Add(tmpAnswer[i]);
                            }
                            tmpAnswer = tmpAnswer.Substring(0, cIndex) + '-' + tmpAnswer.Substring(cIndex + 1, tmpAnswer.Length - cIndex - 1);
                        }
                    }

                    // 輸出結果
                    if (dic["A"].Count == 4)
                    {
                        Console.WriteLine("恭喜猜出正確答案！按下任意鍵後離開程式 ..");
                        Console.ReadKey();
                        break;
                    }
                    else
                    {
                        Console.WriteLine(string.Format("猜測結果為 {0}A{1}B！\n", dic["A"].Count.ToString(), dic["B"].Count.ToString()));
                    }
                }
            }
        }
    }
}
