﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqToTwitter;
using LinqToTwitterTests.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinqToTwitterTests
{
    /// <summary>
    ///This is a test class for TrendRequestProcessorTest and is intended
    ///to contain all TrendRequestProcessorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TrendRequestProcessorTest
    {
        private TestContext testContextInstance;

        #region Test Data

        private string m_testTrendQueryResponse = @"<root type=""object"">
  <trends type=""array"">
    <item type=""object"">
      <name type=""string"">Wolverine</name>
      <url type=""string"">http://search.twitter.com/search?q=Wolverine+OR+%23Wolverine</url>
    </item>
    <item type=""object"">
      <name type=""string"">Swine Flu</name>
      <url type=""string"">http://search.twitter.com/search?q=%22Swine+Flu%22</url>
    </item>
    <item type=""object"">
      <name type=""string"">#swineflu</name>
      <url type=""string"">http://search.twitter.com/search?q=%23swineflu</url>
    </item>
    <item type=""object"">
      <name type=""string"">Dollhouse</name>
      <url type=""string"">http://search.twitter.com/search?q=Dollhouse+OR+%23dollhouse</url>
    </item>
    <item type=""object"">
      <name type=""string"">Hamthrax</name>
      <url type=""string"">http://search.twitter.com/search?q=Hamthrax+OR+%23hamthrax</url>
    </item>
    <item type=""object"">
      <name type=""string"">H1N1</name>
      <url type=""string"">http://search.twitter.com/search?q=H1N1</url>
    </item>
    <item type=""object"">
      <name type=""string"">X-Men Origins</name>
      <url type=""string"">http://search.twitter.com/search?q=%22X-Men+Origins%22</url>
    </item>
    <item type=""object"">
      <name type=""string"">#outdoorplay</name>
      <url type=""string"">http://search.twitter.com/search?q=%23outdoorplay</url>
    </item>
    <item type=""object"">
      <name type=""string"">Earthquake</name>
      <url type=""string"">http://search.twitter.com/search?q=Earthquake+OR+%23earthquake</url>
    </item>
    <item type=""object"">
      <name type=""string"">#jonaslive</name>
      <url type=""string"">http://search.twitter.com/search?q=%23jonaslive</url>
    </item>
  </trends>
  <as_of type=""string"">Sat, 02 May 2009 02:38:00 +0000</as_of>
</root>";

        private string m_testCurrentTrendQueryResponse = @"<root type=""object"">
  <trends type=""object"">
    <a:item xmlns:a=""item"" item=""2009-05-02 03:07:50"" type=""array"">
      <item type=""object"">
        <query type=""string"">Wolverine OR #wolverine</query>
        <name type=""string"">Wolverine</name>
      </item>
      <item type=""object"">
        <query type=""string"">""Swine Flu""</query>
        <name type=""string"">Swine Flu</name>
      </item>
      <item type=""object"">
        <query type=""string"">#SwineFlu</query>
        <name type=""string"">#SwineFlu</name>
      </item>
      <item type=""object"">
        <query type=""string"">H1N1</query>
        <name type=""string"">H1N1</name>
      </item>
      <item type=""object"">
        <query type=""string"">Dollhouse OR #dollhouse</query>
        <name type=""string"">Dollhouse</name>
      </item>
      <item type=""object"">
        <query type=""string"">Hamthrax</query>
        <name type=""string"">Hamthrax</name>
      </item>
      <item type=""object"">
        <query type=""string"">""X-Men Origins""</query>
        <name type=""string"">X-Men Origins</name>
      </item>
      <item type=""object"">
        <query type=""string"">Hawks</query>
        <name type=""string"">Hawks</name>
      </item>
      <item type=""object"">
        <query type=""string"">#jonaslive</query>
        <name type=""string"">#jonaslive</name>
      </item>
      <item type=""object"">
        <query type=""string"">#fitfam</query>
        <name type=""string"">#fitfam</name>
      </item>
    </a:item>
  </trends>
  <as_of type=""number"">1241233670</as_of>
</root>";

        string m_testAvailableQueryResponse = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<locations type=""array"">
<location>
  <woeid>23424969</woeid>
  <name>Turkey</name>
  <placeTypeName code=""12"">Country</placeTypeName>
  <country type=""Country"" code=""TR"">Turkey</country>
  <url>http://where.yahooapis.com/v1/place/23424969</url>
  <parentid>1</parentid>
</location>
<location>
  <woeid>2364559</woeid>
  <name>Birmingham</name>
  <placeTypeName code=""7"">Town</placeTypeName>
  <country type=""Country"" code=""US"">United States</country>
  <url>http://where.yahooapis.com/v1/place/2364559</url>
  <parentid>23424977</parentid>
</location>
<location>
  <woeid>395269</woeid>
  <name>Caracas</name>
  <placeTypeName code=""7"">Town</placeTypeName>
  <country type=""Country"" code=""VE"">Venezuela</country>
  <url>http://where.yahooapis.com/v1/place/395269</url>
  <parentid>23424982</parentid>
</location>
</locations>
";

        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            TestCulture.SetCulture();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for ProcessResults
        ///</summary>
        [TestMethod()]
        public void ProcessTrendResultsTest()
        {
            TrendRequestProcessor<Trend> target = new TrendRequestProcessor<Trend>();

            IList actual = target.ProcessResults(m_testTrendQueryResponse);

            var trends = actual.Cast<Trend>().ToList();
            Assert.AreEqual(10, trends.Count);
            Assert.AreNotEqual(DateTime.MinValue.Date, trends[0].AsOf.Date);
        }

        /// <summary>
        ///A test for ProcessResults
        ///</summary>
        [TestMethod()]
        public void ProcessCurrentTrendResultsTest()
        {
            TrendRequestProcessor<Trend> target = new TrendRequestProcessor<Trend>();

            IList actual = target.ProcessResults(m_testCurrentTrendQueryResponse);

            var trends = actual.Cast<Trend>().ToList();
            Assert.AreEqual(10, trends.Count);
            Assert.AreNotEqual(DateTime.MinValue.Date, trends[0].AsOf.Date);
        }

         /// <summary>
        ///A test for ProcessResults
        ///</summary>
        [TestMethod()]
        public void ProcessAvailableTrendResultsTest()
        {
            TrendRequestProcessor<Trend> target = new TrendRequestProcessor<Trend>();

            IList actual = target.ProcessResults(m_testAvailableQueryResponse);

            var trends = actual.Cast<Trend>().ToList();
            Assert.AreEqual(3, trends[0].Locations.Count);
        }

        [TestMethod]
        public void ProcessResults_Parses_Location()
        {
            var trendProc = new TrendRequestProcessor<Trend>();

            List<Trend> trends = trendProc.ProcessResults(m_testAvailableQueryResponse);

            Assert.AreEqual(3, trends[0].Locations.Count);

            Location loc = trends[0].Locations.First();
            Assert.AreEqual("Turkey", loc.Country);
            Assert.AreEqual("TR", loc.CountryCode);
            Assert.AreEqual("Country", loc.CountryType);
            Assert.AreEqual("Turkey", loc.Name);
            Assert.AreEqual("Country", loc.PlaceTypeName);
            Assert.AreEqual(12, loc.PlaceTypeNameCode);
            Assert.AreEqual("http://where.yahooapis.com/v1/place/23424969", loc.Url);
            Assert.AreEqual("23424969", loc.WoeID);
            Assert.AreEqual("1", loc.ParentID);
        }

        [TestMethod]
        public void ProcessResults_Returns_Empty_Collection_When_Empty_Results()
        {
            var trendProc = new TrendRequestProcessor<Trend>();

            var trends = trendProc.ProcessResults(string.Empty);

            Assert.AreEqual(0, trends.Count);
        }

        /// <summary>
        ///A test for GetParameters
        ///</summary>
        [TestMethod()]
        public void GetParametersTest()
        {
            var currDT = DateTime.Now;

            TrendRequestProcessor<Trend> target = new TrendRequestProcessor<Trend>();
            Expression<Func<Trend, bool>> expression =
                trend =>
                    trend.Type == TrendType.Current &&
                    trend.ExcludeHashtags == true;
            LambdaExpression lambdaExpression = expression as LambdaExpression;

            var queryParams = target.GetParameters(lambdaExpression);

            Assert.IsTrue(
                queryParams.Contains(
                    new KeyValuePair<string, string>("Type", ((int)TrendType.Current).ToString())));
            Assert.IsTrue(
                queryParams.Contains(
                    new KeyValuePair<string, string>("ExcludeHashtags", "True")));
        }

        /// <summary>
        ///A test for BuildURL
        ///</summary>
        [TestMethod()]
        public void BuildTrendsURLTest()
        {
            TrendRequestProcessor<Trend> target = new TrendRequestProcessor<Trend>() { BaseUrl = "https://api.twitter.com/1/" };
            Dictionary<string, string> parameters =
                new Dictionary<string, string>
                {
                    { "Type", ((int)TrendType.Trend).ToString() }
                };
            string expected = "https://api.twitter.com/1/trends.json";

            Request req = target.BuildURL(parameters);

            Assert.AreEqual(expected, req.FullUrl);
        }

        /// <summary>
        ///A test for BuildURL
        ///</summary>
        [TestMethod()]
        public void BuildWeeklyTrendsURLTest()
        {
            TrendRequestProcessor<Trend> target = new TrendRequestProcessor<Trend>() { BaseUrl = "https://api.twitter.com/1/" };
            Dictionary<string, string> parameters =
                new Dictionary<string, string>
                {
                    { "Type", ((int)TrendType.Weekly).ToString() },
                    { "Date", "2009-01-01" },
                    { "ExcludeHashtags", "true" }
                };
            string expected = "https://api.twitter.com/1/trends/weekly.json?date=2009-01-01&exclude=hashtags";

            Request req = target.BuildURL(parameters);

            Assert.AreEqual(expected, req.FullUrl);
        }

        /// <summary>
        ///A test for BuildURL
        ///</summary>
        [TestMethod()]
        public void BuildCurrentTrendsURLTest()
        {
            TrendRequestProcessor<Trend> target = new TrendRequestProcessor<Trend>() { BaseUrl = "https://api.twitter.com/1/" };
            Dictionary<string, string> parameters =
                new Dictionary<string, string>
                {
                    { "Type", ((int)TrendType.Current).ToString() },
                    { "Date", "2009-01-01" },
                    { "ExcludeHashtags", "true" }
                };
            string expected = "https://api.twitter.com/1/trends/current.json?date=2009-01-01&exclude=hashtags";

            Request req = target.BuildURL(parameters);

            Assert.AreEqual(expected, req.FullUrl);
        }

        /// <summary>
        ///A test for BuildURL
        ///</summary>
        [TestMethod()]
        public void BuildDailyTrendsURLTest()
        {
            TrendRequestProcessor<Trend> target = new TrendRequestProcessor<Trend>() { BaseUrl = "https://api.twitter.com/1/" };
            Dictionary<string, string> parameters =
                new Dictionary<string, string>
                {
                    { "Type", ((int)TrendType.Daily).ToString() },
                    { "Date", "2009-01-01" },
                    { "ExcludeHashtags", "true" }
                };
            string expected = "https://api.twitter.com/1/trends/daily.json?date=2009-01-01&exclude=hashtags";

            Request req = target.BuildURL(parameters);

            Assert.AreEqual(expected, req.FullUrl);
        }

        /// <summary>
        ///A test for BuildURL
        ///</summary>
        [TestMethod()]
        public void BuildAvailableTrendsURLTest()
        {
            TrendRequestProcessor<Trend> target = new TrendRequestProcessor<Trend>() { BaseUrl = "http://api.twitter.com/1/" };
            Dictionary<string, string> parameters =
                new Dictionary<string, string>
                {
                    { "Type", ((int)TrendType.Available).ToString() },
                    { "Latitude", "37.78215" },
                    { "Longitude", "-122.40060" }
                };
            string expected = "http://api.twitter.com/1/trends/available.xml?lat=37.78215&long=-122.40060";

            Request req = target.BuildURL(parameters);

            Assert.AreEqual(expected, req.FullUrl);
        }

        /// <summary>
        ///A test for BuildURL
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void BuildAvailableTrendsWithoutLatitudeURLTest()
        {
            TrendRequestProcessor<Trend> target = new TrendRequestProcessor<Trend>() { BaseUrl = "http://api.twitter.com/1/" };
            Dictionary<string, string> parameters =
                new Dictionary<string, string>
                {
                    { "Type", ((int)TrendType.Available).ToString() },
                    { "Longitude", "-122.40060" }
                };

            target.BuildURL(parameters);
        }

        /// <summary>
        ///A test for BuildURL
        ///</summary>
        [TestMethod()]
        public void BuildLocationTrendsURLTest()
        {
            TrendRequestProcessor<Trend> target = new TrendRequestProcessor<Trend>() { BaseUrl = "http://api.twitter.com/1/" };
            Dictionary<string, string> parameters =
                new Dictionary<string, string>
                {
                    { "Type", ((int)TrendType.Location).ToString() },
                    { "WeoID", "1" }
                };
            string expected = "http://api.twitter.com/1/trends/1.xml";

            Request req = target.BuildURL(parameters);

            Assert.AreEqual(expected, req.FullUrl);
        }

        /// <summary>
        ///A test for BuildURL
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void BuildLocationTrendsWithoutWeoIDURLTest()
        {
            TrendRequestProcessor<Trend> target = new TrendRequestProcessor<Trend>() { BaseUrl = "http://api.twitter.com/1/" };
            Dictionary<string, string> parameters =
                new Dictionary<string, string>
                {
                    { "Type", ((int)TrendType.Location).ToString() },
                };

            target.BuildURL(parameters);
        }

        /// <summary>
        ///A test for missing type
        ///</summary>
        [TestMethod()]
        public void MissingTypeTest()
        {
            TrendRequestProcessor<Trend> target = new TrendRequestProcessor<Trend>() { BaseUrl = "https://api.twitter.com/1/" };
            Dictionary<string, string> parameters = new Dictionary<string, string> { };

            try
            {
                target.BuildURL(parameters);

                Assert.Fail("Expected ArgumentException.");
            }
            catch (ArgumentException ae)
            {
                Assert.AreEqual<string>("Type", ae.ParamName);
            }
        }

        /// <summary>
        ///A test for null parameters
        ///</summary>
        [TestMethod]
        public void NullParametersTest()
        {
            TrendRequestProcessor<Trend> target = new TrendRequestProcessor<Trend>() { BaseUrl = "https://api.twitter.com/1/" };
            Dictionary<string, string> parameters = null;

            try
            {
                target.BuildURL(parameters);

                Assert.Fail("Expected ArgumentException.");
            }
            catch (ArgumentException ae)
            {
                Assert.AreEqual<string>("Type", ae.ParamName);
            }
        }
    }
}
