using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Fase_3_Compiladores__tablas_
{
    class NT
    {
        List<int> keynostart;
        List<int> keystart;
        Dictionary<int, ElementoProduccion> ContenidoProduccion;
        Dictionary<string, int> Valores = new Dictionary<string, int>();
        Dictionary<string, string> tok;
        public NT(Dictionary<int, ElementoProduccion> t, Dictionary<string, string> tok)
        {
            ContenidoProduccion = t;
            this.tok = tok;
            keynostart = new List<int>();
            keystart = new List<int>();
            this.ArreglarContenido();
        }
        public Dictionary<int, ElementoProduccion> getelmentosproduccion()
        {
            return ContenidoProduccion;
        }

        public Dictionary<string, int> getValues()
        {
            return Valores;
        }

        #region agregar starts
        void ArreglarContenido()
        {//metodo para arreglar el contenido
            int [] llaves = this.ContenidoProduccion.Keys.ToArray();//copia de las llaves
            foreach (int i in llaves)
            {
                ElementoProduccion iterator = this.ContenidoProduccion [i];
                string s = iterator.Get_llave();
                if (s.CompareTo("<start>") == 0)
                {
                    keystart.Add(i);
                }
                else
                    keynostart.Add(i);
            }
            Dictionary<int, ElementoProduccion> nuevo = new Dictionary<int, ElementoProduccion>();
            List<int> temp1 = new List<int>();
            List<int> temp2 = new List<int>();
            int llavesStar = 1;//comienza a asignar desde 1 a las producciones que comeincen con start
            int llaves_Sin_Star = keystart.Count() + 1;//contador de llaves sin <Start>, inician despues de contar cuantas producciones poseen start
            foreach (int i in llaves)
            {
                if (keystart.Contains(i))
                {
                    nuevo.Add(llavesStar, this.ContenidoProduccion [i]);
                    temp1.Add(llavesStar);
                    llavesStar++;
                }
                else
                {
                    nuevo.Add(llaves_Sin_Star, this.ContenidoProduccion [i]);
                    temp2.Add(llaves_Sin_Star);
                    llaves_Sin_Star++;
                }
            }
            this.ContenidoProduccion = nuevo;
            this.keystart.Clear();
            this.keynostart.Clear();
            this.keystart = temp1;//se asignan las nuevas llaves a las listas de control 
            this.keynostart = temp2;//para las que poseen las llaves de los que tienen start y los que no
        }

        public bool asw(string dato)
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

        public string firstDatos(List<string> first)
        {
            string aux = "";
            foreach (string valorFisrt in first)
            {
                if (asw(valorFisrt))
                {
                    aux += "[" + valorFisrt + "]" + "=>";
                }
                else
                {
                    if (valorFisrt.Equals("ԑ"))
                    {
                        aux += "[" + valorFisrt + "]" + "=>";
                    }
                    else
                    {
                        aux += "[" + "'" + valorFisrt + "'" + "]" + "=>";
                    }
                }
            }
            aux = aux.Substring(0, aux.Length - 2);
            return aux;
        }
        #endregion

        public List<string> CalcularNT()
        {//metodo a corregir metodo a veces funciona a veces no
            int [] llaves = this.ContenidoProduccion.Keys.ToArray();
            Dictionary<int, string> datos = new Dictionary<int, string>();
            List<string> resp = new List<string>();
            List<string> pendientes = new List<string>();
            List<int> subllaves = new List<int>();
            foreach (int item in llaves)
            {
                ElementoProduccion iterator = this.ContenidoProduccion [item];
                string s = iterator.Get_llave();//la llave que devuelve es el <id> en minusculas
                if (!datos.ContainsValue(s))
                {
                    datos.Add(item, s);
                    subllaves.Add(item);
                }

            }
            Dictionary<string, List<string>> LISTAFIRST = this.CalcularFirst();
            foreach (int item in subllaves)
            {//se asignan a los que ya fueron instanciados en la gramatica en formato numero, no terminal, first, produccion
                List<string> first = LISTAFIRST [datos [item].ToString()];
                string FIRST = firstDatos(first);
                string s = item.ToString() + '‡' + datos [item].ToString() + '‡' + FIRST + '‡' + " ";
                resp.Add(s);
            }
            int [] ar1 = subllaves.ToArray();
            int subllaveError = -1;
            foreach (int i in ar1)
            {
                if (i > subllaveError)
                {
                    subllaveError = i;
                }
            }
            if (subllaveError < 0)
            {
                subllaveError = 0;
            }
            subllaveError++;//sieguiente llave para los pendientes o los de error, como los quiera ver uno
            foreach (string item in resp)
            {//para obtener los <pendientes>, los que no se declararon con ->
                String [] r = item.Split('‡');
                int i = Convert.ToInt32(r [0]);
                ElementoProduccion iterator = this.ContenidoProduccion [i];
                List<string> contenido = iterator.listagetcontenido();
                foreach (string x in contenido)
                {
                    bool b = (this.bRango(x));
                    if (b == true && datos.Values.Contains(x) == false && pendientes.Contains(x) == false)
                    {
                        pendientes.Add(x);
                    }
                }
            }
            String [] raux = pendientes.ToArray();
            int c = 0;
            foreach (string i in raux)
            {
                raux [c] = subllaveError.ToString() + '‡' + raux [c] + '‡' + " " + '‡' + "ERROR";
                subllaveError++;
                c++;
            }
            pendientes.Clear();
            pendientes = raux.ToList();
            c = 1;
            //calular la produccion de los demas
            string [] subresp = resp.ToArray();
            int icc = 0;
            foreach (string st in subresp)//dentro de un foreach para que lo haga con cada elemetno
            {
                string [] myr = st.Split('‡');//
                myr [3] = getnumeroProd(myr [1], c, llaves);
                subresp [icc] = myr [0] + '‡' + myr [1] + '‡' + myr [2] + '‡' + myr [3];
                Valores.Add(myr [1], Convert.ToInt32(myr [0]));
                icc++;
                c++;
            }
            //luego aca concatenar y retornar
            resp.Clear();
            resp = subresp.ToList();
            resp = this.OrdenarResp(resp);
            string [] pendientealfinal = pendientes.ToArray();
            foreach (string i in pendientealfinal)
            {
                string [] myr = i.Split('‡');
                Valores.Add(myr [1], Convert.ToInt32(myr [0]));
                resp.Add(i);
            }

            return resp;
        }
        bool bRango(string s)//<id>
        {
            int A = 65;
            int a = 97;
            int Z = 90;
            int z = 122;
            int _0 = 48;
            int _9 = 57;
            int _ = 95;
            char [] palabra = s.ToCharArray();
            try
            {
                int contador = 0, otro = palabra.Length - 1;
                bool inicio, final, contenido;
                inicio = contenido = final = false;
                foreach (char c in palabra)
                {
                    if (contador == 0 && (c == '<'))
                    {
                        inicio = true;
                    }
                    else if ((contador == 0 && (c != '<')))
                    {
                        inicio = false;
                    }
                    else if (contador == 1 && contador != otro && ((c >= a && c <= z) || (c >= A && c <= Z)))
                    {
                        contenido = true;
                    }
                    else if ((contador == 1 && contador != otro && !((c >= a && c <= z) || (c >= A && c <= Z))))
                    {
                        contenido = false;
                    }
                    else if (contador > 1 && contador != otro && ((c >= a && c <= z) || (c >= A && c <= Z) || (c >= _0 && c <= _9) || c == _))
                    {
                        contenido = true;
                    }
                    else if ((contador > 1 && contador != otro && !((c >= a && c <= z) || (c >= A && c <= Z) || (c >= _0 && c <= _9) || c == _)))
                    {
                        contenido = false;
                    }
                    else if (contador > 1 && contador == otro && c == '>')
                    {
                        final = true;
                    }
                    else if ((contador > 1 && contador == otro && c != '>'))
                    {
                        final = false;
                    }
                    contador++;
                }

                if (inicio == true && contenido == inicio && final == inicio)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        List<string> OrdenarResp(List<string> t)
        {
            List<string> listaOrdenada = new List<string>();
            int [] subllabesStart = this.keystart.ToArray();
            if (t.Count != 0)
            {
                List<int> control = new List<int>();
                foreach (int i in subllabesStart)//deprimero ordenamos en base a las llaves de los start
                {
                    foreach (string x in t)
                    {
                        string [] myr = x.Split('‡');//separa el contenido
                        int aux = Convert.ToInt32(myr [0]);
                        if (aux == i && control.Contains(i) == false)
                        {
                            control.Add(i);
                            listaOrdenada.Add(x);
                        }
                    }
                }
                subllabesStart = this.keynostart.ToArray();//se recicla el mismo arreglo, solo que lo lleno de las otras laves
                foreach (int i in subllabesStart)//luego de los que no posean start
                {
                    foreach (string x in t)
                    {
                        string [] myr = x.Split('‡');//separa el contenido
                        int aux = Convert.ToInt32(myr [0]);
                        if (aux == i && control.Contains(i) == false)
                        {
                            control.Add(i);
                            listaOrdenada.Add(x);
                        }
                    }
                }
                t = listaOrdenada;
            }
            return t;
        }
        string getnumeroProd(string s, int v, int [] ikeys)
        { //retorna el # de la produccion de la cadena enviada, asumiendo que se envia <id>
            try
            {
                int produccion = v;
                foreach (int item in ikeys)
                {//numero, no terminal, first, produccion
                    ElementoProduccion iterator = this.ContenidoProduccion [item];
                    string keu = iterator.Get_llave().ToLower();
                    if (keu.CompareTo(s) == 0)
                    {
                        return produccion.ToString();
                    }
                    string aux = iterator.EnqNivelLoPosee(s, produccion);

                    produccion = Convert.ToInt32(aux) - 1;
                }
                return produccion.ToString();
            }
            catch (Exception)
            {
                return "";
                throw;
            }

        }


        #region Calculador de produccion y siguiente produccion
        int Get_No_SiguienteProduccionRepetida(int intllave, string palabra)
        {
            int resp = 1;
            int [] llaves = this.ContenidoProduccion.Keys.ToArray();
            foreach (int i in llaves)
            {
                ElementoProduccion iterator = this.ContenidoProduccion [i];
                if (iterator.Get_llave().ToLower().CompareTo(palabra) == 0 && intllave == i)
                {
                    return resp;
                }
                resp = iterator.Calculador_de_nivelesV2(resp);

                resp++;
            }
            return resp;
        }
        public Dictionary<int, int> CalcularProducciones()
        {
            int [] llaves = this.ContenidoProduccion.Keys.ToArray();//contiene todo sobre las llaves, sin importar si es o no repetida
            Dictionary<int, string> datos = new Dictionary<int, string>();//no repetidos
            List<int> intsubllaves = new List<int>();//llaves enteras de los elementos no repetidos
            List<int> intLlavesDeLosRepetidos = new List<int>();//llaves int de los repetidos
            List<string> subllaves = new List<string>();//llaves de los elementos no repetidos
            List<string> llavesStrdelosRepetidos = new List<string>();//llaves de los repetidos
            Dictionary<int, int> RESPUESTA = new Dictionary<int, int>();
            foreach (int item in llaves)
            {//se leen las producciones y se obtienen por separado las llaves de los no repetidos y de los repetidos, pero loas llaves <id> del diccionario
                ElementoProduccion iterator = this.ContenidoProduccion [item];
                string s = iterator.Get_llave();//la llave que devuelve es el <id> en minusculas
                if (!datos.ContainsValue(s))
                {
                    datos.Add(item, s);
                    subllaves.Add(s);//datos incertados si aparecen almenos 1 vez
                    intsubllaves.Add(item);
                }
                else
                {
                    llavesStrdelosRepetidos.Add(s);
                    intLlavesDeLosRepetidos.Add(item);
                }
            }
            int c = 1;//lleva el conteo de la produccion
            //se calcula la siguiente produccion
            foreach (int item in llaves)
            {//volvemos a leer todas las producciones de la gramatica
                ElementoProduccion iterator = this.ContenidoProduccion [item];
                string s = iterator.Get_llave();//la llave que devuelve es el <id> en minusculas
                if (llavesStrdelosRepetidos.Contains(s))//si las tiene se procede a calcular como llave de los  repetidos
                {
                    string [] StrsRepetidos = llavesStrdelosRepetidos.ToArray();
                    int [] llaverepetidas = intLlavesDeLosRepetidos.ToArray();
                    int j = 0;
                    bool Inserto = false;
                    foreach (string strReps in StrsRepetidos)
                    {
                        if (strReps.CompareTo(s) == 0 && Inserto == false)
                        {

                            int V = this.Get_No_SiguienteProduccionRepetida(llaverepetidas [j], StrsRepetidos [j]);
                            if (iterator.Get_Cantidad_de_niveles() == 1)
                            {
                                RESPUESTA.Add(c, V);
                            }
                            else
                            { //cuando tiene mas de un nivel
                                char separador = Convert.ToChar(1333);
                                string [] r = iterator.Get_SiguieteProduccionPara_repetidos(c, separador, V).Split(separador);//en el arreglo la ultima posicion es la del nivel actual
                                int newnivel = r.Length - 1;//obtiene la posicion del nivel
                                int contador = 0;
                                c = Convert.ToInt32(r [newnivel]);
                                foreach (string ix in r)
                                {
                                    if (contador < r.Length && ix != "")
                                    {
                                        string [] s1 = r [contador].Split(' ');//primera pos es el nivel, siguiente es quien le toca
                                        if (s1.Length != 1)
                                        {
                                            RESPUESTA.Add(Convert.ToInt32(s1 [0]), Convert.ToInt32(s1 [1]));
                                        }
                                    }
                                    contador++;
                                }
                            }

                            llavesStrdelosRepetidos.Remove(StrsRepetidos [j]);//PARA QUE QUITE LOS REPETIDOS QUE YA UTILIZO
                            intLlavesDeLosRepetidos.Remove(llaverepetidas [j]);
                            Inserto = true;
                        }
                        j++;
                    }
                }
                else
                {
                    #region se calcula como llave de los no repetidos
                    if (iterator.Get_Cantidad_de_niveles() == 1)//si es una declarasion sin | y que no este repetida
                    {
                        RESPUESTA.Add(c, 0);
                    }
                    else
                    {//es cuando nopesee almenos otro nivel y no se repite
                        char separador = Convert.ToChar(1333);
                        string [] r = iterator.Get_SiguieteProduccionPara_No_repetidos(c, separador).Split(separador);//en el arreglo la ultima posicion es la del nivel actual
                        int newnivel = r.Length - 1;//obtiene la posicion del nivel
                        int contador = 0;
                        c = Convert.ToInt32(r [newnivel]);
                        foreach (string ix in r)
                        {
                            if (contador < r.Length && ix != "")
                            {
                                string [] s1 = r [contador].Split(' ');//primera pos es el nivel, siguiente es quien le toca
                                if (s1.Length != 1)
                                {
                                    RESPUESTA.Add(Convert.ToInt32(s1 [0]), Convert.ToInt32(s1 [1]));
                                }
                            }
                            contador++;
                        }
                    }
                    #endregion
                }
                c++;
            }
            return RESPUESTA;
        }
        #endregion

        public Dictionary<string, List<string>> CalcularFirst()
        { //metodo para el inicio del calculo del first

            ELEMENTOS_y_Fisrt Lista_Elemtos_y_First = new ELEMENTOS_y_Fisrt();//calse que maneja llave <[id=(string)],fisrt>, maneja las inserciones repetidas
            ELEMENTOS_y_Fisrt Lista_Elemtos_y_First2 = new ELEMENTOS_y_Fisrt();//calse que maneja llave <[id=(string)],fisrt>, maneja las inserciones repetidas
            // y la sobrecarga del first, tanto como el first vacio u otros errores
            int [] llaves = this.ContenidoProduccion.Keys.ToArray();
            List<int> NoSimples = new List<int>();
            foreach (int item in llaves)//se mandan a calcular los que tienen first simple
            {
                ElementoProduccion iterator = this.ContenidoProduccion [item];
                if (iterator.SFrist())// si la produccion posee un first simple, calcular fisrt simples primero
                {
                    List<string> firstSimple = iterator.CalcularFirstSimple().ToList<string>();
                    iterator.set_My_Fisrt(firstSimple);
                    Lista_Elemtos_y_First.set_elementos(iterator.Get_llave(), firstSimple.ToList());
                }
                else
                {
                    NoSimples.Add(item);//seguardan las llaves de las producciones que no lo contiene de manera simple
                }
            }
            foreach (int item in NoSimples)//calculamos el first por individual de cada complejo
            {//calculo inicial
                ElementoProduccion iterator = this.ContenidoProduccion [item];
                String [] pendientes = iterator.GpendFirst().ToArray();
                foreach (string i in pendientes)
                {
                    if (this.bRango(i))
                    {
                        Lista_Elemtos_y_First.set_elementos(iterator.Get_llave(), Lista_Elemtos_y_First.Get_First_de_un_elemento(i));
                    }
                    else
                    {
                        Lista_Elemtos_y_First.set_elemento(iterator.Get_llave(), i);
                    }
                }
            }
            bool cambio = true;
            while (cambio == true)
            {
                Lista_Elemtos_y_First2 = Lista_Elemtos_y_First.CompiaDeActual();

                foreach (int item in NoSimples)//calculamos el first por individual de cada complejo
                {//miemros si la lista cambio
                    ElementoProduccion iterator = this.ContenidoProduccion [item];
                    String [] pendientes = iterator.GpendFirst().ToArray();
                    foreach (string i in pendientes)
                    {
                        if (this.bRango(i))
                        {
                            Lista_Elemtos_y_First.set_elementos(iterator.Get_llave(), Lista_Elemtos_y_First.Get_First_de_un_elemento(i));

                        }
                        else
                        {
                            Lista_Elemtos_y_First.set_elemento(iterator.Get_llave(), i);
                        }
                    }
                }
                if (Lista_Elemtos_y_First.EsigualAOtro(Lista_Elemtos_y_First2))//para ver si dejara de calcularlo
                {//dejara de calcularlo hasta que la Lista_Elemtos_y_First antes de volverlo a calcular sea igual a la restualten despues de calcular
                    cambio = false;
                }
            }
            List<string> anulables = Lista_Elemtos_y_First.Los_anulables().ToList();// porque ya se tiene el first de todos los complejos sin ver anulables
            cambio = true;

            while (cambio == true)//calculo hasta que no haya anulables
            {
                Lista_Elemtos_y_First2 = Lista_Elemtos_y_First.CompiaDeActual();

                foreach (int item in NoSimples)//calculamos el first por individual de cada complejo
                {//miemros si la lista cambio
                    ElementoProduccion iterator = this.ContenidoProduccion [item];
                    String [] pendientes = iterator.GetpenFirst2(anulables).ToArray();

                    foreach (string i in pendientes)
                    {
                        if (this.bRango(i))
                        {
                            Lista_Elemtos_y_First.set_elementos(iterator.Get_llave(), Lista_Elemtos_y_First.Get_First_de_un_elemento(i));

                        }
                        else
                        {
                            Lista_Elemtos_y_First.set_elemento(iterator.Get_llave(), i);
                        }
                    }
                }
                if (Lista_Elemtos_y_First.EsigualAOtro(Lista_Elemtos_y_First2))//para ver si dejara de calcularlo
                {//dejara de calcularlo hasta que la Lista_Elemtos_y_First antes de volverlo a calcular sea igual a la restualten despues de calcular
                    cambio = false;
                }
            }






            foreach (int item in NoSimples)//calculamos el first por individual de cada complejo
            {//calculo inicial
                ElementoProduccion iterator = this.ContenidoProduccion [item];
                String [] pendientes = iterator.GpendFirst().ToArray();
                foreach (string i in pendientes)
                {
                    if (this.bRango(i))
                    {
                        Lista_Elemtos_y_First.set_elementos(iterator.Get_llave(), Lista_Elemtos_y_First.Get_First_de_un_elemento(i));
                    }
                    else
                    {
                        Lista_Elemtos_y_First.set_elemento(iterator.Get_llave(), i);
                    }
                }
            }


            foreach (int item in NoSimples)//calculamos el first por individual de cada complejo
            {//calculo inicial
                ElementoProduccion iterator = this.ContenidoProduccion [item];
                String [] pendientes = iterator.GpendFirst().ToArray();
                foreach (string i in pendientes)
                {
                    if (this.bRango(i))
                    {
                        Lista_Elemtos_y_First.set_elementos(iterator.Get_llave(), Lista_Elemtos_y_First.Get_First_de_un_elemento(i));
                    }
                    else
                    {
                        Lista_Elemtos_y_First.set_elemento(iterator.Get_llave(), i);
                    }
                }
            }


            foreach (int item in NoSimples)//calculamos el first por individual de cada complejo
            {//calculo inicial
                ElementoProduccion iterator = this.ContenidoProduccion [item];
                String [] pendientes = iterator.GpendFirst().ToArray();
                foreach (string i in pendientes)
                {
                    if (this.bRango(i))
                    {
                        Lista_Elemtos_y_First.set_elementos(iterator.Get_llave(), Lista_Elemtos_y_First.Get_First_de_un_elemento(i));
                    }
                    else
                    {
                        Lista_Elemtos_y_First.set_elemento(iterator.Get_llave(), i);
                    }
                }
            }






            foreach (int item in NoSimples)//calculamos el first por individual de cada complejo
            {//calculo inicial
                ElementoProduccion iterator = this.ContenidoProduccion [item];
                String [] pendientes = iterator.GpendFirst().ToArray();
                foreach (string i in pendientes)
                {
                    if (this.bRango(i))
                    {
                        Lista_Elemtos_y_First.set_elementos(iterator.Get_llave(), Lista_Elemtos_y_First.Get_First_de_un_elemento(i));
                    }
                    else
                    {
                        Lista_Elemtos_y_First.set_elemento(iterator.Get_llave(), i);
                    }
                }
            }
            foreach (int item in NoSimples)//calculamos el first por individual de cada complejo
            {//calculo inicial
                ElementoProduccion iterator = this.ContenidoProduccion [item];
                String [] pendientes = iterator.GpendFirst().ToArray();
                foreach (string i in pendientes)
                {
                    if (this.bRango(i))
                    {
                        Lista_Elemtos_y_First.set_elementos(iterator.Get_llave(), Lista_Elemtos_y_First.Get_First_de_un_elemento(i));
                    }
                    else
                    {
                        Lista_Elemtos_y_First.set_elemento(iterator.Get_llave(), i);
                    }
                }
            }







            Dictionary<string, List<string>> FirstResultantes = new Dictionary<string, List<string>>();
            foreach (int item in llaves)//se mandan a calcular los que tienen first simple
            {
                ElementoProduccion iterator = this.ContenidoProduccion [item];
                if (!FirstResultantes.ContainsKey(iterator.Get_llave()))
                {
                    if (iterator.Get_llave().CompareTo("<start>") == 0)
                    {
                        string [] elements = Lista_Elemtos_y_First.Get_First_de_un_elemento(iterator.Get_llave()).ToArray();
                        List<string> sinEpsilon = new List<string>();
                        string epsilon = Convert.ToString("ԑ");
                        foreach (string s in elements)
                        {
                            /// if (s.CompareTo(epsilon) != 0)
                            ///{
                            sinEpsilon.Add(s);
                            ///}
                        }
                        FirstResultantes.Add(iterator.Get_llave(), sinEpsilon.ToList());
                    }
                    else
                    {
                        FirstResultantes.Add(iterator.Get_llave(), Lista_Elemtos_y_First.Get_First_de_un_elemento(iterator.Get_llave()).ToList());
                    }
                }
            }
            return FirstResultantes;
        }

    }
    class ElementoProduccion
    {
        List<List<string>> contenido;
        string llave;//es la declarrasion <id>-> solo a la parte del <id>
        //nueva seccion de estructuras para contener el first
        List<string> MY_first;//contiene el firt por elemento de la produccion
        public ElementoProduccion(string key, List<List<string>> contenido)
        {
            this.llave = key;
            this.contenido = contenido;
            MY_first = new List<string>();
        }
        public void set_My_Fisrt(List<string> t)
        {
            this.MY_first = t;
        }
        public List<string> GetFirst()
        {
            return this.MY_first;
        }
        public List<string> listagetcontenido()
        {
            List<string> resp = new List<string>();

            foreach (var item in this.contenido)
            {//aquie se entra a la lista de listas
                List<string> ob = item.ToList<string>();
                foreach (string i in ob)
                {
                    resp.Add(i);
                }
            }
            return resp;
        }
        public string EnqNivelLoPosee(string s, int v)
        {
            foreach (var item in this.contenido)
            {//aquie se entra a la lista de listas
                List<string> ob = item.ToList<string>();
                v++;
            }
            return v.ToString();
        }
        public List<List<string>> Get_Contenindo()
        {
            return this.contenido;
        }
        public string Get_llave()
        {
            return this.llave;
        }
        /*aqui hacer el metodo get leng por producciones, llevando el conteo de las mismas*/

        public int Get_Cantidad_de_niveles()
        {//retorna la cantidad de listas que posee, cada valor entero representa la cantidad de | que lo conforman
            return this.contenido.Count;
        }

        public string Get_SiguieteProduccionPara_No_repetidos(int nivel, char separador)
        {
            int i = 1;
            string resp = "";
            foreach (var item in this.contenido)
            {//aquie se entra a la lista de listas
                if (i < contenido.Count)
                {
                    resp = resp + separador + nivel + " " + (nivel + 1).ToString();
                    nivel++;
                }
                else
                {
                    resp = resp + separador + nivel + " " + 0 + separador + nivel;
                    return resp;
                }
                i++;
            }
            return resp;
        }
        public string Get_SiguieteProduccionPara_repetidos(int nivel, char separador, int v)
        {
            int i = 1;
            string resp = "";
            foreach (var item in this.contenido)
            {//aquie se entra a la lista de listas
                if (i < contenido.Count)
                {
                    resp = resp + separador + nivel + " " + (nivel + 1).ToString();
                    nivel++;
                }
                else
                {
                    resp = resp + separador + nivel + " " + v + separador + nivel;
                    return resp;
                }
                i++;
            }
            return resp;
        }
        public int Calculador_de_nivelesV2(int v)
        {
            v = v - 1;//se le quita el calculo del nivel adicional
            foreach (var item in this.contenido)
            {//aquie se entra a la lista de listas
                v++;
            }
            return v;
        }
        /*Metodo para facilitar ver o predecir el calculo del first de aquellas producciones que es simpe de calcular, si la produccion es compleja retornara false,
         por complejido es refirdo a que en alguna produccion de las que posee como elemtos contiene inicialmente <id> como primer elemento de alguna de sus listas de
         listas*/
        public bool SFrist()
        {
            bool resp = true;
            foreach (var item in this.contenido)// contenido es List<List<string>> contenido
            {//aquie se entra a la lista de listas, porque item es List<string>, cada lista que posee contenido
                List<string> ob = item.ToList<string>();
                int cont = 1;
                foreach (string i in ob)//ob es la lista de string, porque cada elemento que posee predecira si es simple el first o no
                {
                    if (cont == 1 && esunidad(i))
                    {
                        resp = false;
                        return resp;
                    }
                    cont++;
                }
            }
            return resp;
        }
        public List<string> CalcularFirstSimple()
        {
            List<string> resp = new List<string>();
            foreach (var item in this.contenido)// contenido es List<List<string>> contenido
            {//aquie se entra a la lista de listas, porque item es List<string>, cada lista que posee contenido
                List<string> ob = item.ToList<string>();
                int cont = 1;
                foreach (string i in ob)//ob es la lista de string, porque cada elemento que posee predecira si es simple el first o no
                {
                    if (cont == 1)//para solo insertar el primer elemento
                    {
                        if (!resp.Contains(i))//si no contenia al elemento en el fisrt, lo coloca
                        {
                            resp.Add(i);
                        }
                    }
                    cont++;
                }
            }
            return resp;
        }
        bool esunidad(string u)
        {
            int A = 65;
            int a = 97;
            int Z = 90;
            int z = 122;
            int _0 = 48;
            int _9 = 57;
            int _ = 95;
            char [] palabra = u.ToCharArray();
            try
            {
                int contador = 0, otro = palabra.Length - 1;
                bool inicio, final, contenido;
                inicio = contenido = final = false;
                foreach (char c in palabra)
                {
                    if (contador == 0 && (c == '<'))
                    {
                        inicio = true;
                    }
                    else if ((contador == 0 && (c != '<')))
                    {
                        inicio = false;
                    }
                    else if (contador == 1 && contador != otro && ((c >= a && c <= z) || (c >= A && c <= Z)))
                    {
                        contenido = true;
                    }
                    else if ((contador == 1 && contador != otro && !((c >= a && c <= z) || (c >= A && c <= Z))))
                    {
                        contenido = false;
                    }
                    else if (contador > 1 && contador != otro && ((c >= a && c <= z) || (c >= A && c <= Z) || (c >= _0 && c <= _9) || c == _))
                    {
                        contenido = true;
                    }
                    else if ((contador > 1 && contador != otro && !((c >= a && c <= z) || (c >= A && c <= Z) || (c >= _0 && c <= _9) || c == _)))
                    {
                        contenido = false;
                    }
                    else if (contador > 1 && contador == otro && c == '>')
                    {
                        final = true;
                    }
                    else if ((contador > 1 && contador == otro && c != '>'))
                    {
                        final = false;
                    }
                    contador++;
                }

                if (inicio == true && contenido == inicio && final == inicio)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public List<string> GpendFirst()
        {
            List<string> resp = new List<string>();
            foreach (var item in this.contenido)// contenido es List<List<string>> contenido
            {//aquie se entra a la lista de listas, porque item es List<string>, cada lista que posee contenido
                List<string> ob = item.ToList<string>();
                int cont = 1;
                foreach (string i in ob)//ob es la lista de string, porque cada elemento que posee predecira si es simple el first o no
                {
                    if (cont == 1)//para solo insertar el primer elemento
                    {
                        if (!resp.Contains(i))//si no contenia al elemento en el fisrt, lo coloca
                        {
                            resp.Add(i);
                        }
                    }
                    cont++;
                }
            }
            return resp;
        }

        public List<string> GetpenFirst2(List<string> anulables)
        {
            List<string> resp = new List<string>();
            bool terminar = false;
            foreach (var item in this.contenido)// contenido es List<List<string>> contenido
            {//aquie se entra a la lista de listas, porque item es List<string>, cada lista que posee contenido
                List<string> ob = item.ToList<string>();
                foreach (string i in ob)//ob es la lista de string, porque cada elemento que posee predecira si es simple el first o no
                {

                    if (anulables.Contains(i) && terminar == false)
                    {
                        resp.Add(i);
                    }
                    if (!anulables.Contains(i) && terminar == false)
                    {
                        resp.Add(i);
                        terminar = true;
                    }
                }
            }
            return resp;
        }
    }

    class ELEMENTOS_y_Fisrt
    {
        public ELEMENTOS_y_Fisrt CompiaDeActual()
        {
            ELEMENTOS_y_Fisrt iterador = new ELEMENTOS_y_Fisrt();
            string [] l = this.lista.Keys.ToArray();
            foreach (string x in l)
            {
                List<string> x2 = this.Get_First_de_un_elemento(x).ToList();
                iterador.set_elementos(x, x2);
            }
            return iterador;
        }
        public bool EsigualAOtro(ELEMENTOS_y_Fisrt i)
        {
            bool resp = true;
            if (i.lista.Count != this.lista.Count)
            {
                return false;
            }//si pasa esto es porque tiene la misma cantodad de elementos
            string [] llaves1 = this.lista.Keys.ToArray();
            List<string> llaves2 = i.lista.Keys.ToList<string>();
            string [] k1 = this.lista.Keys.ToArray();
            string [] k2 = i.lista.Keys.ToArray();
            foreach (string x in llaves1)
            {
                if (!llaves2.Contains(x))//si no tiene una llave del primero son diferentes
                {
                    return false;
                }
            }
            //comparando elementos dele origninal con La copia
            foreach (string item in llaves1)
            {
                foreach (string item2 in llaves2)
                {
                    List<string> r1 = this.lista [item].get_First();
                    List<string> r2 = i.lista [item2].get_First();
                    if (item.CompareTo(item2) == 0)
                    {
                        resp = this.ArreglosIguales(r1, r2);
                        if (resp == false)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        bool ArreglosIguales(List<string> r1, List<string> r2)
        {
            if (r1.Count == r2.Count)
            {
                foreach (string it in r2)
                {
                    if (!r1.Contains(it))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        Dictionary<string, LISTA_FIRST> lista;//contendra <elemtento,fisrt>
        public ELEMENTOS_y_Fisrt()
        {
            lista = new Dictionary<string, LISTA_FIRST>();
        }
        public void set_elementos(string s, List<string> otralist)
        {
            LISTA_FIRST newFirts = new LISTA_FIRST();
            newFirts.setElementos(otralist.ToList<string>());
            if (!this.lista.ContainsKey(s))
            {
                this.lista.Add(s, newFirts);
            }
            else
            {
                lista [s].setElementos(otralist.ToList<string>());
            }
        }
        public void set_elemento(string s, string element)
        {
            LISTA_FIRST newFirts = new LISTA_FIRST();
            newFirts.setElemento(element);
            if (!this.lista.ContainsKey(s))
            {
                this.lista.Add(s, newFirts);
            }
            else
            {
                lista [s].setElemento(element);

            }
        }
        public List<string> Get_First_de_un_elemento(string s)
        {
            List<string> nula = new List<string>();
            if (lista.ContainsKey(s))
            {
                return lista [s].get_First();
            }
            return nula;
        }
        public string Get_First_de_un_elemento_En_string(string s)
        {
            List<string> nula = new List<string>();
            if (lista.ContainsKey(s))
            {
                return lista [s].Get_Firts_Formato();
            }
            return "[ ]";
        }
        public List<string> Los_anulables()
        {
            List<string> resp = new List<string>();
            string [] llaves = lista.Keys.ToArray();
            foreach (string l in llaves)
            {
                if (lista [l].Es_anulable())//si es anulable, guardarlo
                {
                    resp.Add(l);
                }
            }
            return resp;
        }
    }

    class LISTA_FIRST
    {//lista que contendra el first, esta inserta los elementos sin repetir
        public bool Es_anulable()
        {
            string epsilon = Convert.ToString("ԑ");
            foreach (string i in elementos)
            {
                if (i.CompareTo(epsilon) == 0)
                {
                    return true;
                }
            }
            return false;//no encontro epislon es anulable
        }
        List<string> elementos = new List<string>();
        public void setElementos(List<string> lista)
        {
            foreach (string i in lista)
            {
                if (!elementos.Contains(i))
                {
                    elementos.Add(i);
                }
            }
        }
        public void setElemento(string elemto)
        {

            if (!elementos.Contains(elemto))
            {

                elementos.Add(elemto);
            }
        }
        public List<string> get_First()
        {
            return this.elementos.ToList();
        }
        public string Get_Firts_Formato()
        {
            string resp = "";
            int cont = 1;
            foreach (string i in elementos)
            {
                if (cont == 1)
                {
                    resp = i;
                }
                else if (cont == elementos.Count && elementos.Count == 2)
                {
                    resp = resp + " ," + i;
                }
                else if (cont < elementos.Count && elementos.Count != 1 && elementos.Count > 2)
                {
                    resp = resp + " ," + i + " ,";
                }
                else if (cont == elementos.Count && elementos.Count > 2)
                {
                    resp = resp + i;
                }
                cont++;
            }
            return "[ " + resp + " ]";
        }
    }
}
