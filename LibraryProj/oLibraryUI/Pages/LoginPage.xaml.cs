using oLibraryUI;
using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LibraryUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {

        LibraryManager Collection;
        Tuple<LibraryManager, Item, Logic.MovePage?, Users> Container;
        public LoginPage()
        {
            this.InitializeComponent();
        }

        //Navigation
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            AddNewUserPanel.Visibility = Visibility.Collapsed;

            base.OnNavigatedTo(e);

            Container = e.Parameter as Tuple<LibraryManager, Item, Logic.MovePage?, Users>;
            Collection = Container.Item1;
        }
        private void LibrarianLoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Users currectUser = Collection.UserCheck(UserNameText.Text, PasswordText.Password); // checks if null
                Container = new Tuple<LibraryManager, Item, Logic.MovePage?, Users>(Container.Item1, Container.Item2, null, currectUser);
                Frame.Navigate(typeof(LibraryPage), Container);
            }
            catch (ArgumentException)
            {
                messagesText.Text = ErrorMassages.WrongUserError();
            }
        }
        private void LoginAsGuestButton_Click(object sender, RoutedEventArgs e)
        {
            Container = new Tuple<LibraryManager, Item, Logic.MovePage?, Users>(Container.Item1, Container.Item2, null, null);
            Frame.Navigate(typeof(LibraryPage), Container);
        }

        //SignUp/LogIn
        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            ClearText();
            SignUpSettings();
        }
        private void AddNewUserButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Collection.AddNewUser(UserNameText.Text, PasswordText.Password, ConfirmPasswordText.Password))
                {
                    ClearText();
                    LogInSettings();
                    UIMassages.AddItemMsg("User");
                }
            }
            catch (NullReferenceException)
            {
                messagesText.Text = ErrorMassages.NullOrEmptyError("Name/Password");
            }
            catch (ArgumentException)
            {
                messagesText.Text = ErrorMassages.PasswordsNotTheSameError();
            }
            catch (Exception)
            {
                messagesText.Text = ErrorMassages.NameExistsError(UserNameText.Text);
            }
        }
        private void backToLoginButton_Click(object sender, RoutedEventArgs e)
        {
            ClearText();
            LogInSettings();
        }

        //UI Settings
        void SignUpSettings()
        {
            UserNameText.PlaceholderText = "New Name";
            PasswordText.PlaceholderText = "New Password";

            AddNewUserPanel.Visibility = Visibility.Visible;
            LoginButtonsPanel.Visibility = Visibility.Collapsed;
        }
        void LogInSettings()
        {

            UserNameText.PlaceholderText = "Name";
            PasswordText.PlaceholderText = "Password";

            AddNewUserPanel.Visibility = Visibility.Collapsed;
            LoginButtonsPanel.Visibility = Visibility.Visible;
        }
        void ClearText()
        {
            UserNameText.Text = string.Empty;
            PasswordText.Password = string.Empty;
            messagesText.Text = string.Empty;

        }


    }
}
