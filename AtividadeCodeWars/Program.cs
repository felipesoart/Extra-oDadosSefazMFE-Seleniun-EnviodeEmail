using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;


namespace AtividadeCodeWars
{
    public class Program
    {

        public static string[] conteudo;

        static void Main(string[] args)
        {

            

            IWebDriver driver = new ChromeDriver(@"C:\Users\admin\source\repos\AtividadeCodeWars\AtividadeCodeWars\bin\Debug");
            driver.Navigate().GoToUrl("https://www.sefaz.ce.gov.br/category/informativos-mfe/");

            if (!driver.Title.Equals("Arquivos Informativos MFE - Secretaria da Fazenda"))
            {
                throw new Exception("Esta não é a página de entrada");
            }
                                              

            foreach (var item in driver.FindElements(By.ClassName("cc-post")))
            {
                IWebElement dataPublicacao = item.FindElement(By.ClassName("cc-post-metas-date"));
                IWebElement descricaoPublicacao = item.FindElement(By.ClassName("cc-post-excerpt"));
                IWebElement siteReferenciaPublicacao = item.FindElement(By.ClassName("cc-post-title"));
                siteReferenciaPublicacao.GetAttribute("href");
                siteReferenciaPublicacao.FindElement(By.TagName("h3"));

                Console.WriteLine("\n" + dataPublicacao.Text + "\n" + siteReferenciaPublicacao.FindElement(By.TagName("h3")).Text + "\n" + descricaoPublicacao.Text + "\n" + siteReferenciaPublicacao.GetAttribute("href") + "\n");
            }          
                
            Console.Read();
            

            //1 capitura tudas as informações em html
            //string pagesource = driver.PageSource;
            //Console.WriteLine(pagesource);
            //Console.Read();

            //2 IWebElement element = driver.FindElement(By.Id("lst-ib"));//escolhe um elemento da page
            //element.SendKeys("selenium Testing");//digita no elemento acima
            //System.Threading.Thread.Sleep(4000);//espera
            //element.Clear();//apaga o que foi escrito no elemento
            //Console.Read();

            //3 coleta as referencias da tag A
            //foreach (var item in driver.FindElements(By.TagName("a")))
            //{
            //    Console.WriteLine(item.GetAttribute("href"));
            //}
            //Console.Read();

            //4 para clicar em um botao
            //IWebElement element = driver.FindElement(By.ClassName("cc-post-title"));
            //System.Threading.Thread.Sleep(5000);
            //element.Click();
            //Console.Read();


            //5 encontra um elemento pode usar varios BY para verificação
            //try
            //{
            //    driver.FindElement(By.Id("webcha"));
            //    Console.WriteLine("Elemento encontrado ID");

            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message+"elemento não encontrado");                
            //}
            //Console.Read();

            //6 traz o titulo da pagira em javascript por meio de execução java script
            //IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            //string title = (string)js.ExecuteScript("return document.title");
            //Console.WriteLine(title);
            //Console.Read();

            //7 abre uma caixa de alert por meio de execução java script
            //IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            //js.ExecuteScript("alert('oi este é o alert');");
            //System.Threading.Thread.Sleep(6000);
            //IAlert alert = driver.SwitchTo().Alert();
            //alert.Accept();            
            //Console.Read();


            //8 volta e avancar da pagina
            //IWebElement element = driver.FindElement(By.LinkText("Descontinuidade do leiaute 0.07 e liberação do DriverMFE"));//nome do local a que deseja a ação 
            //element.Click();
            //System.Threading.Thread.Sleep(4000);
            //driver.Navigate().Back();//voltar
            //System.Threading.Thread.Sleep(4000);
            //driver.Navigate().Forward();//avançar

            //9driver.Manage().Window.Maximize();//deixa a tela grande

            //10driver.Navigate().Refresh();//rezetar page

            //11 em um elemento expecifico clicar com o direito do mouser sobre ele
            //IWebElement element = driver.FindElement(By.ClassName("cc-post-excerpt"));
            //System.Threading.Thread.Sleep(4000);
            //Actions builder = new Actions(driver);
            //builder.ContextClick(element).Build().Perform();

            //12 decer ou subir o scroll
            //IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            //System.Threading.Thread.Sleep(4000);
            //js.ExecuteScript("window.scrollBy(0,950);");
            //Console.Read();

            //13 selecionando um valor em um select dropdown list    não funciona no sefaz pq nao tem select        
            //var option = driver.FindElement(By.Id("my-list"));
            //System.Threading.Thread.Sleep(4000);
            //var selectElement = new SelectElement(option);
            //System.Threading.Thread.Sleep(4000);
            //selectElement.SelectByText("WEBMAIL");
            //Console.Read();

            //14 salva o que tela de conteudo na pagina
            //Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            //ss.SaveAsFile("C:\\sample.png", ScreenshotImageFormat.Png);
            //Console.Read(); 

            //15 verificar elemento esta presente
            //IWebElement element = driver.FindElement(By.ClassName("cc-post-excerpt"));
            //Console.WriteLine(element.Displayed);
            //Console.Read();

            //15.1 obtem o valor do elemento
            //IWebElement element = driver.FindElement(By.ClassName("cc-post-excerpt"));
            //Console.WriteLine(element.Text);
            //Console.Read();

            //16 verificar se o titulo e verdadeiro se não for ele diz o titulo que se encontra na pagina
            //try
            //{
            //    Assert.AreEqual("Arquivos Informativos MFE - Secretaria da Fazenda", driver.Title);
            //    Assert.IsTrue(driver.FindElement(By.ClassName("cc-button")).Displayed); //verifica se existe Displayed=existe na tela Selected = checkbox
            //    Console.WriteLine("assertion pass");
            //}
            //catch (Exception e)
            //{

            //    Console.WriteLine(e);
            //}

            //String html_page2 = "Categoria: Informativos MFE";
            //try
            //{                
            //    Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(html_page2)); //verifica se existe o conteldo text na tagname
            //    Console.WriteLine("assertion pass");
            //}
            //catch (Exception e)
            //{

            //    Console.WriteLine(e);
            //}

            //Console.Read();

            //17 clica em um bottao por execução javascript
            //IWebElement element = driver.FindElement(By.ClassName("cc-button"));
            //System.Threading.Thread.Sleep(4000);
            //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);

            //System.Threading.Thread.Sleep(4000);
            //driver.Quit();


        }



    }

   
}




