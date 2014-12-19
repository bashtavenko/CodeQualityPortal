using System;
using System.Web.Mvc;
using System.Web.Routing;

using NUnit.Framework;

using CodeQualityPortal.Common;

namespace CodeQualityPortal.UnitTests
{
    public class HtmlHelpersTests
    {
        [Test]
        [TestCase("Index", "Traffic", true)]
        [TestCase("Index2", "Traffic", false)]
        public void HtmlHelpers_RouteMatch(string action, string controller, bool expected)
        {
            // Arrange
            var routeData = new RouteData();
            routeData.Values.Add("action", "Index");
            routeData.Values.Add("controller", "Traffic");


            var context = new ViewContext();
            context.RouteData = routeData;
            var helper = new HtmlHelper(context, new FakeContainer());

            // Act
            bool match = helper.RouteMatch(action, controller);
            Assert.AreEqual(match, expected);
        }

        public class FakeContainer : IViewDataContainer
        {
            public ViewDataDictionary ViewData
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }
        }    
    }
}
