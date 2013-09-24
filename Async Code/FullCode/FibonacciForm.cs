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

        private void CalculateOriginalClick(object sender, EventArgs e)
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

        private void CalculateResponsiveClick(object sender, EventArgs e)
        {
            cmdCalculateResponsive.Enabled = false;
            Cursor = Cursors.WaitCursor;
            int start = Environment.TickCount;

            int result = 0;

            Task task = new Task(() =>
            {
                var f = new Fibonacci(int.Parse(nText.Text));
                result = f.Calculate();
            });

            Task secondTask = task.ContinueWith(
                (prev) =>
                {
                    int stop = Environment.TickCount;
                    cmdCalculate.Enabled = true;
                    Cursor = Cursors.Default;

                    WriteResult(result, stop - start);
                }, TaskScheduler.FromCurrentSynchronizationContext());

            task.Start();   
        }

        private void CalculatePerformanceClick(object sender, EventArgs e)
        { 
            int start = Environment.TickCount;

            int result = 0;

            Task task = new Task(() =>
            {
                var f = new Fibonacci(int.Parse(nText.Text));
                result = f.Calculate();
            });

            Task secondTask = task.ContinueWith(
                (prev) =>
                {
                    int stop = Environment.TickCount;

                    WriteResult(result, stop - start);
                }, TaskScheduler.FromCurrentSynchronizationContext());

            task.Start();
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

        private void ExceptionClick(object sender, EventArgs e)
        {
            Task t = Task.Factory.StartNew(
                () =>
                    {
                        int value = 0;
                        int result = 100 / value;            
                    });

            try
            {
                t.Wait();
            }
            catch (AggregateException ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
    }
}
