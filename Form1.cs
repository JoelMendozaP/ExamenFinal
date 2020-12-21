using ExamenFinal.Dato;
using ExamenFinal.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamenFinal
{
    public partial class Form1 : Form
    {
        int index = 0;
        string colorR = "";
        string colorG = "";
        string colorB = "";
        DataTable tabla;
        DataTable tabla2;
        Bitmap imagenGlobal;
        TextureData dato = new TextureData();
        public Form1()
        {
            InitializeComponent();
            //Iniciar();
            Iniciar2();
            Consultar();
        }
        private void CargarImagen()
        {
            //openFileDialog1.Filter = "Imagenes PNG|*.png,*.jpg";
            openFileDialog1.ShowDialog();
            if (openFileDialog1 != null)
            {
                imagenGlobal = new Bitmap(openFileDialog1.FileName);
                image.Image = imagenGlobal;
            }
        }
        private void AgregarColor()
        {
            if (imagenGlobal == null)
            {
                CargarImagen();
            }
            else
            {
                if (nameTexture.Text.Equals("") || colorR.Equals(""))
                {
                    //vacio
                    //rLabel.Text = "vacio";
                }
                else
                {
                    //rLabel.Text = "lleno";
                    //tiene contenido
                    int cr = Int32.Parse((string)colorR);
                    int cg = Int32.Parse((string)colorG);
                    int cb = Int32.Parse((string)colorB);
                    dato.Editar(nameTexture.Text, cr, cg, cb);
                    Consultar();
                }
            }
        }
        private void Iniciar()
        {
            tabla = new DataTable();
            tabla.Columns.Add("Nombre");
            tabla.Columns.Add("R1");
            tabla.Columns.Add("R2");
            tabla.Columns.Add("G1");
            tabla.Columns.Add("G2");
            tabla.Columns.Add("B1");
            tabla.Columns.Add("B2");
            dataGridView1.DataSource = tabla;
        }
        private void Iniciar2()
        {
            tabla2 = new DataTable();
            tabla2.Columns.Add("Concidencias");
            tabla2.Columns.Add("Probabilidad");
            dataGridView2.DataSource = tabla2;
        }
        private void carga_Click(object sender, EventArgs e)
        {
            CargarImagen();
        }
        private void Consultar()
        {
            dataGridView1.DataSource = null;
            Iniciar();
            foreach (var item in dato.Consultar())
            {
                DataRow fila = tabla.NewRow();
                fila["Nombre"] = item.name;
                fila["R1"] = item.R1;
                fila["R2"] = item.R2;
                fila["G1"] = item.G1;
                fila["G2"] = item.G2;
                fila["B1"] = item.B1;
                fila["B2"] = item.B2;
                tabla.Rows.Add(fila);
            }
            dataGridView1.DataSource = tabla;
        }
        private void image_MouseClick(object sender, MouseEventArgs e)
        {
            if (imagenGlobal == null)
            {
                CargarImagen();
            }
            else
            {
                Bitmap bmp = new Bitmap(image.Image);
                Color c = new Color();
                c = bmp.GetPixel(e.X, e.Y);
                colorR = c.R.ToString();
                colorG = c.G.ToString();
                colorB = c.B.ToString();
                rLabel.Text = colorR;
                gLabel.Text = colorG;
                bLabel.Text = colorB;
                buscarRadio(e.X, e.Y);
            }
        }
        private void buscarRadio(int x, int y)
        {
            Bitmap b = new Bitmap(image.Image);
            Bitmap bc = new Bitmap(b.Width, b.Height);
            Color c = new Color();
            dataGridView2.DataSource = null;
            Iniciar2();
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    c = b.GetPixel(i, j);
                    if (i > x + 10 || i < x - 10 || j > y + 10 || j < y - 10)
                    {
                        bc.SetPixel(i, j, Color.FromArgb(c.R, c.G, c.B));
                    }
                    else
                    {
                        bc.SetPixel(i, j, Color.FromArgb(c.R, c.G, c.B));
                        //bc.SetPixel(i, j, Color.FromArgb(255, 0, 0));
                        adicionarEncontrado(c.R, c.G, c.B);
                    }
                }
            }
            image.Image = bc;
        }
        private void adicionarEncontrado(int r, int g, int b)
        {
            if (imagenGlobal == null)
            {
                CargarImagen();
            }
            else
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    int c1 = Int32.Parse((string)dataGridView1.Rows[i].Cells[1].Value);
                    int c2 = Int32.Parse((string)dataGridView1.Rows[i].Cells[2].Value);
                    int c3 = Int32.Parse((string)dataGridView1.Rows[i].Cells[3].Value);
                    int c4 = Int32.Parse((string)dataGridView1.Rows[i].Cells[4].Value);
                    int c5 = Int32.Parse((string)dataGridView1.Rows[i].Cells[5].Value);
                    int c6 = Int32.Parse((string)dataGridView1.Rows[i].Cells[6].Value);
                    //Pintar(c1, c2, c3, c4, c5, c6);
                    if ((c1 >= r - 5 && c2 <= r + 5) && (c3 >= g - 5 && c4 <= g + 5) && (c5 >= b - 5 && c6 <= b + 5))
                    {
                        addTabla2((string)dataGridView1.Rows[i].Cells[0].Value);
                    }
                }
            }
        }
        private void addTabla2(string n)
        {
            DataRow fila = tabla2.NewRow();
            fila["Concidencias"] = n;
            fila["Probabilidad"] = "sads";
            bool sw = false;
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                if ((string)dataGridView2.Rows[i].Cells[0].Value == n)
                {
                    sw = true;
                }
            
            }
            if (!sw)
            {
                tabla2.Rows.Add(fila);
                dataGridView2.DataSource = tabla2;
            }
        }
        private void adicionar_Click(object sender, EventArgs e)
        {
            AgregarColor();
        }

        private void buscar_Click(object sender, EventArgs e)
        {
            if (imagenGlobal == null)
            {
                CargarImagen();
            }
            else
            {
                if (dataGridView1.Rows.Count > 1)
                {
                    //gLabel.Text = dataGridView1.Rows.Count.ToString();
                    int c1 = Int32.Parse((string)dataGridView1.Rows[index].Cells[1].Value);
                    int c2 = Int32.Parse((string)dataGridView1.Rows[index].Cells[2].Value);
                    int c3 = Int32.Parse((string)dataGridView1.Rows[index].Cells[3].Value);
                    int c4 = Int32.Parse((string)dataGridView1.Rows[index].Cells[4].Value);
                    int c5 = Int32.Parse((string)dataGridView1.Rows[index].Cells[5].Value);
                    int c6 = Int32.Parse((string)dataGridView1.Rows[index].Cells[6].Value);
                    Pintar(c1, c2, c3, c4, c5, c6);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int n = e.RowIndex;
            if (n != -1)
            {
                if (dataGridView1.Rows.Count - 1 > n)
                {
                    //label1.Text = (string)dataGridView1.Rows[n].Cells[0].Value;
                    index = n;
                }
            }
        }
        private void Pintar(int r1, int r2, int g1, int g2, int b1, int b2)
        {
            Bitmap b = new Bitmap(image.Image);
            Bitmap bc = new Bitmap(b.Width, b.Height);
            decimal num = 0;
            Color c = new Color();
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    c = b.GetPixel(i, j);
                    if ((c.R >= r1 - num && c.R <= r2 + num) && (c.G >= g1 - num && c.G <= g2 + num) && (c.B - num >= b1 && c.B <= b2 + num))
                    {
                        for (int k = i; k < i + 25; k++)
                        {
                            for (int l = j; l < j + 25; l++)
                            {
                                if (k < b.Width && l < b.Height)
                                    if (k > 0 && l > 0)
                                    {
                                        bc.SetPixel(k, l, Color.FromArgb(255, g2, b2 + 30));
                                    }
                            }
                        }
                        bc.SetPixel(i, j, Color.FromArgb(r2, 255, b2 + 30));
                    }
                    else
                    {
                        bc.SetPixel(i, j, Color.FromArgb(c.R, c.G, c.B));
                    }
                }
            }
            image.Image = bc;
        }

        private void clear_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        private void Limpiar()
        {
            if (imagenGlobal == null)
            {
                CargarImagen();
            }
            else
            {
                image.Image = imagenGlobal;
            }
        }
    }
}
