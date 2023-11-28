using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCAcai.Negócios
{
    public class clsMunicipio
    {
        public clsEstado estado { get; set; }

        #region variaveis
        private DataSet retorno;
        public DataSet Retorno
        {
            get { return retorno; }
            set { retorno = value; }
        }

        private Int32 mun_codigo;
        public Int32 MUN_CODIGO
        {
            get { return mun_codigo; }
            set { mun_codigo = value; }
        }
        private String mun_descricao;
        public String MUN_DESCRICAO
        {
            get { return mun_descricao; }
            set { mun_descricao = value; }
        }
        private Decimal est_codigo;
        public Decimal EST_CODIGO
        {
            get { return est_codigo; }
            set
            {
                est_codigo = value;
                estado.EST_CODIGO = est_codigo;
                estado.BuscaUmRegistro();
            }
        }

        public string sReturn = null;
        clsConexao conexao = new clsConexao();
        NpgsqlCommand cmdSQL = new NpgsqlCommand();
        #endregion

        public clsMunicipio()
        {
            estado = new clsEstado();
        }

        public void Gravar()
        {
            try
            {
                cmdSQL.CommandType = CommandType.Text;
                cmdSQL.CommandText = "insert into tab_municipio(mun_codigo,mun_descricao,est_codigo)values(@param_mun_codigo,@param_mun_descricao,@param_est_codigo)";
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_mun_codigo", MUN_CODIGO);
                cmdSQL.Parameters.AddWithValue("@param_mun_descricao", MUN_DESCRICAO);
                cmdSQL.Parameters.AddWithValue("@param_est_codigo", EST_CODIGO);
                conexao.ExecutarComando(cmdSQL);
            }
            catch { throw; }
        }

        public void Atualizar()
        {
            try
            {
                cmdSQL.CommandType = CommandType.Text;
                cmdSQL.CommandText = "Update tab_municipio set mun_descricao=@param_mun_descricao,est_codigo=@param_est_codigo where mun_codigo=@param_mun_codigo";
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_mun_descricao", MUN_DESCRICAO);
                cmdSQL.Parameters.AddWithValue("@param_est_codigo", EST_CODIGO);
                cmdSQL.Parameters.AddWithValue("@param_mun_codigo", MUN_CODIGO);
                conexao.ExecutarComando(cmdSQL);
            }
            catch { throw; }
        }

        public bool Excluir()
        {
            bool retorno;
            try
            {
                cmdSQL.CommandType = CommandType.Text;
                cmdSQL.CommandText = "delete from tab_municipio where mun_codigo=@param_mun_codigo;";
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_mun_codigo", MUN_CODIGO);
                retorno = conexao.ExecutarComando_bool(cmdSQL);
            }
            catch (Exception) { retorno = false; throw; }
            return retorno;
        }

        public DataTable BuscaTodosMunicipios()
        {
            DataTable retorno;
            try
            {
                cmdSQL.CommandType = CommandType.Text;
                cmdSQL.CommandText = "select ibge as mun_codigo,descricao as mun_descricao,cod_uf as est_codigo,desc_uf from vie_municipios order by est_codigo, mun_descricao;";
                Retorno = conexao.RetornarDataSet(cmdSQL);
                if (Retorno != null && Retorno.Tables.Count > 0)
                {
                    Retorno.Tables[0].TableName = "tab_municipio";
                    retorno = Retorno.Tables[0];
                }
                else
                    retorno = null;
            }
            catch (Exception) { retorno = null; throw; }
            return retorno;
        }

        public DataTable BuscaMunicipiosdoEstado(int idEstado)
        {
            DataTable retorno;
            try
            {
                cmdSQL.CommandType = CommandType.Text;
                cmdSQL.CommandText = "select * from tab_municipio where est_codigo=@param_est_codigo order by mun_descricao;";
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_est_codigo", idEstado);
                Retorno = conexao.RetornarDataSet(cmdSQL);
                if (Retorno != null && Retorno.Tables.Count > 0)
                {
                    Retorno.Tables[0].TableName = "tab_municipio";
                    retorno = Retorno.Tables[0];
                }
                else
                    retorno = null;
            }
            catch (Exception) { retorno = null; throw; }
            return retorno;
        }

        public bool BuscaUmRegistro()
        {
            try
            {
                cmdSQL.CommandType = CommandType.Text;
                cmdSQL.CommandText = "select * from tab_municipio where mun_codigo=@param_id;";
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_id", MUN_CODIGO);
                Retorno = conexao.RetornarDataSet(cmdSQL);
                if (Retorno != null && Retorno.Tables.Count > 0)
                {
                    Retorno.Tables[0].TableName = "tab_municipio";
                    if (Retorno.Tables[0].Rows.Count > 0)
                    {
                        MUN_CODIGO = Retorno.Tables[0].Rows[0]["mun_codigo"].ToInt32();
                        MUN_DESCRICAO = Retorno.Tables[0].Rows[0]["mun_descricao"].ToString();
                        EST_CODIGO = Retorno.Tables[0].Rows[0]["est_codigo"].ToDecimal();
                        return true;
                    }
                }
                return false;
            }
            catch { throw; }
        }

        /// <summary>
        /// Busca o Município e retorna o código de seu respectivo Estado (UF)
        /// </summary>
        /// <returns></returns>
        public short BuscaEstado(int? iCodMunicipio)
        {
            try
            {
                cmdSQL.CommandType = CommandType.Text;
                cmdSQL.CommandText = "select est_codigo from tab_municipio where mun_codigo=@param_id;";
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_id", iCodMunicipio);
                Retorno = conexao.RetornarDataSet(cmdSQL);
                if (Retorno != null && Retorno.Tables.Count > 0)
                {
                    Retorno.Tables[0].TableName = "tab_municipio";
                    if (Retorno.Tables[0].Rows.Count > 0)
                        return Retorno.Tables[0].Rows[0]["est_codigo"].ToInt16();
                }
                return -1;
            }
            catch { throw; }
        }

        /// <summary>
        /// Verifica se o código IBGE está cadastrado
        /// </summary>
        /// <returns>Preenchido = Cadastrado / NULL = Não Cadastrado</returns>
        public string Verifica_IBGE(string sCodIbge)
        {
            try
            {
                sReturn = null;
                cmdSQL.CommandType = CommandType.Text;
                cmdSQL.CommandText = "select * from vie_municipios where ibge=@param_id;";
                cmdSQL.Parameters.Clear();
                cmdSQL.Parameters.AddWithValue("@param_id", sCodIbge);
                Retorno = conexao.RetornarDataSet(cmdSQL);
                if (Retorno != null && Retorno.Tables.Count > 0)
                {
                    Retorno.Tables[0].TableName = "vie_municipios";
                    if (Retorno.Tables[0].Rows.Count > 0)
                        return sReturn = Retorno.Tables[0].Rows[0]["descricao"].ToString() + "/" + Retorno.Tables[0].Rows[0]["desc_uf"].ToString();
                }
            }
            catch (Exception) { sReturn = null; throw; }
            return sReturn;
        }
    }
}
