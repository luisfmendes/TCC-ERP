using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCAcai.Negócios
{
    public class clsCliente
    {
        private NpgsqlCommand cmdSQL;

        #region Atributos
        public DataSet Retorno { get => retorno; set => retorno = value; }
        private DataSet retorno;

        public Int32 ID_CLIENTE { get => id_cliente; set => id_cliente = value; }
        private Int32 id_cliente;
        public string NOME_CLIENTE { get => nome_cliente; set => nome_cliente = value; }
        private string nome_cliente;
        public string TELEFONE { get => telefone; set => telefone = value; }
        private string telefone;
        public string ENDERECO { get => endereco; set => endereco = value; }
        private string endereco;
        public string CIDADE { get => cidade; set => cidade = value; }
        private string cidade;
        public string CEP { get => cep; set => cep = value; }
        private string cep;
        public bool ATIVO { get => ativo; set => ativo = value; }
        private bool ativo;

        #endregion

        clsConexao conexao = new clsConexao();
        DataSet RetornoDs = new DataSet();
        /// <summary>
        /// Função para gravar os dados
        /// </summary>
        public void Gravar()
        {
            try
            {
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = @"INSERT INTO tab_cliente
                    (nome_cliente, telefone, endereco, cidade, cep, ativo)
                    VALUES(@param_nome_cliente, @param_telefone, @param_endereco, @param_cidade, @param_cep, @param_ativo) RETURNING id_cliente;
            "
                };
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_nome_cliente", NOME_CLIENTE);
                cmdSQL.Parameters.AddWithValue("@param_telefone", TELEFONE);
                cmdSQL.Parameters.AddWithValue("@param_endereco", ENDERECO);
                cmdSQL.Parameters.AddWithValue("@param_cidade", CIDADE);
                cmdSQL.Parameters.AddWithValue("@param_cep", CEP);
                cmdSQL.Parameters.AddWithValue("@param_ativo", ATIVO);

                ID_CLIENTE = conexao.ExecutarComando(cmdSQL).ToInt32();
                
            }
            catch(NpgsqlException erro)
            {
                throw;
            }
        }

        /// <summary>
        /// Função para alterar os dados
        /// </summary>
        public void Alterar()
        {
            try
            {
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = @"UPDATE tab_cliente SET nome_cliente = @param_nome_cliente, telefone = @param_telefone, endereco = @param_endereco, cidade = @param_cidade, cep = @param_cep, ativo =  @param_ativo
                    where id_cliente = @param_id_cliente;
            "
                };
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_id_cliente", ID_CLIENTE);
                cmdSQL.Parameters.AddWithValue("@param_nome_cliente", NOME_CLIENTE);
                cmdSQL.Parameters.AddWithValue("@param_telefone", TELEFONE);
                cmdSQL.Parameters.AddWithValue("@param_endereco", ENDERECO);
                cmdSQL.Parameters.AddWithValue("@param_cidade", CIDADE);
                cmdSQL.Parameters.AddWithValue("@param_cep", CEP);
                cmdSQL.Parameters.AddWithValue("@param_ativo", ATIVO);

               conexao.ExecutarComando(cmdSQL);

            }
            catch (NpgsqlException erro)
            {
                throw;
            }
        }

        /// <summary>
        /// Função para excluir um registro
        /// </summary>
        public void Excluir()
        {
            try
            {
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = @"Delete from tab_cliente where id_cliente = @param_id_cliente"
                };
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_id_cliente", ID_CLIENTE);

                conexao.ExecutarComando(cmdSQL);
            }
            catch (NpgsqlException erro)
            {
                throw;
            }
        }

        /// <summary>
        /// Função para Buscar todos os registro
        /// </summary>
        public DataSet BuscaTodos()
        {
            try
            {
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = @"select * from tab_cliente"
                };

                RetornoDs = conexao.RetornarDataSet(cmdSQL);
                if(RetornoDs.Tables.Count > 0)
                {
                    RetornoDs.Tables[0].TableName = "tab_cliente";
                }
                return RetornoDs;
            }
            catch (NpgsqlException erro)
            {
                throw;
            }
        }

        public void BuscaUmRegistro()
        {
            try
            {
                DataTable dt = new DataTable();
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = @"select * from tab_cliente where id_cliente = @param_id_cliente"
                };

                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_id_cliente", ID_CLIENTE);
                Retorno = conexao.RetornarDataSet(cmdSQL);
                if(Retorno != null && Retorno.Tables.Count > 0)
                {
                    dt = Retorno.Tables[0];
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    NOME_CLIENTE = dt.Rows[0]["nome_cliente"].ToString();
                    TELEFONE = dt.Rows[0]["telefone"].ToString();
                    ENDERECO = dt.Rows[0]["endereco"].ToString();
                    CIDADE = dt.Rows[0]["cidade"].ToString();
                    CEP = dt.Rows[0]["cep"].ToString();
                    ATIVO = dt.Rows[0]["ativo"].ToBoolean();
                }
               
            }
            catch (NpgsqlException erro)
            {
                throw;
            }
        }

        /// <summary>
        /// Busca clientes pelo filtro especificado
        /// </summary>
        /// <param name="nomeContem">Nome a ser pesquisado</param>
        /// <param name="bAtivo">A - Ativo | T - Todos | I - Inativo</param>
        /// <param name="tipoFiltro">C - Contem | I - Inicia | T - Termina</param>
        /// <returns></returns>
        public DataSet BuscaTodosFiltro(string nomeContem = "", char bAtivo = 'T', char tipoFiltro = 'C')
        {
            try
            {
                string filtro = "";
                switch (tipoFiltro)
                {
                    case 'C':
                        filtro = $" and nome_cliente like '%{nomeContem}%'";
                        break;
                    case 'I':
                        filtro = $" and nome_cliente like '{nomeContem}%'";
                        break;
                    case 'T':
                        filtro = $" and nome_cliente like '%{nomeContem}'";
                        break;
                }
                
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = $@"select * from tab_cliente where true {(nomeContem.IsNullOrEmpty() ? "" : filtro)} {(bAtivo == 'T' ? "" : $" and ativo = {(bAtivo == 'A' ? "true" : "false")}")};"
                };

                RetornoDs = conexao.RetornarDataSet(cmdSQL);
                if (RetornoDs.Tables.Count > 0)
                {
                    RetornoDs.Tables[0].TableName = "tab_cliente";
                }
                return RetornoDs;
            }
            catch (NpgsqlException erro)
            {
                throw;
            }
        }

        public bool BuscaClienteVenda(int id_cliente)
        {
            try
            {              

                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = $@"select * from tab_venda tv where id_cliente = {id_cliente}"
                };

                RetornoDs = conexao.RetornarDataSet(cmdSQL);
                if (RetornoDs.Tables.Count > 0)
                {
                    RetornoDs.Tables[0].TableName = "tab_cliente";
                    return RetornoDs.Tables[0].Rows.Count > 0;
                }
                return false;
            }
            catch (NpgsqlException erro)
            {
                throw;
            }
        }
    }
}
