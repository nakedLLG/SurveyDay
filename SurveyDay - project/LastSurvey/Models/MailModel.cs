﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LastSurvey.Models;

namespace LastSurvey.Models
{
    public class MailModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Email { get; set; }
    }
}