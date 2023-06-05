using System;
using System.Windows.Forms;

namespace Chess
{
    public partial class TimeSetter : Form
    {
        public TimeSetter()
        {
            InitializeComponent();
        }
        public int Timer { get; set; }
        public int Increment { get; set; }
        private void TimerSet_ValueChanged(object sender, EventArgs e)
        {
            Timer = (int)TimerSet.Value * 60;
            Increment = (int)AddSet.Value;
            if (Timer > 0)
                SetButton.Text = "Установить контроль";
            else
                SetButton.Text = "Играть без часов";
        }

        private void SetButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
