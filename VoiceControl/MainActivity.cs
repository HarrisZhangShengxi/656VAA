using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;

namespace VoiceControl
{
    [Activity(Label = "VoiceControl", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            Button connection = FindViewById<Button>(Resource.Id.connectionM);  
            connection.Click += (object sender, EventArgs e) =>
            {
                Intent conIntent = new Intent(this,typeof(Connection)); //A button for switching to Connection View
                StartActivity(conIntent);
            };

            Button voicetest = FindViewById<Button>(Resource.Id.voicetestM);
            voicetest.Click += (object sender, EventArgs e) =>
            {
                Intent voiIntent = new Intent(this, typeof(Voicetest)); //A button for switching to Voice Test View
                StartActivity(voiIntent);
            };

            Button setting = FindViewById<Button>(Resource.Id.settingM);
            setting.Click += (object sender, EventArgs e) =>
            {
                Intent setIntent = new Intent(this, typeof(Setting));   //A button for switching to Setting View
                StartActivity(setIntent);
            };
        }
    }
}

