using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MPP_CSharp.Service.Service;

namespace MPP_CSharp
{
    public partial class Form1 : Form
    {
        private readonly Service.Service _service;
        public Form1(Service.Service service)
        {
            InitializeComponent();
            _service = service;
        }

        private void LogInbutton_Click(object sender, EventArgs e)
        {
            var username = UsernameTextBox.Text;
            var password = PasswordTextBox.Text;
            var user = _service.SearchUserByUsernamePassword(username, password);
            if (user == null)
            {
                MessageBox.Show("Invalid username or password");
                return;
            }
            var MainWindow = new MainWindow(user, _service, this);
            Hide();
            MainWindow.Show();
        }
    }
}