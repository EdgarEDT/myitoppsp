using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Itop.Client
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.ComponentModel.ToolboxItem(true)]
    [System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedDataSetSchema")]
    [System.Xml.Serialization.XmlRootAttribute("UserToolDataSet")]
    [System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")]
    public partial class UserToolDataSet : System.Data.DataSet
    {
        public static string UserToolPath
        {
            get
            {
                return Application.StartupPath + @"\UserTool";
            }
        }

        private UserToolDataTable tableUserTool;

        private System.Data.SchemaSerializationMode _schemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public UserToolDataSet()
        {
            this.BeginInit();
            this.InitClass();
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            base.Relations.CollectionChanged += schemaChangedHandler;
            this.EndInit();
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected UserToolDataSet(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            :
                base(info, context, false)
        {
            if ((this.IsBinarySerialized(info, context) == true))
            {
                this.InitVars(false);
                System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler1 = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
                this.Tables.CollectionChanged += schemaChangedHandler1;
                this.Relations.CollectionChanged += schemaChangedHandler1;
                return;
            }
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((this.DetermineSchemaSerializationMode(info, context) == System.Data.SchemaSerializationMode.IncludeSchema))
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                ds.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(strSchema)));
                if ((ds.Tables["UserTool"] != null))
                {
                    base.Tables.Add(new UserToolDataTable(ds.Tables["UserTool"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else
            {
                this.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(strSchema)));
            }
            this.GetSerializationData(info, context);
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)]
        public UserToolDataTable UserTool
        {
            get
            {
                return this.tableUserTool;
            }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.BrowsableAttribute(true)]
        [System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public override System.Data.SchemaSerializationMode SchemaSerializationMode
        {
            get
            {
                return this._schemaSerializationMode;
            }
            set
            {
                this._schemaSerializationMode = value;
            }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new System.Data.DataTableCollection Tables
        {
            get
            {
                return base.Tables;
            }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new System.Data.DataRelationCollection Relations
        {
            get
            {
                return base.Relations;
            }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void InitializeDerivedDataSet()
        {
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public override System.Data.DataSet Clone()
        {
            UserToolDataSet cln = ((UserToolDataSet)(base.Clone()));
            cln.InitVars();
            cln.SchemaSerializationMode = this.SchemaSerializationMode;
            return cln;
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override bool ShouldSerializeTables()
        {
            return false;
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override bool ShouldSerializeRelations()
        {
            return false;
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void ReadXmlSerializable(System.Xml.XmlReader reader)
        {
            if ((this.DetermineSchemaSerializationMode(reader) == System.Data.SchemaSerializationMode.IncludeSchema))
            {
                this.Reset();
                System.Data.DataSet ds = new System.Data.DataSet();
                ds.ReadXml(reader);
                if ((ds.Tables["UserTool"] != null))
                {
                    base.Tables.Add(new UserToolDataTable(ds.Tables["UserTool"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else
            {
                this.ReadXml(reader);
                this.InitVars();
            }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override System.Xml.Schema.XmlSchema GetSchemaSerializable()
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            this.WriteXmlSchema(new System.Xml.XmlTextWriter(stream, null));
            stream.Position = 0;
            return System.Xml.Schema.XmlSchema.Read(new System.Xml.XmlTextReader(stream), null);
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal void InitVars()
        {
            this.InitVars(true);
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal void InitVars(bool initTable)
        {
            this.tableUserTool = ((UserToolDataTable)(base.Tables["UserTool"]));
            if ((initTable == true))
            {
                if ((this.tableUserTool != null))
                {
                    this.tableUserTool.InitVars();
                }
            }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitClass()
        {
            this.DataSetName = "UserToolDataSet";
            this.Prefix = "";
            this.Namespace = "http://tempuri.org/UserToolDataSet.xsd";
            this.EnforceConstraints = true;
            this.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            this.tableUserTool = new UserToolDataTable();
            base.Tables.Add(this.tableUserTool);
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private bool ShouldSerializeUserTool()
        {
            return false;
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void SchemaChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e)
        {
            if ((e.Action == System.ComponentModel.CollectionChangeAction.Remove))
            {
                this.InitVars();
            }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static System.Xml.Schema.XmlSchemaComplexType GetTypedDataSetSchema(System.Xml.Schema.XmlSchemaSet xs)
        {
            UserToolDataSet ds = new UserToolDataSet();
            System.Xml.Schema.XmlSchemaComplexType type = new System.Xml.Schema.XmlSchemaComplexType();
            System.Xml.Schema.XmlSchemaSequence sequence = new System.Xml.Schema.XmlSchemaSequence();
            xs.Add(ds.GetSchemaSerializable());
            System.Xml.Schema.XmlSchemaAny any = new System.Xml.Schema.XmlSchemaAny();
            any.Namespace = ds.Namespace;
            sequence.Items.Add(any);
            type.Particle = sequence;
            return type;
        }

        public delegate void UserToolRowChangeEventHandler(object sender, UserToolRowChangeEvent e);

        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        [System.Serializable()]
        [System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
        public partial class UserToolDataTable : System.Data.DataTable, System.Collections.IEnumerable
        {

            private System.Data.DataColumn columnTitle;

            private System.Data.DataColumn columnProgram;

            private System.Data.DataColumn columnProgIco;

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public UserToolDataTable()
            {
                this.TableName = "UserTool";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal UserToolDataTable(System.Data.DataTable table)
            {
                this.TableName = table.TableName;
                if ((table.CaseSensitive != table.DataSet.CaseSensitive))
                {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString()))
                {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace))
                {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected UserToolDataTable(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
                :
                    base(info, context)
            {
                this.InitVars();
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn TitleColumn
            {
                get
                {
                    return this.columnTitle;
                }
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn ProgramColumn
            {
                get
                {
                    return this.columnProgram;
                }
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn ProgIcoColumn
            {
                get
                {
                    return this.columnProgIco;
                }
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [System.ComponentModel.Browsable(false)]
            public int Count
            {
                get
                {
                    return this.Rows.Count;
                }
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public UserToolRow this[int index]
            {
                get
                {
                    return ((UserToolRow)(this.Rows[index]));
                }
            }

            public event UserToolRowChangeEventHandler UserToolRowChanging;

            public event UserToolRowChangeEventHandler UserToolRowChanged;

            public event UserToolRowChangeEventHandler UserToolRowDeleting;

            public event UserToolRowChangeEventHandler UserToolRowDeleted;

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void AddUserToolRow(UserToolRow row)
            {
                this.Rows.Add(row);
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public UserToolRow AddUserToolRow(string Title, string Program, string ProgIco)
            {
                UserToolRow rowUserToolRow = ((UserToolRow)(this.NewRow()));
                rowUserToolRow.ItemArray = new object[] {
                        Title,
                        Program,
                        ProgIco};
                this.Rows.Add(rowUserToolRow);
                return rowUserToolRow;
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public UserToolRow FindByProgIco(string ProgIco)
            {
                return ((UserToolRow)(this.Rows.Find(new object[] {
                            ProgIco})));
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public virtual System.Collections.IEnumerator GetEnumerator()
            {
                return this.Rows.GetEnumerator();
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public override System.Data.DataTable Clone()
            {
                UserToolDataTable cln = ((UserToolDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override System.Data.DataTable CreateInstance()
            {
                return new UserToolDataTable();
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal void InitVars()
            {
                this.columnTitle = base.Columns["Title"];
                this.columnProgram = base.Columns["Program"];
                this.columnProgIco = base.Columns["ProgIco"];
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private void InitClass()
            {
                this.columnTitle = new System.Data.DataColumn("Title", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnTitle);
                this.columnProgram = new System.Data.DataColumn("Program", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnProgram);
                this.columnProgIco = new System.Data.DataColumn("ProgIco", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnProgIco);
                this.Constraints.Add(new System.Data.UniqueConstraint("Constraint1", new System.Data.DataColumn[] {
                                this.columnProgIco}, true));
                this.columnTitle.AllowDBNull = false;
                this.columnTitle.MaxLength = 50;
                this.columnProgram.AllowDBNull = false;
                this.columnProgram.MaxLength = 500;
                this.columnProgIco.AllowDBNull = false;
                this.columnProgIco.Unique = true;
                this.columnProgIco.MaxLength = 100;
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public UserToolRow NewUserToolRow()
            {
                return ((UserToolRow)(this.NewRow()));
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override System.Data.DataRow NewRowFromBuilder(System.Data.DataRowBuilder builder)
            {
                return new UserToolRow(builder);
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override System.Type GetRowType()
            {
                return typeof(UserToolRow);
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanged(System.Data.DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if ((this.UserToolRowChanged != null))
                {
                    this.UserToolRowChanged(this, new UserToolRowChangeEvent(((UserToolRow)(e.Row)), e.Action));
                }
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanging(System.Data.DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if ((this.UserToolRowChanging != null))
                {
                    this.UserToolRowChanging(this, new UserToolRowChangeEvent(((UserToolRow)(e.Row)), e.Action));
                }
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleted(System.Data.DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if ((this.UserToolRowDeleted != null))
                {
                    this.UserToolRowDeleted(this, new UserToolRowChangeEvent(((UserToolRow)(e.Row)), e.Action));
                }
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleting(System.Data.DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if ((this.UserToolRowDeleting != null))
                {
                    this.UserToolRowDeleting(this, new UserToolRowChangeEvent(((UserToolRow)(e.Row)), e.Action));
                }
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void RemoveUserToolRow(UserToolRow row)
            {
                this.Rows.Remove(row);
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public static System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(System.Xml.Schema.XmlSchemaSet xs)
            {
                System.Xml.Schema.XmlSchemaComplexType type = new System.Xml.Schema.XmlSchemaComplexType();
                System.Xml.Schema.XmlSchemaSequence sequence = new System.Xml.Schema.XmlSchemaSequence();
                UserToolDataSet ds = new UserToolDataSet();
                xs.Add(ds.GetSchemaSerializable());
                System.Xml.Schema.XmlSchemaAny any1 = new System.Xml.Schema.XmlSchemaAny();
                any1.Namespace = "http://www.w3.org/2001/XMLSchema";
                any1.MinOccurs = new decimal(0);
                any1.MaxOccurs = decimal.MaxValue;
                any1.ProcessContents = System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any1);
                System.Xml.Schema.XmlSchemaAny any2 = new System.Xml.Schema.XmlSchemaAny();
                any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
                any2.MinOccurs = new decimal(1);
                any2.ProcessContents = System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any2);
                System.Xml.Schema.XmlSchemaAttribute attribute1 = new System.Xml.Schema.XmlSchemaAttribute();
                attribute1.Name = "namespace";
                attribute1.FixedValue = ds.Namespace;
                type.Attributes.Add(attribute1);
                System.Xml.Schema.XmlSchemaAttribute attribute2 = new System.Xml.Schema.XmlSchemaAttribute();
                attribute2.Name = "tableTypeName";
                attribute2.FixedValue = "UserToolDataTable";
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                return type;
            }
        }

        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public partial class UserToolRow : System.Data.DataRow
        {

            private UserToolDataTable tableUserTool;

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal UserToolRow(System.Data.DataRowBuilder rb)
                :
                    base(rb)
            {
                this.tableUserTool = ((UserToolDataTable)(this.Table));
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string Title
            {
                get
                {
                    return ((string)(this[this.tableUserTool.TitleColumn]));
                }
                set
                {
                    this[this.tableUserTool.TitleColumn] = value;
                }
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string Program
            {
                get
                {
                    return ((string)(this[this.tableUserTool.ProgramColumn]));
                }
                set
                {
                    this[this.tableUserTool.ProgramColumn] = value;
                }
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string ProgIco
            {
                get
                {
                    return ((string)(this[this.tableUserTool.ProgIcoColumn]));
                }
                set
                {
                    this[this.tableUserTool.ProgIcoColumn] = value;
                }
            }
        }

        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class UserToolRowChangeEvent : System.EventArgs
        {

            private UserToolRow eventRow;

            private System.Data.DataRowAction eventAction;

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public UserToolRowChangeEvent(UserToolRow row, System.Data.DataRowAction action)
            {
                this.eventRow = row;
                this.eventAction = action;
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public UserToolRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataRowAction Action
            {
                get
                {
                    return this.eventAction;
                }
            }
        }
    }
}