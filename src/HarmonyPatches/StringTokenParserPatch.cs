using System.Collections.Generic;
using HarmonyLib;
using KSA;

namespace KsaLocFramework.HarmonyPatches;

// 游戏只针对了英文单词做了换行适配，故需 Patch
[HarmonyPatch(typeof(StringTokenParser), nameof(StringTokenParser.Parse))]
public static class StringTokenParserPatch
{
    private const char CjkRangeStart = '\u2e80';

    private static void Postfix(ref List<PopupToken> __result)
    {
        List<PopupToken> tokens = new List<PopupToken>();

        foreach (PopupToken token in __result)
        {
            if (token.GetType() == typeof(StringToken) && ContainsCjk(token.Text))
            {
                foreach (char c in token.Text)
                {
                    tokens.Add(new StringToken(c.ToString()));
                }
            }
            else
            {
                tokens.Add(token);
            }
        }

        __result = tokens;
    }

    private static bool ContainsCjk(string text)
    {
        foreach (char c in text)
        {
            if (c >= CjkRangeStart)
            {
                return true;
            }
        }

        return false;
    }
}
