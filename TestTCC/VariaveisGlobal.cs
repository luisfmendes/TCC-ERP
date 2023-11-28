using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCCAcai.Negócios;

namespace TCCAcai
{
    public class VariaveisGlobal
    {
        public enum TipoOperacao { Gravar, Alterar, Consultar}

        public enum eTipoMascara { CPF, CNPJ, RG, IE, PIS, CNAE, CFOP, PLACA, CEP, TELEFONE, CELULAR, TELEFONE_SEMDDD, CELULAR_SEMDDD }
        /// <summary>
        /// Dados de todos os estados do brasil e EX para exterior
        /// </summary>
        public enum UF
        {
            [Description("RONDÔNIA")]
            RO = 11,
            [Description("ACRE")]
            AC = 12,
            [Description("AMAZONAS")]
            AM = 13,
            [Description("RORAIMA")]
            RR = 14,
            [Description("PARÁ")]
            PA = 15,
            [Description("AMAPÁ")]
            AP = 16,
            [Description("TOCANTINS")]
            TO = 17,
            [Description("MARANHÃO")]
            MA = 21,
            [Description("PIAUÍ")]
            PI = 22,
            [Description("CEARÁ")]
            CE = 23,
            [Description("RIO GRANDE DO NORTE")]
            RN = 24,
            [Description("PARAÍBA")]
            PB = 25,
            [Description("PERNAMBUCO")]
            PE = 26,
            [Description("ALAGOAS")]
            AL = 27,
            [Description("SERGIPE")]
            SE = 28,
            [Description("BAHIA")]
            BA = 29,
            [Description("MINAS GERAIS")]
            MG = 31,
            [Description("ESPÍRITO SANTO")]
            ES = 32,
            [Description("RIO DE JANEIRO")]
            RJ = 33,
            [Description("SÃO PAULO")]
            SP = 35,
            [Description("PARANÁ")]
            PR = 41,
            [Description("SANTA CATARINA")]
            SC = 42,
            [Description("RIO GRANDE DO SUL")]
            RS = 43,
            [Description("MATO GROSSO DO SUL")]
            MS = 50,
            [Description("MATO GROSSO")]
            MT = 51,
            [Description("GOIÁS")]
            GO = 52,
            [Description("DISTRITO FEDERAL")]
            DF = 53,
            [Description("EXTERIOR")]
            EX = 99
        }

        public enum TipoMensagem
        {
            Alerta,
            Erro,
            Informacao
        }
        public static List<char> KeyCode_EnterEscBackColar = new List<char>() { (char)Keys.Back, (char)Keys.Enter, (char)Keys.Escape, (char)22 };
        public static List<char> KeyCode_EnterEsc = new List<char>() { (char)Keys.Enter, (char)Keys.Escape };
        public static List<Keys> KeyCode_Navegacao = new List<Keys>() { Keys.Right, Keys.Left, Keys.Up, Keys.Down, Keys.PageUp, Keys.PageDown, Keys.Home, Keys.End };
        public static bool bCEP_auto = true;

        public static DataTable dados_cidade
        {
            get
            {
                try
                {
                    if (dadoscidade == null)
                    {
                        clsMunicipio cidade = new clsMunicipio();
                        dadoscidade = cidade.BuscaTodosMunicipios();
                    }
                    return dadoscidade;
                }
                catch { throw; }
            }
        }
        private static DataTable dadoscidade;
        
    }
}
