using Android.App;
using Android.OS;
using System;
using Android.Widget;
using Android.Content;
using Android.Speech;
using System.Collections.Generic;

using VoiceControl.Net;
using System.Collections;

namespace VoiceControl
{
    [Activity(Label = "Voicetest")]
    public class Voicetest : Activity
    {
        //MediaRecorder recorder;
        TextView vtext;
        Button sp;
        Button set;

        int RESPONCERESULT = 1234;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Voicetest_layout);

            //string path = "/sdcard/audio.raw";

            vtext = FindViewById<TextView>(Resource.Id.voicetestview);
            sp = FindViewById<Button>(Resource.Id.speakV);
            set = FindViewById<Button>(Resource.Id.settingV);
            
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
                intent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelWebSearch);
                intent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);
                intent.PutExtra(RecognizerIntent.ExtraPrompt, "Start Speaking");
                StartActivityForResult(intent, RESPONCERESULT);
            }
            catch (Exception e)
            {
                // TODO: handle exception  
                vtext.Text = e.ToString();
                vtext.Append("Can't find speaker");
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
                vtext.Append(resultString + "/" + StringURL.REG_W(resultString));
                vtext.Append("\n");
            }
            base.OnActivityResult(requestCode, resultCode, data);
        }

        private void onDestroy()
        {
            base.OnDestroy();
        }
    }
}