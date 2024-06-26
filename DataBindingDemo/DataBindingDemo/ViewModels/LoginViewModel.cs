﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace DataBindingDemo.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string username;
        private string password;
        private bool acceptTermsAndConditions;
        private bool isLoggingIn;
        private DateTime? lastLoginDate;

        public LoginViewModel()
        {
            this.LoginCommand = new Command<string>(
                execute: async (p) => await this.LoginAsync(),
                canExecute: (p) => !this.IsLoggingIn);
        }

        public string Username
        {
            get => this.username;
            set
            {
                this.username = value;
                this.OnPropertyChanged(nameof(this.Username));
                this.OnPropertyChanged(nameof(this.IsLoginButtonEnabled));
            }
        }

        public string Password
        {
            get => this.password;
            set
            {
                this.password = value;
                this.OnPropertyChanged(nameof(this.Password));
                this.OnPropertyChanged(nameof(this.IsLoginButtonEnabled));
            }
        }

        public bool AcceptTermsAndConditions
        {
            get => this.acceptTermsAndConditions;
            set
            {
                if (this.acceptTermsAndConditions != value)
                {
                    this.acceptTermsAndConditions = value;
                    this.OnPropertyChanged(nameof(this.AcceptTermsAndConditions));
                    this.OnPropertyChanged(nameof(this.IsLoginButtonEnabled));
                }
            }
        }

        public ICommand LoginCommand { get; set; }

        private async Task LoginAsync()
        {
            this.IsLoggingIn = true;
            await Task.Delay(2000);

            this.Username = null;
            this.Password = null;
            this.LastLoginDate = DateTime.Now;

            // Demo: Reset terms and conditions checkbox
            //       to demonstrate a two-way binding update (binding source -> binding target).
            this.AcceptTermsAndConditions = false;

            // Demo: Following code could be the command handler implementation
            //       for LoginCommand/LoginAsync. We safe-guard the service call
            //       with a try-catch statement and inform the user in case something goes wrong.
            //       The bool flag IsLoggingIn is used to avoid double-execution
            //       of the LoginCommand as well as to indicate the login activity
            //       in the user interface (e.g. with an ActivityIndicator).
            //
            //       try
            //       {
            //           var isAuthenticated = await this.loginService.LoginAsync(this.Username, this.Password);
            //           if (isAuthenticated)
            //           {
            //               await this.navigationService.PushAsync("VeryCoolPage");
            //           }
            //           else
            //           {
            //               this.Password = null;
            //               this.dialogService.ShowAlert("Username or password is not valid.");
            //           }
            //       }
            //       catch (Exception ex)
            //       {
            //           this.logger.LogError(ex, "LoginAsync failed with exception")
            //           this.dialogService.ShowAlert("Something went wrong. Please try again.");
            //       }

            this.IsLoggingIn = false;
        }

        public bool IsLoggingIn
        {
            get => this.isLoggingIn;
            set
            {
                if (this.isLoggingIn != value)
                {
                    this.isLoggingIn = value;
                    this.OnPropertyChanged(nameof(this.IsLoggingIn));
                    this.OnPropertyChanged(nameof(this.IsLoginButtonEnabled));
                }
            }
        }

        public bool IsLoginButtonEnabled
        {
            get
            {
                return
                    !string.IsNullOrEmpty(this.Username) &&
                    !string.IsNullOrEmpty(this.Password) &&
                    this.AcceptTermsAndConditions &&
                    !this.IsLoggingIn;
            }
        }

        public DateTime? LastLoginDate
        {
            get => this.lastLoginDate;
            set
            {
                if (this.lastLoginDate != value)
                {
                    this.lastLoginDate = value;
                    this.OnPropertyChanged(nameof(this.LastLoginDate));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}