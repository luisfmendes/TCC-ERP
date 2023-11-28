using Microsoft.Win32;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using static TCCAcai.VariaveisGlobal;

namespace TCCAcai
{
    class clsComponentes
    {
        
        /// <summary>
        /// Converte Arquivo para Base64
        /// </summary>
        /// <param name="cCamArquivo"></param>
        /// <returns></returns>
        public static string converteBase64(string cCamArquivo)
        {
            try
            {
                string arquivo = "";
                if (System.IO.File.Exists(cCamArquivo))
                {
                    byte[] objByte = System.IO.File.ReadAllBytes(cCamArquivo);
                    arquivo = Convert.ToBase64String(objByte);
                    //File.WriteAllText("c:\\" + Path.GetFileNameWithoutExtension(cCamArquivo) + ".txt", arquivo);
                }
                return arquivo.ToString();
            }
            catch { throw; }
        }

        /// <summary>
        /// Converte Base64 para Arquivo 
        /// </summary>
        /// <param name="cArquivoBD"></param>
        /// <param name="cCamArquivoDest"></param>
        public static void converteArquivo(string cArquivoBD, string cCamArquivoDest)
        {
            try
            {
                //arquivo = File.ReadAllText("c:\\temp\\emtexto.txt");
                byte[] objByte = Convert.FromBase64String(cArquivoBD);
                System.IO.File.WriteAllBytes(cCamArquivoDest, objByte);
            }
            catch { throw; }
        }
        #region EnviaEmail

        /// <summary>
        /// FUNÇÃO PARA ENVIO DE E-MAIL
        /// </summary>
        /// <param name="destinatario">[OBRIGATORIO]Informe o E-mail do destinatário, para mais de um destinatário use ; como separador</param>
        /// <param name="comCopiaPara">[OPCIONAL]Informe o E-mail para enviar uma cópia, para mais de um destinatário use ; como separador</param>
        /// <param name="comCopiaOcultaPara">[OPCIONAL]E-mail para enviar cópia oculta, para mais de um destinatário use ; como separador</param>
        /// <param name="assunto">Assunto do E-mail</param>
        /// <param name="mensagemHTML">Mensagem do E-mail. Permite o uso de Tag HTML</param>
        /// <param name="anexos">[OPCIONAL]Informe o caminho completo do anexo, para mais de um arquivo use ; como separador</param>
        /// <param name="smtp">[OBRIGATORIO]Endereço do servidor SMTP</param>
        /// <param name="porta">[OBRIGATORIO]Porta para envio de do E-mail</param>
        /// <param name="emailDoRemetente">[OBRIGATORIO]E-mail do remetente</param>
        /// <param name="nomeRemetente">Nome do remetente</param>
        /// <param name="senhaDoEmail">[OBRIGATORIO]Senha do email</param>
        /// <param name="ssl">Usar criptografia de segurança SSL</param>
        /// <param name="prioriade">Prioridade do E-mail: 0-Normal, 1-Baixa 2-Alta</param>
        /// <param name="confirmacaoDeLeitura">[OPCIONAL]Solicitar confirmação de leitura</param>
        /// <param name="imagemBase64">[OPCIONAL]Para incorporar uma imagem ao corpo do e-mail</param>
        /// <param name="autenticacao_login">Login de autenticação</param>
        /// <param name="autenticacao_senha">Senha de autenticação</param>
        /// <returns>RETORNA "OK" SE O EMAIL FOI ENVIADO COM SUCESSO</returns>
        public static string EnviarEmail(string destinatario, string comCopiaPara, string comCopiaOcultaPara, string assunto, string mensagemHTML, string anexos, string smtp, string porta, string emailDoRemetente, string nomeRemetente, string senhaDoEmail, bool ssl, int prioridade, bool confirmacaoDeLeitura, string imagemBase64, string autenticacao_login, string autenticacao_senha)
        {
            try
            {
                string erros = "";
                if (destinatario.IsNullOrEmpty())
                {
                    erros += "Informe o E-mail do Destinatário.\n";
                }

                if (smtp.IsNullOrEmpty())
                {
                    erros += "Informe o servidor SMTP.\n";
                }

                if (porta.IsNullOrEmpty())
                {
                    erros += "Informe a porta para envio do E-mail.\n";
                }

                if (emailDoRemetente.IsNullOrEmpty())
                {
                    erros += "Informe o E-mail do Remetente.\n";
                }

                if (senhaDoEmail.IsNullOrEmpty())
                {
                    erros += "Informe a senha do E-mail.\n";
                }

                if (!erros.IsNullOrEmpty())
                {
                    throw new Exception(erros);
                }

                string corpo = "";
                if (imagemBase64.Trim() != string.Empty)
                {
                    corpo =
                    "<html>\r<head>\r<title>\r</title>\r</head>\r<body>\r" +
                    "<p>" + mensagemHTML + "</p>\r" +
                    "<img src=" + (char)34 + "data:image/png;base64," + imagemBase64 + (char)34 + " />" +
                    "\r</body>\r</html>";
                }
                else
                {
                    corpo =
                    "<html>\r<head>\r<title>\r</title>\r</head>\r<body>\r"
                    + "<p>" + mensagemHTML + "</p>\r</body></html>";
                }


                MailMessage oEmail = new MailMessage();

                //Remetente
                if (nomeRemetente.Trim().ToString() != string.Empty)
                {
                    oEmail.From = new MailAddress(emailDoRemetente, nomeRemetente.ToString(), Encoding.UTF8);
                }
                else
                {
                    oEmail.From = new MailAddress(emailDoRemetente);
                }

                //Anexo(s)
                if (anexos.ToString().Trim() != string.Empty)
                {
                    string[] anexo = anexos.ToString().Split(';');
                    if (anexo.Length > 0)
                    {
                        string arquivo = "";
                        for (int i = 0; i < anexo.Length; i++)
                        {
                            arquivo = anexo[i].Trim().ToString().Replace("\r", "").Replace("\n", "");
                            if (arquivo != string.Empty)
                            {
                                oEmail.Attachments.Add(new Attachment(arquivo));
                            }
                        }
                    }
                }

                //Destinatário(s)
                if (destinatario.Trim().ToString() != string.Empty)
                {
                    string[] dest = destinatario.Trim().ToString().Split(';');
                    if (dest.Length > 0)
                    {
                        string destino = "";
                        for (int i = 0; i < dest.Length; i++)
                        {
                            destino = dest[i].Trim().ToString().Replace("\r", "").Replace("\n", "");
                            if (!destino.IsNullOrEmpty())
                            {
                                oEmail.To.Add(new MailAddress(destino));
                            }
                        }
                    }
                }

                //Envio Cópia(s)
                if (comCopiaPara.Trim().ToString() != string.Empty)
                {
                    string[] comcopia = comCopiaPara.Trim().ToString().Split(';');
                    if (comcopia.Length > 0)
                    {
                        string copia = "";
                        for (int i = 0; i < comcopia.Length; i++)
                        {
                            copia = comcopia[i].Trim().ToString().Replace("\r", "").Replace("\n", "");
                            if (!copia.IsNullOrEmpty())
                            {
                                oEmail.CC.Add(new MailAddress(copia));
                            }
                        }
                    }
                }

                //Envio de Cópia(s) Oculta
                if (comCopiaOcultaPara.Trim().ToString() != string.Empty)
                {
                    string[] comcopiaoculta = comCopiaOcultaPara.Trim().ToString().Split(';');
                    if (comcopiaoculta.Length > 0)
                    {
                        string copiaOculta = "";
                        for (int i = 0; i < comcopiaoculta.Length; i++)
                        {
                            copiaOculta = comcopiaoculta[i].Trim().ToString().Replace("\r", "").Replace("\n", "");
                            if (!copiaOculta.IsNullOrEmpty())
                            {
                                oEmail.Bcc.Add(new MailAddress(copiaOculta));
                            }
                        }
                    }
                }

                oEmail.Priority = (prioridade >= 0 && prioridade <= 2) ? (MailPriority)prioridade : 0; //Prioridade de envio
                oEmail.IsBodyHtml = true; //Permite uso de Tag HTML 
                oEmail.Subject = assunto;//Assunto do e-mail
                oEmail.Body = corpo; //Mensagem a ser enviada
                oEmail.SubjectEncoding = Encoding.UTF8;  //ENCODING de Acentuação para o assunto.
                oEmail.BodyEncoding = Encoding.UTF8;  //ENCODING de Acentuação para a mensagem.


                //Solicita confirmação de leitura
                if (confirmacaoDeLeitura)
                {
                    oEmail.Headers.Add("Disposition-Notification-To", emailDoRemetente);
                }

                //Envio do E-mail
                using (SmtpClient oSMTP = new SmtpClient(smtp, porta.ToInt32()))
                {
                    oSMTP.EnableSsl = ssl;
                    oSMTP.Timeout = 60000; //Aguarda até 60 segundos para o envio mensagem
                    oSMTP.DeliveryMethod = SmtpDeliveryMethod.Network;
                    oSMTP.UseDefaultCredentials = false;

                    if (autenticacao_login.IsNullOrEmpty())
                    {
                        oSMTP.Credentials = new NetworkCredential(emailDoRemetente, senhaDoEmail);
                    }
                    else
                    {
                        oSMTP.Credentials = new NetworkCredential(autenticacao_login, autenticacao_senha);
                    }
                    oSMTP.Send(oEmail);
                    oSMTP.Dispose();
                }

                try { oEmail.Dispose(); } catch { }

                return "OK";
            }
            catch
            {
                throw;
            }
        }
        #endregion EnviaEmail
        
        
        /// <summary>
        /// Função que verifica se o form especifico esta aberto e fecha o mesmo
        /// </summary>
        /// <param name="sNomeForm">Nome do form a ser fechado</param>
        public static void Fechar_Form(string sNomeForm)
        {
            if (!Application.OpenForms[sNomeForm].IsNullOrEmpty())
            {
                Application.OpenForms[sNomeForm].Close();
            }
        }

      

        /// <summary>
        /// Abre no windows explorer o arquivo informado no path, ou somente o path caso nao informe arquivo
        /// </summary>
        /// <param name="path"></param>
        public static void Abrir_Diretorio_Informado(string path)
        {
            bool isfile = System.IO.File.Exists(path);
            if (isfile)
            {
                string argument = @"/select, " + path;
                System.Diagnostics.Process.Start("explorer.exe", argument);
            }
            else
            {
                bool isfolder = System.IO.Directory.Exists(Path.GetDirectoryName(path));
                if (isfolder)
                {
                    string argument = @"/select, " + Path.GetDirectoryName(path);
                    System.Diagnostics.Process.Start("explorer.exe", argument);
                }
            }
        }

        
        public static DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties by using reflection   
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
        
        public string fVirgulaPorPonto(double nValor, int nCasas) //troca "," por "." e delimita o n* casas decimais
        {
            string strValor = (String.Format("{0:0." + new string('0', nCasas) + "}", nValor)).ToString().Replace(",", ".");
            return strValor;
        }

        /// <summary>
        /// Validação de CPF
        /// </summary>
        /// <param name="cpf">CPF a ser validado</param>
        /// <returns>TRUE = CPF válido / FALSE = CPF inválido</returns>
        public static bool fCPFValido(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            String tempCpf, digito;
            int soma, resto;

            cpf = cpf.fSoNumeros().Trim();
            if (cpf.Length != 11)
            {
                return false;
            }

            for (int j = 0; j < 10; j++)
            {
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                {
                    return false;
                }
            }

            tempCpf = cpf.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma = soma + int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma = soma + int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }



        /// <summary>
        /// Validação de CNPJ
        /// </summary>
        /// <param name="cnpj">CNPJ a ser validado</param>
        /// <returns>TRUE = CNPJ válido / FALSE = CNPJ inválido</returns>
        public static bool fCNPJValido(string cnpj)//Retorna True se CNPJ válido e False se CNPJ inválido
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            String tempCnpj, digito;
            int soma, resto;

            cnpj = cnpj.fSoNumeros().Trim();
            if (cnpj.Length != 14)
            {
                return false;
            }

            for (int j = 0; j < 10; j++)
            {
                if (j.ToString().PadLeft(14, char.Parse(j.ToString())) == cnpj)
                {
                    return false;
                }
            }

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
            {
                soma = soma + int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            }

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;

            soma = 0;
            for (int i = 0; i < 13; i++)
            {
                soma = soma + int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }

        public bool fEmailValido(string cEmail)
        {
            return Regex.IsMatch(cEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }


        public string fTiraCharRepetido(string cTexto, Char cChar)//Retira o caracter repetido informado
        {
            for (int i = 0; i <= cTexto.Length - 2; i++)
            {
                if (cTexto.Substring(i, 2) == cChar.ToString() + cChar.ToString())
                {
                    cTexto = cTexto.Remove(i, 1);
                    i--;
                }
            }
            return cTexto.ToString().Trim();
        }
        public string fSobrenome(string cTexto)//Retorna o Sobrenome de um nome
        {
            cTexto = cTexto.TrimEnd();
            string sobrenome = "";
            int contador = 0;
            for (int i = cTexto.Length; i > 0; i--)
            {
                if (cTexto.Substring(i - 1, 1) == " ")
                {
                    sobrenome = cTexto.Substring(i, contador);
                    i = 0;
                }
                if (sobrenome == "" && i == 1)
                { sobrenome = cTexto; }
                contador++;
            }
            return sobrenome.ToString().Trim();
        }

        public string fPrimeiroNome(string cTexto)//Retorna o primeiro nome de um nome
        {
            cTexto = cTexto.TrimStart();
            string nome = "";
            int contador = 0;
            for (int i = 0; i <= cTexto.Length; i++)
            {
                if (i < cTexto.Length)
                {
                    if (cTexto.Substring(i, 1) == " ")
                    {
                        nome = cTexto.Substring(0, contador);
                        i = cTexto.Length;
                    }
                }
                if (nome == "" && i == cTexto.Length)
                { nome = cTexto; }
                contador++;
            }
            return nome;
        }

        /// <summary>
        /// Dll para envio de e-mail - Seek Informática jan/2015 - versão 1.01
        /// </summary>
        /// <param name="destinatario">[Requirido] - Informe o destinatário, para mais de um destinatário use ; como separador</param>
        /// <param name="comcopiapara">Informe o destinatário para enviar uma copia do email, para mais de um destinatário use ; como separador</param>
        /// <param name="comcopiaocultapara">Informe o destinatário para enviar uma Copia Oculta do email, para mais de um destinatário use ; como separador</param>
        /// <param name="assunto">Assunto do E-mail</param>
        /// <param name="mensagemHTML">Mensagem do E-mail</param>
        /// <param name="anexos">informe o caminho completo do anexo, para mais de um arquivo use ; como separador</param>
        /// <param name="smtp">[Requirido] - endereço smtp</param>
        /// <param name="porta">[Requerido] - porta</param>
        /// <param name="emaildoremetente">[Requirido] - email do remetente</param>
        /// <param name="nomeremetente">nome do remetente que aparecerá  no email</param>
        /// <param name="emailconfirmacao">email para receber confirmação de leitura</param>
        /// <param name="senha">[Requirido] senha do email</param>
        /// <param name="ssl">[Requerido] - segurança SSL - lógico</param>
        /// <param name="imagemBase64">Para incorporar uma imagem ao corpo do e-mail (Obs.: deve-se converter o arquivo de imagem em StringBase64)</param>
        /// <returns>retorno:  OK - se sucesso / Messagem de Erro - se houve erro</returns>
        public string fEnviaEmail(string destinatario, string comcopiapara, string assunto, string mensagemHTML, string anexos, string smtp, string porta, string emaildoremetente, string nomeremetente, string senha, bool ssl, string emailconfirmacao, string imagemBase64, string comcopiaocultapara)
        {
            //se desejar incorporar uma imagem ao corpo da mensagem transforme a imagem em string base64
            //<img src=" + (char)34 + "data:image/png;base64," + imagemBase64 + (char)34 + " />"
            string corpo = "";
            if (imagemBase64.Trim() != string.Empty)
            {
                corpo =
                "<html>\r<head>\r<title>\r</title>\r</head>\r<body>\r" +
                "<p>" + mensagemHTML + "</p>\r" +
                "<img src=" + (char)34 + "data:image/png;base64," + imagemBase64 + (char)34 + " />" +
                "\r</body>\r</html>";
            }
            else
            {
                corpo =
                "<html>\r<head>\r<title>\r</title>\r</head>\r<body>\r"
                + "<p>" + mensagemHTML + "</p>\r</body></html>";
            }

            try
            {
                MailMessage oEmail = new MailMessage();
                //remetente
                if (nomeremetente.Trim().ToString() != string.Empty)
                {
                    oEmail.From = new MailAddress(emaildoremetente, nomeremetente.ToString());
                }
                else
                {
                    oEmail.From = new MailAddress(emaildoremetente);
                }

                //Anexos
                if (anexos.ToString().Trim() != string.Empty)
                {
                    string[] anexo = anexos.ToString().Split(';');
                    if (anexo.Length > 0)
                    {
                        for (int i = 0; i < anexo.Length; i++)
                        {
                            if (anexo[i].Trim().ToString() != string.Empty)
                            {
                                oEmail.Attachments.Add(new Attachment(anexo[i]));
                            }
                        }
                    }
                }

                //destinarario
                if (destinatario.Trim().ToString() != string.Empty)
                {
                    string[] dest = destinatario.Trim().ToString().Split(';');
                    if (dest.Length > 0)
                    {
                        for (int i = 0; i < dest.Length; i++)
                        {
                            if (dest[i].Trim().ToString() != string.Empty)
                            {
                                oEmail.To.Add(new MailAddress(dest[i].Trim().ToString()));
                            }
                        }
                    }
                }

                //Enviar cópia 
                if (comcopiapara.Trim().ToString() != string.Empty)
                {
                    string[] comcopia = comcopiapara.Trim().ToString().Split(';');
                    if (comcopia.Length > 0)
                    {
                        for (int i = 0; i < comcopia.Length; i++)
                        {
                            if (comcopia[i].Trim().ToString() != string.Empty)
                            {
                                oEmail.CC.Add(new MailAddress(comcopia[i].Trim().ToString()));
                            }
                        }
                    }
                }

                //Enviar Cópia Oculta
                //oEmail.Bcc.Add("oculto@provedor.com.br");
                if (comcopiaocultapara.Trim().ToString() != string.Empty)
                {
                    string[] comcopiaoculta = comcopiaocultapara.Trim().ToString().Split(';');
                    if (comcopiaoculta.Length > 0)
                    {
                        for (int i = 0; i < comcopiaoculta.Length; i++)
                        {
                            if (comcopiaoculta[i].Trim().ToString() != string.Empty)
                            {
                                oEmail.Bcc.Add(new MailAddress(comcopiaoculta[i].Trim().ToString()));
                            }
                        }
                    }
                }
                //prioridade
                oEmail.Priority = MailPriority.Normal;
                //Aceita formato HTML 
                oEmail.IsBodyHtml = true;
                //Assunto do e-mail
                oEmail.Subject = assunto;
                //Mensagem a ser enviada
                oEmail.Body = corpo;
                //ENCODING para Acentuacao para o assunto.
                oEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
                //ENCODING para acentuacao para a mensagem
                oEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");
                //Solicita confirmãção de leitura
                if (emailconfirmacao.Trim().ToString() != string.Empty)
                {
                    oEmail.Headers.Add("Disposition-Notification-To", emailconfirmacao);
                }
                SmtpClient oSmtp = new SmtpClient();

                //endereço smtp
                oSmtp.Host = smtp;
                oSmtp.Port = int.Parse(porta);
                //email autenticado informe: login e senha
                oSmtp.Credentials = new NetworkCredential(emaildoremetente, senha);
                //Habilita envio SSL
                if (ssl)
                {
                    oSmtp.EnableSsl = true;
                }

                //envia o email
                oSmtp.Send(oEmail);
                // Fechando os objetos
                oEmail.Dispose();
                oSmtp = null;
                //sucesso no envio retorna "OK"
                return "OK";
            }
            catch (Exception Erro)
            {
                //Se houve erro retorna o erro
                return Erro.Message.ToString();
            }
        }
        
        #region Rotina para gerar e validar chave de ativação de modulos

        

        
        #endregion

        public void fLerXML(string cPath)
        {
            //XmlDocument xmlDoc = new XmlDocument();
            //string cxml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><myDataz xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><listS><sog><field1>123</field1><field2>a</field2><field3>b</field3></sog><sog><field1>456</field1><field2>c</field2><field3>d</field3></sog></listS></myDataz>";
            //xmlDoc.Load(cxml);

            XmlTextReader leitorXML = new XmlTextReader(cPath);
            ArrayList elementos = new ArrayList();

            while ((leitorXML.Read()))
            {
                switch (leitorXML.NodeType)
                {
                    case XmlNodeType.Element:
                        //Se existirem atributos
                        if (leitorXML.HasAttributes)
                        {
                            while (leitorXML.MoveToNextAttribute())
                            {
                                //Pega o valor do atributo.
                                elementos.Add(leitorXML.Value);
                            }
                        }
                        break;
                    case XmlNodeType.Text:
                        //Incluir o texto do elemento no ArrayList
                        elementos.Add(leitorXML.Value);
                        break;
                }
            }
            foreach (var num in elementos)
            {
                // lstXML.Items.Add(num);
            }
        }

        /// <summary>
        /// Retorna o valor da Tag especificada no XML, formatado conforme cada Tag
        /// </summary>
        /// <param name="cXml">Texto xml</param>
        /// <param name="cTag">Tag a ser procurada no xml</param>
        /// <returns></returns>
        public static string fLerXml(string cXml, string cTag, string cTagMestre = "", XmlDocument xmlDoc_Param = null)
        {
            string cRetorno = "";
            XmlDocument xmlDoc = new XmlDocument();
            if (xmlDoc_Param.IsNullOrEmpty())
            {
                xmlDoc.LoadXml(cXml);
            }
            else
            {
                xmlDoc = xmlDoc_Param;
            }

            XmlNodeList lTag;
            if (cTagMestre.IsNullOrEmpty())
            {
                lTag = xmlDoc.GetElementsByTagName(cTag);
            }
            else
            {
                lTag = xmlDoc.GetElementsByTagName(cTagMestre);
            }

            if (lTag.Count > 0)
            {
                if (cTagMestre.IsNullOrEmpty())
                {
                    if (!string.IsNullOrEmpty(lTag[0].InnerText))
                    {
                        cRetorno = lTag[0].InnerText;
                    }
                }
                else
                {
                    for (int b = 0; b < lTag.Count; b++)
                    {
                        if (lTag[b].Name == cTag)
                        {
                            cRetorno = lTag[b].InnerText;
                            break;
                        }
                        else
                        {
                            if (lTag[b].HasChildNodes)
                            {

                                for (int a = 0; a < (lTag[b].ChildNodes.Count); a++)
                                {
                                    if (Checar_No(lTag[b].ChildNodes[a]))
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                cRetorno = "";
            }
            bool Checar_No(XmlNode no)
            {
                if (no.Name == cTag)
                {
                    cRetorno = no.InnerText;
                    return true;
                }
                for (int i = 0; i < no.ChildNodes.Count; i++)
                {
                    if (no.ChildNodes[i].Name == cTag)
                    {
                        cRetorno = no.ChildNodes[i].InnerText;
                        return true;
                    }
                    if (no.ChildNodes[i].HasChildNodes)
                    {
                        if (Checar_No(no.ChildNodes[i]))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            return cRetorno;
        }

        
        
        #region Verificar se um programa está instalado
        

        #endregion Verificar se um programa está instalado


        #region >>>>>----- Ajusta Formato de Data do Computador -----<<<<<
        public const int LOCALE_SSHORTDATE = 0x1F;
        public const int LOCALE_SDATE = 0x1D;
        [DllImport("kernel32.dll")]
        static extern bool SetLocaleInfo(uint Locale, uint LCType, string lpLCData);
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        static extern int GetLocaleInfo(uint Locale, uint LCType, [In, MarshalAs(UnmanagedType.LPWStr)] string lpLCData, int cchData);
        [DllImport("kernel32.dll")]
        static extern uint GetUserDefaultLCID();

        /// <summary>
        /// Ajusta o formato de Data do PC para ficar da forma em que o sistema irá trabalhar (dd/MM/yyyy)
        /// </summary>       
        public static string AjustaFormatoData()
        {
            try
            {
                string sShortdate = "", sNewshortdate = "dd/MM/yyyy";
                uint lngLCID;
                lngLCID = GetUserDefaultLCID();

                int size = 255;
                String str = new String(' ', size);
                int iRetVal = GetLocaleInfo(lngLCID, LOCALE_SSHORTDATE, str, size);
                if (iRetVal != 0)
                {
                    sShortdate = str.Replace("\0", "").Trim();
                }

                if (sShortdate != sNewshortdate)
                {
                    SetLocaleInfo(lngLCID, LOCALE_SSHORTDATE, sNewshortdate);
                    SetLocaleInfo(lngLCID, LOCALE_SDATE, "/");
                    const int LOCALE_ILANGUAGE = 0x1;
                    SetLocaleInfo(lngLCID, LOCALE_ILANGUAGE, "0x401");

                    str = new String(' ', size);
                    iRetVal = GetLocaleInfo(lngLCID, LOCALE_SSHORTDATE, str, size);
                    if (iRetVal != 0)
                    {
                        sShortdate = str.Replace("\0", "").Trim();
                    }
                }
                return sShortdate;
            }
            catch { throw; }
        }
        #endregion >>>>>----- Ajusta Formato de Data do Computador -----<<<<<

        #region >>>>>----- Ajusta versão do navegador (IE) que será usado no componente WebBrowser do C# -----<<<<<

        public static void SetBrowserFeatureControl(bool browser_atual)
        {
            // http://msdn.microsoft.com/en-us/library/ee330720(v=vs.85).aspx       
            // FeatureControl settings are per-process
            var fileName = System.IO.Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
            // make the control is not running inside Visual Studio Designer
            if (String.Compare(fileName, "devenv.exe", true) == 0 || String.Compare(fileName, "XDesProc.exe", true) == 0)
            {
                return;
            }

            if (browser_atual)
            {
                SetBrowserFeatureControlKey("FEATURE_BROWSER_EMULATION", fileName, GetBrowserEmulationMode()); // As páginas da Web que contêm diretrizes baseadas em padrões !DOCTYPE são exibidas no IE10.
            }
            else
            {
                SetBrowserFeatureControlKey("FEATURE_BROWSER_EMULATION", fileName, 9000); // As páginas da Web que contêm diretrizes baseadas em padrões !DOCTYPE são exibidas no IE9.
            }
            #region Códigos            
            SetBrowserFeatureControlKey("FEATURE_AJAX_CONNECTIONEVENTS", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_ENABLE_CLIPCHILDREN_OPTIMIZATION", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_MANAGE_SCRIPT_CIRCULAR_REFS", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_DOMSTORAGE ", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_GPU_RENDERING ", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_IVIEWOBJECTDRAW_DMLT9_WITH_GDI  ", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_DISABLE_LEGACY_COMPRESSION", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_LOCALMACHINE_LOCKDOWN", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_BLOCK_LMZ_OBJECT", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_BLOCK_LMZ_SCRIPT", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_DISABLE_NAVIGATION_SOUNDS", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_SCRIPTURL_MITIGATION", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_SPELLCHECKING", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_STATUS_BAR_THROTTLING", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_TABBED_BROWSING", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_VALIDATE_NAVIGATE_URL", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_WEBOC_DOCUMENT_ZOOM", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_WEBOC_POPUPMANAGEMENT", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_WEBOC_MOVESIZECHILD", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_ADDON_MANAGEMENT", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_WEBSOCKET", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_WINDOW_RESTRICTIONS ", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_XMLHTTP", fileName, 1);
            #endregion Códigos
        }

        private static void SetBrowserFeatureControlKey(string feature, string appName, uint value)
        {
            using (var key = Registry.CurrentUser.CreateSubKey(
                String.Concat(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\", feature),
                RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                key.SetValue(appName, value, RegistryValueKind.DWord);
            }
        }

        private static UInt32 GetBrowserEmulationMode()
        {
            int browserVersion = 7;
            using (var ieKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer",
                RegistryKeyPermissionCheck.ReadSubTree,
                System.Security.AccessControl.RegistryRights.QueryValues))
            {
                var version = ieKey.GetValue("svcVersion");
                if (null == version)
                {
                    version = ieKey.GetValue("Version");
                    if (null == version)
                    {
                        throw new ApplicationException("Microsoft Internet Explorer é necessário para execução!");
                    }
                }
                int.TryParse(version.ToString().Split('.')[0], out browserVersion);
            }

            UInt32 mode = 11000; // Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE11 Standards mode. Default value for Internet Explorer 11.
            switch (browserVersion)
            {
                case 7:
                    mode = 7000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode. Default value for applications hosting the WebBrowser Control.
                    break;
                case 8:
                    mode = 8000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode. Default value for Internet Explorer 8
                    break;
                case 9:
                    mode = 9000; // Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode. Default value for Internet Explorer 9.
                    break;
                case 10:
                    mode = 10000; // Internet Explorer 10. Webpages containing standards-based !DOCTYPE directives are displayed in IE10 mode. Default value for Internet Explorer 10.
                    break;
                default:
                    // use IE11 mode by default
                    break;
            }
            return mode;
        }

        #endregion >>>>>----- Ajusta versão do navegador (IE) que será usado no componente WebBrowser do C# -----<<<<<                
        

    }

    #region >>>>>----- Funcões adicionais de Control/String/Object/Integer/Decimal/Date/Enum/Image/DataTable -----<<<<<

    #region >>>>>----- Funções de conversão de Data -----<<<<<

#pragma warning disable CS0660 // O tipo define os operadores == ou !=, mas não substitui o Object.Equals(object o)
#pragma warning disable CS0661 // O tipo define os operadores == ou !=, mas não substitui o Object.GetHashCode()
    /// <summary>
    /// Representa um DataType do tipo Data, que será mostrado apenas a Data (sem o horário).
    /// </summary>
    public struct Date : IComparable, IFormattable, IComparable<Date>, IEquatable<Date>
    {
        #region Propriedades

        private DateTime _dateValue;
        /// <summary>
        /// Retorna a data atual em formato Date
        /// </summary>
        public static Date Now => Date_Now();
        /// <summary>
        /// Retorna a data atual em formato Date
        /// </summary>
        public static Date Today => Date_Now();

        /// <summary>
        /// Retorna o primeiro dia do mês, para a data informada
        /// </summary>
        public Date FirstDayMonth => First_Day_Month();

        /// <summary>
        /// Retorna o último dia do mês, para a data informada
        /// </summary>
        public Date LastDayMonth => Last_Day_Month();

        /// <summary>
        /// Retorna o primeiro dia do ano, para a data informada
        /// </summary>
        public Date FirstDayYear => First_Day_Year();

        /// <summary>
        /// Retorna o último dia do ano, para a data informada
        /// </summary>
        public Date LastDayYear => Last_Day_Year();

        #endregion Propriedades        

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            Date otherDateOnly = (Date)obj;
            if (otherDateOnly != null)
            {
                return ToDateTime().CompareTo(otherDateOnly.ToDateTime());
            }
            else
            {
                throw new ArgumentException("Object não é do tipo Date");
            }
        }

        int IComparable<Date>.CompareTo(Date other)
        {
            return this.CompareToOfT(other);
        }
        public int CompareToOfT(Date other)
        {
            // If other is not a valid object reference, this instance is greater.
            if (other == new Date())
            {
                return 1;
            }

            return this.ToDateTime().CompareTo(other.ToDateTime());
        }

        bool IEquatable<Date>.Equals(Date other)
        {
            return this.EqualsOfT(other);
        }
        public bool EqualsOfT(Date other)
        {
            if (other == new Date())
            {
                return false;
            }

            if (this.Year == other.Year && this.Month == other.Month && this.Day == other.Day)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static Date Date_Now()
        {
            return new Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }

        public static bool TryParse(string s, ref Date result)
        {
            DateTime dateValue = default(DateTime);
            if (DateTime.TryParse(s, out dateValue))
            {
                result = new Date(dateValue.Year, dateValue.Month, dateValue.Day);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Date Parse(string s)
        {
            DateTime dateValue = default(DateTime);
            dateValue = DateTime.Parse(s);
            return new Date(dateValue.Year, dateValue.Month, dateValue.Day);
        }

        public static Date ParseExact(string s, string format)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime dateValue = default(DateTime);
            dateValue = DateTime.ParseExact(s, format, provider);
            return new Date(dateValue.Year, dateValue.Month, dateValue.Day);
        }

        public Date(int yearValue, int monthValue, int dayValue) : this()
        {
            Year = yearValue;
            Month = monthValue;
            Day = dayValue;
        }

        public Date AddDays(double value)
        {
            DateTime d = new DateTime(this.Year, this.Month, this.Day);
            d = d.AddDays(value);
            return new Date(d.Year, d.Month, d.Day);
        }

        public Date AddMonths(int months)
        {
            DateTime d = new DateTime(this.Year, this.Month, this.Day);
            d = d.AddMonths(months);
            return new Date(d.Year, d.Month, d.Day);
        }

        public Date AddYears(int years)
        {
            DateTime d = new DateTime(this.Year, this.Month, this.Day);
            d = d.AddYears(years);
            return new Date(d.Year, d.Month, d.Day);
        }

        /// <summary>
        /// Retorna o primeiro dia do mês, para a data informada
        /// </summary>
        Date First_Day_Month()
        {
            DateTime d = new DateTime(this.Year, this.Month, this.Day);
            return new Date(d.Year, d.Month, 1);
        }

        /// <summary>
        /// Retorna o último dia do mês, para a data informada
        /// </summary>
        Date Last_Day_Month()
        {
            DateTime d = new DateTime(this.Year, this.Month, this.Day);
            return new Date(d.Year, d.Month, DateTime.DaysInMonth(d.Year, d.Month));
        }

        /// <summary>
        /// Retorna o primeiro dia do ano, para a data informada
        /// </summary>
        Date First_Day_Year()
        {
            DateTime d = new DateTime(this.Year, this.Month, this.Day);
            return new Date(d.Year, 1, 1);
        }

        /// <summary>
        /// Retorna o último dia do ano, para a data informada
        /// </summary>
        Date Last_Day_Year()
        {
            DateTime d = new DateTime(this.Year, this.Month, this.Day);
            return new Date(d.Year, 12, DateTime.DaysInMonth(d.Year, 12));
        }

        /// <summary>
        /// Retorna o número de dias que o ano/mês informados possui
        /// </summary>
        public static int DaysInMonth(int year, int month)
        {
            return DateTime.DaysInMonth(year, month);
        }

        public DayOfWeek DayOfWeek
        {
            get { return _dateValue.DayOfWeek; }
        }

        public DateTime ToDateTime()
        {
            return _dateValue;
        }

        public int Year
        {
            get { return _dateValue.Year; }
            set { _dateValue = new DateTime(value, Month, Day); }
        }

        public int Month
        {
            get { return _dateValue.Month; }
            set { _dateValue = new DateTime(Year, value, Day); }
        }

        public int Day
        {
            get { return _dateValue.Day; }
            set { _dateValue = new DateTime(Year, Month, value); }
        }

        public static bool operator ==(Date aDateOnly1, Date aDateOnly2)
        {
            return (aDateOnly1.ToDateTime() == aDateOnly2.ToDateTime());
        }

        public static bool operator !=(Date aDateOnly1, Date aDateOnly2)
        {
            return (aDateOnly1.ToDateTime() != aDateOnly2.ToDateTime());
        }

        public static bool operator >(Date aDateOnly1, Date aDateOnly2)
        {
            return (aDateOnly1.ToDateTime() > aDateOnly2.ToDateTime());
        }

        public static bool operator <(Date aDateOnly1, Date aDateOnly2)
        {
            return (aDateOnly1.ToDateTime() < aDateOnly2.ToDateTime());
        }

        public static bool operator >=(Date aDateOnly1, Date aDateOnly2)
        {
            return (aDateOnly1.ToDateTime() >= aDateOnly2.ToDateTime());
        }

        public static bool operator <=(Date aDateOnly1, Date aDateOnly2)
        {
            return (aDateOnly1.ToDateTime() <= aDateOnly2.ToDateTime());
        }

        public static TimeSpan operator -(Date aDateOnly1, Date aDateOnly2)
        {
            return (aDateOnly1.ToDateTime() - aDateOnly2.ToDateTime());
        }

        public override string ToString()
        {
            return _dateValue.ToShortDateString();
        }

        public string ToString(string format)
        {
            return _dateValue.ToString(format);
        }

        public string ToString(string fmt, IFormatProvider provider)
        {
            return string.Format("{0:" + fmt + "}", _dateValue);
        }

        public string ToShortDateString()
        {
            return _dateValue.ToShortDateString();
        }

        public string ToDbFormat()
        {
            return string.Format("{0:yyyy-MM-dd}", _dateValue);
        }
    }
    #endregion >>>>>----- Funções de conversão de Data -----<<<<<

    #region >>>>>----- Funcões adicionais de Control -----<<<<<
    public static class ControlExtensions
    {
        #region Arredondamento de Objetos

        /// <summary>
        /// Arredonda o Controle passado como parâmetro.
        /// </summary>
        /// <param name="cControle">Controle que será efetuado o arredondamento.</param>
        /// <param name="pCanto">Tamanho arredondamento do canto (Altura x Largura) em pixels.</param>
        /// <param name="pTopo">Indica se faz o arredondamento dos cantos superiores.</param>
        /// <param name="pBase">Indica se faz o arredondamento dos cantos inferiores.</param>
        public static void ArredondaObjeto(this Control cControle, int pCanto, bool pTopo, bool pBase)
        {
            // pCanto -> Tamanho do Canto
            // pTopo -> Arredonda o Topo
            // pBase -> Arredonda a Base
            Rectangle r = new Rectangle();
            r = cControle.ClientRectangle;

            cControle.Region = new Region(SuArredondaRect(r, pCanto, pTopo, pBase));
        }
        /// <summary>
        /// Arredonda todos os cantos do Controle passado como parâmetro.
        /// </summary>
        /// <param name="cControle">Controle que será efetuado o arredondamento.</param>
        /// <param name="pCanto">Tamanho arredondamento do canto (Altura x Largura) em pixels.</param>
        public static void ArredondaObjeto(this Control cControle, int pCanto)
        { ArredondaObjeto(cControle, pCanto, true, true); }

        private static GraphicsPath SuArredondaRect(Rectangle pRect, int pCanto, bool pTopo, bool pBase)
        {
            GraphicsPath gp = new GraphicsPath();

            if (pTopo)
            {
                gp.AddArc(pRect.X - 1, pRect.Y - 1, pCanto, pCanto, 180, 90);
                gp.AddArc(pRect.X + pRect.Width - pCanto, pRect.Y - 1, pCanto, pCanto, 270, 90);
            }
            else
            {
                // Se não arredondar o topo, adiciona as linhas para poder fechar o retangulo junto com
                // a base arredondada
                gp.AddLine(pRect.X - 1, pRect.Y - 1, pRect.X + pRect.Width, pRect.Y - 1);
            }

            if (pBase)
            {
                gp.AddArc(pRect.X + pRect.Width - pCanto, pRect.Y + pRect.Height - pCanto, pCanto, pCanto, 0, 90);
                gp.AddArc(pRect.X - 1, pRect.Y + pRect.Height - pCanto, pCanto, pCanto, 90, 90);
            }
            else
            {
                // Se não arredondar a base, adiciona as linhas para poder fechar o retangulo junto com
                // o topo arredondado. Adiciona da direita para esquerda pois é na ordem contrária que 
                // foi adicionado os arcos do topo. E pra fechar o retangulo tem que desenhar na ordem :
                //  1 - Canto Superior Esquerdo
                //  2 - Canto Superior Direito
                //  3 - Canto Inferior Direito 
                //  4 - Canto Inferior Esquerdo.
                gp.AddLine(pRect.X + pRect.Width, pRect.Y + pRect.Height, pRect.X - 1, pRect.Y + pRect.Height);
            }

            return gp;
        }
        #endregion
    }
    #endregion >>>>>----- Funcões adicionais de Control -----<<<<<

    #region >>>>>----- Funcões adicionais de String -----<<<<<
    public static class StringExtensions
    {
        /// <summary>
        /// Retorna um número especificado de caracteres, iniciando a contagem à esquerda e fianlizando a contagem à direita.
        /// </summary>
        /// <param name="sSource">Texto original.</param>
        /// <param name="iLeft">O número de caracteres que irá retornar à esquerda.</param>
        /// <param name="iRight">O número de caracteres que irá retornar à direita.</param>
        public static string Substring2(this string sSource, int iLeft, int iRight)
        {
            sSource = sSource?.Trim();
            if (iRight <= iLeft || sSource?.Length <= iLeft || sSource?.Length <= iRight)
            {
                return sSource;
            }

            return sSource?.Substring(iLeft).Substring(0, iRight - iLeft);
        }
        /// <summary>
        /// Retorna um número especificado de caracteres, iniciando a contagem à esquerda.
        /// </summary>
        /// <param name="sSource">Texto original.</param>
        /// <param name="iLength">O número de caracteres que irá retornar à esquerda.</param>
        public static string Left(this string sSource, int iLength)
        {
            sSource = sSource?.Trim();
            if (sSource?.Length <= iLength)
            {
                return sSource;
            }

            return sSource?.Substring(0, iLength);
        }
        /// <summary>
        /// Retorna um número especificado de caracteres, iniciando a contagem à direita.
        /// </summary>
        /// <param name="sSource">Texto original.</param>
        /// <param name="iLength">O número de caracteres que irá retornar à direita.</param>
        public static string Right(this string sSource, int iLength)
        {
            sSource = sSource?.Trim();
            if (sSource?.Length <= iLength)
            {
                return sSource;
            }

            return sSource?.Substring(sSource.Length - iLength, iLength);
        }

        /// <summary>
        /// Retorna o texto da expressão com a primeira letra de cada palavra em maiúscula.
        /// </summary>
        /// <param name="sTexto">Texto original.</param>
        public static string PrimeiraPalavraMaiuscula(this string sTexto)
        {
            return (sTexto?.Length > 0) ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(sTexto.Trim().ToLower()) : sTexto;
        }

        /// <summary>
        /// Retorna o texto da expressão com apenas a primeira letra maiúscula.
        /// </summary>
        /// <param name="sTexto">Texto original.</param>
        public static string PrimeiraLetraMaiuscula(this string sTexto)
        {
            return (sTexto?.Length > 0) ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(sTexto.Left(1)) + sTexto.Substring(1).ToLower() : sTexto;
        }

        /// <summary>
        /// Retorna o texto da expressão sem apóstrofo (') e barra (\).
        /// </summary>
        /// <param name="sTexto">Texto original.</param>
        public static string RemoveApostrofo(this string sTexto)
        {
            return sTexto?.Trim().Replace("'", "").Replace(@"\", "");
        }

        /// <summary>
        /// Remove os acentos de uma Expressão
        /// </summary>
        /// <param name="cExpressao">Texto original.</param>
        public static string RemoveAcentos(this string cExpressao)
        {
            string cExpRetorno = cExpressao;
            string[] cProcurarPor = new string[] { "Á", "É", "Í", "Ó", "Ú", "á", "é", "í", "ó", "ú", "À", "È", "Ì", "Ò", "Ù", "à", "è", "ì", "ò", "ù", "Â", "Ê", "Î", "Ô", "Û", "â", "ê", "î", "ô", "û", "Ä", "Ë", "Ï", "Ö", "Ü", "ä", "ë", "ï", "ö", "ü", "Ã", "Õ", "ã", "õ", "Ç", "ç", "Ñ", "ñ", "º", "'", "´", "^", "~", "ª" };
            string[] cSubstituirPor = new string[] { "A", "E", "I", "O", "U", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "e", "i", "o", "u", "A", "O", "a", "o", "C", "c", "N", "n", "o", " ", " ", " ", " ", " " };
            char Letra;
            for (int x = 0; x < cExpressao.Length; x++)
            {
                Letra = Char.Parse(cExpressao.Substring(x, 1).ToString());
                for (int y = 0; y < cProcurarPor.Length; y++)
                {
                    if (Char.Parse(cProcurarPor[y]) == Letra)
                    {
                        cExpRetorno = cExpRetorno.Replace(Letra, char.Parse(cSubstituirPor[y]));
                    }
                }
            }
            return cExpRetorno;
        }

        /// <summary>
        /// Retorna apenas os valores numéricos e Letras (Sem nenhuma pontuação) em tipo String
        /// </summary>
        /// <param name="cTexto">Texto a ser convertido em números e Letras (Sem nenhuma pontuação)</param>
        /// <returns>Retorno do texto somente com os números e Letras (Sem nenhuma pontuação)</returns>
        public static string fSoNumeros_Letras(this string cTexto)
        {
            string cRetorno = "";
            foreach (char c in cTexto)
            {
                if (char.IsLetterOrDigit(c))
                {
                    cRetorno = cRetorno + c;
                }
            }

            return cRetorno;
        }

        /// <summary>
        /// Retorna apenas os valores numéricos em tipo String
        /// </summary>
        /// <param name="cTexto">Texto a ser convertido em números</param>
        /// <returns>Retorno do texto somente com os números</returns>
        public static string fSoNumeros(this string cTexto)
        {
            string cRetorno = "";
            if (!cTexto.IsNullOrEmpty())
            {
                foreach (char c in cTexto.Trim())
                {
                    if (char.IsDigit(c))
                    {
                        cRetorno = cRetorno + c;
                    }
                }
            }
            return cRetorno;
        }

        /// <summary>
        /// Retorna apenas os valores numéricos (Tipo Int) da string
        /// </summary>
        /// <param name="cTexto">Texto a ser convertido em números</param>
        /// <returns>Retorno do texto somente com os números</returns>
        public static int fSoNumeros_Int(this string cTexto)
        { return fSoNumeros(cTexto).ToInt32(); }

        /// <summary>
        /// Verifica se o texto possui apenas números
        /// </summary>
        /// <param name="cTexto">Texto a ser verificado</param>
        /// <returns>TRUE = Possui somente números / FALSE = Possui números e letras</returns>
        public static bool fIsNumber(this string cTexto)
        {
            if (cTexto.IsNullOrEmpty())
            {
                return false;
            }
            else
            {
                foreach (char c in cTexto)
                {
                    if (!char.IsDigit(c))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Verifica se o texto possui apenas letras
        /// </summary>
        /// <param name="cTexto">Texto a ser verificado</param>
        /// <returns>TRUE = Possui somente letras / FALSE = Possui números, letras ou outros caracteres</returns>
        public static bool fIsLetter(this string cTexto)
        {
            foreach (char c in cTexto)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Retorna apenas letras da String
        /// </summary>
        /// <param name="cTexto">Texto a ser verificado</param>
        /// <returns>Retorna apenas as letras da String</returns>
        public static string fSoLetras(this string cTexto)
        {
            string cRetorno = "";
            foreach (char c in cTexto)
            {
                if (char.IsLetter(c))
                {
                    cRetorno = cRetorno + c;
                }
            }

            return cRetorno;
        }

        /// <summary>
        /// Verifica se o texto possui apenas letras (todas minusculas), numeros e underline (usado para nomes de colunas do Postgres)
        /// </summary>
        /// <param name="cTexto">Texto a ser verificado</param>
        /// <returns>TRUE = letras (todas minusculas), numeros e underline / FALSE = Possui letra maiuscula ou caracter especial</returns>
        public static bool fIsLetrasMinusculas_postgres(this string cTexto)
        {
            byte[] asciiBytes = Encoding.ASCII.GetBytes(cTexto);
            foreach (char caractere in asciiBytes)
            {
                //  ----LETRA MINUSCULA-----    ---------NUMEROS---------    ---UNDERLINE--- 
                if (!char.IsLower(caractere) && !char.IsNumber(caractere) && caractere != 95)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Retornar texto criptografado em md5
        /// </summary>
        /// <param name="sTexto">Texto que deverá ser gerado em MD5</param>
        /// <returns>Código MD5 do texto</returns>
        public static string fCriptografar_MD5(this string sTexto)
        {
            StringBuilder strBuilder = new StringBuilder();
            try
            {
                MD5 md5Hasher = MD5.Create();
                byte[] valorCriptografado = md5Hasher.ComputeHash(Encoding.Default.GetBytes(sTexto));

                for (int i = 0; i < valorCriptografado.Length; i++)
                {
                    strBuilder.Append(valorCriptografado[i].ToString("x2"));
                }
            }
            catch { }
            return strBuilder.ToString();
        }

        /// <summary>
        /// Retornar texto criptografado 
        /// </summary>
        /// <param name="sTexto">Texto que deverá ser criptografado ou descriptografado</param>
        /// <param name="lDescriptografa">Determina se vai descriptografar ou criptografar</param>
        /// <returns>Texto criptografado ou descriptografado</returns>
        public static string fCriptografar(this string sTexto, bool bDescriptografa = false)
        {
            string sAux = "";
            if (!sTexto.IsNullOrEmpty())
            {
                try
                {

                    if (bDescriptografa)
                    {
                        //BASE64 PARA TEXTO
                        byte[] base64 = Convert.FromBase64String(sTexto);
                        sTexto = Encoding.Unicode.GetString(base64);
                    }

                    Int32 nAsc = 0;
                    for (int i = 0; i < sTexto.Trim().Length; i++)
                    {
                        nAsc = Asc(sTexto.Substring(i, 1));
                        if (bDescriptografa)
                        {
                            nAsc = nAsc + (99 + (i % 11));
                        }
                        else
                        {
                            nAsc = nAsc - (99 + (i % 11));
                        }

                        sAux += Chr(nAsc);
                    }

                    if (!bDescriptografa)
                    {
                        //TEXTO PARA BASE64 
                        byte[] base64 = Encoding.Unicode.GetBytes(sAux);
                        sAux = Convert.ToBase64String(base64);
                    }
                }
                catch { }
            }
            return sAux;
        }

        public static char Chr(int codigo)
        { return (char)codigo; }
        public static int Asc(string letra)
        { return Convert.ToChar(letra); }



        /// <summary>
        /// Tira acentos e caracteres especiais para gravar no PostGres
        /// </summary>
        /// <param name="sExpressao">String com a expressão que deverá ser verificada</param>
        public static string fRemoveAcentos(this string sExpressao)
        {
            string sExpRetorno = sExpressao;
            string[] sProcurarPor = new string[] { "Á", "É", "Í", "Ó", "Ú", "á", "é", "í", "ó", "ú", "À", "È", "Ì", "Ò", "Ù", "à", "è", "ì", "ò", "ù", "Â", "Ê", "Î", "Ô", "Û", "â", "ê", "î", "ô", "û", "Ä", "Ë", "Ï", "Ö", "Ü", "ä", "ë", "ï", "ö", "ü", "Ã", "Õ", "ã", "õ", "Ç", "ç", "Ñ", "ñ", "º", "'", "´", "^", "~", "ª", "/", "?", @"\", "¬", "¨" };
            string[] sSubstituirPor = new string[] { "A", "E", "I", "O", "U", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "e", "i", "o", "u", "A", "O", "a", "o", "C", "c", "N", "n", "o", " ", " ", " ", " ", " ", " ", " ", " ", " ", " " };
            char cLetra;
            for (int x = 0; x < sExpressao.Length; x++)
            {
                cLetra = Char.Parse(sExpressao.Substring(x, 1).ToString());
                for (int y = 0; y < sProcurarPor.Length; y++)
                {
                    if (Char.Parse(sProcurarPor[y]) == cLetra)
                    {
                        sExpRetorno = sExpRetorno.Replace(cLetra, char.Parse(sSubstituirPor[y]));
                    }
                }
            }
            return sExpRetorno;
        }

        /// <summary>
        /// Função replace que troca os valor exato especificado
        /// </summary>
        /// <param name="input">String referencia de dados  </param>
        /// <param name="find">Valor a ser procurado na string</param>
        /// <param name="replace">Valor a ser substituido na string</param>
        /// <returns></returns>
        public static string SafeReplace(this string input, string find, string replace)
        {
            string searchString = find.StartsWith("@") ? $@"@\b{find.Substring(1)}\b" : $@"\b{find}\b";
            string textToFind = searchString;
            return Regex.Replace(input, textToFind, replace);
        }

    }
    #endregion >>>>>----- Funcões adicionais de String -----<<<<<

    #region >>>>>----- Funcões adicionais de Object -----<<<<<
    public static class ObjectExtensions
    {
        static bool bFormatado = false;
        static NumberFormatInfo nfi = CultureInfo.CreateSpecificCulture("en-US").NumberFormat;
        static void FormatCulture_en_US()
        {
            bFormatado = true;
            nfi.CurrencySymbol = "";
            nfi.CurrencyDecimalSeparator = ".";
            nfi.CurrencyGroupSeparator = "";
            nfi.NumberDecimalSeparator = ".";
            nfi.NumberGroupSeparator = "";
        }

        /// <summary>
        /// Formata o valor object para String, na cultura Inglês (separador de milhar: ',' e separador decimal: '.')
        /// </summary>
        /// <param name="oSource">Valor original</param>
        /// <returns>Valor formatado em inglês</returns>
        public static string ToString_Culture_en_US(this object oSource)
        {
            if (oSource != null)
            {
                if (oSource is decimal)
                {
                    if (!bFormatado)
                    {
                        FormatCulture_en_US();
                    }

                    return ((decimal)oSource).ToString("N", nfi).Trim();
                }
                else
                {
                    return oSource.ToString().Trim();
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Função para transformar o nome do Enum em valor. O valor será capturado na descrição do Enum, separado pelo sinal de igual(=).
        /// </summary>
        /// <param name="oSource">Nome do Enum</param>
        /// <returns>Retorna o valor do Enum</returns>
        public static string ToString_ValueEnum(this object oSource)
        {
            if (oSource.IsNullOrEmpty())
            {
                return null;
            }
            
            else if (oSource is UF)
            {
                return ((int)oSource).ToString();
            }
            else
            {
                return oSource.ToString().Trim();
            }
        }




        /// <summary>
        /// Verifica se o conteúdo é nulo ou vazio
        /// </summary>
        /// <param name="oSource">Valor a ser verificado</param>
        /// <returns>TRUE = É Nulo ou vazio / FALSE = Não é nulo ou vazio</returns>
        public static bool IsNullOrEmpty(this object oSource)
        {
            if (oSource is DataTable)
            {
                return oSource == null || (oSource as DataTable).Rows.Count <= 0;
            }
            else if (oSource is DataSet)
            {
                return oSource == null || (oSource as DataSet).Tables.Count <= 0;
            }
            else if (oSource is DataRow[])
            {
                return oSource == null || (oSource as DataRow[]).Length <= 0;
            }
            else if (oSource is DataRow)
            {
                return oSource == null || (oSource as DataRow).ItemArray.Length <= 0;
            }
            else if (oSource is XmlDocument)
            {
                return (oSource == null || (oSource as XmlDocument).ChildNodes.Count == 0);
            }
            else if (oSource is Decimal)
            {
                return (oSource == null || oSource == DBNull.Value || (Decimal)oSource == 0);
            }
            else if (oSource is Int16)
            {
                return (oSource == null || oSource == DBNull.Value || (Int16)oSource == 0);
            }
            else if (oSource is Int32)
            {
                return (oSource == null || oSource == DBNull.Value || (Int32)oSource == 0);
            }
            else if (oSource is Int64)
            {
                return (oSource == null || oSource == DBNull.Value || (Int64)oSource == 0);
            }
            else if (oSource is Array)
            {
                return oSource == null || (oSource as Array).Length <= 0;
            }
            else if (oSource is IList)
            {
                return oSource == null || (oSource as IList).Count <= 0;
            }
            else
            {
                return (oSource == null || oSource == DBNull.Value || string.IsNullOrEmpty(oSource.ToString().Trim()));
            }
        }

        /// <summary>
        /// Determina se a expressão da variável está contida em um conjunto de expressões.
        /// </summary>
        /// <typeparam name="T">Tipo da variável</typeparam>
        /// <param name="source">Origem da expressão procurada</param>
        /// <param name="values">Valores que devem ser procurados</param>
        /// <returns>TRUE=Valor encontrado / FALSE=Valor não encontrado</returns>
        public static bool Inlist<T>(this T source, params T[] values)
        {
            return values.Contains(source);
        }

        /// <summary>
        /// Determina se o valor da variável está contido entre os valores Inicial e Final.
        /// </summary>
        /// <typeparam name="T">Tipo da variável</typeparam>
        /// <param name="source">Origem da expressão procurada</param>
        /// <param name="vInicial">Valor Inicial</param>
        /// <param name="vFinal">Valor Final</param>
        /// <returns>TRUE=Valor está contido / FALSE=Valor não contido</returns>
        public static bool Between<T>(this T source, T vInicial, T vFinal)
        {
            return Comparer<T>.Default.Compare(source, vInicial) >= 0
                && Comparer<T>.Default.Compare(source, vFinal) <= 0;
        }

        #region >>>>>----- Conversão de unidades String/Object para Decimal, Float, Double, Int16, Int32, Int64, DateTime, Boolean -----<<<<<
        /// <summary>
        /// Verifica se a String esta em formato de moeda (com . ou ,) e retorna valor inteiro (sem os centavos e arredondado)
        /// </summary>
        /// <param name="str_valor">Valor a ser verificado</param>
        static string VerificaFormatoMoeda(string str_valor)
        {
            if (str_valor != null && (str_valor.Contains(",") || str_valor.Contains(".")))
            {
                str_valor = Convert.ToInt64(str_valor.ToDecimal()).ToString();
            }

            return str_valor;
        }

        /// <summary>
        /// Converte para Int16 se possível. Se não for retorna 0.
        /// </summary>
        /// <param name="oSource">valor a ser convertido.</param>
        public static short ToInt16(this object oSource)
        {
            if (oSource.IsNullOrEmpty())
            {
                return 0;
            }
            else if (oSource is Enum)
            {
                return Convert.ToInt16(oSource);
            }

            oSource = VerificaFormatoMoeda(oSource.ToString());
            Int16 valor = 0;
            Int16.TryParse(oSource.ToString(), out valor);
            return valor;
        }
        public static void Remove<T>(this Queue<T> oSource, T valueRemove)
        {
            if (!oSource.IsNullOrEmpty())
            {
                Queue<T> q = new Queue<T>();
                q = new Queue<T>(oSource.Where(x => !x.Equals(valueRemove)));

                oSource.Clear();
                foreach (T val in q)
                {
                    oSource.Enqueue(val);
                }
            }
        }
        /// <summary>
        /// Converte para Int16? se possível. Se não for retorna NULL.
        /// </summary>
        /// <param name="oSource">valor a ser convertido.</param>
        public static short? ToInt16_Null(this object oSource)
        {
            if (oSource.IsNullOrEmpty())
            {
                return null;
            }
            else if (oSource is Enum)
            {
                return Convert.ToInt16(oSource);
            }

            oSource = VerificaFormatoMoeda(oSource.ToString());
            Int16 valor = 0;
            Int16? valor_ret = null;
            valor_ret = Int16.TryParse(oSource.ToString(), out valor) ? valor : (Int16?)null;
            return valor_ret;
        }

        /// <summary>
        /// Converte para Int32 se possível. Se não for retorna 0.
        /// </summary>
        /// <param name="oSource">valor a ser convertido.</param>
        public static int ToInt32(this object oSource)
        {
            if (oSource.IsNullOrEmpty())
            {
                return 0;
            }
            else if (oSource is Enum)
            {
                return Convert.ToInt32(oSource);
            }

            oSource = VerificaFormatoMoeda(oSource.ToString());
            Int32 valor = 0;
            Int32.TryParse(oSource.ToString(), out valor);
            return valor;
        }
        /// <summary>
        /// Converte para Int32? se possível. Se não for retorna NULL.
        /// </summary>
        /// <param name="oSource">valor a ser convertido.</param>
        public static int? ToInt32_Null(this object oSource)
        {
            if (oSource.IsNullOrEmpty())
            {
                return null;
            }
            else if (oSource is Enum)
            {
                return Convert.ToInt32(oSource);
            }

            oSource = VerificaFormatoMoeda(oSource.ToString());
            Int32 valor = 0;
            Int32? valor_ret = null;
            valor_ret = Int32.TryParse(oSource.ToString(), out valor) ? valor : (Int32?)null;
            return valor_ret;
        }

        /// <summary>
        /// Converte para Int64 se possível. Se não for retorna 0.
        /// </summary>
        /// <param name="oSource">valor a ser convertido.</param>
        public static long ToInt64(this object oSource)
        {
            if (oSource.IsNullOrEmpty())
            {
                return 0;
            }
            else if (oSource is Enum)
            {
                return Convert.ToInt64(oSource);
            }

            oSource = VerificaFormatoMoeda(oSource.ToString());
            Int64 valor = 0;
            Int64.TryParse(oSource.ToString(), out valor);
            return valor;
        }
        /// <summary>
        /// Converte para Int64? se possível. Se não for retorna NULL.
        /// </summary>
        /// <param name="oSource">valor a ser convertido.</param>
        public static long? ToInt64_Null(this object oSource)
        {
            if (oSource.IsNullOrEmpty())
            {
                return null;
            }
            else if (oSource is Enum)
            {
                return Convert.ToInt64(oSource);
            }

            oSource = VerificaFormatoMoeda(oSource.ToString());
            Int64 valor = 0;
            Int64? valor_ret = null;
            valor_ret = Int64.TryParse(oSource.ToString(), out valor) ? valor : (Int64?)null;
            return valor_ret;
        }

        /// <summary>
        /// Converte para Decimal, se não conseguir retorna 0
        /// </summary>
        /// <param name="oSource">valor a ser convertido.</param>
        public static Decimal ToDecimal(this object oSource)
        {

            if (oSource.IsNullOrEmpty())
            {
                return 0;
            }

            Decimal valor = 0;
            Decimal.TryParse(oSource.ToString(), out valor);
            return valor;
        }

        /// <summary>
        /// Converte valor com Ponto para Decimal, se não conseguir retorna 0
        /// </summary>
        /// <param name="oSource">valor a ser convertido.</param>
        public static Decimal ToDecimalComPonto(this object oSource)
        {
            if (oSource.IsNullOrEmpty())
            {
                return 0;
            }

            return oSource.RemovePonto().ToDecimal();
        }

        /// <summary>
        /// Converte valor com Ponto para Decimal, se não conseguir retorna NULL
        /// </summary>
        /// <param name="oSource">valor a ser convertido.</param>
        public static Decimal? ToDecimalComPonto_Null(this object oSource)
        {
            if (oSource.IsNullOrEmpty())
            {
                return null;
            }

            return oSource.RemovePonto().ToDecimal();
        }

        /// <summary>
        /// Remove o ponto do valor, para converter para decimal/double
        /// </summary>
        /// <param name="oSource"></param>
        /// <returns></returns>
        private static object RemovePonto(this object oSource)
        {
            if (oSource.ToString().Contains("."))
            {
                char cSeparador = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0];
                return oSource = oSource.ToString().Replace('.', cSeparador);
            }
            else
            {
                return oSource;
            }
        }

        /// <summary>
        /// Converte para Double, se não conseguir retorna 0
        /// </summary>
        /// <param name="oSource">valor a ser convertido.</param>
        public static Double ToDouble(this object oSource)
        {
            if (oSource.IsNullOrEmpty())
            {
                return 0;
            }

            Double valor = 0;
            Double.TryParse(oSource.ToString(), out valor);
            return valor;
        }

        /// <summary>
        /// Converte para Double, se não conseguir retorna NULL
        /// </summary>
        /// <param name="oSource">valor a ser convertido.</param>
        public static Double? ToDoubleComPonto(this object oSource)
        {
            if (oSource.IsNullOrEmpty())
            {
                return null;
            }

            return oSource.RemovePonto().ToDouble(); ;
        }

        /// <summary>
        /// Converte para Float, se não conseguir retorna 0
        /// </summary>
        /// <param name="oSource">valor a ser convertido.</param>
        public static float ToFloat(this object oSource)
        {
            if (oSource.IsNullOrEmpty())
            {
                return 0;
            }

            float valor = 0;
            float.TryParse(oSource.ToString(), out valor);
            return valor;
        }

        /// <summary>
        /// Converte para Boolean, se não conseguir retorna FALSE
        /// </summary>
        /// <param name="oSource">valor a ser convertido.</param>
        public static bool ToBoolean(this object oSource)
        {
            if (oSource.IsNullOrEmpty())
            {
                return false;
            }

            Boolean valor = false;
            valor = Boolean.TryParse(oSource.ToString(), out valor) ? valor : false;
            return valor;
        }

        /// <summary>
        /// Converte para DateTime, se não conseguir retorna DATA ATUAL
        /// </summary>
        /// <param name="oSource">valor a ser convertido.</param>
        public static DateTime ToDateTime(this object oSource)
        {
            if (oSource.IsNullOrEmpty())
            {
                return DateTime.Now;
            }

            DateTime valor;
            DateTime.TryParse(oSource.ToString(), out valor);
            return valor;
        }
        /// <summary>
        /// Converte para DateTime, se não conseguir retorna NULL
        /// </summary>
        /// <param name="oSource">valor a ser convertido.</param>
        public static DateTime? ToDateTime_Null(this object oSource)
        {
            if (oSource.IsNullOrEmpty())
            {
                return null;
            }

            DateTime valor;
            DateTime? valor_ret;
            valor_ret = DateTime.TryParse(oSource.ToString(), out valor) ? valor : (DateTime?)null;
            return valor_ret;
        }

        /// <summary>
        /// Converte para DateTimeOffset (Data/Hora/Zona), se não conseguir retorna DATA ATUAL
        /// </summary>
        /// <param name="oSource">valor a ser convertido.</param>
        public static DateTimeOffset ToDateTimeZone(this object oSource)
        {
            if (oSource.IsNullOrEmpty())
            {
                return DateTimeOffset.Now;
            }

            DateTimeOffset valor;
            DateTimeOffset.TryParse(oSource.ToString(), out valor);
            return valor;
        }
        /// <summary>
        /// Converte para DateTimeOffset (Data/Hora/Zona), se não conseguir retorna NULL
        /// </summary>
        /// <param name="oSource">valor a ser convertido.</param>
        public static DateTimeOffset? ToDateTimeZone_Null(this object oSource)
        {
            if (oSource.IsNullOrEmpty())
            {
                return null;
            }

            DateTimeOffset valor;
            DateTimeOffset? valor_ret;
            valor_ret = DateTimeOffset.TryParse(oSource.ToString(), out valor) ? valor : (DateTimeOffset?)null;
            return valor_ret;
        }

        /// <summary>
        /// Converte para Date, se não conseguir retorna DATA ATUAL
        /// </summary>
        /// <param name="oSource">valor a ser convertido.</param>
        public static Date ToDate(this object oSource)
        {
            if (oSource.IsNullOrEmpty())
            {
                return Date.Now;
            }

            Date valor = Date.Now;
            Date.TryParse(oSource.ToString(), ref valor);
            return valor;
        }
        /// <summary>
        /// Converte para Date, se não conseguir retorna NULL
        /// </summary>
        /// <param name="oSource">valor a ser convertido.</param>
        public static Date? ToDate_Null(this object oSource)
        {
            if (oSource.IsNullOrEmpty())
            {
                return null;
            }

            Date valor = Date.Now;
            Date? valor_ret;
            valor_ret = Date.TryParse(oSource.ToString(), ref valor) ? valor : (Date?)null;
            return valor_ret;
        }

        /// <summary>
        /// Converte para Char, se não conseguir retorna VAZIO
        /// </summary>
        /// <param name="oSource">valor a ser convertido.</param>
        public static char ToChar(this object oSource)
        {
            if (oSource.IsNullOrEmpty())
            {
                return default(char);
            }
            else if (oSource is Enum)
            {
                return Convert.ToChar(oSource);
            }

            Char valor;
            valor = Char.TryParse(oSource.ToString(), out valor) ? valor : default(char);
            return valor;
        }

        public static T Clone<T>(this T source)
        {
            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
            var serializeSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source, serializeSettings), deserializeSettings);
        }



        #endregion >>>>>----- Conversão de unidades String/Object para Decimal, Float, Double, Int16, Int32, Int64, DateTime, Boolean -----<<<<<
    }
    #endregion >>>>>----- Funcões adicionais de Object -----<<<<<

    #region >>>>>----- Funcões adicionais de Integer -----<<<<<
    public static class IntegerExtensions
    {
        /// <summary>
        /// Retorna o valor String formatado, conforme o tipo de máscara definido
        /// </summary>
        /// <param name="sSource">valor a ser convertido.</param>
        public static String ToString_Mask(this String sSource, eTipoMascara eMascara)
        { return sSource.Mascara(eMascara); }
        /// <summary>
        /// Retorna o valor String formatado, conforme o tipo de máscara definido
        /// </summary>
        /// <param name="iSource">valor a ser convertido.</param>
        public static String ToString_Mask(this Int64 iSource, eTipoMascara eMascara)
        { return iSource.Mascara(eMascara); }
        /// <summary>
        /// Retorna o valor String formatado, conforme o tipo de máscara definido
        /// </summary>
        /// <param name="iSource">valor a ser convertido.</param>
        public static String ToString_Mask(this Int32 iSource, eTipoMascara eMascara)
        { return iSource.Mascara(eMascara); }

        static String Mascara(this object oSource, eTipoMascara eMascara)
        {
            if (!oSource.IsNullOrEmpty())
            {
                Int64 iSource = 0;
                if (!(oSource is Int64))
                {
                    iSource = Convert.ToInt64(oSource);
                }

                if (eMascara.Inlist(eTipoMascara.TELEFONE, eTipoMascara.TELEFONE_SEMDDD, eTipoMascara.CELULAR, eTipoMascara.CELULAR_SEMDDD))
                {
                    eMascara = MascaraTelCel(iSource, eMascara);
                }

                return String.Format(FormatoMascara(eMascara), iSource);
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// Retorna o formato da máscara, conforme o tipo de máscara especificado
        /// </summary>
        /// <param name="eMascara">Tipo de Máscara a ser formatado</param>
        static String FormatoMascara(eTipoMascara eMascara)
        {
            switch (eMascara)
            {
                case eTipoMascara.CPF:
                    return @"{0:000\.000\.000\-00}";
                case eTipoMascara.CNPJ:
                    return @"{0:00\.000\.000\/0000\-00}";
                case eTipoMascara.RG:
                    return "";
                case eTipoMascara.IE:
                    return "";
                case eTipoMascara.PIS:
                    return @"{0:000\.0000\.000\-0}";
                case eTipoMascara.CNAE:
                    return @"{0:0000\-0\/00}";
                case eTipoMascara.CFOP:
                    return @"{0:0\.000}";
                case eTipoMascara.PLACA:
                    return "";
                case eTipoMascara.CEP:
                    return @"{0:00000\-000}";
                case eTipoMascara.TELEFONE:
                    return @"{0:(00)0000\-0000}";
                case eTipoMascara.TELEFONE_SEMDDD:
                    return @"{0:0000\-0000}";
                case eTipoMascara.CELULAR:
                    return @"{0:(00)00000\-0000}";
                case eTipoMascara.CELULAR_SEMDDD:
                    return @"{0:00000\-0000}";
                default:
                    return "";
            }
        }
        /// <summary>
        /// Verifica se o tipo da máscara de telefone/celular estão com a quantidade de casas corretas para o formato
        /// </summary>
        static eTipoMascara MascaraTelCel(Int64 iSource, eTipoMascara eMascara)
        {
            switch (eMascara)
            {
                case eTipoMascara.TELEFONE:
                    if (iSource.ToString().Length == 11)
                    {
                        return eTipoMascara.CELULAR;
                    }

                    break;
                case eTipoMascara.TELEFONE_SEMDDD:
                    if (iSource.ToString().Length == 9)
                    {
                        return eTipoMascara.CELULAR_SEMDDD;
                    }

                    break;
                case eTipoMascara.CELULAR:
                    if (iSource.ToString().Length == 10)
                    {
                        return eTipoMascara.TELEFONE;
                    }

                    break;
                case eTipoMascara.CELULAR_SEMDDD:
                    if (iSource.ToString().Length == 8)
                    {
                        return eTipoMascara.TELEFONE_SEMDDD;
                    }

                    break;
            }
            return eMascara;
        }
    }
    #endregion >>>>>----- Funcões adicionais de Integer -----<<<<<

    #region >>>>>----- Funcões adicionais de Decimal -----<<<<<
    public static class DecimalExtensions
    {
        /// <summary>
        /// Retorna o valor Decimal com a quantidade de Casas Decimais definidas no parâmetro 'iCasas'.
        /// NAO FAZ ARREDONDAMENTO. Portanto as casas decimais excedentes são eliminadas
        /// </summary>
        /// <param name="iCasas">Quantidade de Casas Decimais que o valor deve retornar</param>       
        /// <param name="bArredonda">Irá arredondar o valor das casas decimais excedentes?</param>
        private static Decimal CasasDecimais(this decimal dSource, int iCasas, bool bArredonda = false)
        {
            if (bArredonda)
            {
                return Math.Round(dSource, iCasas);
            }
            else
            {
                switch (iCasas)
                {
                    case 1:
                        return (Math.Truncate(dSource * 10) / 10);
                    case 2:
                        return (Math.Truncate(dSource * 100) / 100);
                    case 3:
                        return (Math.Truncate(dSource * 1000) / 1000);
                    case 4:
                        return (Math.Truncate(dSource * 10000) / 10000);
                    default: // 2 Casas Decimais
                        return (Math.Truncate(dSource * 100) / 100);
                }
            }
        }
        

       
        

        /// <summary>
        /// Retorna o Valor por Extenso (até 1 trilhão)
        /// </summary>
        public static string fExtenso(this decimal p_valor)
        {
            string[,] v_cifra = new string[6, 2];
            Decimal nValor = Math.Abs(p_valor);
            string m_str = String.Format("{0:0000000000000000.00}", nValor); //p_valor
            //string m_str  =nValor.ToString().Substring(18,2); //Str(nValor,18,2)
            v_cifra[0, 0] = "TRILHAO";
            v_cifra[0, 1] = "TRILHOES";
            v_cifra[1, 0] = "BILHAO";
            v_cifra[1, 1] = "BILHOES";
            v_cifra[2, 0] = "MILHAO";
            v_cifra[2, 1] = "MILHOES";
            v_cifra[3, 0] = "MIL";
            v_cifra[3, 1] = "MIL";
            v_cifra[4, 0] = "";
            v_cifra[4, 1] = "";
            v_cifra[5, 0] = "CENTAVO";
            v_cifra[5, 1] = "CENTAVOS";
            string m_extenso = "";
            string m_subs = "";
            string m_centavos = m_str.Substring(17, 2);
            if (nValor > 0)
            {
                if (int.Parse(m_centavos) > 0)
                {
                    if (int.Parse(m_centavos) == 1)
                    {
                        m_extenso = fValorPorExtenso(int.Parse(m_centavos)) + v_cifra[5, 0];
                    }
                    else
                    {
                        m_extenso = fValorPorExtenso(int.Parse(m_centavos)) + v_cifra[5, 1];
                    }
                }

                if (nValor >= 1)
                {
                    m_extenso = ((Int64)nValor == 1 ? "REAL" : "REAIS") + (int.Parse(m_centavos) > 0 ? " E " : "") + m_extenso;
                }

                for (int x = 5; x > 0; x--)
                {
                    m_subs = m_str.Substring(x * 3 - 2, 3);
                    if (int.Parse(m_subs) > 0)
                    {
                        if (int.Parse(m_subs) == 1)
                        {
                            if ((x - 1) <= 2)
                            {
                                m_extenso = fNumExtenso(Int64.Parse(m_subs)) + " " + v_cifra[x - 1, 0] + " E " + m_extenso; //de
                            }
                            else
                            {
                                m_extenso = fNumExtenso(Int64.Parse(m_subs)) + " " + v_cifra[x - 1, 0] + " " + m_extenso;
                            }
                        }
                        else
                        {
                            m_extenso = fNumExtenso(Int64.Parse(m_subs)) + " " + v_cifra[x - 1, 1] + " " + m_extenso;
                        }
                    }

                }
            }
            return m_extenso.Trim();
        }
        static string fNumExtenso(decimal p_valor)//função auxiliar a fExtenso
        {
            string[,] v_cifra = new string[6, 2];
            Decimal nValor = Math.Abs(p_valor);
            string m_str = String.Format("{0:0000000000000000.00}", p_valor);
            v_cifra[0, 0] = "TRILHAO";
            v_cifra[0, 1] = "TRILHOES";
            v_cifra[1, 0] = "BILHAO";
            v_cifra[1, 1] = "BILHOES";
            v_cifra[2, 0] = "MILHAO";
            v_cifra[2, 1] = "MILHOES";
            v_cifra[3, 0] = "MIL";
            v_cifra[3, 1] = "MIL";
            v_cifra[4, 0] = "";
            v_cifra[4, 1] = "";
            v_cifra[5, 0] = "";
            string m_extenso = "";
            string m_subs = "";
            int m_centavos = int.Parse(m_str.Substring(m_str.Length - 2, 2));//  Substr(m_str,17))
            if (p_valor > 0)
            {
                if (m_centavos > 0)
                {
                    if (m_centavos == 1)
                    {
                        m_extenso = fValorPorExtenso(m_centavos) + v_cifra[5, 0];
                    }
                    else
                    {
                        m_extenso = fValorPorExtenso(m_centavos) + v_cifra[5, 1];
                    }
                }
                if ((p_valor) > 0)
                {
                    m_extenso = ((m_centavos > 0) ? " E " : "") + m_extenso;
                }

                int cont = 0;
                for (int x = 5; x > 0; x--)
                {
                    m_subs = m_str.Substring((x * 3) - 2, 3).ToString();
                    if (int.Parse(m_subs) > 0)
                    {
                        cont++;
                        if (int.Parse(m_subs) == 1)
                        {
                            m_extenso = fValorPorExtenso(int.Parse(m_subs)) + v_cifra[x, 0] + ((m_extenso == "") ? " " : " E ") + m_extenso;
                        }
                        else
                        {
                            string m_extensoAux = fValorPorExtenso(int.Parse(m_subs));
                            m_extenso = m_extensoAux + v_cifra[x, 1];
                        }
                    }

                }
            }
            return m_extenso.ToString().Trim();
        }
        static string fValorPorExtenso(decimal dValor)//função auxiliar a fExtenso
        {
            int[] v_val = new int[3];
            string[] v_cent = new string[9];
            string[] v_vint = new string[9];
            string[] v_dez = new string[9];
            string[] v_unit = new string[9];
            string sValor = String.Format("{0:000}", dValor);
            v_val[0] = int.Parse(sValor.ToString().Substring(0, 1));
            v_val[1] = int.Parse(sValor.ToString().Substring(1, 1));
            v_val[2] = int.Parse(sValor.ToString().Substring(2, 1));
            string m_ext = "";
            v_cent[0] = "CENTO";
            v_cent[1] = "DUZENTOS";
            v_cent[2] = "TREZENTOS";
            v_cent[3] = "QUATROCENTOS";
            v_cent[4] = "QUINHENTOS";
            v_cent[5] = "SEISCENTOS";
            v_cent[6] = "SETECENTOS";
            v_cent[7] = "OITOCENTOS";
            v_cent[8] = "NOVECENTOS";

            v_vint[0] = "ONZE";
            v_vint[1] = "DOZE";
            v_vint[2] = "TREZE";
            v_vint[3] = "QUATORZE";
            v_vint[4] = "QUINZE";
            v_vint[5] = "DEZESSEIS";
            v_vint[6] = "DEZESSETE";
            v_vint[7] = "DEZOITO";
            v_vint[8] = "DEZENOVE";

            v_dez[0] = "DEZ";
            v_dez[1] = "VINTE";
            v_dez[2] = "TRINTA";
            v_dez[3] = "QUARENTA";
            v_dez[4] = "CINQUENTA";
            v_dez[5] = "SESSENTA";
            v_dez[6] = "SETENTA";
            v_dez[7] = "OITENTA";
            v_dez[8] = "NOVENTA";

            v_unit[0] = "UM";
            v_unit[1] = "DOIS";
            v_unit[2] = "TRES";
            v_unit[3] = "QUATRO";
            v_unit[4] = "CINCO";
            v_unit[5] = "SEIS";
            v_unit[6] = "SETE";
            v_unit[7] = "OITO";
            v_unit[8] = "NOVE";
            int nAux;

            if (dValor > 0)
            {
                if (dValor == 100)
                {
                    m_ext = "CEM";
                }
                else
                {
                    if (v_val[0] > 0)
                    {
                        nAux = v_val[0];
                        if ((v_val[1] + v_val[2]) > 0)
                        {
                            m_ext = v_cent[nAux - 1] + " E ";
                        }
                        else
                        {
                            m_ext = v_cent[nAux - 1];
                        }
                    }

                    if ((v_val[1] == 1) && (v_val[2] > 0))
                    {
                        nAux = v_val[2];
                        m_ext = m_ext + " " + v_vint[nAux - 1] + " ";
                    }
                    else
                    {
                        if (v_val[1] > 0)
                        {
                            nAux = v_val[1];
                            if (v_val[2] > 0)
                            {
                                m_ext = m_ext + " " + v_dez[nAux - 1] + " E ";
                            }
                            else
                            {
                                m_ext = m_ext + " " + v_dez[nAux - 1];
                            }
                        }
                        nAux = v_val[2];
                        if (nAux == 1 && v_val[0] == 0 && v_val[1] == 0)
                        {
                            m_ext = "UM";
                        }
                        else
                        {
                            if (nAux > 0)
                            {
                                nAux--;
                            }

                            if (v_val[2] > 0)
                            {
                                m_ext = m_ext + v_unit[nAux];
                            }
                        }
                    }
                }
            }
            m_ext = m_ext + " ";
            return m_ext;
        }
    }
    #endregion >>>>>----- Funcões adicionais de Decimal -----<<<<<



    #region >>>>>----- Funcões adicionais de Date -----<<<<<
    public static class DateExtensions
    {
        /// <summary>
        /// Retorna o primeiro dia do mês, para a data informada
        /// </summary>
        public static DateTime FirstDayMonth(this DateTime d)
        {
            return new DateTime(d.Year, d.Month, 1);
        }

        /// <summary>
        /// Retorna o último dia do mês, para a data informada
        /// </summary>
        public static DateTime LastDayMonth(this DateTime d)
        {
            return new DateTime(d.Year, d.Month, DateTime.DaysInMonth(d.Year, d.Month));
        }

        /// <summary>
        /// Retorna o primeiro dia do ano, para a data informada
        /// </summary>
        public static DateTime FirstDayYear(this DateTime d)
        {
            return new DateTime(d.Year, 1, 1);
        }

        /// <summary>
        /// Retorna o último dia do ano, para a data informada
        /// </summary>
        public static DateTime LastDayYear(this DateTime d)
        {
            return new DateTime(d.Year, 12, DateTime.DaysInMonth(d.Year, 12));
        }

        /// <summary>
        /// Retorna o nome do dia da semana por extenso
        /// </summary>
        public static string fDiadaSemana(this DateTime dData)
        {
            string sDia = "";
            if (DateTime.TryParse(dData.ToString(), out DateTime dataValida))  //verifica se a data é válida (ex: 30/02/2012, nao executa)
            {
                int dow = ((int)dData.DayOfWeek); //dow = Day Of Week
                switch (dow)
                {
                    case 0: sDia = "Domingo"; break;
                    case 1: sDia = "Segunda-Feira"; break;
                    case 2: sDia = "Terça-Feira"; break;
                    case 3: sDia = "Quarta-Feira"; break;
                    case 4: sDia = "Quinta-Feira"; break;
                    case 5: sDia = "Sexta-Feira"; break;
                    case 6: sDia = "Sábado"; break;
                }
            }
            return sDia;
        }

        /// <summary>
        /// Retorna o mês por extenso ex: Outubro
        /// </summary>
        public static string fMesPorExtenso(this DateTime dData)
        {
            if (DateTime.TryParse(dData.ToString(), out DateTime dataValida))
            {
                switch (dData.Month)
                {
                    case 1: return "Janeiro";
                    case 2: return "Fevereiro";
                    case 3: return "Março";
                    case 4: return "Abril";
                    case 5: return "Maio";
                    case 6: return "Junho";
                    case 7: return "Julho";
                    case 8: return "Agosto";
                    case 9: return "Setembro";
                    case 10: return "Outubro";
                    case 11: return "Novembro";
                    case 12: return "Dezembro";
                }
            }
            return "";
        }

        /// <summary>
        /// Retorna uma data no formato dd/MM/yy
        /// </summary>
        public static string ToShortDateYearString(this DateTime dData)
        {
            string cData = dData.ToString("dd/MM/yy");
            return cData;
        }

        /// <summary>
        /// Retorna uma data no formato ddMMyyyy
        /// </summary>
        public static string fData_formato_DDMMAAAA(this DateTime dData)
        {
            string cData = dData.ToString("ddMMyyyy");
            return cData;
        }

        /// <summary>
        /// Retorna uma data no formato dd-MM-yyyy
        /// </summary>
        public static string fData_formato_DD_MM_AAAA(this DateTime dData)
        {
            string cData = dData.ToString("dd-MM-yyyy");
            return cData;
        }
        /// <summary>
        /// Retorna uma data no formato yyyyMMdd
        /// </summary>
        public static string fData_formato_AAAAMMDD(this DateTime dData)
        {
            string cData = dData.ToString("yyyyMMdd");
            return cData;
        }
    }
    #endregion >>>>>----- Funcões adicionais de Date -----<<<<<

    #region >>>>>----- Funcões adicionais de Enum -----<<<<<
    public static class EnumExtensions
    {
        /// <summary>
        /// Recebe o Nome do Enum e retorna sua descrição
        /// </summary>
        /// <param name="enumValue">Nome do Enum</param>
        /// <returns>Retorna a descrição do Enum</returns>
        public static string Enum_GetDescriprion<T>(this T enumValue)
        {
            try { return (enumValue is Enum) ? GetDescriprion_Enum(enumValue) : GetDescriprion_Enum(enumValue.ToEnum<T>()); }
            catch { return ""; }
        }

        /// <summary>
        /// Recebe o Nome do Enum e retorna sua descrição
        /// </summary>
        /// <param name="oEnumValue">Nome do Enum</param>
        /// <returns>Retorna a descrição do Enum</returns>
        private static string GetDescriprion_Enum(object oEnumValue)
        {
            try
            {
                System.Reflection.FieldInfo fi = oEnumValue.GetType().GetField(oEnumValue.ToString());
                if (null != fi)
                {
                    object[] attrs = fi.GetCustomAttributes
                            (typeof(System.ComponentModel.DescriptionAttribute), true);
                    if (attrs != null && attrs.Length > 0)
                    {
                        return ((System.ComponentModel.DescriptionAttribute)attrs[0]).Description;
                    }
                }
                return string.Empty;
            }
            catch { return ""; }
        }

        /// <summary>
        /// Função que recebe o Nome do Enum em formato String e retorna sua descrição
        /// </summary>
        /// <typeparam name="T">Tipo do Enum a ser convertido</typeparam>
        /// <param name="sNomeEmum">Nome do Enum</param>
        /// <returns>Retorna a descrição do Enum ou Vazio se não encontrar o respectivo valor</returns>
        public static string Enum_GetDescription_For_Name<T>(this string sNomeEmum)
        {
            try { return GetDescriprion_Enum(sNomeEmum.ToEnum<T>()); }
            catch { return ""; }
        }

        /// <summary>
        /// Função que recebe o Valor do Enum em formato String e retorna sua descrição
        /// </summary>
        /// <typeparam name="T">Tipo do Enum a ser convertido</typeparam>
        /// <param name="sValueEnum">Valor a ser obtido a descrição</param>
        /// <returns>Retorna a descrição do Enum ou Vazio se não encontrar o respectivo valor</returns>
        public static string Enum_GetDescription_For_Value<T>(this string sValueEnum)
        {
            try
            {
                foreach (T item in Enum.GetValues(typeof(T)))
                {
                    if (item.ToChar().ToString() == sValueEnum)
                    {
                        return GetDescriprion_Enum(item.ToEnum<T>());
                    }
                }

                return "";
            }
            catch { return ""; }
        }

        /// <summary>
        /// Função que recebe o Valor do Enum em formato Int e retorna sua descrição
        /// </summary>
        /// <param name="iValueEnum">Valor a ser obtido a descrição</param>
        /// <returns>Retorna a descrição do Enum ou Vazio se não encontrar o respectivo valor</returns>
        public static string Enum_GetDescription_For_Value<T>(this short iValueEnum)
        {
            try
            {
                foreach (T item in Enum.GetValues(typeof(T)))
                {
                    if (item.ToInt16() == iValueEnum)
                    {
                        return GetDescriprion_Enum(item.ToEnum<T>());
                    }
                }

                return "";
            }
            catch { return ""; }
        }

        /// <summary>
        /// Função que recebe o Nome do Enum e retorna o valor contido em sua descrição (o Valor deve estar no início da descrição e separado pelo sinal de igual(=). Exemplo: 1 = DESCRIÇÃO)
        /// </summary>
        /// <param name="oValue">Nome do Enum</param>
        /// <returns>Retorna o valor através da descrição do Enum</returns>
        public static string Enum_GetDescription_Value<T>(this object oValue)
        {
            try
            {
                string sResult = GetDescriprion_Enum(oValue.ToEnum<T>());
                int nPos = sResult.IndexOf("=");
                return (nPos != -1) ? sResult.Left(nPos).Trim() : sResult.Trim();
            }
            catch { return ""; }
        }

        /// <summary>
        /// Função que recebe a descrição do Enum e retorna seu Item
        /// </summary>
        /// <param name="sDescricao">Descrição do Enum</param>
        /// <returns>Caso exista, retorna o Item do Enum referente a descrição informada em "sDescricao"</returns>
        public static T Enum_FromDescription<T>(this string sDescricao)
        {
            try
            {
                Type t = typeof(T);
                foreach (System.Reflection.FieldInfo fi in t.GetFields())
                {
                    object[] attrs = fi.GetCustomAttributes
                            (typeof(System.ComponentModel.DescriptionAttribute), true);
                    if (attrs != null && attrs.Length > 0)
                    {
                        foreach (System.ComponentModel.DescriptionAttribute attr in attrs)
                        {
                            if (attr.Description.Equals(sDescricao))
                            {
                                return (T)fi.GetValue(null);
                            }
                        }
                    }
                }
                return default(T);
            }
            catch { return default(T); }
        }

        /// <summary>
        /// Função que recebe a descrição do Enum e retorna sua Id
        /// </summary>
        /// <param name="sDescricao">Descrição do Enum</param>
        /// <returns>Caso exista, retorna o ID referente a descrição informada em "sDescricao"</returns>
        public static int Enum_FromDescription_ID<T>(this string sDescricao)
        {
            try
            {
                Type t = typeof(T);
                foreach (System.Reflection.FieldInfo fi in t.GetFields())
                {
                    object[] attrs = fi.GetCustomAttributes
                            (typeof(System.ComponentModel.DescriptionAttribute), true);
                    if (attrs != null && attrs.Length > 0)
                    {
                        foreach (System.ComponentModel.DescriptionAttribute attr in attrs)
                        {
                            if (attr.Description.Equals(sDescricao))
                            {
                                return (int)fi.GetValue(null);
                            }
                        }
                    }
                }
                return default(int);
            }
            catch { return 0; }
        }

        /// <summary>
        /// Recebe uma String e retorna o nome do Enum
        /// </summary>
        /// <typeparam name="T">Tipo do Enum a ser convertido</typeparam>
        /// <param name="oSource">String que deverá ser convertida para o Enum</param>
        /// <param name="bChar">Informa se o Valor do Enum está em formato Char</param>
        /// <returns>Retorna o Valor estiver no Emnum</returns>
        public static T ToEnum<T>(this object oSource, bool bChar = false)
        {
            if (bChar || (oSource.ToString().Length == 1 && Char.IsLetter(oSource.ToString(), 0)))
            {
                return (T)Enum.ToObject(typeof(T), oSource.ToChar());
            }
            else
            {
                return (T)Enum.Parse(typeof(T), oSource.ToString(), true);
            }
        }
        /// <summary>
        /// Recebe uma String e retorna o nome do Enum. Trata se o valor recebido é Nulo
        /// </summary>
        /// <typeparam name="T">Tipo do Enum a ser convertido</typeparam>
        /// <param name="oSource">String que deverá ser convertida para o Enum</param>
        /// <param name="bChar">Informa se o Valor do Enum está em formato Char</param>
        /// <returns>Retorna o Valor estiver no Emnum</returns>
        public static T? ToEnum_Null<T>(this object oSource, bool bChar = false) where T : struct
        {
            if (oSource.IsNullOrEmpty())
            {
                return null;
            }
            else
            {
                return oSource.ToEnum<T>(bChar);
            }
        }

        /// <summary>
        /// Recebe o Nome do Enum, converte para Char e retorna seu valor em String
        /// </summary>
        /// <param name="enumValue">Nome do Enum</param>
        /// <returns>Retorna o valor do Enum em String</returns>
        public static string GetValueChar_ToString(this Enum enumValue)
        {
            try
            {
                if (int.TryParse(enumValue.ToString(), out int i))
                {
                    return enumValue.ToString();
                }
                else
                {
                    return enumValue.GetValue<Char>().ToString();
                }
            }
            catch { return ""; }
        }

        /// <summary>
        /// Recebe o Nome do Enum, converte para Int e retorna seu valor em String
        /// </summary>
        /// <param name="enumValue">Nome do Enum</param>
        /// <param name="iQtdCaracter">Quantidade de Caracteres que a string deverá retornar. Caso seja menor irá preencher com zeros a esquerda do texto</param>
        /// <returns>Retorna o valor do Enum em String</returns>
        public static string GetValueInt_ToString(this Enum enumValue, int iQtdCaracter = -1)
        {
            try
            {
                string sReturn = enumValue.GetValue<int>().ToString();
                if (iQtdCaracter != -1)
                {
                    sReturn = sReturn.PadLeft(iQtdCaracter, '0');
                }

                return sReturn;
            }
            catch { return ""; }
        }

        /// <summary>
        /// Recebe o Nome do Enum, converte para o Tipo informado em <T> e retorna seu valor no Tipo informado em <T>
        /// </summary>
        /// <param name="enumValue">Nome do Enum</param>
        /// <returns>Retorna o valor do Enum no Tipo informado em <T></returns>
        public static T GetValue<T>(this Enum enumValue)
        {
            try { return (T)Convert.ChangeType(enumValue, typeof(T)); }
            catch { return default(T); }
        }



    }
    #endregion >>>>>----- Funcões adicionais de Enum -----<<<<<

    #region >>>>>----- Funcões adicionais de Image -----<<<<<
    public static class ImageExtensions
    {
        /// <summary>
        /// Converte a Imagem para Base64, para que seja possível salvar a String em base64 no banco de dados
        /// </summary>
        /// <param name="img">Imagem a ser transformada em Base64</param>
        /// <returns>String da imagem em Base64</returns>
        public static string ToBase64(this Image img)
        {
            string retorno = null;
            try
            {
                if (img != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms, ImageFormat.Png);
                        retorno = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception) { retorno = null; throw; }
            return retorno;
        }

        /// <summary>
        /// Converte a String em Base64 para Imagem, para que seja possível pegar a String em base64 no banco de dados
        /// </summary>
        /// <param name="StringBase64">String Base64 a ser transformado em Imagem</param>
        /// <param name="iResolucao">Resolução que a Imagem deve ter</param>
        /// <returns>Imagem gerada a partir do Base64</returns>
        public static Image ToImage(this object oStringBase64, int? iResolucao = null)
        {
            Image retorno = null;
            try
            {
                string StringBase64 = (oStringBase64 is string) ? oStringBase64.ToString() : null;
                if (!string.IsNullOrEmpty(StringBase64))
                {
                    //byte[] imageData = Convert.FromBase64String(StringBase64);
                    //retorno = BinarioParaImg(imageData);
                    using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(StringBase64)))
                    {
                        if (iResolucao == null)
                        {
                            retorno = Image.FromStream(ms);
                        }
                        else
                        {
                            retorno = Resize(Image.FromStream(ms), iResolucao.ToInt32());
                        }
                    }
                }
            }
            catch (Exception) { retorno = null; throw; }
            return retorno;
        }

        /// <summary>
        /// Ajusta o tamanho da imagem
        /// </summary>
        /// <param name="imgToResize">Imagem que será redimensionada</param>
        /// <param name="iSize">Tamanho da imagem em Altura e Largura</param>
        public static Image Resize(this Image imgToResize, int iSize)
        {
            return new Bitmap(imgToResize, new Size(iSize, iSize));
        }
        /// <summary>
        /// Ajusta o tamanho da imagem
        /// </summary>
        /// <param name="imgToResize">Imagem que será redimensionada</param>
        /// <param name="iWidth">Largura da imagem</param>
        /// <param name="iHeight">Altura da imagem</param>
        public static Image Resize(this Image imgToResize, int iWidth, int iHeight)
        {
            return new Bitmap(imgToResize, new Size(iWidth, iHeight));
        }
        /// <summary>
        /// Redimensiona o tamanho da imagem automaticamente, para não estourar (Largura/Altura Padrão 100)
        /// </summary>
        /// <param name="img">Imagem a ser redimensionada</param>
        /// <param name="iMaxWith">Largura Máxima da imagem (Padrão 100)</param>
        /// <param name="iMaxHeight">Altura Máxima da imagem (Padrão 100)</param>
        public static Image ResizeAuto(this Image imgToResize, int? iMaxWith = null, int? iMaxHeight = null)
        {
            iMaxWith = (iMaxWith == null) ? 100 : iMaxWith;
            iMaxHeight = (iMaxHeight == null) ? 100 : iMaxHeight;
            float fPercWidth = 1; //100% - Percentual da largura
            float fPercHeight = 1; //100% - Percentual da altura
            if (imgToResize.Width > iMaxWith || imgToResize.Height > iMaxHeight)
            {
                fPercWidth = (iMaxWith / imgToResize.Width.ToFloat()).ToFloat();
                fPercHeight = (iMaxHeight / imgToResize.Height.ToFloat()).ToFloat();
            }
            int iLargura = imgToResize.Width; //largura original da imagem origem
            int iAltura = imgToResize.Height; //altura original da imagem origem

            return imgToResize.Resize((iLargura * fPercWidth).ToInt32(), (iAltura * fPercHeight).ToInt32());
        }

        /// <summary>
        /// Altera a imagem para Preto e Branco (opacidade menor que a original)
        /// </summary>
        /// <param name="img">Imagem que deverá ser alterada</param>
        /// <returns>Retorna a imagem modificada em Preto e Branco</returns>
        public static Image SetDisabled(this Image img)
        {
            Image disabledImage = new Bitmap(img.Width, img.Height);
            try
            {
                using (Graphics G = Graphics.FromImage(disabledImage))
                {
                    using (ImageAttributes IA = new ImageAttributes())
                    {
                        ColorMatrix CM = new ColorMatrix();
                        CM.Matrix33 = .5f;
                        IA.SetColorMatrix(CM);
                        Rectangle R = new Rectangle(0, 0, img.Width, img.Height);
                        G.DrawImage(img, R, R.X, R.Y, R.Width, R.Height, GraphicsUnit.Pixel, IA);
                    }
                }
            }
            catch (Exception) { disabledImage = null; throw; }
            return disabledImage;
        }

        /// <summary>
        /// Altera a transparência de uma imagem
        /// </summary>
        /// <param name="image">Imagem que deverá ser alterada</param>
        /// <param name="fOpacity">% de Opacidade (Transparência)</param>
        /// <returns>Retorna a imagem modificada com a transparência</returns>
        public static Image SetOpacity(this Image image, float fOpacity)
        {
            //create a Bitmap the size of the image provided  
            Bitmap bmp = new Bitmap(image.Width, image.Height);
            try
            {
                if (fOpacity >= 1)
                {
                    fOpacity = fOpacity / 100;
                }
                //create a graphics object from the image  
                using (Graphics gfx = Graphics.FromImage(bmp))
                {
                    //create a color matrix object  
                    ColorMatrix matrix = new ColorMatrix();
                    //set the opacity  
                    matrix.Matrix33 = fOpacity;
                    //create image attributes  
                    ImageAttributes attributes = new ImageAttributes();
                    //set the color(opacity) of the image  
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                    //now draw the image  
                    gfx.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
            }
            catch (Exception) { bmp = null; throw; }
            return bmp;
        }

        /// <summary>
        /// Efetua a comparação entre duas imagens, para verificar se são iguais
        /// </summary>
        /// <param name="firstImage">Primeira imagem</param>
        /// <param name="secondImage">Segunda imagem</param>
        /// <returns>TRUE = Imagens iguais / FALSE = Imagens diferentes</returns>
        public static bool Compare(this Image firstImage, Image secondImage)
        {
            try
            {
                if (firstImage == null && secondImage == null)
                {
                    return true;
                }

                if ((firstImage == null && secondImage != null) || (secondImage == null && firstImage != null))
                {
                    return false;
                }

                MemoryStream ms = new MemoryStream();
                firstImage.Save(ms, ImageFormat.Png);
                String firstBitmap = Convert.ToBase64String(ms.ToArray());
                ms.Position = 0;

                secondImage.Save(ms, ImageFormat.Png);
                String secondBitmap = Convert.ToBase64String(ms.ToArray());

                return firstBitmap.Equals(secondBitmap);
            }
            catch { throw; }
        }

        public static Image RoundCorners(this Image image, int cornerRadius)
        {
            if (image != null)
            {
                cornerRadius *= 2;
                Bitmap roundedImage = new Bitmap(image.Width, image.Height);
                GraphicsPath gp = new GraphicsPath();
                gp.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90);
                gp.AddArc(0 + roundedImage.Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90);
                gp.AddArc(0 + roundedImage.Width - cornerRadius, 0 + roundedImage.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
                gp.AddArc(0, 0 + roundedImage.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
                using (Graphics g = Graphics.FromImage(roundedImage))
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.SetClip(gp);
                    g.DrawImage(image, Point.Empty);
                }
                return roundedImage;
            }
            return null;
        }

        /// <summary>
        /// Salva a imagem na área de trabalho do usuário
        /// </summary>
        /// <param name="foto">Imagem a ser salva</param>
        /// <param name="sNomeImagem">Nome que a imagem deverá receber ao ser salva</param>
        public static void SalvarImagemNaAreaDeTrabalho(this Image foto, string sNomeImagem)
        {
            try
            {
                if (sNomeImagem.IsNullOrEmpty())
                {
                    sNomeImagem = DateTime.Now.Second.ToString();
                }

                foto.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), sNomeImagem + ".png"), ImageFormat.Png);
                //Mensagem.Alerta("A Imagem foi exportada para área de trabalho.");
            }
            catch { throw; }
        }

        /// <summary>
        /// Rotina para conversao de Imagem em Código Binario
        /// </summary>
        /// <param name="img">Imagem a ser convertida</param>
        /// <returns>Retorna a imagem em código binário</returns>
        public static byte[] ImgParaBinario(this Image img)
        {
            byte[] retorno = null;
            try
            {
                byte[] bBinario = new byte[0];
                img = img.ResizeAuto();
                using (MemoryStream mMemoria = new MemoryStream())
                {
                    img.Save(mMemoria, ImageFormat.Png);
                    mMemoria.Close();
                    bBinario = mMemoria.ToArray();
                    retorno = bBinario;
                }
            }
            catch (Exception) { retorno = null; throw; }
            return retorno;
        }

        /// <summary>
        /// Rotina para conversao de Código Binario em Imagem
        /// </summary>
        /// <param name="bBinario">Código Binario a ser convertido em imagem</param>
        /// <returns>Retorna o código binário em formato de imagem</returns>
        public static Image BinarioParaImg(this byte[] bBinario)
        {
            Image retorno = null;
            try
            {
                if (bBinario.Length == 0 || bBinario == null)
                {
                    retorno = null;
                }
                else
                {
                    retorno = (Image.FromStream(new MemoryStream(bBinario)));
                }
            }
            catch (Exception) { retorno = null; throw; }
            return retorno;
        }
    }
    #endregion >>>>>----- Funcões adicionais de Image -----<<<<<

    #region >>>>>----- Funcões adicionais de DataTable -----<<<<<
    public static class DataTableExtensions
    {
        /// <summary>
        /// Altera o DataType de uma coluna em um DataTable
        /// </summary>
        /// <param name="columnname">Nome da Coluna que será alterado o DataType</param>
        /// <param name="newtype">Novo DataType</param>
        /// <returns>TRUE = Se a alteração do DataType doi concluída / FALSE = Alteração não concluída</returns>
        public static bool ChangeColumnDataType(this DataTable table, string columnname, Type newtype)
        {
            if (table.Columns.Contains(columnname) == false)
            {
                return false;
            }

            DataColumn column = table.Columns[columnname];
            if (column.DataType == newtype)
            {
                return true;
            }

            try
            {
                DataColumn newcolumn = new DataColumn("temporary", newtype);
                table.Columns.Add(newcolumn);
                foreach (DataRow row in table.Rows)
                {
                    if (newtype == typeof(Date) && row[columnname].GetType() == typeof(DateTime))
                    {
                        row["temporary"] = Date.Parse(row[columnname].ToString());
                    }
                    else
                    {
                        row["temporary"] = Convert.ChangeType(row[columnname], newtype);
                    }
                }
                table.Columns.Remove(columnname);
                newcolumn.ColumnName = columnname;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Converte um DataTable em List da classe informada em T <T>Classe</T>
        /// </summary>
        /// <typeparam name="T">Classe para a qual será converido o DataTable</typeparam>
        /// <param name="dt">DataTable a converter</param>
        public static List<T> DataTable_ToList<T>(this DataTable dt)
        {
            List<T> data = new List<T>();
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    data.Add(row.GetItem<T>());
                }
            }
            catch { throw; }
            return data;
        }
        private static T GetItem<T>(this DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    string sDisplayName = pro.CustomAttributes?.ElementAtOrDefault(0)?.ConstructorArguments?.ElementAtOrDefault(0).Value?.ToString() ?? "";
                    if (pro.Name == column.ColumnName || sDisplayName == column.ColumnName)
                    {
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return obj;
        }
    }
    #endregion >>>>>----- Funcões adicionais de DataTable -----<<<<<

    #endregion >>>>>----- Funcões adicionais de Control/String/Object/Integer/Decimal/Date/Enum/Image/DataTable -----<<<<<

    #region >>>>>----- Conversão de texto do singular para o plural -----<<<<<

    public static class Plurals
    {
        /// <summary>
        /// Dicionário de palavras no singular e plural
        /// </summary>
        static Dictionary<string, string> _pluralTable = new Dictionary<string, string>
        {
            {"cliente", "clientes"},
            {"produto", "produtos"}
        };

        public static string Plural_Quantidade(string palavra, int count)
        {
            if (count == 0 || count == 1)
            {
                return palavra;
            }

            return _pluralTable[palavra];
        }

        /// <summary>
        /// Passa uma palavra no singular e retorna ela no plural
        /// </summary>
        /// <param name="palavra">Palavra singular a ser convertida para o Plural</param>
        /// <returns>Palavra Convertida para o Plural</returns>
        public static string Plural(string palavra)
        { return _pluralTable[palavra]; }

        public static string PluralPhrase(string word, int count)
        {
            // Returns phrase of complete pluralized phrase.
            // .. Such as "3 files".
            string properPlural = Plural_Quantidade(word, count);
            return count.ToString() + " " + properPlural;
        }
    }

    #endregion >>>>>----- Conversão de texto do singular para o plural -----<<<<<

    #region >>>>>----- Inclusão de Fontes no sistema (Fontes Externas) -----<<<<<

    /// <summary>
    /// Função para incluir Fontes de letras que serão utilizadas no sistema.
    /// Usando essa função não é necessário enviar o arquivo de fonte externamente com o executável
    /// </summary>
    public static class Fonts
    {
        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);
        private static PrivateFontCollection pfc { get; set; }

        static Fonts() { if (pfc == null) { pfc = new PrivateFontCollection(); } }

        /// <summary>
        /// Adiciona o arquivo na matriz pfc (PrivateFontCollection)
        /// </summary>
        /// <param name="fontResource"></param>
        public static void AddMemoryFont(byte[] fontResource)
        {
            IntPtr p;
            uint c = 0;

            p = Marshal.AllocCoTaskMem(fontResource.Length);
            Marshal.Copy(fontResource, 0, p, fontResource.Length);
            AddFontMemResourceEx(p, (uint)fontResource.Length, IntPtr.Zero, ref c);
            pfc.AddMemoryFont(p, fontResource.Length);
            Marshal.FreeCoTaskMem(p);

            p = IntPtr.Zero;
        }

        /// <summary>
        /// Captura o arquivo de fonte que será utilizado na matriz pfc (PrivateFontCollection)
        /// </summary>
        /// <param name="fontIndex">Indice da matriz pfc (PrivateFontCollection), onde a fonte desejada se encontra</param>
        /// <param name="fontSize">Tamanho da fonte (Padrão 20)</param>
        /// <param name="fontStyle">Stilo da fonte (Negrito | Itálico, etc - Padrão Regular)</param>
        /// <returns>Retorna a fonte escolhida e no formato desejado</returns>
        public static Font GetFont(int fontIndex, float fontSize = 20, FontStyle fontStyle = FontStyle.Regular)
        {
            return new Font(pfc.Families[fontIndex], fontSize, fontStyle);
        }

        /// <summary>
        /// Captura o arquivo de fonte que será utilizado, passando o nome do arquivo (nome idêntico ao que está no Resources do projeto)
        /// </summary>
        /// <param name="sFontResource">Nome do arquivo de fonte (Resources do projeto)</param>
        /// <param name="fontSize">Tamanho da fonte (Padrão 20)</param>
        /// <param name="fontStyle">Stilo da fonte (Negrito | Itálico, etc - Padrão Regular)</param>
        /// <returns>Retorna a fonte escolhida e no formato desejado</returns>
        public static Font GetFont(string sFontResource, float fontSize = 20, FontStyle fontStyle = FontStyle.Regular)
        {
            int iPos = -1;
            for (int i = 0; i < pfc.Families.Length; i++)
            {
                if (pfc.Families[i].Name == sFontResource)
                {
                    iPos = i;
                    break;
                }
            }
            if (iPos != -1)
            {
                return GetFont(iPos, fontSize, fontStyle);
            }
            else
            {
                return new Font(new FontFamily("Microsoft Sans Serif"), fontSize, fontStyle);
            }
        }

        /// <summary>
        /// Método útil para passar uma string hexadecimal de 4 dígitos para retornar o caractere unicode
        /// Algumas fontes como FontAwesome exigem essa conversão para acessar os caracteres
        /// </summary>
        /// <param name="hex">String em formato hexadecimal</param>
        public static string UnicodeToChar(string hex)
        {
            int code = int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            string unicodeString = char.ConvertFromUtf32(code);
            return unicodeString;
        }
    }

    #endregion >>>>>----- Inclusão de Fontes no sistema (Fontes Externas) -----<<<<<

    #region >>>>>----- Informações sobre o Sistema Operacional -----<<<<<

    /// <summary>
    /// Fornece informações detalhadas sobre o sistema operacional.
    /// </summary>
    public static class OSInfo
    {
        public static string Versao()
        {
            string sVersao = "Informações do Sistema Operacional:";
            sVersao += "\n----------------------------------------";
            sVersao += "\nNome = " + OSInfo.Name;
            sVersao += "\nEdição = " + OSInfo.Edition;
            sVersao += "\nService Pack = " + OSInfo.ServicePack;
            sVersao += "\nVersão = " + OSInfo.VersionString;
            sVersao += "\nBits = " + OSInfo.Bits;
            return sVersao;
        }

        #region BITS
        /// <summary>
        /// Determina se o Windows é 32 ou 64-bits.
        /// </summary>
        public static int Bits { get { return IntPtr.Size * 8; } }
        #endregion BITS

        #region EDITION
        private static string s_Edition;
        /// <summary>
        ///  Obtém a edição do sistema operacional em execução neste computador.
        /// </summary>
        public static string Edition
        {
            get
            {
                if (s_Edition != null)
                {
                    return s_Edition;  //***** RETURN *****//
                }

                string edition = String.Empty;

                OperatingSystem osVersion = Environment.OSVersion;
                OSVERSIONINFOEX osVersionInfo = new OSVERSIONINFOEX();
                osVersionInfo.dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX));

                if (GetVersionEx(ref osVersionInfo))
                {
                    int majorVersion = osVersion.Version.Major;
                    int minorVersion = osVersion.Version.Minor;
                    byte productType = osVersionInfo.wProductType;
                    short suiteMask = osVersionInfo.wSuiteMask;

                    #region VERSION 4
                    if (majorVersion == 4)
                    {
                        if (productType == VER_NT_WORKSTATION)
                        {
                            // Windows NT 4.0 Workstation
                            edition = "Workstation";
                        }
                        else if (productType == VER_NT_SERVER)
                        {
                            if ((suiteMask & VER_SUITE_ENTERPRISE) != 0)
                            {
                                // Windows NT 4.0 Server Enterprise
                                edition = "Enterprise Server";
                            }
                            else
                            {
                                // Windows NT 4.0 Server
                                edition = "Standard Server";
                            }
                        }
                    }
                    #endregion VERSION 4

                    #region VERSION 5
                    else if (majorVersion == 5)
                    {
                        if (productType == VER_NT_WORKSTATION)
                        {
                            if ((suiteMask & VER_SUITE_PERSONAL) != 0)
                            {
                                // Windows XP Home Edition
                                edition = "Home";
                            }
                            else
                            {
                                // Windows XP / Windows 2000 Professional
                                edition = "Professional";
                            }
                        }
                        else if (productType == VER_NT_SERVER)
                        {
                            if (minorVersion == 0)
                            {
                                if ((suiteMask & VER_SUITE_DATACENTER) != 0)
                                {
                                    // Windows 2000 Datacenter Server
                                    edition = "Datacenter Server";
                                }
                                else if ((suiteMask & VER_SUITE_ENTERPRISE) != 0)
                                {
                                    // Windows 2000 Advanced Server
                                    edition = "Advanced Server";
                                }
                                else
                                {
                                    // Windows 2000 Server
                                    edition = "Server";
                                }
                            }
                            else
                            {
                                if ((suiteMask & VER_SUITE_DATACENTER) != 0)
                                {
                                    // Windows Server 2003 Datacenter Edition
                                    edition = "Datacenter";
                                }
                                else if ((suiteMask & VER_SUITE_ENTERPRISE) != 0)
                                {
                                    // Windows Server 2003 Enterprise Edition
                                    edition = "Enterprise";
                                }
                                else if ((suiteMask & VER_SUITE_BLADE) != 0)
                                {
                                    // Windows Server 2003 Web Edition
                                    edition = "Web Edition";
                                }
                                else
                                {
                                    // Windows Server 2003 Standard Edition
                                    edition = "Standard";
                                }
                            }
                        }
                    }
                    #endregion VERSION 5

                    #region VERSION 6
                    else if (majorVersion == 6)
                    {
                        int ed;
                        if (GetProductInfo(majorVersion, minorVersion,
                            osVersionInfo.wServicePackMajor, osVersionInfo.wServicePackMinor,
                            out ed))
                        {
                            switch (ed)
                            {
                                case PRODUCT_BUSINESS:
                                    edition = "Business";
                                    break;
                                case PRODUCT_BUSINESS_N:
                                    edition = "Business N";
                                    break;
                                case PRODUCT_CLUSTER_SERVER:
                                    edition = "HPC Edition";
                                    break;
                                case PRODUCT_DATACENTER_SERVER:
                                    edition = "Datacenter Server";
                                    break;
                                case PRODUCT_DATACENTER_SERVER_CORE:
                                    edition = "Datacenter Server (core installation)";
                                    break;
                                case PRODUCT_ENTERPRISE:
                                    edition = "Enterprise";
                                    break;
                                case PRODUCT_ENTERPRISE_N:
                                    edition = "Enterprise N";
                                    break;
                                case PRODUCT_ENTERPRISE_SERVER:
                                    edition = "Enterprise Server";
                                    break;
                                case PRODUCT_ENTERPRISE_SERVER_CORE:
                                    edition = "Enterprise Server (core installation)";
                                    break;
                                case PRODUCT_ENTERPRISE_SERVER_CORE_V:
                                    edition = "Enterprise Server without Hyper-V (core installation)";
                                    break;
                                case PRODUCT_ENTERPRISE_SERVER_IA64:
                                    edition = "Enterprise Server for Itanium-based Systems";
                                    break;
                                case PRODUCT_ENTERPRISE_SERVER_V:
                                    edition = "Enterprise Server without Hyper-V";
                                    break;
                                case PRODUCT_HOME_BASIC:
                                    edition = "Home Basic";
                                    break;
                                case PRODUCT_HOME_BASIC_N:
                                    edition = "Home Basic N";
                                    break;
                                case PRODUCT_HOME_PREMIUM:
                                    edition = "Home Premium";
                                    break;
                                case PRODUCT_HOME_PREMIUM_N:
                                    edition = "Home Premium N";
                                    break;
                                case PRODUCT_HYPERV:
                                    edition = "Microsoft Hyper-V Server";
                                    break;
                                case PRODUCT_MEDIUMBUSINESS_SERVER_MANAGEMENT:
                                    edition = "Windows Essential Business Management Server";
                                    break;
                                case PRODUCT_MEDIUMBUSINESS_SERVER_MESSAGING:
                                    edition = "Windows Essential Business Messaging Server";
                                    break;
                                case PRODUCT_MEDIUMBUSINESS_SERVER_SECURITY:
                                    edition = "Windows Essential Business Security Server";
                                    break;
                                case PRODUCT_SERVER_FOR_SMALLBUSINESS:
                                    edition = "Windows Essential Server Solutions";
                                    break;
                                case PRODUCT_SERVER_FOR_SMALLBUSINESS_V:
                                    edition = "Windows Essential Server Solutions without Hyper-V";
                                    break;
                                case PRODUCT_SMALLBUSINESS_SERVER:
                                    edition = "Windows Small Business Server";
                                    break;
                                case PRODUCT_STANDARD_SERVER:
                                    edition = "Standard Server";
                                    break;
                                case PRODUCT_STANDARD_SERVER_CORE:
                                    edition = "Standard Server (core installation)";
                                    break;
                                case PRODUCT_STANDARD_SERVER_CORE_V:
                                    edition = "Standard Server without Hyper-V (core installation)";
                                    break;
                                case PRODUCT_STANDARD_SERVER_V:
                                    edition = "Standard Server without Hyper-V";
                                    break;
                                case PRODUCT_STARTER:
                                    edition = "Starter";
                                    break;
                                case PRODUCT_STORAGE_ENTERPRISE_SERVER:
                                    edition = "Enterprise Storage Server";
                                    break;
                                case PRODUCT_STORAGE_EXPRESS_SERVER:
                                    edition = "Express Storage Server";
                                    break;
                                case PRODUCT_STORAGE_STANDARD_SERVER:
                                    edition = "Standard Storage Server";
                                    break;
                                case PRODUCT_STORAGE_WORKGROUP_SERVER:
                                    edition = "Workgroup Storage Server";
                                    break;
                                case PRODUCT_UNDEFINED:
                                    edition = "Unknown product";
                                    break;
                                case PRODUCT_ULTIMATE:
                                    edition = "Ultimate";
                                    break;
                                case PRODUCT_ULTIMATE_N:
                                    edition = "Ultimate N";
                                    break;
                                case PRODUCT_WEB_SERVER:
                                    edition = "Web Server";
                                    break;
                                case PRODUCT_WEB_SERVER_CORE:
                                    edition = "Web Server (core installation)";
                                    break;
                            }
                        }
                    }
                    #endregion VERSION 6
                    else
                    {
                        string subKey = @"SOFTWARE\Wow6432Node\Microsoft\Windows NT\CurrentVersion";
                        Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine;
                        Microsoft.Win32.RegistryKey skey = key.OpenSubKey(subKey);
                        edition = skey.GetValue("ProductName").ToString().Replace(Name, "");
                    }
                }

                s_Edition = edition;
                return edition;
            }
        }
        #endregion EDITION

        #region NAME
        private static string s_Name;
        /// <summary>
        /// Obtém o nome do sistema operacional em execução neste computador
        /// </summary>
        public static string Name
        {
            get
            {
                if (s_Name != null)
                {
                    return s_Name;  //***** RETURN *****//
                }

                string name = "unknown";

                OperatingSystem osVersion = Environment.OSVersion;
                OSVERSIONINFOEX osVersionInfo = new OSVERSIONINFOEX();
                osVersionInfo.dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX));

                if (GetVersionEx(ref osVersionInfo))
                {
                    int majorVersion = osVersion.Version.Major;
                    int minorVersion = osVersion.Version.Minor;

                    switch (osVersion.Platform)
                    {
                        case PlatformID.Win32Windows:
                            {
                                if (majorVersion == 4)
                                {
                                    string csdVersion = osVersionInfo.szCSDVersion;
                                    switch (minorVersion)
                                    {
                                        case 0:
                                            if (csdVersion == "B" || csdVersion == "C")
                                            {
                                                name = "Windows 95 OSR2";
                                            }
                                            else
                                            {
                                                name = "Windows 95";
                                            }

                                            break;
                                        case 10:
                                            if (csdVersion == "A")
                                            {
                                                name = "Windows 98 Second Edition";
                                            }
                                            else
                                            {
                                                name = "Windows 98";
                                            }

                                            break;
                                        case 90:
                                            name = "Windows Me";
                                            break;
                                    }
                                }
                                break;
                            }

                        case PlatformID.Win32NT:
                            {
                                byte productType = osVersionInfo.wProductType;

                                switch (majorVersion)
                                {
                                    case 3:
                                        name = "Windows NT 3.51";
                                        break;
                                    case 4:
                                        switch (productType)
                                        {
                                            case 1:
                                                name = "Windows NT 4.0";
                                                break;
                                            case 3:
                                                name = "Windows NT 4.0 Server";
                                                break;
                                        }
                                        break;
                                    case 5:
                                        switch (minorVersion)
                                        {
                                            case 0:
                                                name = "Windows 2000";
                                                break;
                                            case 1:
                                                name = "Windows XP";
                                                break;
                                            case 2:
                                                if (productType == VER_NT_WORKSTATION)
                                                {
                                                    name = "Windows Windows XP Professional x64";
                                                }
                                                else
                                                {
                                                    name = "Windows Server 2003";
                                                }

                                                break;
                                        }
                                        break;
                                    case 6:
                                        switch (productType)
                                        {
                                            case 0:
                                                if (productType == VER_NT_WORKSTATION)
                                                {
                                                    name = "Windows Vista";
                                                }
                                                else
                                                {
                                                    name = "Windows Server 2008";
                                                }

                                                break;
                                            case 1:
                                                if (productType == VER_NT_WORKSTATION)
                                                {
                                                    name = "Windows 7";
                                                }
                                                else
                                                {
                                                    name = "Windows Server 2008 R2";
                                                }

                                                break;
                                            case 2:
                                                if (productType == VER_NT_WORKSTATION)
                                                {
                                                    name = "Windows 8";
                                                }
                                                else
                                                {
                                                    name = "Windows Server 2012";
                                                }

                                                break;
                                            case 3:
                                                if (productType == VER_NT_WORKSTATION)
                                                {
                                                    name = "Windows 8.1";
                                                }
                                                else
                                                {
                                                    name = "Windows Server 2012 R2";
                                                }

                                                break;
                                        }
                                        break;
                                    case 10:
                                        if (productType == VER_NT_WORKSTATION)
                                        {
                                            name = "Windows 10";
                                        }
                                        else
                                        {
                                            name = "Windows Server 2016";
                                        }

                                        break;
                                }
                                break;
                            }
                    }
                }
                s_Name = name;
                return name;
            }
        }
        #endregion NAME

        #region PINVOKE
        #region GET
        #region PRODUCT INFO
        [DllImport("Kernel32.dll")]
        internal static extern bool GetProductInfo(
            int osMajorVersion,
            int osMinorVersion,
            int spMajorVersion,
            int spMinorVersion,
            out int edition);
        #endregion PRODUCT INFO

        #region VERSION
        [DllImport("kernel32.dll")]
        private static extern bool GetVersionEx(ref OSVERSIONINFOEX osVersionInfo);
        #endregion VERSION
        #endregion GET

        #region OSVERSIONINFOEX
        [StructLayout(LayoutKind.Sequential)]
        private struct OSVERSIONINFOEX
        {
            public int dwOSVersionInfoSize;
            public int dwMajorVersion;
            public int dwMinorVersion;
            public int dwBuildNumber;
            public int dwPlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;
            public short wServicePackMajor;
            public short wServicePackMinor;
            public short wSuiteMask;
            public byte wProductType;
            public byte wReserved;
        }
        #endregion OSVERSIONINFOEX

        #region PRODUCT
        private const int PRODUCT_UNDEFINED = 0x00000000;
        private const int PRODUCT_ULTIMATE = 0x00000001;
        private const int PRODUCT_HOME_BASIC = 0x00000002;
        private const int PRODUCT_HOME_PREMIUM = 0x00000003;
        private const int PRODUCT_ENTERPRISE = 0x00000004;
        private const int PRODUCT_HOME_BASIC_N = 0x00000005;
        private const int PRODUCT_BUSINESS = 0x00000006;
        private const int PRODUCT_STANDARD_SERVER = 0x00000007;
        private const int PRODUCT_DATACENTER_SERVER = 0x00000008;
        private const int PRODUCT_SMALLBUSINESS_SERVER = 0x00000009;
        private const int PRODUCT_ENTERPRISE_SERVER = 0x0000000A;
        private const int PRODUCT_STARTER = 0x0000000B;
        private const int PRODUCT_DATACENTER_SERVER_CORE = 0x0000000C;
        private const int PRODUCT_STANDARD_SERVER_CORE = 0x0000000D;
        private const int PRODUCT_ENTERPRISE_SERVER_CORE = 0x0000000E;
        private const int PRODUCT_ENTERPRISE_SERVER_IA64 = 0x0000000F;
        private const int PRODUCT_BUSINESS_N = 0x00000010;
        private const int PRODUCT_WEB_SERVER = 0x00000011;
        private const int PRODUCT_CLUSTER_SERVER = 0x00000012;
        private const int PRODUCT_HOME_SERVER = 0x00000013;
        private const int PRODUCT_STORAGE_EXPRESS_SERVER = 0x00000014;
        private const int PRODUCT_STORAGE_STANDARD_SERVER = 0x00000015;
        private const int PRODUCT_STORAGE_WORKGROUP_SERVER = 0x00000016;
        private const int PRODUCT_STORAGE_ENTERPRISE_SERVER = 0x00000017;
        private const int PRODUCT_SERVER_FOR_SMALLBUSINESS = 0x00000018;
        private const int PRODUCT_SMALLBUSINESS_SERVER_PREMIUM = 0x00000019;
        private const int PRODUCT_HOME_PREMIUM_N = 0x0000001A;
        private const int PRODUCT_ENTERPRISE_N = 0x0000001B;
        private const int PRODUCT_ULTIMATE_N = 0x0000001C;
        private const int PRODUCT_WEB_SERVER_CORE = 0x0000001D;
        private const int PRODUCT_MEDIUMBUSINESS_SERVER_MANAGEMENT = 0x0000001E;
        private const int PRODUCT_MEDIUMBUSINESS_SERVER_SECURITY = 0x0000001F;
        private const int PRODUCT_MEDIUMBUSINESS_SERVER_MESSAGING = 0x00000020;
        private const int PRODUCT_SERVER_FOR_SMALLBUSINESS_V = 0x00000023;
        private const int PRODUCT_STANDARD_SERVER_V = 0x00000024;
        private const int PRODUCT_ENTERPRISE_SERVER_V = 0x00000026;
        private const int PRODUCT_STANDARD_SERVER_CORE_V = 0x00000028;
        private const int PRODUCT_ENTERPRISE_SERVER_CORE_V = 0x00000029;
        private const int PRODUCT_HYPERV = 0x0000002A;
        #endregion PRODUCT

        #region VERSIONS
        private const int VER_NT_WORKSTATION = 1;
        private const int VER_NT_DOMAIN_CONTROLLER = 2;
        private const int VER_NT_SERVER = 3;
        private const int VER_SUITE_SMALLBUSINESS = 1;
        private const int VER_SUITE_ENTERPRISE = 2;
        private const int VER_SUITE_TERMINAL = 16;
        private const int VER_SUITE_DATACENTER = 128;
        private const int VER_SUITE_SINGLEUSERTS = 256;
        private const int VER_SUITE_PERSONAL = 512;
        private const int VER_SUITE_BLADE = 1024;
        #endregion VERSIONS
        #endregion PINVOKE

        #region SERVICE PACK
        /// <summary>
        /// Obtém as informações do service pack do sistema operacional em execução neste computador.
        /// </summary>
        public static string ServicePack
        {
            get
            {
                string servicePack = String.Empty;
                OSVERSIONINFOEX osVersionInfo = new OSVERSIONINFOEX();
                osVersionInfo.dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX));
                if (GetVersionEx(ref osVersionInfo))
                {
                    servicePack = osVersionInfo.szCSDVersion;
                }

                return servicePack;
            }
        }
        #endregion SERVICE PACK

        #region VERSION
        #region BUILD
        /// <summary>
        /// Obtém o número da versão de compilação do sistema operacional em execução neste computador
        /// </summary>
        public static int BuildVersion { get { return Environment.OSVersion.Version.Build; } }
        #endregion BUILD

        #region FULL
        #region STRING
        /// <summary>
        /// Obtém a seqüência de versão completa do sistema operacional em execução neste computador.
        /// </summary>
        public static string VersionString { get { return Environment.OSVersion.Version.ToString(); } }
        #endregion STRING

        #region VERSION
        /// <summary>
        /// Obtém a versão completa do sistema operacional em execução neste computador.
        /// </summary>
        public static Version Version { get { return Environment.OSVersion.Version; } }
        #endregion VERSION
        #endregion FULL

        #region MAJOR
        /// <summary>
        /// Obtém o número de versão principal do sistema operacional em execução neste computador.
        /// </summary>
        public static int MajorVersion { get { return Environment.OSVersion.Version.Major; } }
        #endregion MAJOR

        #region MINOR
        /// <summary>
        /// Obtém o número de versão menor do sistema operacional em execução neste computador.
        /// </summary>
        public static int MinorVersion { get { return Environment.OSVersion.Version.Minor; } }
        #endregion MINOR

        #region REVISION
        /// <summary>
        /// Obtém o número da versão de revisão do sistema operacional em execução neste computador.
        /// </summary>
        public static int RevisionVersion { get { return Environment.OSVersion.Version.Revision; } }
        #endregion REVISION
        #endregion VERSION
    }
    #endregion >>>>>----- Informações sobre o Sistema Operacional -----<<<<<   
}


