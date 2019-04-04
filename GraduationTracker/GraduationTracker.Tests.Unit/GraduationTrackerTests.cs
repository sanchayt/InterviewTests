using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace GraduationTracker.Tests.Unit
{
    [TestClass]
    public class GraduationTrackerTests
    {
        [TestMethod]
        public void TestHasCredits()
        {
            var tracker = new GraduationTracker();

            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                Requirements = new int[] { 100, 102, 103, 104 }
            };

            var students = new[]
            {
               new Student
               {
                   Id = 1,
                   Courses = new Course[]
                   {
                        new Course{Id = 1, Name = "Math", Mark=95 },
                        new Course{Id = 2, Name = "Science", Mark=95 },
                        new Course{Id = 3, Name = "Literature", Mark=95 },
                        new Course{Id = 4, Name = "Physichal Education", Mark=95 }
                   }
               },
               new Student
               {
                   Id = 2,
                   Courses = new Course[]
                   {
                        new Course{Id = 1, Name = "Math", Mark=80 },
                        new Course{Id = 2, Name = "Science", Mark=80 },
                        new Course{Id = 3, Name = "Literature", Mark=80 },
                        new Course{Id = 4, Name = "Physichal Education", Mark=80 }
                   }
               },
               new Student
               {
                    Id = 3,
                    Courses = new Course[]
                    {
                        new Course{Id = 1, Name = "Math", Mark=50 },
                        new Course{Id = 2, Name = "Science", Mark=50 },
                        new Course{Id = 3, Name = "Literature", Mark=50 },
                        new Course{Id = 4, Name = "Physichal Education", Mark=50 }
                    }
               },
               new Student
               {
                    Id = 4,
                    Courses = new Course[]
                    {
                        new Course{Id = 1, Name = "Math", Mark=40 },
                        new Course{Id = 2, Name = "Science", Mark=40 },
                        new Course{Id = 3, Name = "Literature", Mark=40 },
                        new Course{Id = 4, Name = "Physichal Education", Mark=40 }
                    }
               }


                //tracker.HasGraduated()
            };
            
            var graduated = new List<Tuple<bool, STANDING>>();

            foreach(var student in students)
            {
                graduated.Add(tracker.HasGraduated(diploma, student));      
            }

            foreach (var item in graduated)
            {
            Debug.Print(item.ToString());
                
            }
            
            Assert.IsTrue(graduated.Any());

        }
        [TestMethod]
        public void HasGraduated_StandingRemedial_False() {

            var remedialStudent = Repository.GetStudent(4);
            var diploma = Repository.GetDiploma(1);
            var tracker = new GraduationTracker();

            var result = tracker.HasGraduated(diploma,remedialStudent);

            Assert.AreEqual(result.Item1, false);
            Assert.AreEqual(result.Item2, STANDING.Remedial);


        }
        [TestMethod]
         public void HasGraduated_StandingAverage_True() {

            var averageStudent = Repository.GetStudent(3);
            var diploma = Repository.GetDiploma(1);
            var tracker = new GraduationTracker();

            var result = tracker.HasGraduated(diploma, averageStudent);

            Assert.AreEqual(result.Item1, true);
            Assert.AreEqual(result.Item2, STANDING.Average);


        }

        [TestMethod]
         public void HasGraduated_StandingSumaCumLaude_True() {

            var sumaCumLaudeStudent = Repository.GetStudent(2);
            var diploma = Repository.GetDiploma(1);
            var tracker = new GraduationTracker();

            var result = tracker.HasGraduated(diploma,sumaCumLaudeStudent);

            Assert.AreEqual(result.Item1, true);
            Assert.AreEqual(result.Item2, STANDING.SumaCumLaude);


        }

        [TestMethod]
         public void HasGraduated_StandingMagnaCumLaude_True() {

            var magnaCumLaudeStudent = Repository.GetStudent(1);
            var diploma = Repository.GetDiploma(1);
            var tracker = new GraduationTracker();

            var result = tracker.HasGraduated(diploma, magnaCumLaudeStudent);

            Assert.AreEqual(result.Item1, true);
            Assert.AreEqual(result.Item2, STANDING.MagnaCumLaude);


        }

        [TestMethod]
         public void HasGraduated_StandingNone_Exception() {

            var unknownStudent = Repository.GetStudent(0);
            var diploma = Repository.GetDiploma(1);
            var tracker = new GraduationTracker();
            try {

            var result = tracker.HasGraduated(diploma, unknownStudent);
            Assert.IsTrue(false);
            }catch(Exception){
                Assert.IsTrue(true);
            }


        }




    }
}
