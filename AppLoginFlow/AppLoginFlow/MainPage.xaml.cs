using Android.Content;
using Android.Content.PM;
using AppLoginFlow.Model;
using AppLoginFlow.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace AppLoginFlow
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            //string sTodo = "1. Login Flow정리" + Environment.NewLine;
            //sTodo += "2. SQLite 사용법";
            //txtToDoList.Text = sTodo;

            //List apps
            //var apps = Android.App.Application.Context.PackageManager.GetInstalledApplications(PackageInfoFlags.MatchAll);
            //var packageList = new List<string>();
            //foreach (var app in apps)
            //{
            //    packageList.Add(app.PackageName);
            //    Debug.WriteLine(app.PackageName);
            //}
            //pickerPackageName.ItemsSource = packageList;


            string sdevDesc = string.Empty;
            List<MyApp> apps = new List<MyApp>();

            //apps.Add(new MyApp() { PackageName = "nakdong.mymobile010", AppDesc = "MyNotes" });
            //apps.Add(new MyApp() { PackageName = "com.google.android.youtube", AppDesc = "YouTube" });
            try
            {
                sdevDesc = GetDevDesc();
                txtToDoList.Text = sdevDesc;

                apps = GetMyApps();
                pickerPackageName.ItemDisplayBinding = new Binding("AppDesc");
                pickerPackageName.ItemsSource = apps;

            }
            catch (Exception ex)
            {
                this.DisplayAlert("Error", ex.ToString(), "Confirm");
                
            }
            btnLogout.Clicked += BtnLogout_Clicked;
            btnPage1.Clicked += BtnPage1_Clicked;
            btnExecPackage.Clicked += BtnExecPackage_Clicked;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            lblUserEmail.Text = App.LoginUserInfo.UserEmail;
            lblLoginTime.Text = App.LoginUserInfo.LoginTime.ToString("yyyy/MM/dd HH:mm:ss");

        }

        /// <summary>
        /// 필수사항 : <uses-permission android:name="android.permission.QUERY_ALL_PACKAGES" />
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExecPackage_Clicked(object sender, EventArgs e)
        {
            if (pickerPackageName.SelectedItem == null)
            {
                DisplayAlert("Alert", "Select package name first", "Ok");
                return;
            }

            PackageManager pm = Android.App.Application.Context.PackageManager;

            //string packageName = @"nakdong.mymobile010";
            MyApp myApp = pickerPackageName.SelectedItem as MyApp;

            //string packageName = pickerPackageName.SelectedItem as string;
            Intent intent = pm.GetLaunchIntentForPackage(myApp.PackageName);
            if (intent == null)
            {
                DisplayAlert("Alert","Invalid package name!!", "Ok");
                return;
            }

//            intent.PutExtra(Android.Content.Intent.ExtraPackageName, "?????.??.??");
            intent.SetFlags(ActivityFlags.NewTask);
            Android.App.Application.Context.StartActivity(intent);

            return;

            //List apps
            //var apps = Android.App.Application.Context.PackageManager.GetInstalledApplications(PackageInfoFlags.MatchAll);
            //foreach (var item in apps)
            //{
            //    Debug.WriteLine(item.PackageName);
            //}

            //var appname = @"nakdong.mymobile010";
            //var result = await DependencyService.Get<IAppHandler>().LaunchApp(appname);


        }

        /// <summary>
        /// Page1 page로 이동
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnPage1_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page1());
            
        }
        /// <summary>
        /// Logout 시 Login page로 이동
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnLogout_Clicked(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();    
        }

        private List<MyApp> GetMyApps()
        {
            try
            {
                SqlCommand dbcmd = new SqlCommand();
                dbcmd.CommandType = System.Data.CommandType.StoredProcedure;
                dbcmd.CommandText = "TESTDB..USP_PACKAGE_SEL";
                //dbcmd.Parameters.Add(new SqlParameter()
                //{
                //    ParameterName = "@LoginName",
                //    SqlDbType = SqlDbType.NVarChar,
                //    Direction = ParameterDirection.Input
                //}).Value = txtUserName.Text;

                //dbcmd.Parameters.Add(new SqlParameter()
                //{
                //    ParameterName = "@Password",
                //    SqlDbType = SqlDbType.NVarChar,
                //    Direction = ParameterDirection.Input
                //}).Value = txtPassword.Text;


                DBClient _cli = new DBClient("BSBAE_HOME");
                DataSet ds = _cli.GetDataSet(dbcmd);

                List<MyApp> apps = new List<MyApp>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    apps.Add(
                        new MyApp()
                        {   PackageName = row["PackageName"].ToString(),
                            AppDesc = row["AppDesc"].ToString()
                        }
                    );
                }
                return apps;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        private string GetDevDesc()
        {
            try
            {
                SqlCommand dbcmd = new SqlCommand();
                dbcmd.CommandType = System.Data.CommandType.StoredProcedure;
                dbcmd.CommandText = "TESTDB..USP_DevDesc_SEL";

                DBClient _cli = new DBClient("BSBAE_HOME");
                DataSet ds = _cli.GetDataSet(dbcmd);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0][0].ToString();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
    }
}
