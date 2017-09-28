using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Fase_3_Compiladores__tablas_
{
    class Acciones
    {
        #region metodos
        bool bsets = false;
        string texto = "";
        private void convertirtokens(string texto)
        {
            if (bsets == true)
            {
                string pattern = @"tokens";
                string replacement = "";
                Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
                string result = rgx.Replace(texto, replacement);

                //Console.WriteLine("Original String: {0}", texto);
                //Console.WriteLine("Replacement sin token: {0}", result);
                this.texto = result;
            }
            // this.texto = texto;
            //  Console.WriteLine(" token: {0}", this.texto);
        }

        private void convertirend(string texto)
        {
            string pattern = @"end.";
            string replacement = "";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            string result = rgx.Replace(texto, replacement);

            // Console.WriteLine("Original String: {0}", texto);
            //Console.WriteLine("Replacement String: {0}", result);
            this.texto = result;
        }

        private void convcheck(string texto)
        {
            string pattern = @"check.";
            string replacement = ".";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            string result = rgx.Replace(texto, replacement);
            this.texto = result;
        }

        private void convertirsets(string texto)
        {
            if (bsets == true)
            {
                string pattern = @"sets";
                string replacement = "tokens";
                Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
                string result = rgx.Replace(texto, replacement);

                // Console.WriteLine("Original String: {0}", texto);
                //Console.WriteLine("Replacement String: {0}", result);
                this.texto = result;
            }
            else
                this.texto = texto;
            Console.WriteLine("final: {0}", this.texto);
        }

        private void conjuntos(string texto)
        {
            string pat = @"sets";
            int inicio = 0;
            // Instantiate the regular expression object.
            Regex r = new Regex(pat, RegexOptions.IgnoreCase);
            // Match the regular expression pattern against a text string.
            Match m = r.Match(texto.ToLower());
            if (m.Success)
            {
                bsets = true;
                //-------------------------------------sets inicio-----------------------------------------------------------
                inicio = m.Index;
                //----------------------------------------toknes final --------------------------------------------------------
                pat = @"tokens";
                int final = 0;
                int final2 = 0;
                // Instantiate the regular expression object.
                r = new Regex(pat, RegexOptions.IgnoreCase);

                // Match the regular expression pattern against a text string.
                m = r.Match(texto);
                if (m.Success)
                {
                    final = m.Index;
                    final2 = final - inicio;
                }
                //------------------------------------------------------------------------------------------------------------
                string primerstr = texto.Substring(0, inicio);
                string segundostr = texto.Substring(inicio, final2); //string que me interesa
                string finalstr = texto.Substring(final);

                Console.WriteLine("segundostr", segundostr);
                string pattern = @"[^(""|\'|<|>)]=|=[^(""|\')]";
                string replacement = "(";
                Regex rgx = new Regex(pattern);
                string result = rgx.Replace(segundostr, replacement);

                // Console.WriteLine("Replacement segundostr: {0}", result);

                pattern = @"([^\'|\.|\)])\.";
                replacement = ")";
                rgx = new Regex(pattern);
                result = rgx.Replace(result, replacement);

                // Console.WriteLine("Replacement String 2: {0}", result);

                segundostr = result;

                this.texto = primerstr + segundostr + finalstr;
                //Console.WriteLine("final: {0}", this.texto);
            }

        }

        private void fix()
        {
            conjuntos(texto);
            convertirtokens(texto);
            convertirend(texto);
            convcheck(texto);
            keyworks(texto);
            convertirsets(texto);
        }

        private void keyworks(string texto)
        {
            string pat = @"keywords";
            int inicio = 0;
            // Instantiate the regular expression object.
            Regex r = new Regex(pat, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.
            Match m = r.Match(texto.ToLower());
            if (m.Success)
            {
                //-------------------------------------keywords inicio-----------------------------------------------------------
                inicio = m.Index;
                //----------------------------------------producciones final --------------------------------------------------------
                pat = @"productions";
                int final = 0;
                int final2 = 0;
                // Instantiate the regular expression object.
                r = new Regex(pat, RegexOptions.IgnoreCase);

                // Match the regular expression pattern against a text string.
                m = r.Match(texto);
                if (m.Success)
                {
                    final = m.Index;
                    final2 = final - inicio;
                }
                //------------------------------------------------------------------------------------------------------------
                string primerstr = texto.Substring(0, inicio);
                string segundostr = texto.Substring(inicio, final2); //string que me interesa
                string finalstr = texto.Substring(final);

                segundostr = "";

                this.texto = primerstr + segundostr + finalstr;
                Console.WriteLine("final: {0}", this.texto);
            }


        }
        #endregion
    }

}
