using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using GymMembers.Model;
using Prism.Commands;
using System;
using System.Windows;
using System.Windows.Input;

namespace GymMembers.ViewModel
{
    /// <summary>
    /// The VM for adding users to the list.
    /// </summary>
    public class AddViewModel : ViewModelBase
    {
        /// <summary>
        /// The currently entered first name in the add window.
        /// </summary>
        private string enteredFName;
        
        /// <summary>
        /// The currently entered last name in the add window.
        /// </summary>
        private string enteredLName;
        
        /// <summary>
        /// The currently entered email in the add window.
        /// </summary>
        private string enteredEmail;
    
        /// <summary>
        /// Initializes a new instance of the AddViewModel class.
        /// </summary>
        public AddViewModel()
        {
            SaveCommand = new RelayCommand<IClosable>(SaveMethod);
            CancelCommand = new RelayCommand<IClosable>(CancelMethod);
        }

        /// <summary>
        /// The command that triggers saving the filled out member data.
        /// </summary>
        public ICommand SaveCommand { get; private set; }
        
        /// <summary>
        /// The command that triggers closing the add window.
        /// </summary>
        public ICommand CancelCommand { get; private set; }

        /// <summary>
        /// Sends a valid member to the Main VM to add to the list, then closes the window.
         /// </summary>
         /// <param name="window">The window to close.</param>
         public void SaveMethod(IClosable window)
         {
            try
            {

                if (window != null)
                {
                    if (enteredEmail.Length > 25 || enteredFName.Length > 25 || enteredLName.Length > 25)
                    {
                        throw new ArgumentException();
                    }
                    if (enteredEmail.Length <= 0 || enteredFName.Length <= 0 || enteredLName.Length <= 0)
                    {
                        throw new NullReferenceException();
                    }
                    if (enteredEmail.IndexOf("@") == -1 || enteredEmail.IndexOf(".") == -1)
                    {
                        throw new FormatException();
                    }
                    else
                    {
                        Messenger.Default.Send(new MessageMember(enteredFName, enteredLName, enteredEmail, "Add"));
                        window.Close();
                    }
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Fields cannot be empty.", "Entry Error");
            }
            catch (FormatException)
            {
                MessageBox.Show("Must be a valid e-mail address.", "Entry Error");
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Fields must be under 25 characters.", "Entry Error");
            }
        }    
        
        /// <summary>
        /// Closes the window.
        /// </summary>
        /// <param name="window">The window to close.</param>
        public void CancelMethod(IClosable window)
        {
            if (window != null)
            {
                window.Close();
            }
        }

        /// <summary>
        /// The currently entered first name in the add window.
        /// </summary>
        public string EnteredFName
        {
            get
            {
                return enteredFName;
            }
            set
            {
                enteredFName = value;
                RaisePropertyChanged("EnteredFName");
            }
        }

        /// <summary>
        /// The currently entered first name in the add window.
        /// </summary>
        public string EnteredLName
        {
            get
            {
                return enteredLName;
            }
            set
            {
                enteredLName = value;
                RaisePropertyChanged("EnteredLName");
            }
        }

        /// <summary>
        /// The currently entered first name in the add window.
        /// </summary>
        public string EnteredEmail
        {
            get
            {
                return enteredEmail;
            }
            set
            {
                enteredEmail = value;
                RaisePropertyChanged("EnteredEmail");
            }
        }
    }
}

    