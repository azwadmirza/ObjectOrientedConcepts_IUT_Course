﻿// See https://aka.ms/new-console-template for more information
using System.Threading;
namespace DelegatesAndEvents
{
    public class Video
    {
        public string title;
        public Video(string title)
        {
            this.title = title;
        }
    }

    public class VideoArgs : EventArgs
    {
        public Video Video { get; set; }
    }
    public class VideoEncoder
    {
        public delegate void VideoEncodedEventHandler(object sender, VideoArgs e);//Instead use EventArgs for default
        public event VideoEncodedEventHandler VideoEncoded;
        /*
        Instead of using a delegate you can just write EventHandler VideoEncoded, EventHandler is a default built-in type for event handlers
         */
        public void Encode(Video video)
        {
            Console.WriteLine("Encoding Video...");
            Thread.Sleep(3000);
            OnVideoEncoded(video);
        }

        protected virtual void OnVideoEncoded(Video video)
        {
            if (VideoEncoded != null){
                VideoEncoded(this,new VideoArgs(){Video= video});//Instead of new VideoArgs()... just use EventArgs instead
                }
        }


    }

    public class MailService
    {

        public void OnVideoEncoded(object sender, VideoArgs e)
        {
            Console.WriteLine("Sending a frickin mail..."+e.Video.title);
        }
    }

    public class SmsService
    {
        public void OnVideoEncoded(object sender, VideoArgs e)
        {
            Console.WriteLine("Sending a frickin sms..."+e.Video.title);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Video video = new Video("Sian") ;
            VideoEncoder bullshit = new VideoEncoder();//publisher
            MailService mailService= new MailService();//subscriber
            SmsService smsService= new SmsService();//subscriber

            bullshit.VideoEncoded += mailService.OnVideoEncoded;
            bullshit.VideoEncoded+=smsService.OnVideoEncoded;//Multicasting
            bullshit.Encode(video);//Note: Make sure the delegate is called after all the required methods are casted
            Console.ReadKey();


        }
    }
}