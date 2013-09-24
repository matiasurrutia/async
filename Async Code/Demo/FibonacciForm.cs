using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Responsivenes
{
    public partial class FibonacciForm : Form
    {
        public FibonacciForm()
        {
            InitializeComponent();
        }

        private void CalculateClick(object sender, EventArgs e)
        {
            int start = Environment.TickCount;
            cmdCalculate.Enabled = false;
            Cursor = Cursors.WaitCursor;

            int result = 0;

            var f = new Fibonacci(int.Parse(nText.Text));

            result = f.Calculate();

            int stop = Environment.TickCount;
            cmdCalculate.Enabled = true;
            Cursor = Cursors.Default;

            WriteResult(result, stop - start); 
        }
       
        #region Responsive solution

        private void CalculateClickResponsive(object sender, EventArgs e)
        {
            int start = Environment.TickCount;
            cmdCalculate.Enabled = false;
            Cursor = Cursors.WaitCursor;

            var f = new Fibonacci(int.Parse(nText.Text));
            
            int result = 0;

            var t = new Task(() => result = f.Calculate());

            var t2 = t.ContinueWith((prev) =>
                                        {
                                            int stop = Environment.TickCount;
                                            cmdCalculate.Enabled = true;
                                            Cursor = Cursors.Default;

                                            WriteResult(result, stop - start);

                                        }, TaskScheduler.FromCurrentSynchronizationContext());

            t.Start();
        }

        #endregion

        private void WriteResult(int result, int ticks)
        {
            resultsText.Text = string.Format("{0:#,#0,0} [{1:#,##0.00} secs]{2}{3}",
                                             result, ticks / 1000.0,
                                             Environment.NewLine,
                                             resultsText.Text);
        }
    }
}
