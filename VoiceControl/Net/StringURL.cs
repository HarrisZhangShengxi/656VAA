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
using System.Collections;
using System.Text.RegularExpressions;

namespace VoiceControl.Net
{
    class StringURL
    {
        public static bool isEmpty(string input)    //Determine the EditView of the IP address and the port are empty
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

        public static string REG_W(string msg)  //Identify the result and get key words for sending signal to server
        {
            int c = 0;
            string results = "";
            string[] a = msg.Split(' ');

            foreach(string i in a)
            {
                c++;
                if (place.Contains(i))
                {
                    switch (i)
                    {
                        case "kitchen":
                            foreach(string j in a)
                            {
                                if (open.Contains(j)) return "A";
                            }
                            foreach (string j in a)
                            {
                                if (close.Contains(j)) return "a";
                            }
                            break;
                        case "bedroom":
                            foreach (string j in a)
                            {
                                if (open.Contains(j)) return "B";
                            }
                            foreach (string j in a)
                            {
                                if (close.Contains(j)) return "b";
                            }
                            break;
                    }
                }
                else if (open.Contains(i))
                {
                    results = "enable";
                }
                else if (close.Contains(i))
                {
                    results = "disable";
                }
                else if (crazy.Contains(i))
                {
                    results = "T";
                }
            }

            if (c == a.Length && results == "") results = "2";

            return results;
        }

        private static string[] place = //A set of string of place
        {    "kitchen",
            "bedroom"
        };

        private static string[] open =  //A set of string of open
        {
            "open",
            "on",
            "up"
        };

        private static string[] close = //A set of string of close
        {
            "close",
            "off",
            "down"
        };

        private static string[] crazy = //A set of string of crazy
        {
            "crazy",
            "dance",
            "shinning"
        };
    }
}