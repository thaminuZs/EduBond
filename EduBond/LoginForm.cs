using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EduBond
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Clear();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IOperations ops = Operations.Instance;
            string regNo = textBox1.Text.Trim().ToLower();
            string passw = textBox2.Text.Trim();

            if(ops.IsRegistered(regNo))
            {
                if(ops.IsValidPassword(regNo, passw))
                {
                    new StudentDash(regNo).Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid Password");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Not Registered");
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
