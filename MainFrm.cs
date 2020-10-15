using System;
using System.Windows.Forms;

namespace Работа_ЭВМ
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                int taskA = Convert.ToInt32(txtTaskA.Text) - Convert.ToInt32(txtGenTimeA.Text),
                    taskB = Convert.ToInt32(txtTaskB.Text) - Convert.ToInt32(txtGenTimeB.Text),
                    taskC = Convert.ToInt32(txtTaskC.Text) - Convert.ToInt32(txtGenTimeC.Text),
                    genTimeA = Convert.ToInt32(txtGenTimeA.Text) * 2,
                    genTimeB = Convert.ToInt32(txtGenTimeB.Text) * 2,
                    genTimeC = Convert.ToInt32(txtGenTimeC.Text) * 2,
                    executeTimeA = Convert.ToInt32(txtExecuteTimeA.Text) - Convert.ToInt32(txtGenExecuteTimeA.Text),
                    executeTimeB = Convert.ToInt32(txtExecuteTimeB.Text) - Convert.ToInt32(txtGenExecuteTimeB.Text),
                    executeTimeC = Convert.ToInt32(txtExecuteTimeC.Text) - Convert.ToInt32(txtGenExecuteTimeC.Text),
                    genExecuteTimeA = Convert.ToInt32(txtGenExecuteTimeA.Text) * 2,
                    genExecuteTimeB = Convert.ToInt32(txtGenExecuteTimeB.Text) * 2,
                    genExecuteTimeC = Convert.ToInt32(txtGenExecuteTimeC.Text) * 2;
                TasksQueue tq = TaskGenerator.GetTasks(taskA, taskB, taskC, genTimeA, genTimeB, genTimeC, executeTimeA, executeTimeB, executeTimeC, genExecuteTimeA, genExecuteTimeB, genExecuteTimeC,
                    Convert.ToInt32(txtTotalHours.Text));
                Results results = Sheduler.Run(tq, Convert.ToInt32(txtTotalHours.Text));
                lblCountA.Text = $@"{results.CountA.ToString()} / {results.A.ToString()}";
                lblCountB.Text = $@"{results.CountB.ToString()} / {results.B.ToString()}";
                lblCountC.Text = $@"{results.CountC.ToString()} / {results.C.ToString()}";
                lblTotalCount.Text = results.TotalCount.ToString();
                lblTotalTime.Text = results.TotalTime.ToString();
                MessageBox.Show("Коэффициент загрузки ="+(1-(results.P / (Convert.ToInt32(txtTotalHours.Text)*60))).ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Ошибка");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtTaskA.Text = "10";
            txtTaskB.Text = "10";
            txtTaskC.Text = "10";
            txtGenTimeA.Text = "0";
            txtGenTimeB.Text = "0";
            txtGenTimeC.Text = "0";
            txtExecuteTimeA.Text = "10";
            txtExecuteTimeB.Text = "10";
            txtExecuteTimeC.Text = "10";
            txtGenExecuteTimeA.Text = "0";
            txtGenExecuteTimeB.Text = "0";
            txtGenExecuteTimeC.Text = "0";
            txtTotalHours.Text = "1";
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {

        }
    }
}