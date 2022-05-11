using AppLoginFlow.Model;
using AppLoginFlow.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppLoginFlow
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            lblPackageName.Text = AppInfo.PackageName;

            btnLogin.Clicked += BtnLogin_Clicked;
            btnSignUp.Clicked += BtnSignUp_Clicked;
            btnExit.Clicked += BtnExit_Clicked;
        }

        /// <summary>
        /// App 종료
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExit_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IExitApp>().ExitApp();    
        }

        /// <summary>
        /// Sign Up Page로 이동
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }
        /// <summary>
        /// Login성공 시 메인 Page로 이동
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            var user = new LoginUser
            {
                UserName = txtUserName.Text,
                Password = txtPassword.Text,
                UserEmail = MyConstants.UserEmail,
                LoginTime = DateTime.Now   
            };
            if (IsValidUser(user))
            {
                App.LoginUserInfo = user;
                Navigation.InsertPageBefore(new MainPage(),this);
                await Navigation.PopAsync();
            }
            else
            {
                lblMessage.Text = "Login Failed";
                txtPassword.Text = String.Empty;
            }
        }
        /// <summary>
        /// 사용자 및 비밀번호 확인
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool IsValidUser(LoginUser user)
        {
            if (user.UserName == MyConstants.Username &&
                user.Password == MyConstants.Password)
            {
                return true;
            }

            return false;
        }
    
    }
}