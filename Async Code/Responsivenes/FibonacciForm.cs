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

        // First Attemp
        private void CalculateClickOriginal(object sender, EventArgs e)
        {
            int start = Environment.TickCount;
            //cmdCalculate.Enabled = false;
            //Cursor = Cursors.WaitCursor;

            int result = 0;

            var f = new Fibonacci(int.Parse(nText.Text));

            result = f.Calculate();

            int stop = Environment.TickCount;
            //cmdCalculate.Enabled = true;
            //Cursor = Cursors.Default;

            WriteResult(result, stop - start);
        }

        private void CalculateClickFirstTry(object sender, EventArgs e)
        {
            int start = Environment.TickCount;
            cmdCalculate.Enabled = false;
            Cursor = Cursors.WaitCursor;

            int result = 0;

            var t = new Task(() =>
                                 {
                                     var f = new Fibonacci(int.Parse(nText.Text));

                                     result = f.Calculate();

                                     int stop = Environment.TickCount;
                                     cmdCalculate.Enabled = true;
                                     Cursor = Cursors.Default;

                                     WriteResult(result, stop - start);
                                 });
            t.Start();

        }

        private void CalculateClickAsd(object sender, EventArgs e)
        {
            int start = Environment.TickCount;
            cmdCalculate.Enabled = false;
            Cursor = Cursors.WaitCursor;

            int result = 0;

            var t = new Task(() =>
            {
                var f = new Fibonacci(int.Parse(nText.Text));

                result = f.Calculate();

                int stop = Environment.TickCount;
                cmdCalculate.Enabled = true;
                Cursor = Cursors.Default;

                WriteResult(result, stop - start);
            });
            t.Start();

        }

        private void CalculateClick2(object sender, EventArgs e)
        {
            int start = Environment.TickCount;
            cmdCalculate.Enabled = false;
            Cursor = Cursors.WaitCursor;

            int result = 0;

            var t = new Task(() =>
            {
                var f = new Fibonacci(int.Parse(nText.Text));

                result = f.Calculate();
            });

            var t2 = t.ContinueWith((prev) =>
            {
                int stop = Environment.TickCount;
                cmdCalculate.Enabled = true;
                Cursor = Cursors.Default;

                WriteResult(result, stop - start);
            }, TaskScheduler.FromCurrentSynchronizationContext());

            t.Start();
        }

        #region Donia Petrona

        private void CalculateClick(object sender, EventArgs e)
        {
            int start = Environment.TickCount;
            //cmdCalculate.Enabled = false;
            //Cursor = Cursors.WaitCursor;

            var f = new Fibonacci(int.Parse(nText.Text));
            
            int result = 0;

            var t = new Task(() => result = f.Calculate());

            var t2 = t.ContinueWith((prev) =>
            {
                int stop = Environment.TickCount;
                //cmdCalculate.Enabled = true;
                //Cursor = Cursors.Default;
                
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

        private void cmdBlowUp_Click(object sender, EventArgs e)
        {
            var task = Task.Factory.StartNew(() =>
                {
                    var childTask = Task.Factory.StartNew(
                        () =>
                            {
                                int value = 0;
                                int result = 500 / value; 
                            });

                    var childTask2 = Task.Factory.StartNew(
                        () =>
                        {
                            throw new ApplicationException("Child task 2");
                        });

                    var tasks = new Task[] { childTask, childTask2 };

                    Task.WaitAll(tasks);
                });

            try
            {
                task.Wait();
            }
            catch (AggregateException ex)
            {
                var singleLevelExceptions = ex.Flatten();

                foreach (var exception in singleLevelExceptions.InnerExceptions)
                {
                    MessageBox.Show(exception.Message);    
                }                
            }            
        }

        private void MultiTask()
        {
            var facebookTask = Task.Factory.StartNew(
                () =>
                    {
                        // Gather from Facebook
                        var a = 1;
                    });

            var twitterTask = Task.Factory.StartNew(
                () =>
                {
                    // Gather from Twitter
                    var a = 1;
                });

            var gplusTask = Task.Factory.StartNew(
                () =>
                {
                    // Gather from Facebook
                    var a = 1;
                });

            var tasks = new Task[] { facebookTask, twitterTask, gplusTask };

            var index = Task.WaitAny(tasks);
            var winner = tasks[index];
        }
    }
}
