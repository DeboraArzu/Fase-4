using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace Fase_3_Compiladores__tablas_
{
    public partial class Compilador : Form
    {
        #region variables
        string texto = "";
        bool bsets = false;
        #endregion
        string mens = "";
        public Compilador()
        {
            InitializeComponent();
        }

        static List<string> contenido = new List<string>();

        private void cARGARDOCUMENTOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ventana = new OpenFileDialog();
            ventana.Title = "SELECCIONE ARCHIVO";
            ventana.ShowDialog();
            this.rchtb_Texto.Clear();
            try
            {
                if (ventana.FileName != "")
                {
                    string root = ventana.FileName.ToString();
                    string[] lines = System.IO.File.ReadAllLines(root);
                    string ln = "";
                    foreach (string line in lines)
                    {
                        ln = ln + line + "\n";
                    }
                    this.rchtb_Texto.Text = ln + "  ";
                }
            }
            catch (IOException)
            {
                MessageBox.Show("ERROR EN LA LECTURA DEL ARCHIVO");
                throw;
            }
        }


        private void aNALIZARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            texto = this.rchtb_Texto.Text;
            fix();
            List<string> p = new List<string>();
            List<string> extra = new List<string>();
            rchtb_Texto.Select(0, rchtb_Texto.Text.Length);
            rchtb_Texto.SelectionBackColor = Color.Black;
            contenido.Clear();
            if (this.rchtb_Texto.Text != "")
            {
                Console.WriteLine(texto);
                Analisis gramatica = new Analisis(texto);
                mens = gramatica.analizar();
                if (mens != "")
                {
                    rchtb_Texto.Select(0, gramatica.posicion());
                    this.tokenstabla.Rows.Clear();
                    this.noterminalesdg.Rows.Clear();
                    this.noterminalesdg.Rows.Clear();
                    btnSave.Visible = false;
                    btnGenerar.Visible = false;
                }
                else
                {
                    rchtb_Texto.Select(0, gramatica.posicion());
                    mens = "Archivo analizado correctamente!!.";
                    NT Tabla2 = new NT(gramatica.getProduccion(), gramatica.TOK());
                    p = gramatica.TABLA1();
                    TablaTokensllenar(p);
                    contenido = escribir_TOK(p, gramatica.TOK(), gramatica.CONJ(), gramatica.TABLA_key());
                    this.NTTabla(Tabla2.CalcularNT());
                    produccionesT Tabla3 = new produccionesT(Tabla2.getValues(), gramatica.getSimbolos(), Tabla2.getelmentosproduccion());
                    Tproducciones(Tabla3.Elementos(), Tabla2.CalcularProducciones(), Tabla2.getValues(), gramatica.getContenidos(), gramatica.TOK());
                    btnSave.Visible = true;
                    btnGenerar.Visible = true;
                }
                MessageBox.Show(mens);
            }
            else
                MessageBox.Show("No hay archivo para cargar");
        }

        public void TablaTokensllenar(List<string> elementos)
        {
            this.tokenstabla.Rows.Clear();
            this.tokenstabla.Rows.Add(elementos.Count);
            int fila = 0;
            List<string> da = new List<string>();
            foreach (string part in elementos)
            {
                string[] diviciones = part.Split('‡');
                tokenstabla.Rows[fila].Cells[0].Value = diviciones[0];
                tokenstabla.Rows[fila].Cells[1].Value = diviciones[1];
                if (diviciones[2].Equals("0"))
                {
                    tokenstabla.Rows[fila].Cells[2].Value = "";
                }
                else
                {
                    tokenstabla.Rows[fila].Cells[2].Value = diviciones[2];
                }
                tokenstabla.Rows[fila].Cells[3].Value = diviciones[3];
                fila++;
            }
        }

        public void NTTabla(List<string> elementos)
        {
            this.noterminalesdg.Rows.Clear();
            this.noterminalesdg.Rows.Add(elementos.Count);
            int fila = 0;
            List<string> da = new List<string>();
            foreach (string part in elementos)
            {
                string[] diviciones = part.Split('‡');
                noterminalesdg.Rows[fila].Cells[0].Value = diviciones[0];
                noterminalesdg.Rows[fila].Cells[1].Value = diviciones[1];
                noterminalesdg.Rows[fila].Cells[2].Value = diviciones[3];
                noterminalesdg.Rows[fila].Cells[3].Value = diviciones[2];
                fila++;
            }
        }

        public static bool convertirnumero(object str)
        {
            bool isNumber;
            double isItNumeric;
            isNumber = Double.TryParse(Convert.ToString(str), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out isItNumeric);
            return isNumber;
        }

        public String ObtenerValores(List<int> x, Dictionary<string, int> Terminales, Dictionary<string, int> dtokensdef, Dictionary<string, string> dtoken)
        {
            string aux = "";
            if (x.Count > 0)
            {
                for (int i = 0; i < x.Count; i++)
                {
                    if (convertirnumero(x[i]))
                    {
                        if (x[i].ToString().Contains("-"))
                        {
                            foreach (string dato in Terminales.Keys)
                            {
                                if (Terminales[dato] == x[i] * (-1))
                                {
                                    aux += "[" + dato + "]" + "=>";
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (x[i].ToString().Contains("0"))
                            {
                                aux += "[" + "ԑ" + "]" + "=>";
                            }
                            else
                            {
                                foreach (string dato in dtokensdef.Keys)
                                {
                                    if (dtokensdef[dato] == x[i])
                                    {
                                        if (asw(dtoken, dato))
                                        {
                                            aux += "[" + dato + "]" + "=>";
                                            break;
                                        }
                                        else
                                        {
                                            aux += "[" + "'" + dato + "'" + "]" + "=>";
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                aux = aux.Substring(0, aux.Length - 2);
            }
            return aux;
        }

        public bool asw(Dictionary<string, string> tok, string dato)
        {
            foreach (string tokw in tok.Keys)
            {
                if (tokw.Equals(dato))
                {
                    return true;
                }
            }
            return false;
        }

        public void Tproducciones(List<List<int>> datos, Dictionary<int, int> pr, Dictionary<string, int> Terminales, Dictionary<string, int> tokens, Dictionary<string, string> tok)
        {
            dproductions.Rows.Clear();
            if (datos != null)
            {
                dproductions.Rows.Add(datos.Count);
                int fila = 0;
                foreach (List<int> s in datos)
                {
                    if (ObtenerValores(s, Terminales, tokens, tok).Contains("ԑ"))
                    {
                        dproductions[0, fila].Value = fila + 1;
                        dproductions[1, fila].Value = s.Count - 1;
                        dproductions[2, fila].Value = pr[fila + 1];
                        dproductions[3, fila].Value = ObtenerValores(s, Terminales, tokens, tok);
                    }
                    else
                    {
                        dproductions[0, fila].Value = fila + 1;
                        dproductions[1, fila].Value = s.Count;
                        dproductions[2, fila].Value = pr[fila + 1];
                        dproductions[3, fila].Value = ObtenerValores(s, Terminales, tokens, tok);
                    }
                    fila++;
                }
            }
        }

        private void FRMPRINCIPAL_Load(object sender, EventArgs e)
        {

        }

        public void writetabla(string ruta)
        {
            TextWriter sw = new StreamWriter(ruta);
            sw.WriteLine("[TABLA DE OPERADORES]");
            sw.Write("NO. TOKEN".PadRight(20, ' ') + "|");
            sw.Write("SIMBOLO".PadRight(20, ' ') + "|");
            sw.Write("PRECEDENCIA".PadRight(20, ' ') + "|");
            sw.WriteLine("ASOCIATIVIDAD".PadRight(20, ' ') + "|");
            string datos = "";
            for (int i = 0; i < tokenstabla.Rows.Count - 1; i++)
            {
                for (int j = 0; j < tokenstabla.Columns.Count; j++)
                {
                    datos += tokenstabla.Rows[i].Cells[j].Value.ToString().PadRight(20, ' ') + "|";
                }
                sw.WriteLine(datos);
                datos = "";
            }
            sw.WriteLine("\n");
            sw.WriteLine("[TABLA DE SIMBOLOS NO TERMINALES]");
            sw.Write("NUMERO".PadRight(35, ' ') + "|");
            sw.Write("SIMBOLO".PadRight(35, ' ') + "|");
            sw.Write("PRODUCCION".PadRight(35, ' ') + "|");
            sw.WriteLine("FIRST".PadRight(35, ' ') + "|");
            for (int i = 0; i < noterminalesdg.Rows.Count - 1; i++)
            {
                for (int j = 0; j < noterminalesdg.Columns.Count; j++)
                {
                    datos += noterminalesdg.Rows[i].Cells[j].Value.ToString().PadRight(35, ' ') + "|";
                }
                sw.WriteLine(datos);
                datos = "";
            }
            sw.WriteLine("\n");
            sw.WriteLine("[TABLA DE PRODUCCIONES]");
            sw.Write("PRODUCCION".PadRight(15, ' ') + "|");
            sw.Write("LONGITUD".PadRight(15, ' ') + "|");
            sw.Write("SIGUIENTE".PadRight(15, ' ') + "|");
            sw.WriteLine("ELEMENTOS".PadRight(35, ' ') + "|");
            for (int i = 0; i < dproductions.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dproductions.Columns.Count; j++)
                {
                    datos += dproductions.Rows[i].Cells[j].Value.ToString().PadRight(15, ' ') + "|";
                }
                sw.WriteLine(datos);
                datos = "";
            }
            sw.Close();
        }

        public List<string> escribir_TOK(List<string> ttoken, Dictionary<string, string> tokens, Dictionary<string, string> conjuntos, List<string> Lkeywords)
        {
            List<string> archivo = new List<string>();
            int num = 0;
            string ca = "";
            archivo.Add("Tokens");
            if (conjuntos.Count != 0)
            {
                foreach (string con in conjuntos.Keys)
                {
                    string datos = conjuntos[con];
                    archivo.Add("   " + con + "" + datos);
                }
            }
            for (int i = 0; i < ttoken.Count - 1; i++)
            {
                string co = "";
                string[] prue = ttoken[i].Split('‡');
                if (tokens.Keys.Contains(prue[1]))
                {
                    string[] k = tokens[prue[1]].Split('‡');
                    if (k[1] == "check")
                    {
                        num = i + 1;
                        co = "   " + "TOKEN " + num + " = " + k[0] + " [ RESERVADAS() ]";
                        archivo.Add(co);
                    }
                    else
                    {
                        num = i + 1;
                        co = "   " + "TOKEN " + num + " = " + k[0];
                        archivo.Add(co);
                    }
                }
                else
                {
                    char[] p = prue[1].ToArray();
                    string d = Obtenertokens(p);
                    num = i + 1;
                    co = "   " + "TOKEN " + num + " = " + d;
                    archivo.Add(co);
                }
            }
            archivo.Add("Acciones");
            archivo.Add("RESERVADAS()");
            archivo.Add("{");
            foreach (string key in Lkeywords)
            {
                num++;
                ca = "   " + num + " = " + "'" + key + "'";
                archivo.Add(ca);
            }
            archivo.Add("}");
            archivo.Add("Error = " + (num + 1));
            return archivo;
        }

        public String Obtenertokens(char[] elementos)
        {
            string aux = "";
            if (elementos.Length != 0)
            {
                for (int i = 0; i < elementos.Length; i++)
                {
                    if (!elementos[i].Equals('\''))
                    {
                        aux += "'" + elementos[i] + "'";
                    }
                    else
                    {
                        aux += "\"" + elementos[i] + "\"";
                    }
                }
            }
            return aux;
        }

        public void Llenar_tok(string ruta)
        {
            TextWriter sw = new StreamWriter(ruta);
            foreach (string linea in contenido)
            {
                sw.WriteLine(linea);
            }
            sw.Close();
        }

        #region metodos
        private void convertirtokens()
        {
            if (esets)
            {
                string pattern = @"tokens";
                string replacement = "";
                Regex rgx = new Regex(pattern);
                string result = rgx.Replace(texto, replacement);
                this.texto = "";
                this.texto = result;
            }
        }

        private void convertirsets()
        {
            if (esets)
            {
                string pattern = @"sets";
                string replacement = "tokens";
                Regex rgx = new Regex(pattern);
                string result = rgx.Replace(texto, replacement);
                texto = result;
            }
        }

        bool esets = false;
        private void conjuntos(string texto)
        {
            //-------------------------------------sets inicio-----------------------------------------------------------
            string pat = @"sets";
            int inicio = 0;
            esets = false;
            // Instantiate the regular expression object.
            Regex r = new Regex(pat, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.
            Match m = r.Match(texto);
            if (m.Success)
            {
                esets = true;
                inicio = m.Index;
            }
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
            string segundostr = texto.Substring(inicio, final2); //string que me interesa contiene la palabra sets
            string finalstr = texto.Substring(final); //contiene la palabra tokens
            string pattern, pattern2, replacement, replacement2, result = "";
            Regex rgx, rgx2, rgx3;
            //pre arreglo 
            if (esets)
            {
                pattern2 = @"(\'\.)(?!\.)";
                replacement2 = "\' .";
                rgx2 = new Regex(pattern2);
                result = rgx2.Replace(segundostr, replacement2);
                //Console.WriteLine(result);

                pattern = @"(\)\.)(?!\.)";
                replacement = ") .";
                rgx = new Regex(pattern);
                result = rgx.Replace(result, replacement);
                // Console.WriteLine(result);

                pattern = @"[^(""|\'|<|>)]=|=[^(""|\')]";
                replacement = "(";
                rgx3 = new Regex(pattern);
                result = rgx3.Replace(result, replacement);
                // Console.WriteLine(result);

                pattern = @"([^\'|\.|\)])\."; //((\'|\))\.)(?!(\.))
                replacement = ")";
                rgx = new Regex(pattern);
                result = rgx.Replace(result, replacement);
                // Console.WriteLine(result);
                segundostr = result;
            }
            // Console.WriteLine(segundostr);
            this.texto = primerstr + segundostr + finalstr;
            // Console.WriteLine(this.texto);
        }

        private void End()
        {
            string pat = @"end\.";
            // Instantiate the regular expression object.
            Regex r = new Regex(pat, RegexOptions.IgnoreCase);
            // Match the regular expression pattern against a text string.
            Match m = r.Match(texto);
            if (m.Success)
            {
                //pre arreglo  
                string pattern2 = @"end\.";
                string replacement2 = "";
                Regex rgx2 = new Regex(pattern2, RegexOptions.IgnoreCase);
                string result = rgx2.Replace(this.texto, replacement2);
                // Console.WriteLine(result);
                this.texto = result;
            }
            else
            {
                mens = "Falta End. al final del archivo";
                MessageBox.Show("Falta End. al final del archivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string pat2 = @"(start\=)";
            // Instantiate the regular expression object.
            Regex r2 = new Regex(pat2, RegexOptions.IgnoreCase);
            // Match the regular expression pattern against a text string.
            Match m2 = r2.Match(texto);
            if (m2.Success)
            {
                //pre arreglo  
                string pattern2 = @"(start\=)";
                string replacement2 = "start = ";
                Regex rgx2 = new Regex(pattern2, RegexOptions.IgnoreCase);
                string result = rgx2.Replace(this.texto, replacement2);
                // Console.WriteLine(result);
                this.texto = result;
            }
        }

        private void fix()
        {
            conjuntos(texto);
            convertirtokens();
            convertirsets();
            End();
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

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                writetabla(savefile.FileName + ".txt");
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Compilador_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                writetabla(savefile.FileName + ".txt");
            }
        }

        private void btnGenerar_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                Llenar_tok(savefile.FileName + ".txt");
            }
        }
    }
}
