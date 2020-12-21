using ExamenFinal.Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamenFinal.Dato
{
    class TextureData
    {
        List<TextureModel> lista = new List<TextureModel>();
        public void Guardar(TextureModel modelo)
        {
            lista.Add(modelo);
        }
        public void Editar(String n, int r, int g, int b)
        {
            bool stado = false;
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].name == n)
                {
                    stado = true;
                    lista[i].name = n;

                    if (lista[i].R1 >= r){
                        lista[i].R1 = r;
                    }else{
                        lista[i].R2 = r;
                    }
                    if (lista[i].G1 >= g){
                        lista[i].G1 = g;
                    }else{
                        lista[i].G2 = g;
                    }
                    if (lista[i].B1 >= b){
                        lista[i].B1 = b;
                    }else{
                        lista[i].B2 = b;
                    }
                }
            }
            if (!stado)
            {
                TextureModel modelo = new TextureModel()
                {
                    name = n,
                    R1 = r,
                    R2 = r,
                    G1 = g,
                    G2 = g,
                    B1 = b,
                    B2 = b,
                };
                lista.Add(modelo);
            }
        }
        public List<TextureModel> Consultar()
        {
            return lista;
        }
    }
}
