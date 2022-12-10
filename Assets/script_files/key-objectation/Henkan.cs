using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Henkan
{
    //private char[] g0 = { 'あ', 'い', 'う', 'え', 'お' };
    //private char[] g1 = { 'か', 'き', 'く', 'け', 'こ' };
    //private char[] g2 = { 'さ', 'し', 'す', 'せ', 'そ' };
    //private char[] g3 = { 'た', 'ち', 'つ', 'て', 'と' };
    //private char[] g4 = { 'な', 'に', 'ぬ', 'ね', 'の' };
    //private char[] g5 = { 'は', 'ひ', 'ふ', 'へ', 'ほ' };
    //private char[] g6 = { 'ま', 'み', 'む', 'め', 'も' };
    //private char[] g7 = { 'や', 'ゐ', 'ゆ', 'ゑ', 'よ' };
    //private char[] g8 = { 'ら', 'り', 'る', 'れ', 'ろ' };
    //private char[] g9 = { 'わ', 'を', 'ん', 'ー' };
    //private char[] g10 = { '、', '。', '？', '!' };
    private char[] g11 = { 'か', 'が' };
    //, g12 = { 'き', 'ぎ' }, g13 = { 'く', 'ぐ' }, g14 = { 'け', 'げ' }, g15 = { 'こ', 'ご' };
    //private char[] g21 = { 'さ', 'ざ' }, g22 = { 'し', 'じ' }, g23 = { 'す', 'ず' }, g24 = { 'せ', 'ぜ' }, g25 = { 'そ', 'ぞ' };
    //private char[] g31 = { 'た', 'だ' }, g32 = { 'ち', 'ぢ' }, g34 = { 'て', 'で' }, g35 = { 'と', 'ど' };
    //private char[] g41 = { 'や', 'ゃ' }, g42 = { 'ゆ', 'ゅ' }, g43 = { 'よ', 'ょ' };
    //private char[] g51 = { 'う', 'ゔ' };
    //private char[] g61 = { 'は', 'ば', 'ぱ' }, g62 = { 'ひ', 'び', 'ぴ' }, g63 = { 'ふ', 'ぶ', 'ぷ' }, g64 = { 'へ', 'べ', 'ぺ' }, g65 = { 'ほ', 'ぼ', 'ぽ' };
    //private char[] g71 = { 'つ', 'っ', 'づ' };

    ////    private ArrayList<Character> henkan = new ArrayList<>();

    //private char[][] goju = { g0, g1, g2, g3, g4, g5, g6, g7, g8, g9 };
    //private char[][] henkan = { g11, g12, g13, g14, g15, g21, g22, g23, g24, g25, g31, g32, g34, g35, g41, g42, g43, g51, g61, g62, g63, g64, g65, g71 };

    //    private ArrayList<Character> henkan = new ArrayList<>();

    //public string moji(int x, int y, int inte, string str)
    //{

    //    char lastword;

    //    switch (x)
    //    {
    //        case 1:
    //            break;
    //        case 2:
    //            switch (y)
    //            {
    //                case 1:
    //                    return str + g0[inte];
    //                case 2:
    //                    return str + g3[inte];
    //                case 3:
    //                    return str + g6[inte];
    //                case 4:
    //                    // 変換キー
    //                    if (str.length() >= 1)
    //                    {
    //                        lastword = str.charAt(str.length() - 1);
    //                        for (int i = 0; i < 24; i++)
    //                        {
    //                            if (i < 18)
    //                            {
    //                                for (int j = 0; j < 2; j++)
    //                                {
    //                                    if (lastword == henkan[i][j] && j == 0)
    //                                    {
    //                                        return str.substring(0, str.length() - 1) + henkan[i][1];
    //                                    }
    //                                    else if (lastword == henkan[i][j] && j == 1)
    //                                    {
    //                                        return str.substring(0, str.length() - 1) + henkan[i][0];
    //                                    }
    //                                }
    //                            }
    //                            else
    //                            {
    //                                for (int j = 0; j < 3; j++)
    //                                {
    //                                    if (lastword == henkan[i][j] && j == 0)
    //                                    {
    //                                        return str.substring(0, str.length() - 1) + henkan[i][1];
    //                                    }
    //                                    else if (lastword == henkan[i][j] && j == 1)
    //                                    {
    //                                        return str.substring(0, str.length() - 1) + henkan[i][2];
    //                                    }
    //                                    else if (lastword == henkan[i][j] && j == 2)
    //                                    {
    //                                        return str.substring(0, str.length() - 1) + henkan[i][0];
    //                                    }
    //                                }
    //                            }
    //                        }
    //                        // 濁音半濁音がない音はそのまま返す
    //                        return str;
    //                    }
    //                    return str;
    //            }
    //            break;
    //        case 3:
    //            switch (y)
    //            {
    //                case 1:
    //                    return str + g1[inte];
    //                //                        break;
    //                case 2:
    //                    return str + g4[inte];
    //                //                        break;
    //                case 3:
    //                    return str + g7[inte];
    //                //                        break;
    //                case 4:
    //                    if (inte != 4)
    //                    {
    //                        return str + g9[inte];
    //                    }
    //                    //                        break;
    //            }
    //            break;
    //        case 4:
    //            switch (y)
    //            {
    //                case 1:
    //                    return str + g2[inte];
    //                case 2:
    //                    return str + g5[inte];
    //                case 3:
    //                    return str + g8[inte];
    //                case 4:
    //                    if (inte != 4)
    //                    {
    //                        return str + g10[inte];
    //                    }
    //            }
    //            break;
    //        case 5:
    //            switch (y)
    //            {
    //                case 1:
    //                    // 消去キー
    //                    if (inte == 1)
    //                    {
    //                        return "";
    //                    }
    //                    else
    //                    {
    //                        Log.d("消去キー", "文字列長：" + str.length());
    //                        if (str.length() >= 1)
    //                        {
    //                            return str.substring(0, str.length() - 1);
    //                        }
    //                        else
    //                        {
    //                            return str;
    //                        }
    //                    }
    //                case 2:
    //                    return str + " ";
    //            }
    //            break;
    //    }

    //    return str;
    //}
}
