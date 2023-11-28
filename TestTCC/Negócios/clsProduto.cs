using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCAcai.Negócios
{
    public class clsProduto
    {
        private NpgsqlCommand cmdSQL;

        #region Atributos
        public DataSet Retorno { get => retorno; set => retorno = value; }
        private DataSet retorno;

        public Int32 ID_PRODUTO { get => id_produto; set => id_produto = value; }
        private Int32 id_produto;
        public string NOME_PRODUTO { get => nome_produto; set => nome_produto = value; }
        private string nome_produto;
        public string DESCRICAO { get => descricao; set => descricao = value; }
        private string descricao;
        public decimal PRECO_VENDA { get => preco_venda; set => preco_venda = value; }
        private decimal preco_venda;
        public decimal PRECO_COMPRA { get => preco_compra; set => preco_compra = value; }
        private decimal preco_compra;

        public Int32 QUANTIDADE { get => quantidade; set => quantidade = value; }
        private Int32 quantidade;

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
                    CommandText = @"INSERT INTO tab_produto
                    (nome_produto, descricao, preco_venda, preco_compra, ativo)
                    VALUES(@param_nome_produto, @param_descricao, @param_preco_venda, @param_preco_compra, @param_ativo) RETURNING id_produto;
            "
                };
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_nome_produto", NOME_PRODUTO);
                cmdSQL.Parameters.AddWithValue("@param_descricao", DESCRICAO);
                cmdSQL.Parameters.AddWithValue("@param_preco_venda", PRECO_VENDA);
                cmdSQL.Parameters.AddWithValue("@param_preco_compra", PRECO_COMPRA);
                cmdSQL.Parameters.AddWithValue("@param_ativo", ATIVO);

                ID_PRODUTO = conexao.ExecutarComando(cmdSQL).ToInt32();
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
                    CommandText = @"UPDATE tab_produto SET nome_produto=@param_nome_produto, descricao=@param_descricao, preco_venda=@param_preco_venda, preco_compra=@param_preco_compra, ativo=@param_ativo
                    where id_produto = @param_id_produto;
            "
                };
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_id_produto", ID_PRODUTO);
                cmdSQL.Parameters.AddWithValue("@param_nome_produto", NOME_PRODUTO);
                cmdSQL.Parameters.AddWithValue("@param_descricao", DESCRICAO);
                cmdSQL.Parameters.AddWithValue("@param_preco_venda", PRECO_VENDA);
                cmdSQL.Parameters.AddWithValue("@param_preco_compra", PRECO_COMPRA);
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
                    CommandText = @"Delete from tab_produto where id_produto = @param_id_produto"
                };
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_id_produto", ID_PRODUTO);

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
        public DataSet BuscaTodos()
        {
            try
            {
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = @"select tp.*, te.quantidade  from tab_produto tp
                                    left join tab_estoque te on te.id_produto = tp.id_produto  "
                };

                RetornoDs = conexao.RetornarDataSet(cmdSQL);
                if (RetornoDs.Tables.Count > 0)
                {
                    RetornoDs.Tables[0].TableName = "tab_produto";
                }
                return RetornoDs;
            }
            catch (NpgsqlException erro)
            {
                throw;
            }
        }
        public bool BuscaProdutoVenda(int id_produto)
        {
            try
            {
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = $@"select * from tab_pedido_item tpi  where id_produto = {id_produto};
                                    select * from tab_compra_produto tcp  where id_produto = {id_produto};"
                };

                RetornoDs = conexao.RetornarDataSet(cmdSQL);
                if (RetornoDs.Tables.Count > 0)
                {
                    RetornoDs.Tables[0].TableName = "tab_produto";
                    RetornoDs.Tables[1].TableName = "tab_compra";
                    return RetornoDs.Tables[0].Rows?.Count > 0 || RetornoDs.Tables[1].Rows?.Count > 0;
                }
                return false;
            }
            catch (NpgsqlException erro)
            {
                throw;
            }
        }
        
        public void BuscaUmRegistro(bool bAtivo = false)
        {
            try
            {
                DataTable dt = new DataTable();
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = $@"select tp.*, te.quantidade  from tab_produto tp
                                    left join tab_estoque te on te.id_produto = tp.id_produto where tp.id_produto = @param_id_produto {(bAtivo ? " and tp.ativo = true" : "")}"
                };

                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_id_produto", ID_PRODUTO);
                dt = conexao.RetornarDataTable(cmdSQL);
                if (dt != null && dt.Rows.Count > 0)
                {
                    NOME_PRODUTO = dt.Rows[0]["nome_produto"].ToString();
                    DESCRICAO = dt.Rows[0]["descricao"].ToString();
                    PRECO_VENDA = dt.Rows[0]["preco_venda"].ToDecimal();
                    PRECO_COMPRA = dt.Rows[0]["preco_compra"].ToDecimal();
                    QUANTIDADE = dt.Rows[0]["quantidade"].ToInt16();
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
                        filtro = $" and nome_produto like '%{nomeContem}%'";
                        break;
                    case 'I':
                        filtro = $" and nome_produto like '{nomeContem}%'";
                        break;
                    case 'T':
                        filtro = $" and nome_produto like '%{nomeContem}'";
                        break;
                }

                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = $@"select * from tab_produto where true {(nomeContem.IsNullOrEmpty() ? "" : filtro)} {(bAtivo == 'T' ? "" : $" and ativo = {(bAtivo == 'A' ? "true" : "false")}")};"
                };

                RetornoDs = conexao.RetornarDataSet(cmdSQL);
                if (RetornoDs.Tables.Count > 0)
                {
                    RetornoDs.Tables[0].TableName = "tab_produto";
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
