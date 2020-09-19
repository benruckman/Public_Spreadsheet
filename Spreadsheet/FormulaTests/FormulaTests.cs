using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace FormulaTests
{
    [TestClass]
    public class FormulaTests
    {
        [TestMethod]
        public void BasicConstructorTest()
        {
            Formula f = new Formula("1+1");
        }

        [TestMethod]
        public void ConstructorWithVariableTest()
        {
            Formula f = new Formula("A1 + b6 / 5");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void InvalidCharacterInFormulaTest()
        {
            Formula f = new Formula("4+3*2-s");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void StartingWithPlusTest()
        {
            Formula f = new Formula("+7*9");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void StartingWithMinusTest()
        {
            Formula f = new Formula("-7*9");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void StartingWithMultiplyTest()
        {
            Formula f = new Formula("*7*9");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void StartingWithDivideTest()
        {
            Formula f = new Formula("/7*9");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void StartingWithCloseParenthesesTest()
        {
            Formula f = new Formula(")7*9");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void EndingWithPlusTest()
        {
            Formula f = new Formula("7*9+");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void EndingWithMinusTest()
        {
            Formula f = new Formula("7*9-");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void EndingWithMultiplyTest()
        {
            Formula f = new Formula("7*9*");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void EndingWithDivideTest()
        {
            Formula f = new Formula("7*9/");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void EndingWithOpenParenthesesTest()
        {
            Formula f = new Formula("7*9(");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void EmptyStringTest()
        {
            Formula f = new Formula("");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void MoreCloseThanOpenParenthesesTest()
        {
            Formula f = new Formula("(9+1)/2)");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void MoreOpenThanCloseParenthesesTest()
        {
            Formula f = new Formula("((9+1)/2");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void PlusFollowsOpenParenthesesTest()
        {
            Formula f = new Formula("8+7*(+2)");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void MinusFollowsOpenParenthesesTest()
        {
            Formula f = new Formula("8+7*(-2)");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void DivideFollowsOpenParenthesesTest()
        {
            Formula f = new Formula("8+7*(/2)");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void MultiplyFollowsOpenParenthesesTest()
        {
            Formula f = new Formula("8+7*(*2)");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void CloseParenthesesFollowsOpenParenthesesTest()
        {
            Formula f = new Formula("8+7*()+2");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void CloseParenthesesFollowsPlusTest()
        {
            Formula f = new Formula("(8+7+)2");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void PlusFollowsPlusTest()
        {
            Formula f = new Formula("8+7++2");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void MinusFollowsPlusTest()
        {
            Formula f = new Formula("8+7+-2");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void MultiplyFollowsPlusTest()
        {
            Formula f = new Formula("8+7+*2");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void DividesFollowsPlusTest()
        {
            Formula f = new Formula("8+7+/2");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void ClosedParenthesesFollowsMinusTest()
        {
            Formula f = new Formula("(8+7-)2");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void PlusFollowsMinusTest()
        {
            Formula f = new Formula("8+7-+2");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void MinusFollowsMinusTest()
        {
            Formula f = new Formula("8+7--2");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void MultiplyFollowsMinusTest()
        {
            Formula f = new Formula("8+7-*2");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void DivideFollowsMinusTest()
        {
            Formula f = new Formula("8+7-/2");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void ClosedParenthesesFollowsMultiplyTest()
        {
            Formula f = new Formula("(8+7*)2");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void PlusFollowsMultiplyTest()
        {
            Formula f = new Formula("8+7*+2");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void MinusFollowsMultiplyTest()
        {
            Formula f = new Formula("8+7*-2");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void MultiplyFollowsMultiplyTest()
        {
            Formula f = new Formula("8+7**2");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void DivideFollowsMultiplyTest()
        {
            Formula f = new Formula("8+7*/2");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void ClosedParenthesesFollowsDivideTest()
        {
            Formula f = new Formula("(8+7/)2");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void PlusFollowsDivideTest()
        {
            Formula f = new Formula("8+7/+2");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void MinusFollowsDivideTest()
        {
            Formula f = new Formula("8+7/-2");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void MultiplyFollowsDivideTest()
        {
            Formula f = new Formula("8+7/*2");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void DivideFollowsDivideTest()
        {
            Formula f = new Formula("8+7//2");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void CloseParentheseBeginsFormulaTest()
        {
            Formula f = new Formula(")7+8");
        }

        [TestMethod]
        public void BasicEvaluateTest()
        {
            Formula f = new Formula(" 7 + 8/4 - 2 * 3");
            Assert.AreEqual(Double.Parse("3"), f.Evaluate(s => 0));
        }

        [TestMethod]
        public void AreNotEqualWithVariables()
        {
            Assert.IsFalse(new Formula("x1+y2").Equals(new Formula("y2+x1")));
        }

        [TestMethod]
        public void AreEqualWithDoublesTest()
        {
            Assert.IsTrue(new Formula("2.0 + x7").Equals(new Formula("2.000 + x7")));
        }

        [TestMethod]
        public void AreEqualOperatorTest()
        {
            Assert.IsTrue(new Formula("2.0 + x7") == new Formula("2.000 + x7"));
        }

        [TestMethod]
        public void AreNotEqualOperatorTest()
        {
            Assert.IsTrue(new Formula("2.0 + x7") != new Formula("2.001 + x7"));
        }

        [TestMethod]
        public void GetVariablesTest()
        {
            Formula f = new Formula("a2 - a5 + d4");

            Assert.IsTrue(f.GetVariables().Contains("a2"));
            Assert.IsTrue(f.GetVariables().Contains("a5"));
            Assert.IsTrue(f.GetVariables().Contains("d4"));
        }

        [TestMethod]
        public void DivideByZeroTest()
        {
            Formula f = new Formula("420 / (69 - 69)");
            f.Evaluate(s => 0);
        }

        [TestMethod]
        public void EqualHashCodeTest()
        {
            Assert.IsTrue(new Formula("2.0 + x7").GetHashCode() == new Formula("2.000 + x7").GetHashCode());
        }
    }
}
