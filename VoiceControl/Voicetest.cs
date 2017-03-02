using Android.App;
using Android.OS;
using System;
using Android.Widget;
using Android.Content;
using Android.Speech;
using Java.Util;
using System.Collections.Generic;

namespace VoiceControl
{
    [Activity(Label = "Voicetest")]
    public class Voicetest : Activity
    {
        //MediaRecorder recorder;
        TextView vtext;
        Button sp;
        Button set;

        int RESPONCERESULT = 99;

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
                
                /*recorder.SetAudioSource(AudioSource.Mic);
                recorder.SetOutputFormat(OutputFormat.RawAmr);
                recorder.SetAudioEncoder(AudioEncoder.AmrNb);
                recorder.SetOutputFile(path);
                recorder.Prepare();
                recorder.Start();
                recorder.Stop();
                recorder.Reset();
                recorder.Release();

                var speech = SpeechClient.Create();
                var longOperation = speech.AsyncRecognize(new RecognitionConfig()
                {
                    Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                    SampleRate = 16000,
                }, RecognitionAudio.FromFile(path));
                longOperation = longOperation.PollUntilCompleted();
                var response = longOperation.Result;
                foreach (var result in response.Results)
                {
                    foreach (var alternative in result.Alternatives)
                    {
                        vtext.SetText(alternative.Transcript,TextView.BufferType.Normal);
                    }
                }*/
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
                    resultString += results[i];
                }
                vtext.Append(resultString);
            }
            base.OnActivityResult(requestCode, resultCode, data);
        }

        private void onDestroy()
        {
            base.OnDestroy();
        }
    }
}