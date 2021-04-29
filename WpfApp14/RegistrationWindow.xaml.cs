using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp14
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void CreateClient(object sender, RoutedEventArgs e)
        {
            if (checkNames() && checkMail() && checkPassword())
            {
                var user = new User
                {
                    Name = nameTxtBox.Text,
                    FamilyName = familyNameTxtBox.Text,
                    Email = loginTxtBox.Text,
                    PhoneNumber = phoneNumberTxtBox.Text,
                };
                _applicationContext.Users.Add(user);
                _applicationContext.SaveChanges();
            }
        }
        private bool checkNames()
        {
            if (string.IsNullOrWhiteSpace(nameTxtBox.Text) && string.IsNullOrWhiteSpace(familyNameTxtBox.Text))
            {
                SnackBarRegister.MessageQueue.Enqueue("Please fill name and familyName");
                return false;
            }
            return true;
        }
        private bool checkPassword()
        {
            if (passwordTxtBox.Password.Length > 4)
            {
                return true;
            }
            SnackBarRegister.MessageQueue.Enqueue("Invalid Password, password should contain more than 4 numbers");
            return false;
        }
        private bool checkMail()
        {
            try
            {
                var mail = new MailAddress(loginTxtBox.Name);
                return true;
            }
            catch (ArgumentException)
            {
                SnackBar.MessageQueue.Enqueue("Please insert email");
                throw;
            }
            catch (FormatException)
            {
                SnackBarRegister.MessageQueue.Enqueue("Please insert correct email");
                return false;
            }
        }
        private void PhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
