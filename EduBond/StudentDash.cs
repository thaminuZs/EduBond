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
    }
}
