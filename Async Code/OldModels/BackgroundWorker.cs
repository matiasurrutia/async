// src: http://www.codeproject.com/Articles/99143/BackgroundWorker-Class-Sample-for-Beginners

using System;
using System.ComponentModel;
using System.Threading;

namespace OldModels
{
    class BackgroundWorkerClass
    {
        private readonly BackgroundWorker worker;

        public BackgroundWorkerClass()
        {
            worker = new BackgroundWorker();

            // Create a background worker thread that ReportsProgress &
            // SupportsCancellation

            // Hook up the appropriate events.
            worker.DoWork += DoWork;
            worker.ProgressChanged += ProgressChanged;
            worker.RunWorkerCompleted += RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
        }

        public void Main()
        {
            worker.RunWorkerAsync();
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            // The sender is the BackgroundWorker object we need it to
            // report progress and check for cancellation.
            //NOTE : Never play with the UI thread here...
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(100);

                // Periodically report progress to the main thread so that it can
                // update the UI.  In most cases you'll just need to send an
                // integer that will update a ProgressBar                    
                worker.ReportProgress(i);

                // Periodically check if a cancellation request is pending.
                // If the user clicks cancel the line
                // m_AsyncWorker.CancelAsync(); if ran above.  This
                // sets the CancellationPending to true.
                // You must check this flag in here and react to it.
                // We react to it by setting e.Cancel to true and leaving
                if (worker.CancellationPending)
                {
                    // Set the e.Cancel flag so that the WorkerCompleted event
                    // knows that the process was cancelled.
                    e.Cancel = true;
                    worker.ReportProgress(0);

                    return;
                }
            }

            //Report 100% completion on operation completed
            worker.ReportProgress(100);
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // This function fires on the UI thread so it's safe to edit
            // the UI control directly, no funny business with Control.Invoke :)
            // Update the progressBar with the integer supplied to us from the
            // ReportProgress() function.  

            Console.WriteLine("Processing......" + e.ProgressPercentage + "%");
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // The background process is complete. We need to inspect
            // our response to see if an error occurred, a cancel was
            // requested or if we completed successfully.  
            if (e.Cancelled)
            {
                Console.WriteLine("Task Cancelled.");
            }

            // Check to see if an error occurred in the background process.
            else if (e.Error != null)
            {
                Console.WriteLine("Error while performing background operation.");
            }
            else
            {
                // Everything completed normally.
                Console.WriteLine("Task Completed...");
            }
        }
    }
}
