// **********************************************************************
//
// Copyright (c) 2003-2015 ZeroC, Inc. All rights reserved.
//
// This copy of Ice is licensed to you under the terms described in the
// ICE_LICENSE file included in this distribution.
//
// **********************************************************************
//
// Ice version 3.6.1
//
// <auto-generated>
//
// Generated from file `Printer.ice'
//
// Warning: do not edit this file.
//
// </auto-generated>
//


using _System = global::System;
using _Microsoft = global::Microsoft;

#pragma warning disable 1591

namespace IceCompactId
{
}

namespace ZerocICE.Common
{
    [_System.Runtime.InteropServices.ComVisible(false)]
    [_System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704")]
    [_System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707")]
    [_System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709")]
    [_System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710")]
    [_System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711")]
    [_System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1715")]
    [_System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716")]
    [_System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720")]
    [_System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1722")]
    [_System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1724")]
    public partial interface Printer : Ice.Object, PrinterOperations_, PrinterOperationsNC_
    {
    }
}

namespace ZerocICE.Common
{
    [_System.CodeDom.Compiler.GeneratedCodeAttribute("slice2cs", "3.6.1")]
    public delegate void Callback_Printer_PrintString();
}

namespace ZerocICE.Common
{
    [_System.CodeDom.Compiler.GeneratedCodeAttribute("slice2cs", "3.6.1")]
    public interface PrinterPrx : Ice.ObjectPrx
    {
        void PrintString(string s);
        void PrintString(string s, _System.Collections.Generic.Dictionary<string, string> context__);

        Ice.AsyncResult<ZerocICE.Common.Callback_Printer_PrintString> begin_PrintString(string s);
        Ice.AsyncResult<ZerocICE.Common.Callback_Printer_PrintString> begin_PrintString(string s, _System.Collections.Generic.Dictionary<string, string> ctx__);

        Ice.AsyncResult begin_PrintString(string s, Ice.AsyncCallback cb__, object cookie__);
        Ice.AsyncResult begin_PrintString(string s, _System.Collections.Generic.Dictionary<string, string> ctx__, Ice.AsyncCallback cb__, object cookie__);

        void end_PrintString(Ice.AsyncResult r__);
    }
}

namespace ZerocICE.Common
{
    [_System.CodeDom.Compiler.GeneratedCodeAttribute("slice2cs", "3.6.1")]
    public interface PrinterOperations_
    {
        void PrintString(string s, Ice.Current current__);
    }

    [_System.CodeDom.Compiler.GeneratedCodeAttribute("slice2cs", "3.6.1")]
    public interface PrinterOperationsNC_
    {
        void PrintString(string s);
    }
}

namespace ZerocICE.Common
{
    [_System.Runtime.InteropServices.ComVisible(false)]
    [_System.CodeDom.Compiler.GeneratedCodeAttribute("slice2cs", "3.6.1")]
    public sealed class PrinterPrxHelper : Ice.ObjectPrxHelperBase, PrinterPrx
    {
        #region Synchronous operations

        public void PrintString(string s)
        {
            this.PrintString(s, null, false);
        }

        public void PrintString(string s, _System.Collections.Generic.Dictionary<string, string> context__)
        {
            this.PrintString(s, context__, true);
        }

        private void PrintString(string s, _System.Collections.Generic.Dictionary<string, string> context__, bool explicitCtx__)
        {
            end_PrintString(begin_PrintString(s, context__, explicitCtx__, true, null, null));
        }

        #endregion

        #region Asynchronous operations

        public Ice.AsyncResult<ZerocICE.Common.Callback_Printer_PrintString> begin_PrintString(string s)
        {
            return begin_PrintString(s, null, false, false, null, null);
        }

        public Ice.AsyncResult<ZerocICE.Common.Callback_Printer_PrintString> begin_PrintString(string s, _System.Collections.Generic.Dictionary<string, string> ctx__)
        {
            return begin_PrintString(s, ctx__, true, false, null, null);
        }

        public Ice.AsyncResult begin_PrintString(string s, Ice.AsyncCallback cb__, object cookie__)
        {
            return begin_PrintString(s, null, false, false, cb__, cookie__);
        }

        public Ice.AsyncResult begin_PrintString(string s, _System.Collections.Generic.Dictionary<string, string> ctx__, Ice.AsyncCallback cb__, object cookie__)
        {
            return begin_PrintString(s, ctx__, true, false, cb__, cookie__);
        }

        private const string __PrintString_name = "PrintString";

        public void end_PrintString(Ice.AsyncResult r__)
        {
            end__(r__, __PrintString_name);
        }

        private Ice.AsyncResult<ZerocICE.Common.Callback_Printer_PrintString> begin_PrintString(string s, _System.Collections.Generic.Dictionary<string, string> ctx__, bool explicitContext__, bool synchronous__, Ice.AsyncCallback cb__, object cookie__)
        {
            IceInternal.OnewayOutgoingAsync<ZerocICE.Common.Callback_Printer_PrintString> result__ = getOnewayOutgoingAsync<ZerocICE.Common.Callback_Printer_PrintString>(__PrintString_name, PrintString_completed__, cookie__);
            if(cb__ != null)
            {
                result__.whenCompletedWithAsyncCallback(cb__);
            }
            try
            {
                result__.prepare(__PrintString_name, Ice.OperationMode.Normal, ctx__, explicitContext__, synchronous__);
                IceInternal.BasicStream os__ = result__.startWriteParams(Ice.FormatType.DefaultFormat);
                os__.writeString(s);
                result__.endWriteParams();
                result__.invoke();
            }
            catch(Ice.Exception ex__)
            {
                result__.abort(ex__);
            }
            return result__;
        }

        private void PrintString_completed__(ZerocICE.Common.Callback_Printer_PrintString cb__)
        {
            if(cb__ != null)
            {
                cb__();
            }
        }

        #endregion

        #region Checked and unchecked cast operations

        public static PrinterPrx checkedCast(Ice.ObjectPrx b)
        {
            if(b == null)
            {
                return null;
            }
            PrinterPrx r = b as PrinterPrx;
            if((r == null) && b.ice_isA(ice_staticId()))
            {
                PrinterPrxHelper h = new PrinterPrxHelper();
                h.copyFrom__(b);
                r = h;
            }
            return r;
        }

        public static PrinterPrx checkedCast(Ice.ObjectPrx b, _System.Collections.Generic.Dictionary<string, string> ctx)
        {
            if(b == null)
            {
                return null;
            }
            PrinterPrx r = b as PrinterPrx;
            if((r == null) && b.ice_isA(ice_staticId(), ctx))
            {
                PrinterPrxHelper h = new PrinterPrxHelper();
                h.copyFrom__(b);
                r = h;
            }
            return r;
        }

        public static PrinterPrx checkedCast(Ice.ObjectPrx b, string f)
        {
            if(b == null)
            {
                return null;
            }
            Ice.ObjectPrx bb = b.ice_facet(f);
            try
            {
                if(bb.ice_isA(ice_staticId()))
                {
                    PrinterPrxHelper h = new PrinterPrxHelper();
                    h.copyFrom__(bb);
                    return h;
                }
            }
            catch(Ice.FacetNotExistException)
            {
            }
            return null;
        }

        public static PrinterPrx checkedCast(Ice.ObjectPrx b, string f, _System.Collections.Generic.Dictionary<string, string> ctx)
        {
            if(b == null)
            {
                return null;
            }
            Ice.ObjectPrx bb = b.ice_facet(f);
            try
            {
                if(bb.ice_isA(ice_staticId(), ctx))
                {
                    PrinterPrxHelper h = new PrinterPrxHelper();
                    h.copyFrom__(bb);
                    return h;
                }
            }
            catch(Ice.FacetNotExistException)
            {
            }
            return null;
        }

        public static PrinterPrx uncheckedCast(Ice.ObjectPrx b)
        {
            if(b == null)
            {
                return null;
            }
            PrinterPrx r = b as PrinterPrx;
            if(r == null)
            {
                PrinterPrxHelper h = new PrinterPrxHelper();
                h.copyFrom__(b);
                r = h;
            }
            return r;
        }

        public static PrinterPrx uncheckedCast(Ice.ObjectPrx b, string f)
        {
            if(b == null)
            {
                return null;
            }
            Ice.ObjectPrx bb = b.ice_facet(f);
            PrinterPrxHelper h = new PrinterPrxHelper();
            h.copyFrom__(bb);
            return h;
        }

        public static readonly string[] ids__ =
        {
            "::ZerocICE.Common::Printer",
            "::Ice::Object"
        };

        public static string ice_staticId()
        {
            return ids__[0];
        }

        #endregion

        #region Marshaling support

        public static void write__(IceInternal.BasicStream os__, PrinterPrx v__)
        {
            os__.writeProxy(v__);
        }

        public static PrinterPrx read__(IceInternal.BasicStream is__)
        {
            Ice.ObjectPrx proxy = is__.readProxy();
            if(proxy != null)
            {
                PrinterPrxHelper result = new PrinterPrxHelper();
                result.copyFrom__(proxy);
                return result;
            }
            return null;
        }

        #endregion
    }
}

namespace ZerocICE.Common
{
    [_System.Runtime.InteropServices.ComVisible(false)]
    [_System.CodeDom.Compiler.GeneratedCodeAttribute("slice2cs", "3.6.1")]
    public abstract class PrinterDisp_ : Ice.ObjectImpl, Printer
    {
        #region Slice operations

        public void PrintString(string s)
        {
            PrintString(s, Ice.ObjectImpl.defaultCurrent);
        }

        public abstract void PrintString(string s, Ice.Current current__);

        #endregion

        #region Slice type-related members

        public static new readonly string[] ids__ = 
        {
            "::ZerocICE.Common::Printer",
            "::Ice::Object"
        };

        public override bool ice_isA(string s)
        {
            return _System.Array.BinarySearch(ids__, s, IceUtilInternal.StringUtil.OrdinalStringComparer) >= 0;
        }

        public override bool ice_isA(string s, Ice.Current current__)
        {
            return _System.Array.BinarySearch(ids__, s, IceUtilInternal.StringUtil.OrdinalStringComparer) >= 0;
        }

        public override string[] ice_ids()
        {
            return ids__;
        }

        public override string[] ice_ids(Ice.Current current__)
        {
            return ids__;
        }

        public override string ice_id()
        {
            return ids__[0];
        }

        public override string ice_id(Ice.Current current__)
        {
            return ids__[0];
        }

        public static new string ice_staticId()
        {
            return ids__[0];
        }

        #endregion

        #region Operation dispatch

        [_System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011")]
        public static Ice.DispatchStatus PrintString___(Printer obj__, IceInternal.Incoming inS__, Ice.Current current__)
        {
            Ice.ObjectImpl.checkMode__(Ice.OperationMode.Normal, current__.mode);
            IceInternal.BasicStream is__ = inS__.startReadParams();
            string s;
            s = is__.readString();
            inS__.endReadParams();
            obj__.PrintString(s, current__);
            inS__.writeEmptyParams__();
            return Ice.DispatchStatus.DispatchOK;
        }

        private static string[] all__ =
        {
            "PrintString",
            "ice_id",
            "ice_ids",
            "ice_isA",
            "ice_ping"
        };

        public override Ice.DispatchStatus dispatch__(IceInternal.Incoming inS__, Ice.Current current__)
        {
            int pos = _System.Array.BinarySearch(all__, current__.operation, IceUtilInternal.StringUtil.OrdinalStringComparer);
            if(pos < 0)
            {
                throw new Ice.OperationNotExistException(current__.id, current__.facet, current__.operation);
            }

            switch(pos)
            {
                case 0:
                {
                    return PrintString___(this, inS__, current__);
                }
                case 1:
                {
                    return Ice.ObjectImpl.ice_id___(this, inS__, current__);
                }
                case 2:
                {
                    return Ice.ObjectImpl.ice_ids___(this, inS__, current__);
                }
                case 3:
                {
                    return Ice.ObjectImpl.ice_isA___(this, inS__, current__);
                }
                case 4:
                {
                    return Ice.ObjectImpl.ice_ping___(this, inS__, current__);
                }
            }

            _System.Diagnostics.Debug.Assert(false);
            throw new Ice.OperationNotExistException(current__.id, current__.facet, current__.operation);
        }

        #endregion

        #region Marshaling support

        protected override void writeImpl__(IceInternal.BasicStream os__)
        {
            os__.startWriteSlice(ice_staticId(), -1, true);
            os__.endWriteSlice();
        }

        protected override void readImpl__(IceInternal.BasicStream is__)
        {
            is__.startReadSlice();
            is__.endReadSlice();
        }

        #endregion
    }
}
