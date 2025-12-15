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
    public partial class StudentDash : Form
    {
        private readonly string _regNo;
        public StudentDash(string regNo)
        {
            InitializeComponent();
            _regNo = regNo;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StudentDash_Load(object sender, EventArgs e)
        {
            label1.Text = "Welcome "+_regNo.ToUpper();

            IOperations ops = Operations.Instance;
            var stdLevels = ops.FetchLevels(_regNo);

            progressBar3.Value = stdLevels[0];
            progressBar4.Value = stdLevels[1];
            progressBar2.Value = stdLevels[2];
            progressBar5.Value = stdLevels[3];
            progressBar1.Value = stdLevels[4];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string msg = richTextBox1.Text;
            IOperations ops = Operations.Instance;

            bool isSuccess = ops.SendMessage(_regNo, msg);
            if (isSuccess)
            {
                MessageBox.Show("Message sent");
            }
            else
            {
                MessageBox.Show("Error");
            }
            richTextBox1.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            IOperations ops = Operations.Instance;
            var msgList = ops.FetchMessages();

            var todayMsgList = msgList.Where(m => m.MessageTime.Date == DateTime.Today).ToList();

            listBox1.DataSource = todayMsgList;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            IOperations ops = Operations.Instance;

            string subject = comboBox1.SelectedItem.ToString();
            DateTime timeSlot = dateTimePicker1.Value;
            string instructor = _regNo;
            string description = richTextBox3.Text;
            string link = richTextBox4.Text;

            var kuppi = new Kuppi()
            {
                Subject = subject,
                TimeSlot = timeSlot,
                Instructor = instructor,
                Description = description,
                Link = link
            };

            bool isSuccess = ops.ScheduleKuppi(kuppi);

            if (isSuccess)
            {
                MessageBox.Show("Kuppi Session Scheduled");
            }
            else
            {
                MessageBox.Show("Error"); 
            }

        }
    }
}
