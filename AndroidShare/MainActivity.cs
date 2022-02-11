using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Essentials;

namespace AndroidShare
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button _ShareText;
        Button _ShareLink;
        Button _ShareAttachment;
        Button _ShareMultipleAttachment;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            UIRef();
            UIClickevents();
           
        }

        public void UIRef()
        {
            _ShareText = FindViewById<Button>(Resource.Id.SText);
            _ShareLink = FindViewById<Button>(Resource.Id.SLink);
            _ShareAttachment = FindViewById<Button>(Resource.Id.SAttach);
            _ShareMultipleAttachment = FindViewById<Button>(Resource.Id.SMAttach);
        }

        private void UIClickevents()
        {
            _ShareText.Click += ShareText_Click;
            _ShareLink.Click += ShareLink_Click;
            _ShareAttachment.Click += ShareAttachment_Click;
            _ShareMultipleAttachment.Click += ShareMultipleAttachment_Click;


        }

        private void ShareMultipleAttachment_Click(object sender, EventArgs e)
        {
             ShareMultipleFiles();
        }

        public void ShareMultipleFiles()
        {
            var file1 = Path.Combine(FileSystem.CacheDirectory, "Attachment1.txt");
            File.WriteAllText(file1, "Content 1");
            var file2 = Path.Combine(FileSystem.CacheDirectory, "Attachment2.txt");
            File.WriteAllText(file2, "Content 2");

             Share.RequestAsync(new ShareMultipleFilesRequest
            {
                Title = "Title",
                Files = new List<ShareFile> { new ShareFile(file1), new ShareFile(file2) }
            });
        }

       private void ShareAttachment_Click(object sender, EventArgs e)
        {
             ShareFile();
        }

        public  void  ShareFile()
        {
            var fn = "Attachment.txt";
            var file = Path.Combine(FileSystem.CacheDirectory, fn);
            File.WriteAllText(file, "Hello World");

             Share.RequestAsync(new ShareFileRequest
            {
                Title = Title,
                File = new ShareFile(file)
            });
        }

        private void ShareLink_Click(object sender, EventArgs e)
        {
            ShareUri("https://www.google.co.in/");
        }

        public void ShareUri(string uri)
        {
             Share.RequestAsync(new ShareTextRequest
            {
                Uri = uri,
                Title = "Share Web Link"
            });
        }

        private void ShareText_Click(object sender, EventArgs e)
        {
            ShareText("Hello World");
        }

        public void ShareText(string text)
        {
            {
                Share.RequestAsync(new ShareTextRequest
                {
                    Text = text,
                    Title = "Share Text"
                });
            }
        }

       

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}