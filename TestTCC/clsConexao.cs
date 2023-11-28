using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TCCAcai
{
    public class clsConexao
    {
        string strConexao = "Server=motty.db.elephantsql.com;Port=5432;User Id=evxiehtc;Password=Ve2rV7Mm_DYTl4a8OlUvyud_mcmJZ-5o;Database=evxiehtc;CommandTimeout=240;Pooling=false;";
       

        public string sCmd_SQl_Erro;

       
        /// <summary>
        /// Efetua o teste de conexão com o Banco de Dados
        /// </summary>
        /// <returns>TRUE = Conexão realizada com sucesso / FALSE = Conexão com problemas</returns>
        public bool TestaConexao()
        {
            NpgsqlConnection cn = new NpgsqlConnection();
            bool bReturn = false;
            string strConexaoBkp = strConexao;
            try
            {
                strConexao = null;
                cn = AbrirBanco();
                if (cn != null)
                {
                    bReturn = true;
                }
            }
            catch (NpgsqlException)
            {
                bReturn = false;
            }
            finally
            {
                if (cn != null)
                {
                    FecharBanco(cn);
                }
                strConexao = strConexaoBkp;
            }
            return bReturn;
        }

        /// <summary>
        /// Abrir conexao com o BD
        /// </summary>
        /// <returns>Retorna a conexão</returns>
        public NpgsqlConnection AbrirBanco(string strConexao_Param = "")
        {

            if (!strConexao_Param.IsNullOrEmpty())
            {
                strConexao = strConexao_Param;
            }
            
            NpgsqlConnection cn = new NpgsqlConnection(strConexao);
            try
            {
                try
                {
                    
                    cn.Open();                    
                }
                catch
                {
                    cn = null; throw;
                }
            }
            catch
            {
                cn = null;
                throw;
            }
            return cn;
        }

        /// <summary>
        /// Fechar conexao com o BD
        /// </summary>
        /// <param name="cn">Conexão do banco de dados</param>
        public void FecharBanco(NpgsqlConnection cn)
        {
            if (cn != null)
            {
                if (cn.State == ConnectionState.Open)
                {
                    try
                    {
                        cn.Close();
                    }
                    catch { }
                }
            }
        }
        /// <summary>
        /// Novo Id gerado em caso de erro (Coluna 0=Nome do campo / Coluna 1=Novo ID do campo)
        /// </summary>
        string[] sNovoId = new string[2];

        /// <summary>
        /// Execução de Transação. Retorna o Id gerado no banco de dados.
        /// </summary>
        /// <param name="cmdSQL">Lista de Comandos SQL a serem executados</param>
        /// <returns>Retorna o ID gerado no banco de dados</returns>
        public List<int> ExecutarTransacao(List<NpgsqlCommand> listaComando)
        {
            NpgsqlConnection cn = new NpgsqlConnection();
            NpgsqlTransaction transacao = null;

            List<int> retorno_id = new List<int>();
            bool bTratouErro = (sNovoId != null && !string.IsNullOrEmpty(sNovoId[0]));
            try
            {
                cn = AbrirBanco(); //Conexao ao banco
                transacao = cn.BeginTransaction(); //Inicia Transação
                List<string> cmdsql = new List<string>();
                sCmd_SQl_Erro = "";
                for (int i = 0; i < listaComando.Count; i++)
                {
                    if (bTratouErro)
                    {
                        listaComando[i].Parameters[string.Format("@param_{0}", sNovoId[0])].Value = sNovoId[1].ToInt32();
                    }

                    if (listaComando[i].CommandText.Trim().ToUpper().StartsWith("INSERT") || listaComando[i].CommandText.Trim().ToUpper().StartsWith("UPDATE") || listaComando[i].CommandText.Trim().ToUpper().StartsWith("DELETE"))
                    {
                        if (!listaComando[i].CommandText.ToUpper().Contains("RETURNING"))
                        {
                            while (true)
                            {
                                listaComando[i].CommandText = listaComando[i].CommandText.Trim();
                                if (listaComando[i].CommandText.EndsWith(";"))
                                {
                                    listaComando[i].CommandText = listaComando[i].CommandText.Remove(listaComando[i].CommandText.Length - 1, 1);
                                }
                                else
                                {
                                    listaComando[i].CommandText += " RETURNING *;";
                                    break;
                                }
                                if (string.IsNullOrEmpty(listaComando[i].CommandText))
                                {
                                    break;
                                }
                            }
                        }
                    }


                    listaComando[i].Transaction = transacao;
                    listaComando[i].Connection = cn;
                    var var_id = listaComando[i].ExecuteScalar();

                    /*
                    if (var_id.IsNullOrEmpty())
                    {
                        var_id = 1;
                    }
                    */

                    if (var_id != null)
                    {
                        retorno_id.Add(var_id.ToInt32());
                    }
                }

                transacao.Commit(); //Confirma transação
                sNovoId[0] = sNovoId[1] = null;
            }
            catch (NpgsqlException erro)
            {
                transacao.Rollback(); //Desfaz transação em caso de erro
               
                    try
                    {
                        sNovoId[0] = sNovoId[1] = null;
                        throw;
                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        listaComando.Clear();
                        retorno_id = new List<int>();
                    }
                
            }
            catch (Exception) { }
            finally
            {
                FecharBanco(cn); //Fecha conexão com o banco
            }
            return retorno_id;
        }

        /// <summary>
        /// Execução de Transação. Retorna TRUE/FALSE se a transação foi executada e se a qtdade de registros afetados foi maior que 1.
        /// </summary>
        /// <param name="cmdSQL">Lista de Comandos SQL a serem executados</param>
        /// <returns>Retorna TRUE/FALSE se a transação foi executada e se a qtdade de registros afetados foi maior que 1</returns>
        public bool ExecutarTransacao_bool(List<NpgsqlCommand> cmdSQL)
        {
            bool bRetorno = false;
            try
            {
                bRetorno = (ExecutarTransacao(cmdSQL).Count == cmdSQL.Count);
            }
            catch
            {
                bRetorno = false;
                throw;
            }
            return bRetorno;
        }

        /// <summary>
        /// Execução de comandos. Retorna o Id gerado no banco de dados.
        /// </summary>
        /// <param name="cmdSQL">Comando SQL a ser executado</param>
        /// <returns>Retorna o ID gerado no banco de dados</returns>
        public string ExecutarComando(NpgsqlCommand cmdSQL)
        {
            string sRetorno = null; bool bUpdate = false, bDelete = false;
            NpgsqlConnection cn = new NpgsqlConnection();
            try
            {
                cn = AbrirBanco();
                cmdSQL.Connection = cn;
                if (cmdSQL.CommandText.ToLower().IndexOf("update") == 0 || cmdSQL.CommandText.ToLower().IndexOf("delete") == 0)
                {
                    bUpdate = (cmdSQL.CommandText.ToLower().IndexOf("update") == 0);
                    bDelete = (cmdSQL.CommandText.ToLower().IndexOf("delete") == 0);
                    string sReturning = (cmdSQL.CommandText.ToLower().IndexOf("returning") == -1) ? " returning *" : "";
                    if (cmdSQL.CommandText.Trim().Substring(cmdSQL.CommandText.Trim().Length - 1) == ";")
                    {
                        cmdSQL.CommandText = cmdSQL.CommandText.Trim().Substring(0, cmdSQL.CommandText.Trim().Length - 1);
                    }
                    cmdSQL.CommandText = string.Format("With registros as ({0}{1}) Select Count(*) from registros", cmdSQL.CommandText, sReturning);
                }

                var var_id = cmdSQL.ExecuteScalar();
                if (var_id != null)
                {
                    if (var_id is int && (var_id.ToInt32() == 0) && (bUpdate || bDelete))
                    {
                        throw new Exception(string.Format("##Erro Seek##Registro não localizado no Banco de Dados.\nNão foi possível realizar a {0}.", (bUpdate) ? "Atualização" : "Exclusão"));
                    }
                    else
                    {
                        sRetorno = var_id.ToString();
                    }
                }
            }
            catch (Exception erro)
            {
              
                    sRetorno = null;
                    throw;
                
            }
            finally
            {
                FecharBanco(cn);
            }
            return sRetorno;
        }
       
        /// <summary>
        /// Execução de comandos. Retorna TRUE/FALSE se o comando foi executado e se a qtdade de registros afetados foi maior que 1 registro.
        /// </summary>
        /// <param name="cmdSQL">Comando SQL a ser executado</param>
        /// <returns>Retorna TRUE/FALSE se o comando foi executado e se a qtdade de registros afetados foi maior que 1 registro</returns>
        public bool ExecutarComando_bool(NpgsqlCommand cmdSQL)
        {
            bool bRetorno = false;
            try
            {
                bRetorno = ExecutarComando(cmdSQL).ToInt16() >= 1;
            }
            catch
            {
                bRetorno = false;
                throw;
            }
            return bRetorno;
        }

        

        //Retorna DataSet
        public DataSet RetornarDataSet(NpgsqlCommand cmdSQL, bool bNao_Gerar_Log = false)
        {
            NpgsqlConnection cn = new NpgsqlConnection();
            DataSet ds = new DataSet();
            try
            {

                cn = AbrirBanco();
                if (cn == null)
                {
                    return null;
                }

                cmdSQL.Connection = cn;
                NpgsqlDataAdapter da = new NpgsqlDataAdapter();
                da.SelectCommand = cmdSQL;
                Task.Run(() => {
                    da.Fill(ds);
                }).Wait();

            }
            catch (NpgsqlException ex)
            {
                if (ex.ErrorCode != 22021) // Erro de caracter inválido
                {
                    ds = null;
                    throw;
                }
            }
            finally
            {
                FecharBanco(cn);
            }
            return ds;
        }

        //Retorna DataTable
        public DataTable RetornarDataTable(NpgsqlCommand cmdSQL, bool bGera_Log = false)
        {
            NpgsqlConnection cn = new NpgsqlConnection();
            DataTable ds = new DataTable();
            string deuErro = "";
            try
            {
                cn = AbrirBanco();
                if (cn == null)
                {
                    return null;
                }
                cmdSQL.Connection = cn;
                NpgsqlDataAdapter da = new NpgsqlDataAdapter();
                da.SelectCommand = cmdSQL;

                try
                {
                    da.Fill(ds);
                }
                catch (NpgsqlException erro)
                {
                    deuErro = erro.Message;
                    throw;
                }
            }
            catch
            {
                ds = null;
                throw;
            }
            finally
            {
                FecharBanco(cn);
                if (!deuErro.IsNullOrEmpty())
                {
                    throw new Exception(deuErro);
                }
            }
            return ds;
        }

        //Classe para retornar um DataReader()
        public NpgsqlDataReader RetornarDataReader(string strQuery)
        {
            NpgsqlConnection cn = new NpgsqlConnection();
            NpgsqlDataReader dr;
            try
            {
                cn = AbrirBanco();
                if (cn == null)
                {
                    return null;
                }

                NpgsqlCommand cmd = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = strQuery.ToString()
                };
                cmd.Connection = cn;
                dr = cmd.ExecuteReader();
            }
            catch
            {
                dr = null;
                throw;
            }
            finally
            {
                FecharBanco(cn);
            }
            return dr;
        }

       

    }
}
