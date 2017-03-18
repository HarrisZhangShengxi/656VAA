using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Speech;
using Java.IO;

using VoiceControl.Net;
using System.Collections.Generic;
using System.Collections;
using Android.Views;
using System.Timers;

namespace VoiceControl
{
    [Activity(Label = "Connection")]
    public class Connection : Activity
    {
        private const int RESPONCERESULT = 1234;
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

            can.Enabled = false;
            sp.Enabled = false;

            sub.Click += (object sender, EventArgs e) =>
            {
                if (StringURL.isEmpty(ip.Text) || StringURL.isEmpty(port.Text)) //Determine whether the IP address and the port are empty
                {
                    cv.Append("Please input IP address and port.\n");
                }
                else if (SocketClient.clientSocket != null && SocketClient.clientSocket.Connected == true)  //Determine whether the client connects to server
                {
                    cv.Append("Already Connected.\n");
                }
                else
                {
                    can.Enabled = true;
                    sp.Enabled = true;
                    cv.Append("Connection in process.\n");
                    cv.Append(SocketClient.SocketConnect(ip.Text, Convert.ToInt32(port.Text))); //Connect to server
                }
            };

            can.Click += (object sender, EventArgs e) =>
            {
                cv.Append(SocketClient.disconnect() + "\n");
                can.Enabled = false;
                sp.Enabled = false;
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

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back && e.Action == KeyEventActions.Down)
            {
                if (SocketClient.clientSocket == null)
                {
                    Finish();
                }
                else
                {
                    Toast.MakeText(this.ApplicationContext, SocketClient.disconnect(), ToastLength.Short).Show();   //Set the back key for disconnecting the connection
                    Finish();
                }
                return true;
            }
            return base.OnKeyDown(keyCode, e);
        }

        public void speak()
        {
            try
            {
                Intent intent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
                intent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelWebSearch);
                intent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);
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
                    resultString = results[i];
                }
                vt.Append(resultString + "\n");
                cv.Append(SocketClient.send(resultString + "/" + StringURL.REG_W(resultString)));   //Send the results to the server
            }
            base.OnActivityResult(requestCode, resultCode, data);
        }
        
        private void onDestroy()
        {
            base.OnDestroy();
        }
    }
}