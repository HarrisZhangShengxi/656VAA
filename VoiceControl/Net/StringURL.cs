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
        /*
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
                        case "living":
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
                else if (i == endis[0])
                {
                    results = "1";
                }
                else if (i == endis[1])
                {
                    results = "0";
                }
                else if (crazy.Contains(i))
                {
                    results = "t";
                }
            }

            if (c == a.Length && results == "") results = "2";

            return results;
        }*/

        public static string REG_W(string str)  //Identify the result and get key words for sending signal to server
        {
            if (cmd_endis(str) == 1) return "1";
            if (cmd_endis(str) == 0) return "0";

            if (cmd_cra(str) == 1) return "t";
            if (cmd_cra(str) == 0) return "y";

            switch(cmd_eq(str))
            {
                case 1:
                    if (cmd_oc(str) == 1) return "A";
                    else if (cmd_oc(str) == 0) return "a";
                    else if (cmd_fla(str) == 1) return "w";
                    break;
                case 2:
                    if (cmd_oc(str) == 1) return "B";
                    else if (cmd_oc(str) == 0) return "b";
                    else if (cmd_fla(str) == 1) return "z";
                    break;
                case 3:
                    break;
            }
            return "2";
        }

        private static int cmd_endis(string en)
        {
            en = en.ToLower();
            if (en.Contains(endis[0])) return 1;
            if (en.Contains(endis[1])) return 0;
            return 2;
        }

        private static int cmd_fla(string fla)
        {
            fla = fla.ToLower();
            string[] a = fla.Split(' ');

            for (int i = 0; i < a.Count(); i++)
            {
                if (flash.Contains(a[i])) return 1;
            }
            return 0;
        }

        private static int cmd_cra(string cra)
        {
            cra = cra.ToLower();
            string[] a = cra.Split(' ');

            for (int i = 0; i < a.Count(); i++)
            {
                if (crazy.Contains(a[i])) return 1;
                if (dance.Contains(a[i])) return 0;
            }
            return 2;
        }

        private static int cmd_oc(string oc)
        {
            int st_o = 0;
            int st_c = 0;
            oc = oc.ToLower();
            string[] a = oc.Split(' ');
            
            for (int i = 0; i < a.Count(); i++)
            {
                if (open.Contains(a[i])) st_o++;
                if (close.Contains(a[i])) st_c++;
            }
            if (st_o > st_c) return 1;
            else if (st_c > st_o) return 0;
            return 2;
        }

        private static int cmd_eq(string eq)
        {
            eq = eq.ToLower();

            if (eq.Contains(place[0]))
            {
                return 1;
            }
            else if (eq.Contains(place[1]))
            {
                return 2;
            }
            else return 3;
        }

        private static string[] endis =
        {
            "enable",
            "disable"
        };

        private static string[] place = //A set of string of place
        {
            "kitchen",
            "living"
        };

        private static string[] open =  //A set of string of open
        {
            "open",
            "on",
            "up"
        };

        private static string[] flash = //A set of string of flash
        {
            "flash",
            "shinning"
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
        };

        private static string[] dance = //A set of string of dance
        {
            "dance",
        }; 
    }
}