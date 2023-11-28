using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCAcai.Negócios
{
    public class clsEstado
    {
        #region variaveis 
        public DataSet Retorno { get { return retorno; } set { retorno = value; } }
        private DataSet retorno;

        public Decimal EST_CODIGO { get { return est_codigo; } set { est_codigo = value; } }
        private Decimal est_codigo;
        public String EST_SIGLA { get { return est_sigla; } set { est_sigla = value; } }
        private String est_sigla;
        public String EST_NOME { get { return est_nome; } set { est_nome = value; } }
        private String est_nome;
        public Decimal EST_ALIQ_FCP { get { return est_aliq_fcp; } set { est_aliq_fcp = value; } }
        private Decimal est_aliq_fcp;
        public Boolean EST_EXCECAO_FCP { get { return est_excecao_fcp; } set { est_excecao_fcp = value; } }
        private Boolean est_excecao_fcp;

        public String EST_LINK_CONSULTA_NFCE { get => est_link_consulta_nfce; set => est_link_consulta_nfce = value; }
        private String est_link_consulta_nfce;
        public String EST_MODO_ENVIO_NFE { get => est_modo_envio_nfe; set => est_modo_envio_nfe = value; }
        private String est_modo_envio_nfe;
        public String EST_MODO_ENVIO_NFCE { get => est_modo_envio_nfce; set => est_modo_envio_nfce = value; }
        private String est_modo_envio_nfce;

        public bool bCadastraEstado = false;
        clsConexao conexao = new clsConexao();
        NpgsqlCommand cmdSQL = new NpgsqlCommand();
        #endregion

        public void Gravar()
        {
            try
            {
                if (bCadastraEstado)
                    Gravar_EstadoEnum();

                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = "insert into tab_estado(est_codigo,est_sigla,est_nome,est_aliq_fcp,est_excecao_fcp)values(@param_est_codigo,@param_est_sigla,@param_est_nome,@param_est_aliq_fcp,@param_est_excecao_fcp);"
                };
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_est_codigo", EST_CODIGO);
                cmdSQL.Parameters.AddWithValue("@param_est_sigla", EST_SIGLA);
                cmdSQL.Parameters.AddWithValue("@param_est_nome", EST_NOME);
                cmdSQL.Parameters.AddWithValue("@param_est_aliq_fcp", EST_ALIQ_FCP);
                cmdSQL.Parameters.AddWithValue("@param_est_excecao_fcp", EST_EXCECAO_FCP);
                conexao.ExecutarComando(cmdSQL);
            }
            catch { throw; }
        }

        void Gravar_EstadoEnum()
        {
            try
            {
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = "ALTER TYPE uf ADD VALUE '" + EST_SIGLA + "';"
                };
                conexao.ExecutarComando(cmdSQL);
                bCadastraEstado = false;
            }
            catch { throw; }
        }

        public void Atualizar()
        {
            try
            {
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = "Update tab_estado set est_sigla=@param_est_sigla,est_nome=@param_est_nome,est_aliq_fcp=@param_est_aliq_fcp,est_excecao_fcp=@param_est_excecao_fcp where est_codigo=@param_est_codigo;"
                };
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_est_sigla", EST_SIGLA);
                cmdSQL.Parameters.AddWithValue("@param_est_nome", EST_NOME);
                cmdSQL.Parameters.AddWithValue("@param_est_aliq_fcp", EST_ALIQ_FCP);
                cmdSQL.Parameters.AddWithValue("@param_est_excecao_fcp", EST_EXCECAO_FCP);
                cmdSQL.Parameters.AddWithValue("@param_est_codigo", EST_CODIGO);
                conexao.ExecutarComando(cmdSQL);
            }
            catch { throw; }
        }

        public bool Excluir()
        {
            try
            {
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = "delete from tab_estado where est_codigo=@param_est_codigo;"
                };
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_est_codigo", EST_CODIGO);
                return conexao.ExecutarComando_bool(cmdSQL);
            }
            catch { throw; }
        }

        public DataTable BuscaTodos()
        {
            try
            {
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = "select * from tab_estado order by est_nome;"
                };
                Retorno = conexao.RetornarDataSet(cmdSQL);
                if (Retorno != null && Retorno.Tables.Count > 0)
                {
                    Retorno.Tables[0].TableName = "tab_estado";
                    return Retorno.Tables[0];
                }
            }
            catch { throw; }
            return null;
        }

        public bool BuscaUmRegistro()
        {
            try
            {
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = "select * from tab_estado where est_codigo=@param_id;"
                };
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_id", EST_CODIGO);
                Retorno = conexao.RetornarDataSet(cmdSQL);
                if (Retorno != null && Retorno.Tables.Count > 0)
                {
                    Retorno.Tables[0].TableName = "tab_estado";
                    if (Retorno.Tables[0].Rows.Count > 0)
                    {
                        EST_CODIGO = Retorno.Tables[0].Rows[0]["est_codigo"].ToDecimal();
                        EST_SIGLA = Retorno.Tables[0].Rows[0]["est_sigla"].ToString();
                        EST_NOME = Retorno.Tables[0].Rows[0]["est_nome"].ToString();
                        EST_ALIQ_FCP = Retorno.Tables[0].Rows[0]["est_aliq_fcp"].ToDecimal();
                        EST_EXCECAO_FCP = Retorno.Tables[0].Rows[0]["est_excecao_fcp"].ToBoolean();
                        EST_LINK_CONSULTA_NFCE = Retorno.Tables[0].Rows[0]["est_link_consulta_nfce"].ToString();
                        EST_MODO_ENVIO_NFE = Retorno.Tables[0].Rows[0]["est_modo_envio_nfe"].ToString();
                        EST_MODO_ENVIO_NFCE = Retorno.Tables[0].Rows[0]["est_modo_envio_nfce"].ToString();
                        return true;
                    }
                }
                return false;
            }
            catch { throw; }
        }

        public DataTable BuscaEstado()
        {
            try
            {
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = "select *, null as per_sequencia from tab_estado order by est_sigla::text;"
                };
                Retorno = conexao.RetornarDataSet(cmdSQL);
                if (Retorno != null && Retorno.Tables.Count > 0)
                {
                    Retorno.Tables[0].TableName = "tab_estado";
                    return Retorno.Tables[0];
                }
            }
            catch { throw; }
            return null;
        }

        /// <summary>
        /// Busca todos os estados cadastrados, exceto o Exterior. 
        /// Mostra também uma coluna (est_sigla_nome) com a sigla e descrição do estado (Ex.: SP - São Paulo)
        /// </summary>
        public DataTable BuscaEstado_SemExterior()
        {
            try
            {
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = "select *, est_sigla ||' - '|| est_nome as est_sigla_nome from tab_estado where est_codigo != 99 order by est_sigla::text;"
                };
                Retorno = conexao.RetornarDataSet(cmdSQL);
                if (Retorno != null && Retorno.Tables.Count > 0)
                {
                    Retorno.Tables[0].TableName = "tab_estado";
                    return Retorno.Tables[0];
                }
            }
            catch { throw; }
            return null;
        }

        public bool Verifica_Estado(string sEstadoSigla)
        {
            try
            {
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = "select * from tab_estado where est_sigla=@param_estado;"
                };
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_estado", sEstadoSigla);
                Retorno = conexao.RetornarDataSet(cmdSQL);
                if (Retorno != null && Retorno.Tables.Count > 0)
                {
                    Retorno.Tables[0].TableName = "tab_estado";
                    return (Retorno.Tables[0].Rows.Count > 0);
                }
            }
            catch { throw; }
            return false;
        }

        /// <summary>
        /// Verifica se o código IBGE do estado está cadastrado
        /// </summary>
        /// <returns>TRUE = Cadastrado / FALSE = Não Cadastrado</returns>
        public string Verifica_IBGE(string sCodIbge)
        {
            try
            {
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = "select est_codigo, est_nome from tab_estado where est_codigo=@param_id;"
                };
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_id", sCodIbge);
                Retorno = conexao.RetornarDataSet(cmdSQL);
                if (Retorno != null && Retorno.Tables.Count > 0)
                {
                    Retorno.Tables[0].TableName = "tab_estado";
                    if (Retorno.Tables[0].Rows.Count > 0)
                    {
                        return Retorno.Tables[0].Rows[0]["est_codigo"].ToString() + "-" + Retorno.Tables[0].Rows[0]["est_nome"].ToString();
                    }
                }
            }
            catch { throw; }
            return null;
        }

        /// <summary>
        /// Verifica se o código IBGE do estado está cadastrado e retorna a sigla
        /// </summary>
        /// <returns>Sigla do estado</returns>
        public string Retorna_Sigla(string sCodIbge)
        {
            try
            {
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = "select est_sigla from tab_estado where est_codigo=@param_id;"
                };
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_id", sCodIbge.Enum_GetDescription_Value<VariaveisGlobal.UF>().Enum_FromDescription_ID<VariaveisGlobal.UF>());
                Retorno = conexao.RetornarDataSet(cmdSQL);
                if (Retorno != null && Retorno.Tables.Count > 0)
                {
                    Retorno.Tables[0].TableName = "tab_estado";
                    if (Retorno.Tables[0].Rows.Count > 0)
                        return Retorno.Tables[0].Rows[0]["est_sigla"].ToString();
                }
            }
            catch { throw; }
            return null;
        }

        /// <summary>
        /// Verifica se o estado está cadastrado no Enum UF
        /// </summary>
        /// <returns>TRUE = Cadastrado / FALSE = Não Cadastrado</returns>
        public bool Verifica_Enum_UF(string sEstado)
        {
            try
            {
                cmdSQL = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = "select * from vie_enum_lista where enum_name=@param_enum AND enum_value=@param_uf;"
                };
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_enum", "uf");
                cmdSQL.Parameters.AddWithValue("@param_uf", sEstado);
                Retorno = conexao.RetornarDataSet(cmdSQL);
                if (Retorno != null && Retorno.Tables.Count > 0)
                {
                    Retorno.Tables[0].TableName = "vie_enum_lista";
                    return Retorno.Tables[0].Rows.Count > 0;
                }
            }
            catch { throw; }
            return false;
        }
    }
}
