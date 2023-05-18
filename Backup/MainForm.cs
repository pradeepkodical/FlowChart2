using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FlowChart2.ControllerModel;
using FlowChart2.Utility;
using PK.Controls;
using FlowChart2.Model;
using PK.Grid.Def;

namespace FlowChart2
{
    public partial class MainForm : Form
    {
        private bool blnRestoring = false;
        private GridControl<FlowChart> gridControl1;
        private List<FlowChart> undoList = new List<FlowChart>();

        public MainForm()
        {
            InitializeComponent();
            this.OnResize(new EventArgs());
            nPanel1.ComponentAdded += new Action<NComponent>(nPanel1_ComponentAdded);
            nPanel1.Change +=new Action<string>(nPanel1_Change);

            
            this.gridControl1 = new PK.Controls.GridControl<FlowChart>();            
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Dock = DockStyle.Fill;
            pnlUndo.Controls.Add(gridControl1);

            this.gridControl1.CellClick += new Action<FlowChart, PK.Grid.Def.ColumnDef<FlowChart>>(gridControl1_CellClick);

            gridControl1.SetData(new List<ColumnDef<FlowChart>> { 
                new ColumnDef<FlowChart>
                {
                     Width = 50,
                     HeaderText = "Undo",
                     CellRenderer = x => "UNDO"
                },
                new ColumnDef<FlowChart>
                {
                     Flex = 1,
                     HeaderText = "Content",
                     CellRenderer = x => x.Timestamp
                },
            }, undoList);           
            
        }

        void gridControl1_CellClick(FlowChart flowChart, PK.Grid.Def.ColumnDef<FlowChart> column)
        {
            if (column.HeaderText == "Undo")
            {
                blnRestoring = true;
                nPanel1.SetFlatModeJSON(JSONUtil.Serialize(flowChart));
                blnRestoring = false;
            }
        }

        void nPanel1_Change(string strJSON)
        {
            if (!blnRestoring)
            {
                FlowChart flowChart = JSONUtil.Deserialize<FlowChart>(strJSON);
                flowChart.Timestamp = DateTime.Now.ToString("hh:mm:ss");
                undoList.Add(flowChart);
                gridControl1.Invalidate();
            }
        }

        void nPanel1_ComponentAdded(NComponent obj)
        {
            
        }

        private void btnAddBox_Click(object sender, EventArgs e)
        {
            nPanel1.AddComponent(new NBox
            {
                X = 5 * NConfig.BLOCK_SIZE_2,
                Y = 5 * NConfig.BLOCK_SIZE_2,
                Width = 5 * NConfig.BLOCK_SIZE_2,
                Height = 5 * NConfig.BLOCK_SIZE_2
            });
        }

        private void btnAddRound_Click(object sender, EventArgs e)
        {
            nPanel1.AddComponent(new NRound
            {
                X = 5* NConfig.BLOCK_SIZE_2,
                Y = 5 * NConfig.BLOCK_SIZE_2,
                Width = 5 * NConfig.BLOCK_SIZE_2,
                Height = 5 * NConfig.BLOCK_SIZE_2
            });
        }

        private void btnAddRhombus_Click(object sender, EventArgs e)
        {
            nPanel1.AddComponent(new NRhombus
            {
                X = 5 * NConfig.BLOCK_SIZE_2,
                Y = 5 * NConfig.BLOCK_SIZE_2,
                Width = 5 * NConfig.BLOCK_SIZE_2,
                Height = 5 * NConfig.BLOCK_SIZE_2
            });
        } 

        private void btnAddConnector_Click(object sender, EventArgs e)
        {
            nPanel1.AddComponent(new NConnector(
                    5 * NConfig.BLOCK_SIZE_2,
                    5* NConfig.BLOCK_SIZE_2,
                    10 * NConfig.BLOCK_SIZE_2,
                    10 * NConfig.BLOCK_SIZE_2));            
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            nPanel1.Width = 900;
            nPanel1.Height = 1200;
            nPanel1.Left = (panel1.Width - nPanel1.Width) / 2;            
        }        

        private void btnCloud_Click(object sender, EventArgs e)
        {
            nPanel1.AddComponent(new NCloud
            {
                X = 5 * NConfig.BLOCK_SIZE_2,
                Y = 5 * NConfig.BLOCK_SIZE_2,
                Width = 5 * NConfig.BLOCK_SIZE_2,
                Height = 5 * NConfig.BLOCK_SIZE_2
            });
        }

        private void btnDatabase_Click(object sender, EventArgs e)
        {
            nPanel1.AddComponent(new NDatabase
            {
                X = 5 * NConfig.BLOCK_SIZE_2,
                Y = 5 * NConfig.BLOCK_SIZE_2,
                Width = 5 * NConfig.BLOCK_SIZE_2,
                Height = 5 * NConfig.BLOCK_SIZE_2
            });
        }

        private void btnAddProcess_Click(object sender, EventArgs e)
        {
            nPanel1.AddComponent(new NProcess
            {
                X = 5 * NConfig.BLOCK_SIZE_2,
                Y = 5 * NConfig.BLOCK_SIZE_2,
                Width = 5 * NConfig.BLOCK_SIZE_2,
                Height = 5 * NConfig.BLOCK_SIZE_2
            });
        }

        private void btnSaveDoc_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void btnOpenDoc_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void btnNewDoc_Click(object sender, EventArgs e)
        {
            nPanel1.CreateNew();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            System.IO.File.WriteAllText(saveFileDialog1.FileName, nPanel1.GetFlatModelJSON());
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            nPanel1.SetFlatModeJSON(System.IO.File.ReadAllText(openFileDialog1.FileName));            
        }
    }
}
