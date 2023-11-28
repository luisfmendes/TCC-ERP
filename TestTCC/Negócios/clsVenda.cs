using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace TCCAcai.Negócios
{
    public class clsVenda
    {
        private NpgsqlCommand cmdSQL;
        clsConexao conexao = new clsConexao();
        DataSet RetornoDs = new DataSet();

        public DataSet Retorno { get => retorno; set => retorno = value; }
        private DataSet retorno;

        public Int32 ID_PRODUTO
        {
            get
            {
                return this.id_produto;
            }
            set
            {
                this.id_produto = value;
                produtoDados.ID_PRODUTO = value;
                produtoDados.BuscaUmRegistro();
            }
        }
        private Int32 id_produto;
        public Int32 QUANTIDADE { get => quantidade; set => quantidade = value; }
        private Int32 quantidade;
        public clsProduto produtoDados = new clsProduto();



        public void Gravar(DataTable dadosVenda, int cli_id, decimal vlr_total)
        {
            try
            {
                List<NpgsqlCommand> listaComandos = new List<NpgsqlCommand>();
                
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = $@"INSERT INTO tab_pedido
                                    (data_pedido)
                                    VALUES(@param_data_venda);"
                };
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_data_venda", DateTime.Now.ToString("yyyy-MM-dd"));                
                listaComandos.Add(cmdSQL);

                foreach (DataRow item in dadosVenda.Rows)
                {
                    cmdSQL = new NpgsqlCommand
                    {
                        CommandType = CommandType.Text,
                        CommandText = $@"INSERT INTO tab_pedido_item
                                    (id_produto, id_pedido, quantidade)
                                    VALUES({item["id_produto"]}, (SELECT coalesce(MAX(id_pedido), 1) AS last_id FROM tab_pedido limit 1), {item["quantidade"]});
                                    "
                    };
                    listaComandos.Add(cmdSQL);
                }
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = $@"INSERT INTO tab_venda
                                (id_pedido, id_cliente, valor_total)
                                VALUES((select max(id_pedido) from tab_pedido), {cli_id}, @param_vlr_total);"
                };
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_vlr_total", vlr_total);
                listaComandos.Add(cmdSQL);

                conexao.ExecutarTransacao(listaComandos);

            }
            catch
            {
                throw;
            }
        }

        public DataTable BuscaUmRegistoVazio()
        {
            try
            {
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = @"select 0 as id_produto, '' as nome_produto, 0.00::numeric(14,2) as preco_venda, 0 as quantidade, 0.00::numeric(14,2) as total where 1 < 0;"
                };

                return conexao.RetornarDataTable(cmdSQL);
            }
            catch (NpgsqlException)
            {
                throw;
            }
        }

        public DataTable BuscaTodasVendasCliente(int cli_id)
        {
            try
            {
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = @"select tv.id_venda, tv.id_pedido, tv.id_cliente, tv.valor_total, to_char(tp.data_pedido, 'dd/MM/yyyy') as data_pedido  from tab_venda tv 
left join tab_pedido tp on tp.id_pedido  = tv.id_pedido where tv.id_cliente = @param_id_cliente;"
                };
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_id_cliente", cli_id);
                return conexao.RetornarDataTable(cmdSQL);
            }
            catch (NpgsqlException)
            {
                throw;
            }
        }

        public DataTable BuscaProdutosVenda(int ven_id)
        {
            try
            {
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = @"select tpi.*, (tpi.quantidade * tp.preco_venda) as total, tp.preco_venda from tab_pedido_item tpi 
left join tab_produto tp on tp.id_produto = tpi.id_produto
left join tab_venda tv on tv.id_pedido = tpi.id_pedido 
where tv.id_venda = @param_id_venda;"
                };
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_id_venda", ven_id);
                return conexao.RetornarDataTable(cmdSQL);
            }
            catch (NpgsqlException)
            {
                throw;
            }
        }

        public void ExcluiVenda(int ven_id)
        {
            try
            {
                List<NpgsqlCommand> list = new List<NpgsqlCommand>();
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = @"select tpi.*, (tpi.quantidade * tp.preco_venda) as total, tp.preco_venda from tab_pedido_item tpi 
left join tab_produto tp on tp.id_produto = tpi.id_produto
left join tab_venda tv on tv.id_pedido = tpi.id_pedido 
where tv.id_venda = @param_id_venda;"
                };
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_id_venda", ven_id);
                DataTable itens = conexao.RetornarDataTable(cmdSQL);
                int id_pedido = itens.Rows[0]["id_pedido"].ToInt32();
                foreach (DataRow item in itens.Rows)
                {
                    cmdSQL = new NpgsqlCommand
                    {
                        CommandType = CommandType.Text,
                        CommandText = $"update tab_estoque set quantidade = quantidade + {item["quantidade"]} where id_produto = @param_id_produto;"
                    };
                    cmdSQL.Parameters.Clear();
                    cmdSQL.Parameters.AddWithValue("@param_id_produto", item["id_produto"].ToInt16());
                    list.Add(cmdSQL);
                }
                //Deleta os itens do pedido
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = $@"delete from tab_venda where id_pedido = @param_id_pedido;
                                    delete from tab_pedido_item where id_pedido =  @param_id_pedido;
                                    delete from tab_pedido where id_pedido = @param_id_pedido;
                                    "
                };
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_id_pedido",id_pedido);
                list.Add(cmdSQL);

                conexao.ExecutarTransacao(list);
            }
            catch (NpgsqlException)
            {
                throw;
            }
        }

    }
}
