using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using AttributeSolution.Attribute;

namespace AttributeSolution.Common
{
    public class Util
    {
        public static IEnumerable<string> ObjectToString<T>(IEnumerable<T> layoutSaida)
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


                        switch (Type.GetTypeCode(p.PropertyType))
                        {
                            case TypeCode.DateTime:
                                if (atributo.DataFormat.Length > 0)
                                {
                                    valorTratado = Convert.ToDateTime(p.GetValue(reg)).ToString(atributo.DataFormat).PadLeft(atributo.Tamanho, atributo.Complemento);
                                    linha += valorTratado.Length > atributo.Tamanho ? valorTratado.Substring(0, atributo.Tamanho) : valorTratado;
                                }
                                break;
                            case TypeCode.Decimal: 
                            case TypeCode.Double:
                                valorTratado = new Regex("[^0-9]").Replace(String.Format("{0:F" + atributo.Precisao + "}", p.GetValue(reg)), "").PadLeft(atributo.Tamanho, atributo.Complemento);
                                linha += valorTratado.Length > atributo.Tamanho ? valorTratado.Substring(0, atributo.Tamanho) : valorTratado;
                                break;
                            case TypeCode.Int16:
                            case TypeCode.Int32: 
                            case TypeCode.Int64:
                                valorTratado = p.GetValue(reg).ToString().PadLeft(atributo.Tamanho, atributo.Complemento);
                                linha += valorTratado.Length > atributo.Tamanho ? valorTratado.Substring(0, atributo.Tamanho) : valorTratado;
                                break;
                            case TypeCode.String:
                                valorTratado = p.GetValue(reg).ToString().PadRight(atributo.Tamanho, atributo.Complemento);
                                linha += valorTratado.Length > atributo.Tamanho ? valorTratado.Substring(0, atributo.Tamanho) : valorTratado;
                                break;
                        }
                    }
                }
                
                retorno.Add(linha);
            }
            
            return retorno;
        }
        
        public static IEnumerable<T> StringToObject<T>(IEnumerable<string> linhas, T classe)
        {
            var retorno = new List<T>();
            
            var i = 0;

            foreach (var linha in linhas)
            {
                i = 0;
                var item = Activator.CreateInstance(classe.GetType());
                
                foreach (PropertyInfo p in classe.GetType().GetProperties().OrderBy(prop => prop.GetCustomAttribute<PositionalAttribute>().Posicao))
                {
                    foreach (System.Attribute a in p.GetCustomAttributes(false))
                    {
                        PositionalAttribute atributo = (PositionalAttribute) a;

                        switch (Type.GetTypeCode(p.PropertyType))
                        {
                            case TypeCode.DateTime:
                                item.GetType().GetProperty(p.Name).SetValue(item, DateTime.ParseExact(linha.Substring(i, atributo.Tamanho), atributo.DataFormat, CultureInfo.InvariantCulture), null);
                                break;
                            case TypeCode.Decimal:
                                item.GetType().GetProperty(p.Name).SetValue(item, Convert.ToDecimal(
                                    (linha.Substring(i, atributo.Tamanho - atributo.Precisao) + "," + linha.Substring(i + atributo.Tamanho - atributo.Precisao, atributo.Precisao))
                                ), null);
                                break;
                            case TypeCode.Double:
                                item.GetType().GetProperty(p.Name).SetValue(item, Convert.ToDouble(
                                    (linha.Substring(i, atributo.Tamanho - atributo.Precisao ) + "," + linha.Substring(i + atributo.Tamanho - atributo.Precisao, atributo.Precisao))
                                    ), null);
                                break;
                            case TypeCode.Int16:
                                item.GetType().GetProperty(p.Name).SetValue(item, Convert.ToInt16(linha.Substring(i, atributo.Tamanho)), null);
                                break;
                            case TypeCode.Int32:
                                item.GetType().GetProperty(p.Name).SetValue(item, Convert.ToInt32(linha.Substring(i, atributo.Tamanho)), null);
                                break;
                            case TypeCode.Int64:
                                item.GetType().GetProperty(p.Name).SetValue(item, Convert.ToInt64(linha.Substring(i, atributo.Tamanho)), null);
                                break;
                            case TypeCode.String:
                                item.GetType().GetProperty(p.Name).SetValue(item, linha.Substring(i, atributo.Tamanho).Trim(), null);
                                break;
                        }
                        i += atributo.Tamanho;
                    }

                }
                retorno.Add((T) item);
            }
            
            return retorno;
        }
    }
}