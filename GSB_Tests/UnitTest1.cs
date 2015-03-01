using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GSB_ClassLibrary;

namespace GSB_Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void EmailTestValid()
        {
            Customer e = new Customer();
            string expected = "prenom.nom@domain.com";
            string actual;
            // action 
            e.Email = expected; actual = e.Email;
            // assertion 
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EmailTestNull()
        {
            Customer e = new Customer();
            e.Email = "mail_non_valide";
            Assert.IsNull(e.Email);
        }
    }
}
