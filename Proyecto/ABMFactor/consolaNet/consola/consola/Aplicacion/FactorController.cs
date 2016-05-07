using consola.Context;
using consola.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consola.Aplicacion
{
    class FactorController
    {
        public List<Factor> Listar()
        {
            using (FactorContext db = new FactorContext())
            {
                List<Factor> lista = db.Factores.ToList();

                if (lista.Count() == 0)
                {
                    Console.WriteLine("No hay factores creados");
                }

                return lista;
            }
        }

        public List<Factor> ListarHabilitados()
        {
            using (FactorContext db = new FactorContext())
            {
                List<Factor> lista = db.Factores.Where(c => c.estado == 1).ToList();

                if (lista.Count() == 0)
                {
                    Console.WriteLine("No hay factores Habilitados para modificar");
                }

                return lista;
            }
        }

        public List<Factor> ListarInhabilitados()
        {
            using (FactorContext db = new FactorContext())
            {
                List<Factor> lista = db.Factores.Where(c => c.estado == 0).ToList();

                if (lista.Count() == 0)
                {
                    Console.WriteLine("No hay factores Inhabilitados para modificar");
                }

                return lista;
            }
        }

        public string Agragar(String nombre, String[] descripcion,int[] valor)
        {
            using (FactorContext db = new FactorContext())
            {
                String mensaje = "";
                if (nombre.Equals(""))
                {
                    mensaje += "# Nombre vacio \n";
                }
                foreach (var dato in db.Factores)
                {
                    if(String.Compare(dato.Nombre,nombre) == 0)
                    {
                        mensaje += "# El nombre \'" + nombre + "\' ya existe \n";
                    }
                }
                if (nombre.Length > 50)
                {
                    mensaje += "# El nombre no puede tener mas de 50 caracteres \n";
                }
                if (descripcion[0].Equals(""))
                {
                    mensaje += "# Descripcio1 vacia \n";
                }
                if (descripcion[0].Length > 50)
                {
                    mensaje += "# La descripcion1 no puede tener mas de 50 caracteres \n";
                }
                if (descripcion[1].Equals(""))
                {
                    mensaje += "# Descripcio2 vacia \n";
                }
                if (descripcion[1].Length > 50)
                {
                    mensaje += "# La descripcion2 no puede tener mas de 50 caracteres \n";
                }
                if (String.Compare(descripcion[0],descripcion[1]) == 0)
                {
                    mensaje += "# La descripcion2 \'" + descripcion[1] + "\' ya existe \n";
                }
                if (descripcion[2].Length > 50)
                {
                    mensaje += "# La descripcion3 no puede tener mas de 50 caracteres \n";
                }
                if (descripcion[2].Equals(""))
                {
                    mensaje += "# Descripcio3 vacia \n";
                }
                foreach (var dato in db.Valores)
                {
                    if (String.Compare(descripcion[0],descripcion[2]) == 0 || String.Compare(descripcion[1],descripcion[2]) == 0)
                    {
                        mensaje += "# La descripcion3 \'" + descripcion[2] + "\' ya existe \n";
                    }
                }
                if (valor[0].Equals(null))
                {
                    mensaje += "# Valor1 vacio \n";
                }
                if (valor[0] < 0 || valor[0] > 2)
                {
                    mensaje += "# Valor1 fuera de rango \n";
                }
                if (valor[0].Equals(null))
                {
                    mensaje += "# Valor2 vacio \n";
                }
                if (valor[1] < 0 || valor[1] > 2)
                {
                    mensaje += "# Valor2 fuera de rango \n";
                }
                if (valor[2].Equals(null))
                {
                    mensaje += "# Valor3 vacio \n";
                }
                if (valor[2] < 0 || valor[2] > 2)
                {
                    mensaje += "# Valor3 fuera de rango \n";
                }
                if (mensaje.Equals(""))
                {
                    try
                    {
                        Factor factor = new Factor();
                        factor = new Factor
                        {
                            Nombre = nombre,
                            estado = 1
                        };

                        db.Factores.Add(factor);

                        Valor[] arrayValor = new Valor[3];
                        arrayValor[0] = new Valor
                        {
                            Descripcion = descripcion[0],
                            valor = valor[0]
                        };
                        arrayValor[1] = new Valor
                        {
                            Descripcion = descripcion[1],
                            valor = valor[1]
                        };
                        arrayValor[2] = new Valor
                        {
                            Descripcion = descripcion[2],
                            valor = valor[2]
                        };

                        db.Valores.Add(arrayValor[0]);
                        db.Valores.Add(arrayValor[1]);
                        db.Valores.Add(arrayValor[2]);
                        db.SaveChanges();

                        mensaje = "Datos Grabados Correctamente";
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Se ha producido el siguiente error: {0}", e);
                    }
                }
                return mensaje;
            }
        }

        public string Modificar(String nuevoValor,int factorID,int opcion)
        {
            using (FactorContext db = new FactorContext())
            {
                String mensaje = "";
                if (opcion == 0)
                {
                    if (nuevoValor.Equals(""))
                    {
                        mensaje += "# Campo vacio \n";
                    }
                    foreach (var dato in db.Factores)
                    {
                        if (String.Compare(dato.Nombre,nuevoValor,true) == 0)
                        {
                            mensaje += "# El nombre \'" + nuevoValor + "\' ya existe \n";
                        }
                    }
                    if (nuevoValor.Length > 50)
                    {
                        mensaje += "# El nombre debe ser menor a 50 caracteres \n";
                    }
                    if (mensaje == "")
                    {
                        try
                        {
                            var factorEdit = db.Factores.Find(factorID);
                            factorEdit.Nombre = nuevoValor;
                            db.SaveChanges();
                            mensaje = "Datos Grabados Correctamente";
                        }
                        catch (Exception e)
                        {
                            mensaje = "Se ha producido el siguiente error: " + e;
                        }
                    }
                }
                else 
                {
                    if (nuevoValor.Equals(""))
                    {
                        mensaje += "# Descripcio1 vacia \n";
                    }
                    var descripciones = db.Valores.Where(c => c.FactorId == factorID && c.ValorId != opcion);
                    foreach (var dato in descripciones)
                    {
                        if (String.Compare(dato.Descripcion,nuevoValor,true) == 0)
                        {
                            mensaje += "# Descripcio \'" + nuevoValor + "\' ya existe en el favor \n";
                        } 
                    }
                    if (nuevoValor.Length > 50)
                    {
                        mensaje += "# La descripcion debe ser menor a 50 caracteres \n";
                    }
                    if (mensaje == "")
                    {
                        try
                        {
                            var valorEdit = db.Valores.Find(opcion,factorID);
                            valorEdit.Descripcion = nuevoValor;
                            db.SaveChanges();
                            mensaje = "Datos Grabados Correctamente";
                        }
                        catch (Exception e)
                        {
                            mensaje = "Se ha producido el siguiente error: " + e;
                        }
                    }
                }
                return mensaje;
            }
        }

        public Factor Consultar(int id)
        {

            using (FactorContext db = new FactorContext())
            {
                Factor obj = db.Factores.Find(id);
                try
                {
                    if (obj.Equals(null))
                    {
                        Console.WriteLine("Opcion no valida");
                    }     
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ha ocurrido el siguiente error: {0}", e);
                }

                return obj;
            }
        }

        public List<Valor> ValoresDeFactor(int id)
        {
            using (FactorContext db = new FactorContext())
            {
                List<Valor> lista = db.Valores.Where(c => c.FactorId == id).ToList();

                return lista;
            }
        }

        public String habilitarFactor(int id)
        {
            using (FactorContext db = new FactorContext())
            {
                try
                {
                    var factor = db.Factores.Find(id);
                    factor.estado = 1;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ha ocurrido el siguiente error: {0}", e);
                }

                return "El factor se ha habilitado exitosamente";
            }
        }

        public String inhabilitarFactor(int id)
        {
            using (FactorContext db = new FactorContext())
            {
                try
                {
                    var factor = db.Factores.Find(id);
                    factor.estado = 0;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ha ocurrido el siguiente error: {0}", e);
                }

                return "El factor se ha inhabilitado exitosamente";
            }
        }
    }
}
