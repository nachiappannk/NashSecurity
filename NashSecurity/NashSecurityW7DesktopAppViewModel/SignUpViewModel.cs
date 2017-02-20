using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using NashSecurity;
using Prism.Commands;

namespace NashSecurityW7DesktopAppViewModel
{
    public class SignUpViewModel : INotifyPropertyChanged
    {
        private ISecuritySystem securitySystem;
        private readonly SessionTokenHolder _sessionTokenHolder;

        public SignUpViewModel(ISecuritySystem securitySystem, SessionTokenHolder sessionTokenHolder)
        {
            this.securitySystem = securitySystem;
            _sessionTokenHolder = sessionTokenHolder;
            SignUpCommand = new DelegateCommand(ExecutedSignUpCommand, CanExecutedSignUpCommand);
        }

        private void RaiseCanExeuteSignUpCommandChanged()
        {
            (SignUpCommand as DelegateCommand).RaiseCanExecuteChanged();
        }

        private void ExecutedSignUpCommand()
        {
            _sessionTokenHolder.SetSessionToken(securitySystem.SignUp(UserName, MasterPassword, LoginPassword));
        }

        private bool CanExecutedSignUpCommand()
        {
            try
            {
                securitySystem.AssertUserNameIsValidForSignUp(UserName);
                securitySystem.AssertLoginPasswordIsValidForSignUp(LoginPassword);
                securitySystem.AssertMasterPasswordIsValidForSignUp(MasterPassword);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { this.ViewModelSet(ref _userName, value, RaiseCanExeuteSignUpCommandChanged);}
        }

        private string _masterPassword;

        public string MasterPassword
        {
            get { return _masterPassword; }
            set { this.ViewModelSet(ref _masterPassword, value, RaiseCanExeuteSignUpCommandChanged);}
        }

        private string _loginPassword;
        public string LoginPassword
        {
            get { return _loginPassword; }
            set { this.ViewModelSet(ref _loginPassword, value, RaiseCanExeuteSignUpCommandChanged);}
        }

        public ICommand SignUpCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }

    public static class ViewModelExtentions
    {
        public static void ViewModelSet<T>(this object owner, ref T oldValue, T newValue,
            PropertyChangedEventHandler propertyChangedEventHandler, Action postEventAction = null, 
            [CallerMemberName] string propertyName = null) where T : class
        {
            if (oldValue != newValue)
            {
                oldValue = newValue;
                var handler = propertyChangedEventHandler;
                if (handler != null) handler(owner, new PropertyChangedEventArgs(propertyName));
                if (postEventAction != null) postEventAction.Invoke();
            }
        }

        public static void ViewModelSet<T>(this object owner, ref T oldValue, T newValue,
            Action postSetAction = null) where T : class
        {
            if (oldValue != newValue)
            {
                oldValue = newValue;
                if (postSetAction != null) postSetAction.Invoke();
            }
        }
    }
}