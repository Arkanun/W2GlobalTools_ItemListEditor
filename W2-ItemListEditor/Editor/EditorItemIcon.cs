using ItemListEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace W2ItemListEditor.Editor
{
   
    public partial class EditorItemIcon : Form
    {
        public int itemSelecionado = -1;
        public STRUCT_ITEMICON ItemIcon = new STRUCT_ITEMICON();
		public string FilePath = "";

		public EditorItemIcon()
        {
            InitializeComponent();
        }
		public void LoadItemIcon()
		{
			try
			{

				using (OpenFileDialog find = new OpenFileDialog())
				{
					find.Filter = "ItemIcon.bin|ItemIcon.bin";
					find.Title = "Selecione sua ItemIcon.bin";

					find.ShowDialog();

					if (find.CheckFileExists)
					{
						this.FilePath = find.FileName;

						if (File.Exists(this.FilePath))
						{
							byte[] temp = File.ReadAllBytes(this.FilePath);

							if (temp.Length != 26000 && temp.Length != 25992)
							{
								MessageBox.Show("ItemIcon inválida", "ItemIcon.bin inválida", MessageBoxButtons.OK, MessageBoxIcon.Error);
								this.LoadItemIcon();
							}
							else
							{


								this.ItemIcon = Pak.ToStruct<STRUCT_ITEMICON>(temp);

								if (listBox1.Items.Count > 0)
									listBox1.Items.Clear();


								for (int i = 0; i < this.ItemIcon.Item.Length; i++)
								{
									this.listBox1.Items.Add($"{i.ToString().PadLeft(4, '0')}: {this.ItemIcon.Item[i].Icone}");
								}


								salvarItemIconbinToolStripMenuItem.Enabled = true;

							}
						}
						else
						{
							Environment.Exit(0);
						}
					}
					else
					{
						Environment.Exit(0);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            itemSelecionado = listBox1.SelectedIndex;
            label2.Text = "" + itemSelecionado;
            if(itemSelecionado != -1 && itemSelecionado < listBox1.Items.Count)
            {
                textBox1.Text = "" + ItemIcon.Item[itemSelecionado].Icone;
            }
        }

		private void button2_Click(object sender, EventArgs e)
		{
			if (itemSelecionado != -1 && itemSelecionado < listBox1.Items.Count)
			{
				ItemIcon.Item[itemSelecionado].Icone = int.Parse(textBox1.Text);


				if (listBox1.Items.Count > 0)
					listBox1.Items.Clear();



				for (int i = 0; i < this.ItemIcon.Item.Length; i++)
				{
					this.listBox1.Items.Add($"{i.ToString().PadLeft(4, '0')}: {this.ItemIcon.Item[i].Icone}");
				}



				listBox1.SelectedIndex = itemSelecionado;
			}
		}

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

		private void abrirItemIconbinToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.LoadItemIcon();
		}
		public void SalvaritemIconBIN()
		{
			try
			{
				using (SaveFileDialog save = new SaveFileDialog())
				{
					save.Filter = "*.bin|*.bin";
					save.Title = "Selecione onde deseja salvar sua ItemIcon.bin";
					save.ShowDialog();

					if (save.FileName != "")
					{
						byte[] toSave = Pak.ToByteArray(this.ItemIcon);
						File.Create(save.FileName).Close();
						File.WriteAllBytes(save.FileName, toSave);
						MessageBox.Show($"Arquivo {save.FileName} salvo no modo Encode com sucesso!");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void salvarItemIconbinToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.SalvaritemIconBIN();
		}
	}
}
