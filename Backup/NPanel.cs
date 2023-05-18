using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FlowChart2.View;
using FlowChart2.ControllerModel;
using FlowChart2.Visitor;
using FlowChart2.Utility;
using FlowChart2.Model;
using FlowChart2.Modifiers;

namespace FlowChart2
{
    public partial class NPanel : UserControl
    {
        public event Action<String> Change;
        public event Action<NComponent> ComponentAdded;
        
        public List<NComponent> NComponents { get; private set; }
        public NComponent SelectedComponent { get; set; }
        private NMouseTool mouseTool;

        private NViewVisitor viewVisitor;
        private NVertexMovedVisitor vertexMovedVisitor;
        private NPathResolver pathResolver;

        private string lastModel = string.Empty;
        
        public NPanel()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            NComponents = new List<NComponent>();
            mouseTool = new NBoxTool(this.NComponents);

            mouseTool.ReDraw += new Action(mouseTool_ReDraw);
            mouseTool.ComponentSelected += new Action<NComponent>(mouseTool_ComponentSelected);
            mouseTool.Change += new Action(mouseTool_Change);

            pathResolver = new NPathResolver
            {
                BoundingBox = new NBox
                {
                    X = 0,
                    Y = 0,
                    Width = 900,
                    Height = 1200
                },
                Components = this.NComponents
            };

            viewVisitor = new NViewVisitor(this.Font);
            vertexMovedVisitor = new NVertexMovedVisitor(pathResolver, this.NComponents);            
            
            this.MouseDown += new MouseEventHandler(Box_MouseDown);
            this.MouseUp += new MouseEventHandler(Box_MouseUp);
            this.MouseMove += new MouseEventHandler(Box_MouseMove);
            this.MouseDoubleClick += new MouseEventHandler(NPanel_MouseDoubleClick);

            this.txtText.TextChanged  += new EventHandler(TextBoxTextChange);
            this.txtText.KeyDown += new KeyEventHandler(txtText_KeyDown);

            this.ComponentAdded += new Action<NComponent>(NPanel_ComponentAdded);
            
        }
        
        void mouseTool_Change()
        {
            OnChange();
        }

        void OnChange()
        {
            if (Change != null)
            {
                string changedModel = this.GetFlatModelJSON();
                if (lastModel != changedModel)
                {
                    Change(changedModel);
                    lastModel = changedModel;
                }
            }
        }

        void mouseTool_ComponentSelected(NComponent obj)
        {
            this.SelectedComponent = obj;
        }

        void mouseTool_ReDraw()
        {
            this.Invalidate();
        }

        void NPanel_ComponentAdded(NComponent obj)
        {
            this.NComponents.ForEach(x =>
            {
                x.IsSelected = false;
                x.IsCloseEnough = false;
                x.SelectedPoint = null;
            });

            this.SelectedComponent = obj;
            mouseTool.SelectedComponent = obj;

            obj.IsSelected = true;
            HideTextBox();
            OnChange();
        }

        void txtText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                HideTextBox();            
            }
        }

        void TextBoxTextChange(object sender, EventArgs e)
        {
            if (SelectedComponent != null)
            {
                SelectedComponent.GetModel().Text = txtText.Text.Trim();
            }
        }

        void HideTextBox()
        {
            txtText.Visible = false;
        }

        void NPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (SelectedComponent != null)
            { 
                FlatModel model = SelectedComponent.GetFlatModel();
                txtText.Left = (int)model.StartPoint.X;
                txtText.Width = (int)(model.EndPoint.X - model.StartPoint.X);
                txtText.Top = (int)model.StartPoint.Y;
                txtText.Height = (int)(model.EndPoint.Y - model.StartPoint.Y);
                txtText.Text = model.Text;
                txtText.Visible = true;
                txtText.Focus();
            }
        }

        void Box_MouseMove(object sender, MouseEventArgs e)
        {
            mouseTool.Move(e);
        }

        void Box_MouseUp(object sender, MouseEventArgs e)
        {
            mouseTool.Up(e);
        }

        void Box_MouseDown(object sender, MouseEventArgs e)
        {
            mouseTool.Down(e);
            HideTextBox();
        }
        
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.WhiteSmoke);
            for (int y = 0; y < this.Height; y += NConfig.BLOCK_SIZE_2)
            {
                for (int x = 0; x < this.Width; x += NConfig.BLOCK_SIZE_2)
                {
                    e.Graphics.FillEllipse(Brushes.Black, x-1, y-1, 2, 2);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            NComponents.ForEach(x => {
                x.View.Draw(e.Graphics);
            });
            
            if (this.SelectedComponent != null)
            {
                this.SelectedComponent.View.Draw(e.Graphics);
            }
        }

        internal void AddComponent(NComponent component)
        {
            NComponents.Add(component);
            
            pathResolver.BuildGraph();
            
            component.Accept(viewVisitor);
            component.Accept(vertexMovedVisitor);

            if (ComponentAdded != null)
            {
                ComponentAdded(component);
            }
            
            this.Invalidate();
        }

        internal String GetFlatModelJSON()
        {
            FlowChart chart = new FlowChart
            {
                FlatModels = new List<FlatModel>()                
            };
            
            NComponents.ForEach(x =>
            {
                chart.FlatModels.Add(x.GetFlatModel());
            });
            return JSONUtil.Serialize<FlowChart>(chart);
        }

        internal void SetFlatModeJSON(string strJSON)
        {
            this.NComponents.Clear();
            NVisitor visitor = new NLoadVisitor(this.NComponents);
            FlowChart chart = JSONUtil.Deserialize<FlowChart>(strJSON);
            chart.FlatModels.OrderBy(x => x.SortOrder).ToList().ForEach(x =>
            {
                NComponent component = (NComponent)Activator.CreateInstance(Type.GetType(x.ControllerClassName));
                component.SetFlatModel(x);
                this.AddComponent(component);
                component.Accept(visitor);
            });

            visitor = new NConnectorConnetVisitor(this.pathResolver);
            this.NComponents.ForEach(x =>
            {
                x.Accept(visitor);
            });            
        }

        internal void CreateNew()
        {
            NComponents.Clear();
            this.Invalidate();
        }
    }
}
