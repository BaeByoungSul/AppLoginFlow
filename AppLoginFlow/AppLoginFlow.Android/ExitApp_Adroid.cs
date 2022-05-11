using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppLoginFlow.Droid;
using AppLoginFlow.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(ExitApp_Adroid ))]
namespace AppLoginFlow.Droid
{

    public class ExitApp_Adroid : IExitApp
    {
        public void ExitApp()
        {
            //For platform equivalents of Xamarin.Forms.Application.Current.Quit(),
            //iOS: System.Threading.Thread.CurrentThread.Abort()
            //Android: Android.OS.Process.KillProcess(Android.OS.Process.MyPid())
            //UWP: Windows.UI.Xaml.Application.Current.Exit()

            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}