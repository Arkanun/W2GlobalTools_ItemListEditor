using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using W2ItemListEditor.Editor;

namespace ItemListEditor {
	public partial class W2_ItemListEditor : Form {
		public string FilePath = "";
		public st_ItemList List;
		public STRUCT_ITEMNAME ItemName;
		public STRUCT_ITEMNAME SaveItemName;
		public int Editing;
		public int itemCount = 0;
		public int itenameRead = 0;
		private EditorItemIcon EditorForm;
		public W2_ItemListEditor ( ) {
			try {
				this.InitializeComponent ( );
				
				this.FixNumericValues ( );

				this.Shown += this.FormShow;

				this.listBox.MouseDoubleClick += this.ChangeItemSelect;

				this.slot0.CheckStateChanged += this.SlotChange;
				this.slot1.CheckStateChanged += this.SlotChange;
				this.slot2.CheckStateChanged += this.SlotChange;
				this.slot3.CheckStateChanged += this.SlotChange;
				this.slot4.CheckStateChanged += this.SlotChange;
				this.slot5.CheckStateChanged += this.SlotChange;
				this.slot6.CheckStateChanged += this.SlotChange;
				this.slot7.CheckStateChanged += this.SlotChange;
				this.slot8.CheckStateChanged += this.SlotChange;
				this.slot9.CheckStateChanged += this.SlotChange;
				this.slot10.CheckStateChanged += this.SlotChange;
				this.slot11.CheckStateChanged += this.SlotChange;
				this.slot12.CheckStateChanged += this.SlotChange;
				this.slot13.CheckStateChanged += this.SlotChange;
				this.slot14.CheckStateChanged += this.SlotChange;
				this.slot15.CheckStateChanged += this.SlotChange;
				this.slot16.CheckStateChanged += this.SlotChange;
				this.slot17.CheckStateChanged += this.SlotChange;

				this.ItemName = new STRUCT_ITEMNAME();
				this.ItemName.Item = new st_ItemNameItem[6500];


				this.SaveItemName = new STRUCT_ITEMNAME();
				this.SaveItemName.Item = new st_ItemNameItem[6500];

			}
			catch ( Exception ex ) {
				MessageBox.Show ( ex.Message );
			}
		}

		private void FormShow ( object sender , EventArgs e ) {
			try {
				//this.LoadItemList ( );
			}
			catch ( Exception ex ) {
				MessageBox.Show ( ex.Message );
			}
		}

		private void ChangeItemSelect ( object sender , MouseEventArgs e ) {
			try {
				this.Editing = this.listBox.SelectedIndex;

				this.LoadItem ( );
			}
			catch ( Exception ex ) {
				MessageBox.Show ( ex.Message );
			}
		}

		private void SlotChange ( object sender , EventArgs e ) {
			try {
				this.posicao.Text = this.UpdatePosition ( ).ToString ( );
			}
			catch ( Exception ex ) {
				MessageBox.Show ( ex.Message );
			}
		}



		public void SaveItemListCSV()
		{
			try
			{
				using (SaveFileDialog save = new SaveFileDialog())
				{
					save.Filter = "*.csv|*.csv";
					save.Title = "Selecione onde deseja salvar sua ItemList.csv";
					save.ShowDialog();

					if (save.FileName != "")
					{
						File.Create(save.FileName).Close();

						List<string> Itens = new List<string>();
						ComboBox Combo = new ComboBox();
						string Temp = "";

						Defines.ItemEffects(Combo);

						for (int i = 0; i < 6500; i++)
						{
							st_ItemListItem Item = this.List.Item[i];

							if (Item.Name != "")
							{


								Temp = $"{i},{Item.Name},{Item.Mesh}.{Item.Texture},{Item.Level}.{Item.Str}.{Item.Int}.{Item.Dex}.{Item.Con},{Item.Unique},{Item.Price},{Item.Pos},{Item.Extreme},{Item.Grade},{Item.MountType},{Item.MountData}";

								for (int j = 0; j < Item.Effect.Length; j++)
								{
									if (Item.Effect[j].Index != 0 && Item.Effect[j].Index > 0 && Item.Effect[j].Index < Combo.Items.Count)
									{
										if (Item.Effect[j].Index == 0)
											continue;
										Temp += $",{Combo.Items[Item.Effect[j].Index]},{Item.Effect[j].Value}";
									}
								}

								Itens.Add(Temp);
							}
						}

						File.WriteAllLines(save.FileName, Itens);

						MessageBox.Show($"Arquivo {save.FileName} salvo com sucesso!");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		public void SaveItemNameCSV()
		{
			try
			{
				using (SaveFileDialog save = new SaveFileDialog())
				{
					save.Filter = "*.csv|*.csv";
					save.Title = "Selecione onde deseja salvar sua ItemName.csv";
					save.ShowDialog();

					if (save.FileName != "")
					{
						File.Create(save.FileName).Close();

						List<string> Itens = new List<string>();
						ComboBox Combo = new ComboBox();
						string Temp = "";

						Defines.ItemEffects(Combo);

						for (int i = 0; i < 6500; i++)
						{
							st_ItemListItem _Item = this.List.Item[i];
							st_ItemNameItem _itemName = this.SaveItemName.Item[i];

							if (!string.IsNullOrEmpty(_Item.Name) && !string.IsNullOrEmpty(_itemName.Name))
							{
								Temp = $"{i} {_Item.Name} {_itemName.Name}";
							}
							else
								continue;


							Itens.Add(Temp);
						}

						File.WriteAllLines(save.FileName, Itens);

						MessageBox.Show($"Arquivo {save.FileName} salvo com sucesso!");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void FixNumericValues ( ) {
			try {

				this.mesh.Minimum = short.MinValue;
				this.mesh.Maximum = short.MaxValue;

				this.textura.Minimum = int.MinValue;
				this.textura.Maximum = int.MaxValue;


				this.Visual.Minimum = short.MinValue;
				this.Visual.Maximum = short.MaxValue;


				this.level.Minimum = short.MinValue;
				this.level.Maximum = short.MaxValue;

				this.forca.Minimum = short.MinValue;
				this.forca.Maximum = short.MaxValue;

				this.inteligencia.Minimum = short.MinValue;
				this.inteligencia.Maximum = short.MaxValue;

				this.destreza.Minimum = short.MinValue;
				this.destreza.Maximum = short.MaxValue;

				this.constituicao.Minimum = short.MinValue;
				this.constituicao.Maximum = short.MaxValue;

				this.preco.Minimum = int.MinValue;
				this.preco.Maximum = int.MaxValue;

				this.unique.Minimum = ushort.MinValue;
				this.unique.Maximum = ushort.MaxValue;

				this.anct.Minimum = int.MinValue;
				this.anct.Maximum = int.MaxValue;

				this.grau.Minimum = ushort.MinValue;
				this.grau.Maximum = ushort.MaxValue;



				this.UNK1.Minimum = int.MinValue;
				this.UNK1.Maximum = int.MaxValue;

				this.UNK2.Minimum = int.MinValue;
				this.UNK2.Maximum = int.MaxValue;


				this.UNK3.Minimum = int.MinValue;
				this.UNK3.Maximum = int.MaxValue;


				this.UNK4.Minimum = int.MinValue;
				this.UNK4.Maximum = int.MaxValue;


				Defines.ItemEffects ( this.EF1 );
				Defines.ItemEffects ( this.EF2 );
				Defines.ItemEffects ( this.EF3 );
				Defines.ItemEffects ( this.EF4 );
				Defines.ItemEffects ( this.EF5 );
				Defines.ItemEffects ( this.EF6 );
				Defines.ItemEffects ( this.EF7 );
				Defines.ItemEffects ( this.EF8 );
				Defines.ItemEffects ( this.EF9 );
				Defines.ItemEffects ( this.EF10 );
				Defines.ItemEffects ( this.EF11 );
				Defines.ItemEffects ( this.EF12 );

				this.EF1.SelectedIndex = 0;
				this.EF2.SelectedIndex = 0;
				this.EF3.SelectedIndex = 0;
				this.EF4.SelectedIndex = 0;
				this.EF5.SelectedIndex = 0;
				this.EF6.SelectedIndex = 0;
				this.EF7.SelectedIndex = 0;
				this.EF8.SelectedIndex = 0;
				this.EF9.SelectedIndex = 0;
				this.EF10.SelectedIndex = 0;
				this.EF11.SelectedIndex = 0;
				this.EF12.SelectedIndex = 0;

				this.EFV1.Minimum = short.MinValue;
				this.EFV1.Maximum = short.MaxValue;

				this.EFV2.Minimum = short.MinValue;
				this.EFV2.Maximum = short.MaxValue;

				this.EFV3.Minimum = short.MinValue;
				this.EFV3.Maximum = short.MaxValue;

				this.EFV4.Minimum = short.MinValue;
				this.EFV4.Maximum = short.MaxValue;

				this.EFV5.Minimum = short.MinValue;
				this.EFV5.Maximum = short.MaxValue;

				this.EFV6.Minimum = short.MinValue;
				this.EFV6.Maximum = short.MaxValue;

				this.EFV7.Minimum = short.MinValue;
				this.EFV7.Maximum = short.MaxValue;

				this.EFV8.Minimum = short.MinValue;
				this.EFV8.Maximum = short.MaxValue;

				this.EFV9.Minimum = short.MinValue;
				this.EFV9.Maximum = short.MaxValue;

				this.EFV10.Minimum = short.MinValue;
				this.EFV10.Maximum = short.MaxValue;

				this.EFV11.Minimum = short.MinValue;
				this.EFV11.Maximum = short.MaxValue;

				this.EFV12.Minimum = short.MinValue;
				this.EFV12.Maximum = short.MaxValue;
			}
			catch ( Exception ex ) {
				MessageBox.Show ( ex.Message );
			}
		}

		public void LoadItemName()
		{
			try
			{



				using (OpenFileDialog find = new OpenFileDialog())
				{
					find.Filter = "ItemName.bin|ItemName.bin";
					find.Title = "Selecione sua ItemName.bin";

					find.ShowDialog();

					if (find.CheckFileExists)
					{
						this.FilePath = find.FileName;

						if (File.Exists(this.FilePath))
						{

							byte[] read = File.ReadAllBytes(this.FilePath);

							for (int i = 0; i < read.Length; i += 68)
							{
								for (int j = i + 4, k = 0; j < i + 68; j++, k++)
									read[j] -= (byte)(k);
							}

							for (int i = 0, id = 0; i < read.Length; i += 68)
							{
								id = BitConverter.ToInt32(read, i);
								this.ItemName.Item[id].Id = id;
								this.ItemName.Item[id].Name = Encoding.UTF8.GetString(read, i + 4, 62).Replace("\0", "");

								

								this.List.Item[id].Name = Encoding.UTF8.GetString(read, i + 4, 62).Replace("\0", "");
							}
							
							Task.Run(() =>
							{
								this.listBox.Invoke(new MethodInvoker(delegate ()
								{
									if (listBox.Items.Count > 0)
										listBox.Items.Clear();

									itemCount = 0;
									for (int i = 0; i < this.List.Item.Length; i++)
									{

										if (this.List.Item[i].Name != "")
										{
											itemCount++;
											label10.Text = "Quantidade de itens: (" + itemCount + " / 6500)";
										}

										this.listBox.Items.Add($"{i.ToString().PadLeft(4, '0')}: {this.List.Item[i].Name}");
									}
								}));
							});
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		public void LoadItemList()
		{
			try
			{
	
				using (OpenFileDialog find = new OpenFileDialog())
				{
					find.Filter = "ItemList.bin|ItemList.bin";
					find.Title = "Selecione sua ItemList.bin";

					find.ShowDialog();

					if (find.CheckFileExists)
					{
						this.FilePath = find.FileName;

						if (File.Exists(this.FilePath))
						{
							byte[] temp = File.ReadAllBytes(this.FilePath);

							if (temp.Length != 988004)
							{
								MessageBox.Show("Itemlist inválida", "ItemList.bin inválida", MessageBoxButtons.OK, MessageBoxIcon.Error);
								this.LoadItemList();
							}
							else
							{
								byte[] pKeyList = File.ReadAllBytes("./Keys.bin");
								Array.Resize(ref pKeyList, pKeyList.Length + 1);

								if (temp[0] == 0x5A && temp[1] == 0x5A)
								{
									for (int i = 0; i < temp.Length; i++)
										temp[i] ^= 0x5A;
								}
								else
								{
									for (int i = 0; i < temp.Length; i++)
										temp[i] ^= (pKeyList[i & 63]);
								}
 
							 
								this.List = Pak.ToStruct<st_ItemList>(temp);
 
								Task.Run(() =>
								{
									if(listBox.Items.Count > 0)
								      	listBox.Items.Clear();

									itemCount = 0;
									this.listBox.Invoke(new MethodInvoker(delegate ()
									{
										for (int i = 0; i < this.List.Item.Length; i++)
										{
										
											if (this.List.Item[i].Name != "")
											{
												itemCount++;
												label10.Text = "Quantidade de itens: (" + itemCount + " / 6500)";
											}
											this.SaveItemName.Item[i].Id = i;
											this.SaveItemName.Item[i].Name = this.List.Item[i].Name;

											this.listBox.Items.Add($"{i.ToString().PadLeft(4, '0')}: {this.List.Item[i].Name}");
										}
									}));
								});

								abrirItemNamebinToolStripMenuItem.Enabled = true;
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

		public void LoadItem()
		{

			st_ItemListItem item = this.List.Item[this.Editing];

			this.name.Text = item.Name;
			this.mesh.Text = item.Mesh.ToString();
			this.textura.Text = item.Texture.ToString();
			this.Visual.Value = item.IndexVisualEffect;
			this.level.Text = item.Level.ToString();
			this.forca.Text = item.Str.ToString();
			this.inteligencia.Text = item.Int.ToString();
			this.destreza.Text = item.Dex.ToString();
			this.constituicao.Text = item.Con.ToString();
			this.preco.Text = item.Price.ToString();
			this.unique.Text = item.Unique.ToString();
			this.posicao.Text = item.Pos.ToString();
			this.UNK1.Value = item.Unk;
			this.anct.Text = item.Extreme.ToString();
			this.grau.Text = item.Grade.ToString();
			this.UNK2.Value = item.UNK_2;
			this.UNK3.Value = item.MountType;
			this.UNK4.Value = item.MountData;

			label9.Text = "Item Acient / Lendário";
			if (item.Extreme >= 0 && item.Extreme < 6500)
			{
				label28.Text = this.List.Item[item.Extreme].Name;
			}
			else
			{
				label9.Text = "Fama Nescessária:";
				label28.Text = "";
			}




			this.EF1.SelectedIndex = item.Effect[0].Index;
			this.EF2.SelectedIndex = item.Effect[1].Index;
			this.EF3.SelectedIndex = item.Effect[2].Index;
			this.EF4.SelectedIndex = item.Effect[3].Index;
			this.EF5.SelectedIndex = item.Effect[4].Index;
			this.EF6.SelectedIndex = item.Effect[5].Index;
			this.EF7.SelectedIndex = item.Effect[6].Index;
			this.EF8.SelectedIndex = item.Effect[7].Index;
			this.EF9.SelectedIndex = item.Effect[8].Index;
			this.EF10.SelectedIndex = item.Effect[9].Index;
			this.EF11.SelectedIndex = item.Effect[10].Index;
			this.EF12.SelectedIndex = item.Effect[11].Index;

			this.EFV1.Value = item.Effect[0].Value;
			this.EFV2.Value = item.Effect[1].Value;
			this.EFV3.Value = item.Effect[2].Value;
			this.EFV4.Value = item.Effect[3].Value;
			this.EFV5.Value = item.Effect[4].Value;
			this.EFV6.Value = item.Effect[5].Value;
			this.EFV7.Value = item.Effect[6].Value;
			this.EFV8.Value = item.Effect[7].Value;
			this.EFV9.Value = item.Effect[8].Value;
			this.EFV10.Value = item.Effect[9].Value;
			this.EFV11.Value = item.Effect[10].Value;
			this.EFV12.Value = item.Effect[11].Value;

			this.CheckPosition(item.Pos);

			this.itemBox.Text = $"Item Selcionado: ( {this.Editing.ToString().PadLeft(4, '0')} )";

		}
	 

		public void CheckPosition(int Pos) {


			if (Pos >= 131072)
			{
				Pos -= 131072;
				this.slot17.Checked = true;
			}
			else
			{
				this.slot17.Checked = false;
			}



			if (Pos >= 65536)
			{
				Pos -= 65536;
				this.slot16.Checked = true;
			}
			else
			{
				this.slot16.Checked = false;
			}



			if (Pos >= 32768) {
				Pos -= 32768;
				this.slot15.Checked = true;
			}
			else {
				this.slot15.Checked = false;
			}

			if (Pos >= 16384) {
				Pos -= 16384;
				this.slot14.Checked = true;
			}
			else {
				this.slot14.Checked = false;
			}

			if (Pos >= 8192) {
				Pos -= 8192;
				this.slot13.Checked = true;
			}
			else {
				this.slot13.Checked = false;
			}

			if (Pos >= 4096) {
				Pos -= 4096;
				this.slot12.Checked = true;
			}
			else {
				this.slot12.Checked = false;
			}

			if (Pos >= 2048) {
				Pos -= 2048;
				this.slot11.Checked = true;
			}
			else {
				this.slot11.Checked = false;
			}

			if (Pos >= 1024) {
				Pos -= 1024;
				this.slot10.Checked = true;
			}
			else {
				this.slot10.Checked = false;
			}

			if (Pos >= 512) {
				Pos -= 512;
				this.slot9.Checked = true;
			}
			else {
				this.slot9.Checked = false;
			}

			if (Pos >= 256) {
				Pos -= 256;
				this.slot8.Checked = true;
			}
			else {
				this.slot8.Checked = false;
			}

			if (Pos >= 128) {
				Pos -= 128;
				this.slot7.Checked = true;
			}
			else {
				this.slot7.Checked = false;
			}

			if (Pos >= 64) {
				Pos -= 64;
				this.slot6.Checked = true;
			}
			else {
				this.slot6.Checked = false;
			}

			if (Pos >= 32) {
				Pos -= 32;
				this.slot5.Checked = true;
			}
			else {
				this.slot5.Checked = false;
			}

			if (Pos >= 16) {
				Pos -= 16;
				this.slot4.Checked = true;
			}
			else {
				this.slot4.Checked = false;
			}

			if (Pos >= 8) {
				Pos -= 8;
				this.slot3.Checked = true;
			}
			else {
				this.slot3.Checked = false;
			}

			if (Pos >= 4) {
				Pos -= 4;
				this.slot2.Checked = true;
			}
			else {
				this.slot2.Checked = false;
			}

			if (Pos >= 2) {
				Pos -= 2;
				this.slot1.Checked = true;
			}
			else {
				this.slot1.Checked = false;
			}

			if (Pos >= 1) {
				Pos -= 1;
				this.slot0.Checked = true;
			}
			else {
				this.slot0.Checked = false;
			}
		}
		public int UpdatePosition() {
			int Pos = 0;

			if (this.slot0.Checked)
				Pos += 1;

			if (this.slot1.Checked)
				Pos += 2;

			if (this.slot2.Checked)
				Pos += 4;

			if (this.slot3.Checked)
				Pos += 8;

			if (this.slot4.Checked)
				Pos += 16;

			if (this.slot5.Checked)
				Pos += 32;

			if (this.slot6.Checked)
				Pos += 64;

			if (this.slot7.Checked)
				Pos += 128;

			if (this.slot8.Checked)
				Pos += 256;

			if (this.slot9.Checked)
				Pos += 512;

			if (this.slot10.Checked)
				Pos += 1024;

			if (this.slot11.Checked)
				Pos += 2048;

			if (this.slot12.Checked)
				Pos += 4096;

			if (this.slot13.Checked)
				Pos += 8192;

			if (this.slot14.Checked)
				Pos += 16384;

			if (this.slot15.Checked)
				Pos += 32768;

			if (this.slot16.Checked)
				Pos += 65536;

			if (this.slot17.Checked)
				Pos += 131072;
	


			return Pos;
		}


		private void listBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				this.Editing = this.listBox.SelectedIndex;

				this.LoadItem();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void save_Click(object sender, EventArgs e)
		{

			if (this.Editing < 0 || this.Editing >= 6500)
				return;

			st_ItemListItem item = new st_ItemListItem().New();

			item.Name = this.name.Text.Replace(" ", "_");

			item.Mesh = Convert.ToInt16(this.mesh.Value);
			item.Texture = Convert.ToInt16(this.textura.Value);
			item.IndexVisualEffect = Convert.ToInt16(this.Visual.Value);
			item.Level = Convert.ToInt16(this.level.Value);
			item.Str = Convert.ToInt16(this.forca.Value);
			item.Int = Convert.ToInt16(this.inteligencia.Value);
			item.Dex = Convert.ToInt16(this.destreza.Value);
			item.Con = Convert.ToInt16(this.constituicao.Value);

			item.Price = Convert.ToInt32(this.preco.Value);
			item.Unique = Convert.ToUInt16(this.unique.Value);
			item.Pos = Convert.ToInt32(this.posicao.Text);
			item.Unk = Convert.ToInt16(this.UNK1.Value);
			item.Extreme = Convert.ToUInt16(this.anct.Value);
			item.Grade = Convert.ToUInt16(this.grau.Value);

			item.UNK_2 = Convert.ToInt32(this.UNK2.Value);
			item.MountType = Convert.ToInt16(this.UNK3.Value);
			item.MountData = Convert.ToInt16(this.UNK4.Value);



			item.Effect[0].Index = Convert.ToInt16(this.EF1.SelectedIndex);
			item.Effect[1].Index = Convert.ToInt16(this.EF2.SelectedIndex);
			item.Effect[2].Index = Convert.ToInt16(this.EF3.SelectedIndex);
			item.Effect[3].Index = Convert.ToInt16(this.EF4.SelectedIndex);
			item.Effect[4].Index = Convert.ToInt16(this.EF5.SelectedIndex);
			item.Effect[5].Index = Convert.ToInt16(this.EF6.SelectedIndex);
			item.Effect[6].Index = Convert.ToInt16(this.EF7.SelectedIndex);
			item.Effect[7].Index = Convert.ToInt16(this.EF8.SelectedIndex);
			item.Effect[8].Index = Convert.ToInt16(this.EF9.SelectedIndex);
			item.Effect[9].Index = Convert.ToInt16(this.EF10.SelectedIndex);
			item.Effect[10].Index = Convert.ToInt16(this.EF11.SelectedIndex);
			item.Effect[11].Index = Convert.ToInt16(this.EF12.SelectedIndex);

			item.Effect[0].Value = Convert.ToInt16(this.EFV1.Value);
			item.Effect[1].Value = Convert.ToInt16(this.EFV2.Value);
			item.Effect[2].Value = Convert.ToInt16(this.EFV3.Value);
			item.Effect[3].Value = Convert.ToInt16(this.EFV4.Value);
			item.Effect[4].Value = Convert.ToInt16(this.EFV5.Value);
			item.Effect[5].Value = Convert.ToInt16(this.EFV6.Value);
			item.Effect[6].Value = Convert.ToInt16(this.EFV7.Value);
			item.Effect[7].Value = Convert.ToInt16(this.EFV8.Value);
			item.Effect[8].Value = Convert.ToInt16(this.EFV9.Value);
			item.Effect[9].Value = Convert.ToInt16(this.EFV10.Value);
			item.Effect[10].Value = Convert.ToInt16(this.EFV11.Value);
			item.Effect[11].Value = Convert.ToInt16(this.EFV12.Value);

			this.listBox.Items[this.Editing] = $"{this.Editing.ToString().PadLeft(4, '0')}: {item.Name}";

			this.List.Item[this.Editing] = item;
			itemCount = 0;
			for (int i = 0; i < 6500; i++)
			{
				if (this.List.Item[i].Name != "")
					itemCount++;
			}

			label10.Text = "Quantidade de itens: (" + itemCount + " / 6500)";
		}

		private void cancel_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Ao confirmar será carregado o ultimo save deste item.", "Deseja cancelar a edição desse item?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				this.LoadItem();
			}
		}

		private void clean_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Ao confirmar você terá de salvar para que a mudança tome efeito.", "Deseja limpar este item?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				this.name.Text = "";
				this.mesh.Value = 0;
				this.textura.Value = 0;
				this.Visual.Value = 0;
				this.level.Value = 0;
				this.forca.Value = 0;
				this.inteligencia.Value = 0;
				this.destreza.Value = 0;
				this.constituicao.Value = 0;
				this.preco.Value = 0;
				this.unique.Value = 0;
				this.UNK1.Value = 0;
				this.posicao.Text = "";
				this.anct.Value = 0;
				this.grau.Value = 0;

				this.UNK2.Value = 0;
				this.UNK3.Value = 0;
				this.UNK4.Value = 0;

				this.EF1.SelectedIndex = 0;
				this.EF2.SelectedIndex = 0;
				this.EF3.SelectedIndex = 0;
				this.EF4.SelectedIndex = 0;
				this.EF5.SelectedIndex = 0;
				this.EF6.SelectedIndex = 0;
				this.EF7.SelectedIndex = 0;
				this.EF8.SelectedIndex = 0;
				this.EF9.SelectedIndex = 0;
				this.EF10.SelectedIndex = 0;
				this.EF11.SelectedIndex = 0;
				this.EF12.SelectedIndex = 0;

				this.EFV1.Value = 0;
				this.EFV2.Value = 0;
				this.EFV3.Value = 0;
				this.EFV4.Value = 0;
				this.EFV5.Value = 0;
				this.EFV6.Value = 0;
				this.EFV7.Value = 0;
				this.EFV8.Value = 0;
				this.EFV9.Value = 0;
				this.EFV10.Value = 0;
				this.EFV11.Value = 0;
				this.EFV12.Value = 0;

				this.CheckPosition(0);
			}
		}

		private void export_Click(object sender, EventArgs e)
		{
			this.SaveItemListCSV();
		}
		public void SalvarItemListBinEncode()
		{
			try
			{
				using (SaveFileDialog save = new SaveFileDialog())
				{
					save.Filter = "*.bin|*.bin";
					save.Title = "Selecione onde deseja salvar sua ItemList.bin";
					save.ShowDialog();

					if (save.FileName != "")
					{
						byte[] toSave = Pak.ToByteArray(this.List);
						byte[] pKeyList = File.ReadAllBytes("./Keys.bin");
						Array.Resize(ref pKeyList, pKeyList.Length + 1);


						for (int i = 0; i < toSave.Length; i++)
							toSave[i] ^= (pKeyList[i & 63]);

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
		private void button1_Click(object sender, EventArgs e)
		{
			this.SalvarItemListBinEncode();
		}
		public void SalvarItemListBin()
		{
			try
			{
				using (SaveFileDialog save = new SaveFileDialog())
				{
					save.Filter = "*.bin|*.bin";
					save.Title = "Selecione onde deseja salvar sua ItemList.bin";
					save.ShowDialog();

					if (save.FileName != "")
					{
						byte[] toSave = Pak.ToByteArray(this.List);

						for (int i = 0; i < toSave.Length; i++)
						{
							toSave[i] ^= 0x5A;
						}

						File.Create(save.FileName).Close();
						File.WriteAllBytes(save.FileName, toSave);

						MessageBox.Show($"Arquivo {save.FileName} salvo com sucesso!");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void saveall_Click(object sender, EventArgs e)
		{
			this.SalvarItemListBin();
		}

		private void unique_ValueChanged(object sender, EventArgs e)
		{
	 
		}
	 
		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			for (int i = 0; i < listBox.Items.Count; i++)
			{
				if (listBox.Items[i].ToString().ToLower().Contains(Pesquisa.Text.ToLower()))
				{
					listBox.SetSelected(i, true);
					break;
				}
			}

		}

		private void button2_Click(object sender, EventArgs e)
		{
		}
		public void SalvaritemNameBin()
		{
			try
			{
				using (SaveFileDialog save = new SaveFileDialog())
				{
					save.Filter = "*.bin|*.bin";
					save.Title = "Selecione onde deseja salvar sua ItemName.bin";
					save.ShowDialog();

					if (save.FileName != "")
					{

						for (int i = 0; i < 6500; i++)
						{
							if (string.IsNullOrEmpty(this.List.Item[i].Name))
								continue;

							this.ItemName.Item[i].Id = i;
							this.ItemName.Item[i].Name = this.List.Item[i].Name;
						}


						byte[] toSave = Pak.ToByteArray(this.ItemName);


						for (int i = 0; i < toSave.Length; i += 68)
						{
							for (int j = i + 4, k = 0; j < i + 68; j++, k++)
								toSave[j] += (byte)(k);
						}

						File.Create(save.FileName).Close();
						File.WriteAllBytes(save.FileName, toSave);

						MessageBox.Show($"Arquivo {save.FileName} salvo com sucesso!");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void button3_Click(object sender, EventArgs e)
		{
			this.SalvaritemNameBin();
		}

		private void abrirItemNamebinToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.LoadItemName();
		}

		private void abrirItemListbinToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.LoadItemList();
		}

		private void salvarItemNamebinToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.SalvaritemNameBin();
		}

		private void salvarItemListbinEncodeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.SalvarItemListBinEncode();
		}

		private void salvarItemListbinToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.SalvarItemListBin();
		}

		private void gerarItemNamecsvToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.SaveItemListCSV();
		}

		private void gerarItemNamecsvToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			this.SaveItemNameCSV();
		}

		private void salvarItemNamebinToolStripMenuItem1_Click(object sender, EventArgs e)
		{

		}

		private void editorDeItemIonToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (EditorForm = new EditorItemIcon())
			{
				EditorForm.ShowDialog();
			}

		}
	}
}