using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCAcai.Negócios
{
    public class clsCompra
    {
        private NpgsqlCommand cmdSQL;
        clsConexao conexao = new clsConexao();
        DataSet RetornoDs = new DataSet();
        #region Atributos
        public DataSet Retorno { get => retorno; set => retorno = value; }
        private DataSet retorno;

        public Int32 ID_COMPRA { get => id_compra; set => id_compra = value; }
        private Int32 id_compra;
        public Int32 ID_FORNECEDOR { get => id_fornecedor; set => id_fornecedor = value; }
        private Int32 id_fornecedor;
        public Date DATA_COMPRA { get => data_compra; set => data_compra = value; }
        private Date data_compra;
        public decimal VALOR_TOTAL { get => valor_total; set => valor_total = value; }
        private decimal valor_total;

        public List<clsCompraProduto> produtos = new List<clsCompraProduto>();
        #endregion

        public DataTable BuscaUmRegistoVazio()
        {
            try
            {
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = @"select 0 as id_produto, '' as nome_produto, 0.00::numeric(14,2) as preco_compra, 0 as quantidade, 0.00::numeric(14,2) as total, '' as nome_contato, 0 as id_fornecedor where 1 < 0;"
                };

                return conexao.RetornarDataTable(cmdSQL);
            }
            catch (NpgsqlException)
            {
                throw;
            }
        }
        public void Gravar(DataTable dadosCompra, int id_fornecedor)
        {
            try
            {
                List<NpgsqlCommand> list = new List<NpgsqlCommand>();
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = @"INSERT INTO tab_compra
                                    (id_fornecedor, data_compra, valor_total)
                                    VALUES(@param_id_fornecedor, @param_data_compra, @param_valor_total);
                                    "
                };
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_id_fornecedor", id_fornecedor);
                cmdSQL.Parameters.AddWithValue("@param_data_compra", DateTime.Now.ToString("yyyy-MM-dd"));
                cmdSQL.Parameters.AddWithValue("@param_valor_total", dadosCompra.Compute("Sum(total)", string.Empty));
                list.Add(cmdSQL);

                foreach (DataRow item in dadosCompra.Rows)
                {
                    cmdSQL = new NpgsqlCommand
                    {
                        CommandType = CommandType.Text,
                        CommandText = @"INSERT INTO tab_compra_produto
                                    (id_compra, id_produto, quantidade)
                                    VALUES((SELECT coalesce(MAX(id_compra), 1) AS last_id FROM tab_compra limit 1), @param_id_produto, @param_quantidade);

                                    "
                    };
                    cmdSQL.Parameters.Clear();
                    cmdSQL.Parameters.AddWithValue("@param_id_produto", item["id_produto"].ToInt16());
                    cmdSQL.Parameters.AddWithValue("@param_quantidade", item["quantidade"].ToInt16());
                    list.Add(cmdSQL);
                }
               

                conexao.ExecutarTransacao(list);
            }
            catch (NpgsqlException)
            {
                throw;
            }
        }
        public DataTable BuscaTodasCompras()
        {
            try
            {
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = @"select tc.*, tf.nome_contato  from tab_compra tc 
                                left join tab_fornecedor tf on tf.id_fornecedor = tc.id_fornecedor ;"
                };

                return conexao.RetornarDataTable(cmdSQL);
            }
            catch (NpgsqlException)
            {
                throw;
            }
        }

        public DataTable BuscaProdutosCompra(int id_compra)
        {
            try
            {
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = @"select *, (tcp.quantidade * tp.preco_compra) as total from tab_compra_produto tcp 
left join tab_produto tp on tp.id_produto = tcp.id_produto 
where tcp.id_compra = @param_id_compra;"
                };
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_id_compra", id_compra);

                return conexao.RetornarDataTable(cmdSQL);
            }
            catch (NpgsqlException)
            {
                throw;
            }
        }
        public void ExcluiCompra(int id_compra)
        {
            try
            {
                List<NpgsqlCommand> list = new List<NpgsqlCommand>();

                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = @"select * from tab_compra_produto tcp where id_compra = @param_id_compra"
                };
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_id_compra", id_compra);
                DataTable itens = conexao.RetornarDataTable(cmdSQL);
                foreach (DataRow item in itens.Rows)
                {
                    cmdSQL = new NpgsqlCommand
                    {
                        CommandType = CommandType.Text,
                        CommandText = $@"update tab_estoque set quantidade = quantidade - {item["quantidade"]} where id_produto = @param_id_produto;"
                    };
                    cmdSQL.Parameters.Clear();
                    cmdSQL.Parameters.AddWithValue("@param_id_produto", item["id_produto"]);
                    list.Add(cmdSQL);
                }
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = @"Delete from tab_compra_produto where id_compra = @param_id_compra"
                };
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_id_compra", id_compra);
                list.Add(cmdSQL);
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = @"Delete from tab_compra where id_compra = @param_id_compra"
                };
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_id_compra", id_compra);
                list.Add(cmdSQL);

                conexao.ExecutarTransacao(list);
            }
            catch (NpgsqlException)
            {
                throw;
            }
        }
    }
    public class clsCompraProduto
    {
        public Int32 ID_COMPRA { get => id_compra; set => id_compra = value; }
        private Int32 id_compra;
        public Int32 ID_PRODUTO { get => id_produto; set => id_produto = value; }
        private Int32 id_produto;
        public decimal QUANTIDADE { get => quantidade; set => quantidade = value; }
        private decimal quantidade;
    }
}
