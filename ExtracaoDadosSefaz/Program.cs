using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ExtracaoDadosSefaz
{
    public class Program
    {

        SqlConnection conexao = new SqlConnection();       
        //string dataColeta = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        
        string tituloEmail = "Novidades Sobre MFE de Sefaz do Ceará. Data Coleta: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

        public static string conteudoEmail = string.Empty;


        static void Main(string[] args)
        {
        
          IWebDriver driver = new ChromeDriver(@"C:\tfs\Checkout\Source-1.3.1.2\AtividadeCodeWars");
            driver.Navigate().GoToUrl("https://www.sefaz.ce.gov.br/category/informativos-mfe/");

            if (!driver.Title.Equals("Arquivos Informativos MFE - Secretaria da Fazenda"))
            {
                throw new Exception("Esta não é a página de entrada");
            }


            foreach (var item in driver.FindElements(By.ClassName("cc-post")))
            {
                IWebElement dataPublicacao2 = item.FindElement(By.ClassName("cc-post-metas-date"));
                IWebElement descricaoPublicacao2 = item.FindElement(By.ClassName("cc-post-excerpt"));
                IWebElement siteReferenciaPublicacao = item.FindElement(By.ClassName("cc-post-title"));
                siteReferenciaPublicacao.GetAttribute("href");
                siteReferenciaPublicacao.FindElement(By.TagName("h3"));

                string dataPublicacao = dataPublicacao2.Text;
                string tituloPublicacao = siteReferenciaPublicacao.FindElement(By.TagName("h3")).Text;
                string descricaoPublicacao = descricaoPublicacao2.Text;
                string urlPublicacao = siteReferenciaPublicacao.GetAttribute("href");


                Program sql = new Program();
                sql.inserrirDecNoticiasMFESefazCe(dataPublicacao, tituloPublicacao, descricaoPublicacao, urlPublicacao);

                //Console.WriteLine("\n" + dataPublicacao.Text + "\n" + siteReferenciaPublicacao.FindElement(By.TagName("h3")).Text + "\n" + descricaoPublicacao.Text + "\n" + siteReferenciaPublicacao.GetAttribute("href") + "\n");

            }

            Program email = new Program();
            if (!string.IsNullOrEmpty(conteudoEmail))
            {
                email.enviarEmail();
            }
           
            driver.Quit();
            //Console.Read();

        }

        public void conectar()
        {
            conexao.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            try
            {
                conexao.Open();
                //Console.WriteLine("Conexão Aberta");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\nproblema na comunicação com o banco");
                Console.WriteLine(ex.Message + "\n\n");
                Console.Read();
            }            

        }
        string dataColeta = string.Empty;
        public void inserrirDecNoticiasMFESefazCe(string dataPublicacao, string tituloPublicacao, string descricaoPublicacao, string urlPublicacao)
        {
            conectar();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao;
            cmd.CommandType = System.Data.CommandType.Text;
            //cmd.CommandText = string.Format (@"SELECT * FROM DECNOVIDADESMFESEFAZCE (NOLOCK) WHERE URLPUBLICACAO = '{0}'", urlPublicacao);
            //cmd.CommandText = string.Format(urlPublicacao);

            var reader = verificarURL(urlPublicacao);
                        
            if (!reader)
            {

                cmd.CommandText = string.Format(@"INSERT [dbo].[DECNOVIDADESMFESEFAZCE] ([DATAPUBLICACAO], [DATACRIACAO], [TITULOPUBLICACAO], [DESCRICAOPUBLICACAO],URLPUBLICACAO)
                                VALUES ('{0}',GETDATE(),'{1}','{2}','{3}')", dataPublicacao, tituloPublicacao, descricaoPublicacao, urlPublicacao);

                cmd.ExecuteNonQuery();

                conteudoEmail = conteudoEmail
                            + "<br />Data da Publicação: " + dataPublicacao
                            + "<br />Título da Publicação: " + tituloPublicacao
                            + "<br />Descrição da Publicação: " + descricaoPublicacao
                            + "<br />Url da Publicação: " + urlPublicacao + "<br /><br />";
            }

            conexao.Close();
            //Console.WriteLine("Conexão fechada" + conteudoEmail);
        }

        public bool verificarURL(string urlPublicacao)
        {
            bool ret = false;
            using (var conexao = new SqlConnection())
            {

                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = string.Format(@"SELECT * FROM DECNOVIDADESMFESEFAZCE (NOLOCK) WHERE URLPUBLICACAO = '{0}'", urlPublicacao);                    


                    var reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        ret=true;
                    }
                }
            }

            return ret;

        }

        public void enviarEmail()
        {
            
            var client = new SmtpClient()
            {
                Host = ConfigurationManager.AppSettings["EmailServidor"],
                Port = Convert.ToInt32(ConfigurationManager.AppSettings["EmailPorta"]),
                EnableSsl = (ConfigurationManager.AppSettings["EmailSsl"] == "S"),
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(
                    ConfigurationManager.AppSettings["EmailUsuario"],
                    ConfigurationManager.AppSettings["EmailSenha"])
            };
            var mensagem = new MailMessage();
            mensagem.From = new MailAddress(ConfigurationManager.AppSettings["EmailOrigem"], "Decodificar LTDA");
            mensagem.To.Add("felipe.souza@decodificar.net.br, dsscaze@decodificar.net.br");
            mensagem.Subject = tituloEmail;
            mensagem.Body = string.Format(conteudoEmail);
            mensagem.IsBodyHtml = true;

            client.Send(mensagem);
        }
       
    }
}
