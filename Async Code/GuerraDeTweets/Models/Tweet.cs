using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TweetSharp;

namespace GuerraDeTweets.Models
{
    public class Tweet
    {
        public Tweet(TwitterStatus status)
        {
            this.ScreenName = status.User.ScreenName;
            this.Avatar = status.User.ProfileImageUrl.Replace("_normal", "_bigger");
            this.TweetText = status.Text;
        }

        public string ScreenName { get; set; }
        
        public string Avatar { get; set; }

        public string TweetText { get; set; }
    }
}