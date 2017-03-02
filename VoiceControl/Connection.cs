using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Speech;
using Java.IO;

using VoiceControl.Net;
using System.Collections.Generic;

namespace VoiceControl
{
    [Activity(Label = "Connection")]
    public class Connection : Activity
    {
        private const int RESPONCERESULT = 99;
        private const int TIMEOUT_CONNECTION = 2000;
        private const int RETRY_TIME = 3;

        private Button sp;
        private Button sub;
        private Button can;
        private Button set;

        private TextView vt;
        private TextView cv;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Connection_layout);

            EditText ip = FindViewById<EditText>(Resource.Id.IpAddress);
            EditText port = FindViewById<EditText>(Resource.Id.port);
            
            vt = FindViewById<TextView>(Resource.Id.voicetestview);
            cv = FindViewById<TextView>(Resource.Id.connectionview);

            sp = FindViewById<Button>(Resource.Id.speakC);
            sub = FindViewById<Button>(Resource.Id.submitC);
            can = FindViewById<Button>(Resource.Id.cancelC);
            set = FindViewById<Button>(Resource.Id.settingC);

            sub.Click += (object sender, EventArgs e) =>
            {
                if(StringURL.isEmpty(ip.Text) || StringURL.isEmpty(port.Text))
                {
                    cv.Append("Please input IP address and port.\n");
                }
                else
                {
                    cv.Append("Connection in process.\n");
                    cv.Append(SocketClient.SocketConnect(ip.Text, Convert.ToInt32(port.Text)));
                }
            };

            can.Click += (object sender, EventArgs e) =>
            {
                cv.Append(SocketClient.disconnect());
            };

            sp.Click += (object sender, EventArgs e) =>
            {
                speak();
            };

            set.Click += (object sender, EventArgs e) =>
            {
                Intent setIntent = new Intent(this, typeof(Setting));
                StartActivity(setIntent);
            };
        }

        public void speak()
        {
            try
            {
                Intent intent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
                intent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
                intent.PutExtra(RecognizerIntent.ExtraPrompt, "Start Speaking");
                StartActivityForResult(intent, RESPONCERESULT);
            }
            catch (IOException e)
            {
                // TODO: handle exception
                vt.Append("Can't find speaker\n");
                e.PrintStackTrace();
            }
        }
        
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            // TODO Auto-generated method stub

            if (requestCode == RESPONCERESULT && resultCode == Result.Ok)
            {
                IList<string> results = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                string resultString = "";

                for (int i = 0; i < results.Count; i++)
                {
                    resultString += results[i];
                }
                vt.Append(resultString);

                cv.Append(SocketClient.send(resultString));
                
            }
            base.OnActivityResult(requestCode, resultCode, data);
        }
        
        private void onDestroy()
        {
            base.OnDestroy();
        }
    }
}