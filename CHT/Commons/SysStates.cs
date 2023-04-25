using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHT.Commons
{
    public class SysStates
    {
        public enum EComState
        {
            [Description("...")]
            NONE,
            [Description("Đã mở COM")]
            OPENED,
            [Description("Đang nhận dữ liệu")]
            RECEIVING_DATA,
            [Description("Đã đóng COM")]
            CLOSED
        }
        public enum EPrinterState
        {
            [Description("...")]
            NONE,
            [Description("Đã kết nối máy in")]
            CONNECTED,
            [Description("Ngắt kết nối máy in")]
            DISCONECTED,
        }
        public enum EMessageState
        {
            [Description("Đã cập nhật bản tin")]
            UPDATE_FIELD_SUCCESS,
            [Description("...")]
            NORMAL
        }
    }
}
