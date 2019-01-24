using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using NUnit.Framework;
using System.Threading;

namespace Kariyer.netTestProject
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        IWebDriver driver = new FirefoxDriver();

        [SetUp]
        public void Initialize()
        {
            driver.Url = "https://www.kariyer.net/";
        }

        [Test]
        public void TestMethod()
        {
            Thread.Sleep(2000);
            //Üye girişi linkine tıklandı
            driver.FindElement(By.LinkText("ÜYE GİRİŞİ")).Click();
            Thread.Sleep(2000);
            //Login sayfasında inputlar dolduruldu ve giriş butonuna tıklandı
            driver.FindElement(By.Id("lgnUserName")).SendKeys("test_acount_1@hotmail.com");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("lgnPassword")).SendKeys("bjk12345");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("chkBeniHatirlaKariyerim")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.LinkText("Giriş")).Click();
            Thread.Sleep(2000);

            //İndex sayfasında karşımıza çıkan modal'ı kapatırız.
            //driver.FindElement(By.Id("btnIlgilenmiyorum")).Click();
            //Thread.Sleep(2000);

            //İş Arama sayfasına geçiş yapılır.
            driver.FindElement(By.Id("lnkIsAra")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("txtSearchKeyword")).Clear();
            Thread.Sleep(2000);

            //İş Arama sayfasında "Selenium" yazılıp arama yapılır.
            driver.FindElement(By.Id("txtSearchKeyword")).SendKeys("Selenium" + Keys.Enter);
            Thread.Sleep(2000);

            //İş ilanları sayfasında ilk iki ilanın görüntülenmesi kontrol edilir.
            int ilk_ilan = driver.FindElements(By.Id("ilan0")).Count;
            //Console.WriteLine(ilk_ilan);
            Assert.AreEqual(ilk_ilan, 1);
            int ikinci_ilan = driver.FindElements(By.Id("ilan1")).Count;
            Assert.AreEqual(ikinci_ilan, 1);
            Thread.Sleep(2000);

            // İş ilanları sayfasında ikinci ilanın show sayfasına gidilir.
            driver.FindElement(By.LinkText("Kıdemli Test Uzmanı / Paytrek")).Click();
            Thread.Sleep(3000);

            //İlan seçildiğinde detay sayfası yeni sekmede açılmaktadır, 
            //ama test akışı ilanların listelendiği sayfada devam etmeye çalışmaktadır,
            //bu nedenle teste ilanın detay sayfasından devam edebilmek için,
            //bir yan sekme olan ilanın detay sayfasına geçiş yapmak için switchTo methodu kullanıldı.
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            Thread.Sleep(3000);

            //İlanın Detay sayfasında olduğu kontrol edilir.
            string bodyText_giris = driver.FindElement(By.TagName("body")).Text;
            Assert.IsTrue(bodyText_giris.Contains("GENEL NİTELİKLER"));
            Thread.Sleep(2000);

            //Başvur butonuna tıklanarak başvuru tamamlama sayfasına geçiş yapılmıştır.
            driver.FindElement(By.Id("btnJobApply")).Click();
            Thread.Sleep(3000);

            //Başvuru tamamlama sayfasına geçiş yapıldığı kontrol edilir.
            string bodyText_basvuru = driver.FindElement(By.TagName("body")).Text;
            Assert.IsTrue(bodyText_basvuru.Contains("FİRMANIN SORULARI"));
            Thread.Sleep(2000);

            //Başvuruyu tamamlamak için doldurulması gerekli olan ücret beklentisi seçilir.
            driver.FindElement(By.XPath("//*[contains(@class, 'firmanin-sorulari')]//*[contains(@class, 'select2-arrow')]")).Click();
            Thread.Sleep(2000);

            int count1 = driver.FindElements(By.XPath("//*[contains(@id, 'select2-results-3')]")).Count;
            Console.WriteLine(count1);
            Thread.Sleep(2000);
            int count2 = driver.FindElements(By.XPath("//*[contains(@id, 'select2-results-3')]//*[contains(@class, 'select2-results-dept-0 select2-result select2-result-selectable')]")).Count;
            Console.WriteLine(count2);

            driver.FindElement(By.XPath("//*[contains(@id, 'select2-results-3')]//*[contains(@class, 'select2-results-dept-0 select2-result select2-result-selectable')]")).Click();
            Thread.Sleep(3000);

            //Başvuruyu tamamla butonuna tıklanır ve sonuç görüntüleme sayfasına geçiş yapılır.
            driver.FindElement(By.Id("btnBasvuruTamamla")).Click();
            Thread.Sleep(3000);

            //Başvuru tamamlandıktan sonra sonuç yazısının görüntülendiği kontrol edilir.
            string basvuru_sonuc = driver.FindElement(By.TagName("body")).Text;
            Assert.IsTrue(basvuru_sonuc.Contains("Başvurun iletildi, seçilme şansını daha da artırmak ister misin?"));

        }

    }

}
