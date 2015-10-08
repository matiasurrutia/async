using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GuerraDeTweets.Models;
using TweetSharp;

namespace GuerraDeTweets.Controllers
{
    public class TweetsController : Controller
    {
        private TwitterService service;

        public TwitterService Service
        {
            get
            {
                if (this.service == null)
                {
                    var tService = new TwitterService("JA4gYmQRJTW1SoQ0DcAqWdp4B", "5xs2uFbPMP1Z9SU6yrpx9e9ZjW16GfuRAYH1dqULoBaqbVM8lE");
                    tService.AuthenticateWith("78066999-2rEuwupFgIpNYdHfBp6GQfcCw8ck3bJsA3pvupv7e", "PI88iqBxBhegY5wzgLiqgFLUKLPYfAj9EtMg81YqhJcoL");
                    this.service = tService;
                }                

                return this.service;
            }
        }

        private List<string> screenNames = new List<string>()
        {           
           "Gigolo_Ok",
           "VXipolitakis",
           "justinbieber",
           "Natalia_Oreiro",
           "fantinofantino",
           "ViaJuani",
           "mmatiasale",
           "Moria_Casan",
           "DiegoAMaradona",
           "karinacumbia"           
        }; 

        // GET: Tweets
        public ActionResult Index()
        {
            ViewBag.Mode = "Sync";
            var timer = new Stopwatch();
            timer.Start();
            var list = new List<Tweet>();

            // Magia

            foreach (var screenName in screenNames)
            {
                var tweet = this.GetLastTweet(screenName);
                list.Add(new Tweet(tweet));
            }

            // Fin Magia

            timer.Stop();
            
            ViewBag.Time = timer.Elapsed.TotalSeconds;

            return View("Async", list);
        }

        public ActionResult Tasks()
        {
            ViewBag.Mode = "Tasks";
            var timer = new Stopwatch();
            timer.Start();
            var taskList = new List<Task<TwitterStatus>>();

            // Magia

            foreach (var screenName in screenNames)
            {
                var task = Task.Factory.StartNew(() =>
                {
                    return this.GetLastTweet(screenName);
                });

                
                taskList.Add(task);
            }

            Task.WaitAll(taskList.ToArray());

            var list = taskList.Select(task => new Tweet(task.Result)).ToList();

            // Fin Magia

            timer.Stop();

            ViewBag.Time = timer.Elapsed.TotalSeconds;

            return View("Async", list);
        }

        public ActionResult Parallel()
        {
            ViewBag.Mode = "Parallel";
            var timer = new Stopwatch();
            timer.Start();
            var list = new List<Tweet>();

            // Magia
            System.Threading.Tasks.Parallel.ForEach(this.screenNames, (screenName) =>
            {
                var tweet = this.GetLastTweet(screenName);
                list.Add(new Tweet(tweet));
            });

            // Fin Magia

            timer.Stop();

            ViewBag.Time = timer.Elapsed.TotalSeconds;

            return View("Async", list);
        }

        public async Task<ActionResult> TweetsAsync()
        {
            ViewBag.Mode = "Async";
            var timer = new Stopwatch();
            timer.Start();

            // Magia

            var tasksList = screenNames.Select((screenName) => 
            {
                return this.Service.ListTweetsOnUserTimelineAsync(new ListTweetsOnUserTimelineOptions() { ScreenName = screenName });
            }).ToList();

            await Task.WhenAll(tasksList);

            var list = tasksList.Select(task => new Tweet(task.Result.Value.FirstOrDefault())).ToList();

            // Fin Magia

            timer.Stop();

            ViewBag.Time = timer.Elapsed.TotalSeconds;

            return View("Async", list);
        }

        private TwitterStatus GetLastTweet(string screenName)
        {           
            var tweets =
                this.Service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions() {ScreenName = screenName});

            return tweets.FirstOrDefault();
        }
    }
}