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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            groupBox1.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach(Control c in this.Controls)
            {
                if(c is TextBox)
                {
                    ((TextBox)c).Clear();
                }
                if(c is ComboBox)
                {
                    ((ComboBox)c).ResetText();
                }
                if(c is GroupBox)
                {
                    foreach(Control d in c.Controls)
                    {
                        if(d is TrackBar)
                        {
                            ((TrackBar)d).Value = 0;
                        }
                    }
                    c.Hide();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox1.Show();
            switch (comboBox1.SelectedIndex)
            {
                case 0 :
                    label4.Text = "Visual Programming";
                    label9.Text = "Computer Graphics";
                    label8.Text = "Data Structures";
                    label7.Text = "Sofrware Engineering";
                    label5.Text = "Adv. Web Programming";
                    break;
                default :
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var student = new Student
            {
                RegNo = textBox1.Text.Trim(),
                Password = textBox4.Text.Trim(),
                Name = textBox2.Text.Trim(),
                Email = textBox3.Text.Trim(),
                YearOfStudy = comboBox1.SelectedIndex == 0 ? 1 : comboBox1.SelectedIndex == 1 ? 2 : comboBox1.SelectedIndex == 2 ? 3 : 4
            };

            IOperations ops = Operations.Instance;
            ops.AddStudent(student);
            MessageBox.Show("Success");
            this.Close();
        }
    }
}
