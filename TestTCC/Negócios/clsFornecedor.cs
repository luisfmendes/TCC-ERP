using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCAcai.Negócios
{
    public class clsFornecedor
    {
        private NpgsqlCommand cmdSQL;

        #region Atributos
        public DataSet Retorno { get => retorno; set => retorno = value; }
        private DataSet retorno;

        public Int32 ID_FORNECEDOR { get => id_fornecedor; set => id_fornecedor = value; }
        private Int32 id_fornecedor;
        public string NOME_CONTATO { get => nome_contato; set => nome_contato = value; }
        private string nome_contato;
        public string RAZAO_SOCIAL { get => razao_social; set => razao_social = value; }
        private string razao_social;
        public string CNPJ { get => cnpj; set => cnpj = value; }
        private string cnpj;
        public string TELEFONE { get => telefone; set => telefone = value; }
        private string telefone;
        public string ENDERECO { get => endereco; set => endereco = value; }
        private string endereco;
        public string CEP { get => cep; set => cep = value; }
        private string cep;
        public string EMAIL { get => email; set => email = value; }
        private string email;
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
                    CommandText = @"INSERT INTO tab_fornecedor
(nome_contato, razao_social, cnpj, telefone, endereco, cep, email, ativo)
                    VALUES(@param_nome_contato, 
                    @param_razao_social, 
                    @param_cnpj, 
                    @param_telefone, 
                    @param_endereco, 
                    @param_cep, 
                    @param_email, 
                    @param_ativo) RETURNING id_fornecedor;
            "
                };
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_nome_contato", NOME_CONTATO);
                cmdSQL.Parameters.AddWithValue("@param_razao_social", RAZAO_SOCIAL);
                cmdSQL.Parameters.AddWithValue("@param_cnpj", CNPJ);
                cmdSQL.Parameters.AddWithValue("@param_telefone", TELEFONE);
                cmdSQL.Parameters.AddWithValue("@param_endereco", ENDERECO);
                cmdSQL.Parameters.AddWithValue("@param_cep", CEP);
                cmdSQL.Parameters.AddWithValue("@param_email", EMAIL);
                cmdSQL.Parameters.AddWithValue("@param_ativo", ATIVO);

                ID_FORNECEDOR = conexao.ExecutarComando(cmdSQL).ToInt32();

            }
            catch (NpgsqlException erro)
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
                    CommandText = @"UPDATE tab_fornecedor SET nome_contato =  @param_nome_contato, 
                    razao_social = @param_razao_social, 
                    cnpj = @param_cnpj, 
                    telefone = @param_telefone, 
                    endereco = @param_endereco, 
                    cep = @param_cep, 
                    email = @param_email, 
                    ativo = @param_ativo
                    where id_fornecedor = @param_id_fornecedor;
            "
                };

                    

                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_id_fornecedor", ID_FORNECEDOR);
                cmdSQL.Parameters.AddWithValue("@param_nome_contato", NOME_CONTATO);
                cmdSQL.Parameters.AddWithValue("@param_razao_social", RAZAO_SOCIAL);
                cmdSQL.Parameters.AddWithValue("@param_cnpj", CNPJ);
                cmdSQL.Parameters.AddWithValue("@param_telefone", TELEFONE);
                cmdSQL.Parameters.AddWithValue("@param_endereco", ENDERECO);
                cmdSQL.Parameters.AddWithValue("@param_cep", CEP);
                cmdSQL.Parameters.AddWithValue("@param_email", EMAIL);
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
                    CommandText = @"Delete from tab_fornecedor where id_fornecedor = @param_id_fornecedor"
                };
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_id_fornecedor", ID_FORNECEDOR);

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
                    CommandText = @"select * from tab_fornecedor"
                };

                RetornoDs = conexao.RetornarDataSet(cmdSQL);
                if (RetornoDs.Tables.Count > 0)
                {
                    RetornoDs.Tables[0].TableName = "tab_fornecedor";
                }
                return RetornoDs;
            }
            catch (NpgsqlException erro)
            {
                throw;
            }
        }
        /// <summary>
        /// Função para Buscar todos os registro
        /// </summary>
        public bool BuscaFornecedorCompra(int id_fornecedor)
        {
            try
            {
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = $@"select * from tab_compra tc where id_fornecedor = {id_fornecedor};"
                };

                RetornoDs = conexao.RetornarDataSet(cmdSQL);
                if (RetornoDs.Tables.Count > 0)
                {
                    RetornoDs.Tables[0].TableName = "tab_fornecedor";
                    return RetornoDs.Tables[0].Rows.Count > 0;
                }
                return false;
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
                    CommandText = @"select * from tab_fornecedor where id_fornecedor = @param_id_fornecedor"
                };

                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_id_fornecedor", ID_FORNECEDOR);
                Retorno = conexao.RetornarDataSet(cmdSQL);
                if (Retorno != null && Retorno.Tables.Count > 0)
                {
                    dt = Retorno.Tables[0];
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    NOME_CONTATO = dt.Rows[0]["nome_contato"].ToString();
                    RAZAO_SOCIAL = dt.Rows[0]["razao_social"].ToString();
                    CNPJ = dt.Rows[0]["cnpj"].ToString();
                    ENDERECO = dt.Rows[0]["endereco"].ToString();
                    TELEFONE = dt.Rows[0]["telefone"].ToString();
                    CEP = dt.Rows[0]["cep"].ToString();
                    EMAIL = dt.Rows[0]["email"].ToString();
                    ATIVO = dt.Rows[0]["ativo"].ToBoolean();
                }

            }
            catch (NpgsqlException erro)
            {
                throw;
            }
        }

        /// <summary>
        /// Busca fornecedores pelo filtro especificado
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
                        filtro = $" and nome_contato like '%{nomeContem}%'";
                        break;
                    case 'I':
                        filtro = $" and nome_contato like '{nomeContem}%'";
                        break;
                    case 'T':
                        filtro = $" and nome_contato like '%{nomeContem}'";
                        break;
                }

                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = $@"select * from tab_fornecedor where true {(nomeContem.IsNullOrEmpty() ? "" : filtro)} {(bAtivo == 'T' ? "" : $" and ativo = {(bAtivo == 'A' ? "true" : "false")}")};"
                };

                RetornoDs = conexao.RetornarDataSet(cmdSQL);
                if (RetornoDs.Tables.Count > 0)
                {
                    RetornoDs.Tables[0].TableName = "tab_fornecedor";
                }
                return RetornoDs;
            }
            catch (NpgsqlException erro)
            {
                throw;
            }
        }
    }
}
