using LastSurvey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LastSurvey.Controllers
{
    public class UserAnswersController : Controller
    {
        private ModelSurvey db = new ModelSurvey();
        
        public ActionResult IndexAdmin()
        {
            return View(db.Surveys.ToList());
        }

        public ActionResult Display(int? id)
        {
            ViewBag.Surv = db.Surveys.Where(s => s.SurveyID == id).FirstOrDefault();
            //Getting a list of the questions related to the surveyID == id
            var questions = db.Questions.Where(q => q.SurveyID == id).ToList();

            //Getting a list of userAnswers where the Answer.QuestionID == QuestionID
            List<UserAnswer> uanswers = new List<UserAnswer>();
            foreach (Question q in questions) 
            {
                uanswers.AddRange(db.UserAnswers.Where(ua => ua.Answer.QuestionID == q.QuestionID));
            }

            //Counting the number of answer related to the questionType
            var pointsAnswers = 0;
            var yesNoAnswers = 0;
            var openAnswers = 0;
            foreach (Question q in questions)
            {
                if (q.QuestionType.QuestionType1 == "points")
                {
                    pointsAnswers += 1;
                }
                else
                {
                    if (q.QuestionType.QuestionType1 == "yes/no")
                    {
                        yesNoAnswers += 1;
                    }
                    else
                    {
                        openAnswers += 1;
                    }
                }

            }

            //Getting the users and the dates
            List<User> users = new List<User>();
            List<DateTime> dates = new List<DateTime>();
            List<User> usersDistinct = new List<User>();
            List<DateTime> datesDistinct = new List<DateTime>();

            foreach (UserAnswer ua in uanswers) 
            {
                        dates.Add(ua.SubmitDate.Value);
                        users.Add(ua.User);

            }
            //Getting the data of the users and the dates distinct
            usersDistinct = users.Distinct().ToList();
            datesDistinct = dates.Distinct().ToList();
            int symptom = 0; //Total value for symptom
            int medication = 0; //Total value for the medication
            double mScore = 0; //Average value for the medication
            double sScore = 0; //Average value for the symptom
            int sDiv = 0; //Contains the product of the number of points questions * the number of users * the number of dates
            int mDiv = 0; //Contains the product of the number of yes/no questions * the number of users * the number of dates

            //Getting the total value for symptom and medication
            foreach (DateTime d in datesDistinct) 
            {
                foreach(UserAnswer ua in uanswers)
                {
                    if (ua.SubmitDate.Value == d) 
                    {
                        foreach (Question q in questions) 
                        {
                            if (ua.Answer.QuestionID == q.QuestionID) 
                            {
                                if (q.QuestionType.QuestionType1 == "points")
                                {
                                    int resInt = Convert.ToInt32(ua.Answer.Answer1);
                                    symptom += resInt;
                                }
                                else
                                {
                                    if (q.QuestionType.QuestionType1 == "yes/no")
                                    {
                                        int resInt = Convert.ToInt32(ua.Answer.Answer1);
                                        medication += resInt;
                                    }
                                }
                            }
                        }
                    }

                }
            }
            
            //Calculating the Average
            if (usersDistinct.Count > 1) { 
            sDiv = pointsAnswers * (usersDistinct.Count + datesDistinct.Count);
           
            }
            else
            {
                sDiv = pointsAnswers;
            }
            if (datesDistinct.Count > 1)
            {
                mDiv = yesNoAnswers * (usersDistinct.Count + datesDistinct.Count);
            }
            else
            {
                mDiv = yesNoAnswers;
            }
           
            if (sDiv != 0) 
            {
                sScore = symptom / sDiv;
            }
            if (mDiv != 0)
            {
                mScore = medication / mDiv;
            }
            ViewBag.Nusers = usersDistinct.Count;
            ViewBag.NDates = datesDistinct.Count;
            ViewBag.Medication = medication;
            ViewBag.Symptom = symptom;
            ViewBag.MScore = mScore;
            ViewBag.SScore = sScore;

            return View(uanswers);
        }
    }

}