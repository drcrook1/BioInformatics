using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BioInfo.Web.Core.Interfaces;

namespace BioInfo.Tests.Web.Core.Utilitys
{
    [TestClass]
    public class FunctionResultTests
    {
        [TestMethod]
        public void Passed_only_result_Did_fail_should_be_false()
        {
            var result = new FunctionResult<string>("result");

            Assert.IsFalse(result.DidFail());
        }

        [TestMethod]
        public void Passed_only_result_returs_value()
        {
            var result = new FunctionResult<string>("result");

            Assert.AreEqual(result.GetResult(),"result");
        }

        [TestMethod]
        public void Passed_error_message_and_did_fail_true_should_be_true()
        {
            var result = new FunctionResult<string>("result", "error message", true);
                
            Assert.IsTrue(result.DidFail());
        }

        [TestMethod]
        public void Passed_error_message_returns_error_message()
        {
            var result = new FunctionResult<string>("result", "error message", true);

            Assert.AreEqual(result.GetFriendlyError(), "error message");
        }

        [TestMethod]
        public void Failed_shortcut_marks_as_did_fail_with_error_message()
        {
            var result = FunctionResult<bool>.Fail("error message");

            Assert.IsTrue(result.DidFail());
            Assert.AreEqual(result.GetFriendlyError(), "error message");
        }
    }
}
