using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace VoiceControl.Net
{
    class StringURL
    {
        public static bool isEmpty(string input)
        {
            if (input == null || "".Equals(input))
            {
                return true;
            }
            else
            {
                for (int i = 0; i < input.Length; i++)
                {
                    char c = input[i];
                    if (c != ' ' && c != '\t' && c != '\r' && c != '\n')
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}