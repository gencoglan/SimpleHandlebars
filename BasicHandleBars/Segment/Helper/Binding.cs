using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace BasicHandleBars.Segment.Helper
{
    public class Binding
    {
        private object oItem;
        private object oValue;
        private string sMemberName = "";
        private object oValueItem;
        private string ValueMemberName = "";
        public bool CreateIfReferenceIsNothing { get; set; }
        public event ItemInitiliazedEventHandler ItemInitiliazed;

        public delegate void ItemInitiliazedEventHandler(object Item);

        public event ValueInitiliazedEventHandler ValueInitiliazed;

        public delegate void ValueInitiliazedEventHandler(ref object Value);

        private bool bIgnoreNullReferences = false;
        public object Item
        {
            get
            {
                return this.oItem;
            }
            set
            {
                this.oItem = value;
                this.Refresh();
                ItemInitiliazed?.Invoke(this.oItem);
            }
        }

        public object Value
        {
            get
            {
                if (this.oValue == null)
                    this.GetValue();
                return this.oValue;
            }
            set
            {
                if (value != this.oValue)
                {
                    this.oValue = value;
                }
            }
        }
        public object GetRefreshedValue(string MemberName)
        {
            this.MemberName = MemberName;
            this.Refresh();
            return this.Value;
        }
        private bool SetSessionLanguage { get; set; }
        public string MemberName
        {
            get
            {
                return this.sMemberName;
            }
            set
            {
                this.sMemberName = value;
            }
        }
        public bool IgnoreNullReferences
        {
            get
            {
                return this.bIgnoreNullReferences;
            }
            set
            {
                this.bIgnoreNullReferences = value;
            }
        }
        public object ValueItem
        {
            get
            {
                this.GetValue();
                return this.oValueItem;
            }
        }
        public void ResetItem()
        {
            this.oItem = null;
        }
        public void UpdateValue(object Value)
        {
            this.oValue = Value;
        }
        public void Refresh()
        {
            this.oValue = null;
            this.oValueItem = null;
        }
        protected virtual object GetValue()
        {
            try
            {
                if (this.oValue == null && this.Item != null)
                {
                    if (this.MemberName != "")
                    {
                        string Index = "";
                        bool Indexed = false;
                        if (this.MemberName == "ItemItself")
                        {
                            this.oValue = this.Item;
                            ValueInitiliazed?.Invoke(ref this.oValue);
                        }
                        else
                        {
                            if (this.MemberName.IndexOf(".") > -1 || this.MemberName.IndexOf("(") > -1)
                            {
                                System.Collections.Generic.List<string> sArrReferences = new List<string>();
                                System.Collections.Generic.List<string> sTempArrReferences = this.MemberName.Split('.').ToList();
                                bool Start = false;
                                string TempStr = string.Empty;
                                foreach (string entry in sTempArrReferences)
                                {
                                    if ((entry.IndexOf("(") > -1))
                                        Start = true;
                                    if ((!Start))
                                        sArrReferences.Add(entry);
                                    else if ((!string.IsNullOrEmpty(TempStr)))
                                        TempStr += "." + entry;
                                    else
                                        TempStr = entry;
                                    if ((entry.IndexOf(")") > -1))
                                    {
                                        Start = false;
                                        sArrReferences.Add(TempStr);
                                    }
                                }


                                object TempValue = this.Item;
                                int n;
                                string TemporaryMemberName = "";
                                try
                                {
                                    for (n = 0; n <= sArrReferences.Count - 1; n++)
                                    {
                                        if (!(TempValue == null && this.IgnoreNullReferences))
                                        {
                                            TemporaryMemberName = sArrReferences[n];
                                            if (n == sArrReferences.Count - 1)
                                            {
                                                this.oValueItem = TempValue;
                                                this.ValueMemberName = sArrReferences[n];
                                            }
                                            if (TemporaryMemberName.IndexOf("(") > -1)
                                            {
                                                Indexed = true;
                                                Index = TemporaryMemberName.Substring(TemporaryMemberName.IndexOf("(") + 1, TemporaryMemberName.Length - TemporaryMemberName.IndexOf("(") - 2);
                                                TemporaryMemberName = TemporaryMemberName.Substring(0, TemporaryMemberName.Length - 2 - Index.ToString().Length);
                                            }
                                            if (Indexed)
                                            {
                                                List<object> Parameters = this.ReArrangeParameters(Index);
                                                try
                                                {
                                                    TempValue = TempValue.GetType().GetProperty(TemporaryMemberName).GetValue(TempValue, Parameters.ToArray());
                                                }
                                                catch (Exception ex)
                                                {
                                                    try
                                                    {
                                                        TempValue = TempValue.GetType().InvokeMember(TemporaryMemberName, System.Reflection.BindingFlags.InvokeMethod, null/* TODO Change to default(_) if this is not a reference type */, TempValue, Parameters.ToArray());
                                                    }
                                                    catch (Exception ex2)
                                                    {
                                                        if (TempValue != null)
                                                            throw new Exception("Before " + TemporaryMemberName + " property binded, binding value became nothing.", ex);
                                                        else
                                                            throw new Exception(TemporaryMemberName + " property not found at the type of " + TempValue.GetType().FullName, ex);
                                                    }
                                                }
                                            }
                                            else if (TempValue != null)
                                            {
                                                object LastTempValue = TempValue;
                                                TempValue = TempValue.GetType().GetProperty(TemporaryMemberName).GetValue(TempValue, null);
                                                if (TempValue == null && this.CreateIfReferenceIsNothing)
                                                {
                                                    System.Reflection.PropertyInfo PropInfo = LastTempValue.GetType().GetProperty(TemporaryMemberName);
                                                    if ((PropInfo.PropertyType.IsClass))
                                                    {
                                                        TempValue = Activator.CreateInstance(PropInfo.PropertyType);
                                                        LastTempValue.GetType().GetProperty(TemporaryMemberName).SetValue(LastTempValue, TempValue, null);
                                                    }
                                                }
                                            }
                                            Indexed = false;
                                        }
                                        else
                                            return null;
                                    }
                                    this.oValue = TempValue;
                                    this.ValueMemberName = TemporaryMemberName;
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception("Exception occured while getting requested property.  Member of Value Member : " + TemporaryMemberName, ex);
                                }
                            }
                            else
                            {
                                this.ValueMemberName = this.MemberName;
                                try
                                {
                                    this.oValue = this.Item.GetType().GetProperty(this.ValueMemberName).GetValue(this.Item, null);
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception(this.ValueMemberName + " property not found at the type of " + this.Item.GetType().FullName, ex.InnerException);
                                }
                            }
                            ValueInitiliazed?.Invoke(ref this.oValue);
                        }
                    }
                }
                if (this.oValueItem == null)
                    this.oValueItem = this.Item;
            }
            catch
            {
                throw;
            }
            return this.oValue;
        }
        private List<object> ReArrangeParameters(string RawParameters)
        {
            string[] Parameters = RawParameters.Split(',');
            List<object>  Array = new List<object>();
            int n;
            for (n = 0; n <= Parameters.Length - 1; n++)
            {
                if (Microsoft.VisualBasic.Information.IsNumeric(Parameters[n]))
                {
                    if (Parameters[n].IndexOf(".") > -1)
                        Array.Add(System.Convert.ToDouble(Parameters[n]));
                    else
                        Array.Add(System.Convert.ToInt32(Parameters[n]));
                }
                else if (Microsoft.VisualBasic.Information.IsDate(Parameters[n]))
                    Array.Add(Convert.ToDateTime(Parameters[n]));
                else
                    Array.Add(Parameters[n]);
            }
            return Array;
        }
    }
}



