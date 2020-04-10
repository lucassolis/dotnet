using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AttributeSolution.Attribute
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class PositionalAttribute : System.Attribute
    {
        public int Posicao { get; }
        public int Tamanho { get; }
        public char Complemento { get; }
        public string DataFormat { get; }
        public Int16 Precisao { get; }

        public PositionalAttribute(int posicao, int tamanho, char complemento, string dataFormat = "", Int16 precisao = 0)
        {
            Posicao = posicao;
            Tamanho = tamanho;
            Complemento = complemento;
            DataFormat = dataFormat;
            Precisao = precisao;
        }
        
        public static IEnumerable<string> Builder<T>(IEnumerable<T> layoutSaida)
        {
            var retorno = new List<string>();
            var linha = "";
            var valorTratado = "";
            
            foreach (var reg in layoutSaida)
            {
                linha = "";
                
                foreach (PropertyInfo p in reg.GetType().GetProperties().OrderBy(prop => prop.GetCustomAttribute<PositionalAttribute>().Posicao))
                {
                    foreach (System.Attribute a in p.GetCustomAttributes(false))
                    {
                        PositionalAttribute atributo = (PositionalAttribute) a;

                        if (p.PropertyType == typeof(DateTime))
                        {
                            if (atributo.DataFormat.Length > 0)
                            {
                                valorTratado = Convert.ToDateTime(p.GetValue(reg)).ToString(atributo.DataFormat).PadRight(atributo.Tamanho, atributo.Complemento);
                                linha += valorTratado.Length > atributo.Tamanho ? valorTratado.Substring(0, atributo.Tamanho) : valorTratado;
                            }
                        }
                        else if (p.PropertyType == typeof(decimal) || p.PropertyType == typeof(double) || p.PropertyType == typeof(float))
                        {
                            valorTratado = new Regex("[^0-9]").Replace(String.Format("{0:F" + atributo.Precisao + "}", p.GetValue(reg)), "").PadRight(atributo.Tamanho, atributo.Complemento);
                            linha += valorTratado.Length > atributo.Tamanho ? valorTratado.Substring(0, atributo.Tamanho) : valorTratado;
                        }
                        else
                        {
                            valorTratado = p.GetValue(reg).ToString().PadRight(atributo.Tamanho, atributo.Complemento);
                            linha += valorTratado.Length > atributo.Tamanho ? valorTratado.Substring(0, atributo.Tamanho) : valorTratado;
                        }
                    }
                }
                
                retorno.Add(linha);
            }
            
            return retorno;
        }
    }
}